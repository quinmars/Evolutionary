using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;
using System.Reactive;
using Evolutionary;
using Evolutionary.Individuals;

namespace EightQueens
{
    public class RunViewModel : ReactiveObject
    {
        public class Boardfield
        {
            public int Column { get; set; }
            public int Row { get; set; }
            public bool IsHighlighted { get; set; }
        }

        private Parameters _parameters;
        public Parameters Parameters
        {
            get => _parameters;
            set => this.RaiseAndSetIfChanged(ref _parameters, value);
        }

        private readonly ObservableAsPropertyHelper<Boardfield[]> _board;
        public Boardfield[] Board => _board.Value;

        private readonly ObservableAsPropertyHelper<RunResult> _lastRun;
        public RunResult LastRun => _lastRun.Value;

        private readonly ObservableAsPropertyHelper<Boardfield[]> _currentQueens;
        public Boardfield[] CurrentQueens => _currentQueens.Value;

        private readonly ObservableAsPropertyHelper<bool> _isNotRunning;
        public bool IsNotRunning => _isNotRunning.Value;

        public ReactiveCommand<Parameters, IEnumerable<RunResult>> Run { get; }
        public ReactiveCommand<Unit, Unit> Cancel { get; }

        public RunViewModel()
        {
            this.WhenAnyValue(x => x.Parameters)
                .Where(x => x != null)
                .Select(p =>
                    from col in Enumerable.Range(0, p.QueenCount)
                    from row in Enumerable.Range(0, p.QueenCount)
                    select new Boardfield
                    {
                        Column = col,
                        Row = row,
                        IsHighlighted = col % 2 != row % 2
                    }
                )
                .Select(b => b.ToArray())
                .ToProperty(this, x => x.Board, out _board);

            Run = ReactiveCommand.CreateFromObservable((Parameters p) => RunPopulation(p).TakeUntil(Cancel));
            Run.Select(r => r.Last())
                .ToProperty(this, x => x.LastRun, out _lastRun, default(RunResult));
            Run.IsExecuting
                .Select(x => !x)
                .ToProperty(this, x => x.IsNotRunning, out _isNotRunning);

            Cancel = ReactiveCommand.Create(() => { }, Run.IsExecuting);

            this.WhenAnyValue(x => x.LastRun)
                .Where(x => x != null)
                .Select(x => x.BestIndividual
                                .Select((r, c) => new Boardfield
                                {
                                    Row = r,
                                    Column = c,
                                    IsHighlighted = x.BestIndividual
                                        .Select((r2, c2) => Math.Abs(r2 - r) == Math.Abs(c2 - c))
                                        .Where(e => e)
                                        .Count() > 1
                                })
                                .ToArray())
                .ToProperty(this, x => x.CurrentQueens, out _currentQueens);
        }

        private IObservable<IEnumerable<RunResult>> RunPopulation(Parameters param)
        {
            Parameters = param;

            Func<Permutation, double> fitness = p => p
                .Select((r1, c1) => p
                    .Select((r2, c2) => Math.Abs(r2 - r1) == Math.Abs(c2 - c1) && r2 != r1)
                    .Where(e => e)
                    .Count())
                .Sum();

            var n = param.QueenCount;

            return Observable
                .Repeat(Unit.Default, RxApp.TaskpoolScheduler)
                .Scan(
                    seed: Population
                        .Create(fitness)
                        .AddRandomIndividuals(20, rnd => Permutation.CreateRandomly(n, rnd)),
                    accumulator: (population, _) => population
                        .SelectParentsByRank()
                        .SelectElite(param.EliteSelection)
                        .Recombine(param.CycleCrossover, Permutation.CycleCrossover)
                        .Recombine(param.PartiallyMappedCrossover, Permutation.PartiallyMappedCrossover)
                        .Recombine(param.CutAndCrossfillCrossover, Permutation.CutAndCrossfillCrossover)
                        .Mutate(param.InsertMutation, Permutation.InsertMutation)
                        .Mutate(param.InversionMutation, Permutation.InversionMutation)
                        .ToPopulation()
                )
                .Select((p, i) => new RunResult
                {
                    Run = i,
                    BestFitness = fitness(p.Individuals[0]),
                    BestIndividual = p.Individuals[0]
                })
                .Do(r => Console.WriteLine($"{r.Run}: {r.BestFitness}"))
                .TakeUntil(r => r.BestFitness == 0.0)
                .Buffer(TimeSpan.FromSeconds(0.5), RxApp.MainThreadScheduler)
                .Where(b => b.Any());
        }
    }
}
