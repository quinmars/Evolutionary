using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Troschuetz.Random;

namespace Evolutionary
{
    /// <summary>
    /// The population class.
    /// </summary>
    /// <typeparam name="TIndividual">The individuals type.</typeparam>
    public class Population<TIndividual>
    {
        private static readonly IReadOnlyList<TIndividual> _emptyIndividuals = new List<TIndividual>();

        public Func<TIndividual, double> Fitness { get; }
        public TRandom Random { get; }

        /// <summary>
        /// The individuals of the population. The individuals are order by their fitness increasingly.
        /// Hence the first element is always the best fitting individual.
        /// </summary>
        public IReadOnlyList<TIndividual> Individuals { get; }

        /// <summary>
        /// Creates a new population with zero individuals.
        /// </summary>
        /// <param name="enviroment">The enviroment.</param>
        public Population(Func<TIndividual, double> fitness, TRandom random)
        {
            if (fitness is null)
                throw new ArgumentNullException(nameof(fitness));
            if (random is null)
                throw new ArgumentNullException(nameof(random));

            Fitness = fitness;
            Random = random;
            Individuals = _emptyIndividuals;
        }

        /// <summary>
        /// Creates a new population with some individuals.
        /// </summary>
        /// <param name="fitness">The fitness function.</param>
        /// <param name="individuals">The individuals.</param>
        public Population(Func<TIndividual, double> fitness, TRandom random, IEnumerable<TIndividual> individuals)
        {
            if (fitness is null)
                throw new ArgumentNullException(nameof(fitness));
            if (random is null)
                throw new ArgumentNullException(nameof(random));
            if (individuals is null)
                throw new ArgumentNullException(nameof(individuals));

            Fitness = fitness;
            Random = random;
            Individuals = individuals
                .OrderBy(fitness)
                .ToList();
        }
    }
}
