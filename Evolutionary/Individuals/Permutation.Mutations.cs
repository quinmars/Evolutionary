using System;
using System.Collections.Generic;
using System.Text;
using Troschuetz.Random;

namespace Evolutionary.Individuals
{
    public readonly partial struct Permutation
    {
        /*
         * Mutations
         */
        public static Permutation SwapMutation(Permutation permutation, TRandom random)
        {
            var array = permutation.ToArray();
            permutation.GetRandomIndexPair(random, out var a, out var b);

            array.AsSpan().Swap(a, b);
            return new Permutation(array, permutation.IsCircular);
        }

        public static Permutation InsertMutation(Permutation permutation, TRandom random)
        {
            var array = permutation.ToArray();
            permutation.GetRandomIndexPair(random, out var a, out var b);

            array.AsSpan(a, b - a).RightShift();
            return new Permutation(array, permutation.IsCircular);
        }

        public static Permutation ScrambleMutation(Permutation permutation, TRandom random)
        {
            var array = permutation.ToArray();
            permutation.GetRandomIndexPair(random, out var a, out var b);

            array.AsSpan(a, b - a).Scramble(random);
            return new Permutation(array, permutation.IsCircular);
        }

        public static Permutation InversionMutation(Permutation permutation, TRandom random)
        {
            var array = permutation.ToArray();
            permutation.GetRandomIndexPair(random, out var a, out var b);

            array.AsSpan(a, b - a).Reverse();
            return new Permutation(array, permutation.IsCircular);
        }

    }
}
