using System;
using System.Collections.Generic;
using System.Text;
using Troschuetz.Random;

namespace Evolutionary.Individuals
{
    public readonly partial struct Permutation
    {
        private void GetRandomIndexPair(TRandom random, out int a, out int b)
        {
            var start = IsCircular ? 1 : 0;
            GetRandomIndexPair(random, _elements.Length, start, out a, out b);
        }

        private static void GetRandomIndexPair(TRandom random, int n, int start, out int a, out int b)
        {
            a = random.Next(start, n);

            do
            {
                b = random.Next(start, n);
            }
            while (a == b);

            if (b < a)
            {
                var tmp = a;
                a = b;
                b = tmp;
            }
        }
    }
}
