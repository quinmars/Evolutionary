using System;
using System.Collections.Generic;
using System.Text;
using Troschuetz.Random;

namespace Evolutionary.Individuals
{
    public readonly partial struct Permutation
    {
        /*
         * Recombinations
         */
        public static Permutation PartiallyMappedCrossover(Permutation p1, Permutation p2, TRandom random)
        {
            if (p1.Count != p2.Count)
            {
                throw new ArgumentException("The element count differs.", nameof(p2));
            }

            if (p1.IsCircular != p2.IsCircular)
            {
                throw new ArgumentException("The circularity differs.", nameof(p2));
            }

            var array = new int[p1.Count];
            p1.GetRandomIndexPair(random, out var a, out var b);

            SpanExtensions.PartiallyMappedCrossover(p1, p2, a, b - a, array);

            return new Permutation(array, p1.IsCircular);
        }
    }
}
