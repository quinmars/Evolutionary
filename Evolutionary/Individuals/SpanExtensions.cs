using System;
using System.Collections.Generic;
using System.Text;
using Troschuetz.Random;

namespace Evolutionary.Individuals
{
    public static class SpanExtensions
    {
        public static void Swap(this Span<int> span, int a, int b)
        {
            int tmp = span[a];
            span[a] = span[b];
            span[b] = tmp;
        }

        public static void RightShift(this Span<int> span)
        {
            var lastIndex = span.Length - 1;

            if (lastIndex < 1)
            {
                return;
            }

            int tmp = span[lastIndex];
            for (int i = lastIndex; i > 0; i--)
            {
                span[i] = span[i - 1];
            }

            span[0] = tmp;
        }

        public static void Scramble(this Span<int> span, TRandom random)
        {
            var n = span.Length;
            for (int i = 0; i < n - 1; i++)
            {
                var j = random.Next(i + 1, n);
                span.Swap(i, j);
            }
        }

        public static void PartiallyMappedCrossover(this ReadOnlySpan<int> p1, ReadOnlySpan<int> p2, int start, int length, Span<int> offspring)
        {
            // The following description is taken from the book
            // A.E. Eiben and J.E. Smith: "Introduction to Evolutionary Computing", 2015 (2nd Edition)
            // 
            offspring.Fill(-1);

            var segment1 = p1.Slice(start, length);

            // Step 1: copy the segment from the first parent into the offspring
            //
            //      123456789
            //                   -->  ___4567__
            //      937826514
            //
            segment1.CopyTo(offspring.Slice(start, length));

            // Step 2: Starting from the crossover point look for elements in
            // that segment of the second parent that have not been copied.
            for (int i = start; i < start + length; i++)
            {
                var e = p2[i];
                if (segment1.IndexOf(e) != -1)
                {
                    continue;
                }

                // Step 3: For each of these (say e), look in the offspring to
                // see what element (say f) has been copied in its place from
                // p1.
                //
                //  123456789
                //               -->  ___4567_8
                //  937826514
                //     ^    ^
                //     i    j

                // Step 4: Place e into the position occupied by f in p2,
                // since we know that we will not be putting f there.
                //
                // Step5: If the place occupied by j in p2 has already been
                // filled in the offspring by an element f, put i in the
                // position occupied by f in p2.
                //
                //        f
                //  123456789
                //               -->  __24567_8
                //  937826514
                //    f ^ ^
                //      i j
                var j = i;
                do
                {
                    var f = offspring[j];
                    j = p2.IndexOf(f);
                }
                while (offspring[j] != -1);

                offspring[j] = e;
            }

            // Step 6: The remaining positions in this offspring can be filled
            // from p2.
            for (int i = 0; i < offspring.Length; i++)
            {
                if (offspring[i] == -1)
                {
                    offspring[i] = p2[i];
                }
            }
        }
        
        public static void CycleCrossover(this ReadOnlySpan<int> p1, ReadOnlySpan<int> p2, Span<int> offspring)
        {
            offspring.Fill(-1);

            var main = p1;
            var compliment = p2;

            for (var i = 0; i < offspring.Length; i++)
            {
                if (offspring[i] != -1)
                {
                    continue;
                }

                var j = i;

                do
                {
                    offspring[j] = main[j];
                    j = main.IndexOf(compliment[j]);
                }
                while (j != i);

                var tmp = main;
                main = compliment;
                compliment = tmp;
            }
        }

        public static void CutAndCrossfillCrossover(this ReadOnlySpan<int> p1, ReadOnlySpan<int> p2, int position, Span<int> offspring)
        {
            var span = p1.Slice(0, position);
            span.CopyTo(offspring);

            int j = 0;
            for (int i = position; i < offspring.Length; i++)
            {
                var element = p2[j];
                while (j < p2.Length && span.IndexOf(p2[j]) != -1)
                {
                    j++;
                }
                offspring[i] = p2[j];
                j++;
            }
        }
    }
}
