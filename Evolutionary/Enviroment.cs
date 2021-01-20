using System;
using System.Collections.Generic;
using System.Text;
using Troschuetz.Random;

namespace Evolutionary
{
    public class Enviroment<TIndividual, TDataSet>
    {
        public TDataSet DataSet { get; }
        public Func<TIndividual, TDataSet, double> Fitness { get; }
        public TRandom Random { get; }

        public Enviroment(TDataSet dataSet, Func<TIndividual, TDataSet, double> fitness, TRandom random)
        {
            if (fitness is null)
                throw new ArgumentNullException(nameof(fitness));
            if (random is null)
                throw new ArgumentNullException(nameof(random));

            DataSet = dataSet;
            Fitness = fitness;
            Random = random;
        }
    }
}
