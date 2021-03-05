using Evolutionary.Individuals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EightQueens
{
    public class RunResult
    {
        public int Run { get; set; }
        public double BestFitness { get; set; }
        public Permutation BestIndividual { get; set; }
    }
}
