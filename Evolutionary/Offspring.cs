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
    /// <typeparam name="TDataSet">The data set type.</typeparam>
    public class Offspring<TIndividual, TDataSet>
    {
        /// <summary>
        /// The enviroment.
        /// </summary>
        public Enviroment<TIndividual, TDataSet> Enviroment { get; }

        /// <summary>
        /// The complete sorted list of the parents.
        /// </summary>
        public IEnumerable<TIndividual> Parents { get; }

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
        public Offspring(Enviroment<TIndividual, TDataSet> enviroment, IEnumerable<TIndividual> parents, IEnumerable<TIndividual> randomParents,  IEnumerable<TIndividual> children)
        {
            if (enviroment is null)
                throw new ArgumentNullException(nameof(enviroment));
            if (parents is null)
                throw new ArgumentNullException(nameof(parents));
            if (randomParents is null)
                throw new ArgumentNullException(nameof(randomParents));
            if (children is null)
                throw new ArgumentNullException(nameof(children));

            Enviroment = enviroment;
            Parents = parents;
            RandomParents = randomParents;
            Children = children;
        }
    }
}
