﻿using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;
using System.Reactive;
using Evolutionary;
using Evolutionary.Individuals;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

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

        private RunResult[] _runResults = new RunResult[] { };
        public RunResult[] RunResults
        {
            get => _runResults;
            set => this.RaiseAndSetIfChanged(ref _runResults, value);
        }

        private readonly ObservableAsPropertyHelper<Boardfield[]> _currentQueens;
        public Boardfield[] CurrentQueens => _currentQueens.Value;

        private readonly ObservableAsPropertyHelper<bool> _isNotRunning;
        public bool IsNotRunning => _isNotRunning.Value;

        public ReactiveCommand<Parameters, IEnumerable<RunResult>> Run { get; }
        public ReactiveCommand<Unit, Unit> Cancel { get; }

        public PlotModel FitnessEvolution { get; }

        public RunViewModel()
        {
            FitnessEvolution = new PlotModel
            {
                Axes =
                {
                    new LinearAxis
                    {
                        Title = "Generation",
                        Position = AxisPosition.Bottom,
                        MaximumPadding = 0.0,
                        MinimumPadding = 0.0
                    },
                    new LinearAxis
                    {
                        Title = "Fitness",
                        Position = AxisPosition.Left,
                        Minimum = 0.0,
                    }
                }
            };

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

            Run = ReactiveCommand.CreateFromObservable((Parameters p) =>
            {
                RunResults = new RunResult[] { };
                return RunPopulation(p).TakeUntil(Cancel);
            });
            Run.Select(r => r.Last())
                .ToProperty(this, x => x.LastRun, out _lastRun, default(RunResult));
            Run.IsExecuting
                .Select(x => !x)
                .ToProperty(this, x => x.IsNotRunning, out _isNotRunning);

            Cancel = ReactiveCommand.Create(() => { }, Run.IsExecuting);

            Run
                .Subscribe(x => RunResults = RunResults.Concat(x).ToArray());

            this.WhenAnyValue(x => x.RunResults)
                .Subscribe(results =>
                {
                    FitnessEvolution.Series.Clear();

                    if (results != null)
                    {
                        var area = new AreaSeries
                        {
                            Fill = OxyColors.WhiteSmoke,
                            Color = OxyColors.Gray,
                            Color2 = OxyColors.Crimson
                        };
                        area.Points.AddRange(results.Select(r => new DataPoint(r.Run, r.WorstFitness)));
                        area.Points2.AddRange(results.Select(r => new DataPoint(r.Run, r.BestFitness)));
                        FitnessEvolution.Series.Add(area);

                        var median = new LineSeries
                        {
                            Color = OxyColors.Silver,
                            LineJoin = LineJoin.Round
                        };
                        median.Points.AddRange(results.Select(r => new DataPoint(r.Run, r.MedianFitness)));
                        FitnessEvolution.Series.Add(median);
                    }

                    FitnessEvolution.ResetAllAxes();
                    FitnessEvolution.InvalidatePlot(true);
                });

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
                    BestIndividual = p.Individuals[0],
                    BestFitness = fitness(p.Individuals[0]),
                    MedianFitness = fitness(p.Individuals[p.Individuals.Count/2]),
                    WorstFitness = fitness(p.Individuals.Last()),
                })
                .Do(r => Console.WriteLine($"{r.Run}: {r.BestFitness}"))
                .TakeUntil(r => r.BestFitness == 0.0)
                .Buffer(TimeSpan.FromSeconds(0.5), RxApp.MainThreadScheduler)
                .Where(b => b.Any());
        }
    }
}
