using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EightQueens
{
    public class ParametersViewModel : ReactiveObject
    {
        public int QueenCount
        {
            get => _queenCount;
            set => this.RaiseAndSetIfChanged(ref _queenCount, value);
        }
        private int _queenCount = 8;

        /*
         * Mutation
         */
        // Elite selection
        public int EliteSelection
        {
            get => _eliteSelection;
            set => this.RaiseAndSetIfChanged(ref _eliteSelection, value);
        }
        private int _eliteSelection = 1;

        // Insert mutation
        public int InsertMutation
        {
            get => _insertMutation;
            set => this.RaiseAndSetIfChanged(ref _insertMutation, value);
        }
        private int _insertMutation = 10;

        // Inversion mutation
        public int InversionMutation
        {
            get => _inversionMutation;
            set => this.RaiseAndSetIfChanged(ref _inversionMutation, value);
        }
        private int _inversionMutation = 10;

        /*
         * Recombination
         */
        // Cycle crossover
        public int CycleCrossover
        {
            get => _cycleCrossover;
            set => this.RaiseAndSetIfChanged(ref _cycleCrossover, value);
        }
        private int _cycleCrossover = 10;

        // Partially mapped crossover
        public int PartiallyMappedCrossover
        {
            get => _partiallyMappedCrossover;
            set => this.RaiseAndSetIfChanged(ref _partiallyMappedCrossover, value);
        }
        private int _partiallyMappedCrossover = 10;
        
        // Cut and crossfill crossover
        public int CutAndCrossfillCrossover
        {
            get => _cutAndCrossfillCrossover;
            set => this.RaiseAndSetIfChanged(ref _cutAndCrossfillCrossover, value);
        }
        private int _cutAndCrossfillCrossover = 10;

        public Parameters Parameters => new Parameters
        {
            QueenCount = QueenCount,
            EliteSelection = EliteSelection,
            InsertMutation = InsertMutation,
            InversionMutation = InversionMutation,
            CycleCrossover = CycleCrossover,
            PartiallyMappedCrossover = PartiallyMappedCrossover,
            CutAndCrossfillCrossover = CutAndCrossfillCrossover
        };

        public ParametersViewModel()
        {
            Changed
                .Where(e => e.PropertyName != nameof(Parameters))
                .Subscribe(e => this.RaisePropertyChanged(nameof(Parameters)));
        }
    }
}
