using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Troschuetz.Random;

namespace Evolutionary
{
    public static class Population
    {
        /// <summary>
        /// Creates a new population with zero individuals.
        /// </summary>
        /// <typeparam name="TIndividual">The individuals type.</typeparam>
        /// <typeparam name="TDataSet">The data set type.</typeparam>
        /// <param name="dataSet">The data set.</param>
        /// <param name="fitness">The fitness function.</param>
        /// <param name="random">The random generator.</param>
        /// <returns>The population.</returns>
        public static Population<TIndividual, TDataSet> Create<TIndividual, TDataSet>(TDataSet dataSet, Func<TIndividual, TDataSet, double> fitness, TRandom random)
        {
            if (fitness is null)
                throw new ArgumentNullException(nameof(fitness));
            if (random is null)
                throw new ArgumentNullException(nameof(random));

            var enviroment = new Enviroment<TIndividual, TDataSet>(dataSet, fitness, random);
            return new Population<TIndividual, TDataSet>(enviroment);
        }
        
        /// <summary>
        /// Creates a new population with zero individuals and the default random generator.
        /// </summary>
        /// <typeparam name="TIndividual">The individuals type.</typeparam>
        /// <typeparam name="TDataSet">The data set type.</typeparam>
        /// <param name="dataSet">The data set.</param>
        /// <param name="fitness">The fitness function.</param>
        /// <returns>The population.</returns>
        public static Population<TIndividual, TDataSet> Create<TIndividual, TDataSet>(TDataSet dataSet, Func<TIndividual, TDataSet, double> fitness)
        {
            if (fitness is null)
                throw new ArgumentNullException(nameof(fitness));

            return Create(dataSet, fitness, new TRandom());
        }

        /// <summary>
        /// Generates an initial offspring generation. The random parents are selected by
        /// rank lineary.
        /// </summary>
        /// <typeparam name="TIndividual">The individuals type.</typeparam>
        /// <typeparam name="TDataSet">The data set type.</typeparam>
        /// <param name="population">The parent generation.</param>
        /// <returns>The offspring.</returns>
        public static Offspring<TIndividual, TDataSet> SelectParentsByRank<TIndividual, TDataSet>(this Population<TIndividual, TDataSet> population)
        {
            if (population is null)
                throw new ArgumentNullException(nameof(population));

            var randomParents = population.GetRandomIndividualsByRank();
            return new Offspring<TIndividual, TDataSet>(population.Enviroment, population.Individuals, randomParents, Enumerable.Empty<TIndividual>());
        }
        
        /// <summary>
        /// Adds some individuals to the population.
        /// </summary>
        /// <typeparam name="TIndividual">The individuals type.</typeparam>
        /// <typeparam name="TDataSet">The data set type.</typeparam>
        /// <param name="population">The populations.</param>
        /// <param name="individuals">The individuals.</param>
        /// <returns>The extended population.</returns>
        public static Population<TIndividual, TDataSet> AddIndividuals<TIndividual, TDataSet>(this Population<TIndividual, TDataSet> population, IEnumerable<TIndividual> individuals)
        {
            if (population is null)
                throw new ArgumentNullException(nameof(population));
            if (individuals is null)
                throw new ArgumentNullException(nameof(individuals));

            var env = population.Enviroment;
            var allIndividuals = population.Individuals.Concat(individuals);

            return new Population<TIndividual, TDataSet>(env, allIndividuals);
        }

        /// <summary>
        /// Adds some random individuals to the population.
        /// </summary>
        /// <typeparam name="TIndividual">The individuals type.</typeparam>
        /// <typeparam name="TDataSet">The data set type.</typeparam>
        /// <param name="population">The populations.</param>
        /// <param name="count">The count of individuals to add.</param>
        /// <param name="randomIndividual">The function to generate the individuals.</param>
        /// <returns>The extended population.</returns>
        public static Population<TIndividual, TDataSet> AddRandomIndividuals<TIndividual, TDataSet>(this Population<TIndividual, TDataSet> population, int count, Func<TDataSet, TRandom, TIndividual> randomIndividual)
        {
            if (population is null)
                throw new ArgumentNullException(nameof(population));
            if (randomIndividual is null)
                throw new ArgumentNullException(nameof(randomIndividual));

            var env = population.Enviroment;

            var individuals = Enumerable
                .Repeat(0, count)
                .Select(_ => randomIndividual(env.DataSet, env.Random));

            return population.AddIndividuals(individuals);
        }

        private static IEnumerable<TIndividual> GetRandomIndividualsByRank<TIndividual, TDataSet>(this Population<TIndividual, TDataSet> population)
        {
            var rnd = population.Enviroment.Random;
            var individuals = population.Individuals;

            while (true)
            {
                yield return GetRandomIndiviual(individuals, rnd);
            }
        }

        private static TIndividual GetRandomIndiviual<TIndividual>(IReadOnlyList<TIndividual> individuals, TRandom rnd)
        {
            double count = individuals.Count;
            double p(int index)
            {
                double rank = count - index;
                return 2 * rank / (count * (count + 1.0));
            }

            var r = rnd.NextDouble(0, 1);

            var minP = 0.0;
            var i = 0;

            foreach (var individual in individuals)
            {
                var maxP = p(i) + minP;

                if (r >= minP && r <= maxP)
                {
                    return individual;
                }
                i++;
                minP = maxP;
            }

            return individuals.Last();
        }
    }
}
