using System;
using System.Collections.Generic;
using System.Text;
using Troschuetz.Random;

namespace Evolutionary
{
    public class Enviroment<TIndividual, TDataSet>
    {
        public TRandom Random { get; }
        public TDataSet DataSet { get; }
        public Func<TIndividual, TDataSet, double> Fitness { get; }

        public Enviroment(TDataSet dataSet, Func<TIndividual, TDataSet, double> fitness, TRandom rnd)
        {
            Random = rnd;

            DataSet = dataSet;
            Fitness = fitness;
        }
    }
}
