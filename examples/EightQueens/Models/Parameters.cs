using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EightQueens
{
    public class Parameters
    {
        public int QueenCount { get; set; }

        // Mutation
        public int EliteSelection { get; set; }
        public int InsertMutation { get; set; }
        public int InversionMutation { get; set; }

        // Recombination
        public int CycleCrossover { get; set; }
        public int PartiallyMappedCrossover { get; set; }
        public int CutAndCrossfillCrossover { get; set; }
    }
}
