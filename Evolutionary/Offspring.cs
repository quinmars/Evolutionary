using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Troschuetz.Random;

namespace Evolutionary
{
    /// <summary>
    /// The offspring class.
    /// </summary>
    /// <typeparam name="TIndividual">The individuals type.</typeparam>
    public class Offspring<TIndividual>
    {
        /// <summary>
        /// The parent population.
        /// </summary>
        public Population<TIndividual> ParentPopulation { get; }

        /// <summary>
        /// A never ending enumeration of a random selection of the parents.
        /// </summary>
        public IEnumerable<TIndividual> RandomParents { get; }

        /// <summary>
        /// The list of children of the generation.
        /// </summary>
        public IEnumerable<TIndividual> Children { get; }

        /// <summary>
        /// Creates a new offspring.
        /// </summary>
        /// <param name="enviroment">The enviroment..</param>
        /// <param name="parents">The sorted parent collection.</param>
        /// <param name="randomParents">A never ending random enumeration over the parents.</param>
        /// <param name="children">The children.</param>
        public Offspring(Population<TIndividual> parentPopulation, IEnumerable<TIndividual> randomParents,  IEnumerable<TIndividual> children)
        {
            if (parentPopulation is null)
                throw new ArgumentNullException(nameof(parentPopulation));
            if (randomParents is null)
                throw new ArgumentNullException(nameof(randomParents));
            if (children is null)
                throw new ArgumentNullException(nameof(children));

            ParentPopulation = parentPopulation;
            RandomParents = randomParents;
            Children = children;
        }
    }
}
