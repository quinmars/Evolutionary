using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evolutionary
{
    /// <summary>
    /// The population class.
    /// </summary>
    /// <typeparam name="TIndividual">The individuals type.</typeparam>
    /// <typeparam name="TDataSet">The data set type.</typeparam>
    public class Population<TIndividual, TDataSet>
    {
        private static readonly IReadOnlyList<TIndividual> _emptyIndividuals = new List<TIndividual>();

        /// <summary>
        /// The enviroment of the population.
        /// </summary>
        public Enviroment<TIndividual, TDataSet> Enviroment { get; }

        /// <summary>
        /// The individuals of the population. The individuals are order by their fitness increasingly.
        /// Hence the first element is always the best fitting individual.
        /// </summary>
        public IReadOnlyList<TIndividual> Individuals { get; }

        /// <summary>
        /// Creates a new population with zero individuals.
        /// </summary>
        /// <param name="enviroment">The enviroment.</param>
        public Population(Enviroment<TIndividual, TDataSet> enviroment)
        {
            if (enviroment is null)
                throw new ArgumentNullException(nameof(enviroment));

            Enviroment = enviroment;
            Individuals = _emptyIndividuals;
        }

        /// <summary>
        /// Creates a new population with some individuals.
        /// </summary>
        /// <param name="enviroment">The enviroment.</param>
        /// <param name="individuals">The individuals.</param>
        public Population(Enviroment<TIndividual, TDataSet> enviroment, IEnumerable<TIndividual> individuals)
        {
            if (enviroment is null)
                throw new ArgumentNullException(nameof(enviroment));
            if (individuals is null)
                throw new ArgumentNullException(nameof(individuals));

            var fitness = enviroment.Fitness;
            var dataSet = enviroment.DataSet;

            Enviroment = enviroment;
            Individuals = individuals
                .OrderBy(x => fitness(x, dataSet))
                .ToList();
        }
    }
}
