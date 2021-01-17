using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evolutionary
{
    public class Population<TIndividual, TDataSet>
    {
        private static readonly IReadOnlyList<TIndividual> _emptyIndividuals = new List<TIndividual>();

        public Enviroment<TIndividual, TDataSet> Enviroment { get; }
        public IReadOnlyList<TIndividual> Individuals { get; }

        public Population(Enviroment<TIndividual, TDataSet> enviroment)
        {
            Enviroment = enviroment;
            Individuals = _emptyIndividuals;
        }

        public Population(Enviroment<TIndividual, TDataSet> enviroment, IEnumerable<TIndividual> individuals)
        {
            var fitness = enviroment.Fitness;
            var dataSet = enviroment.DataSet;

            Enviroment = enviroment;
            Individuals = individuals
                .OrderBy(x => fitness(x, dataSet))
                .ToList();
        }
    }
}
