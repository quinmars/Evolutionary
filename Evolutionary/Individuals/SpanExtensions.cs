namespace Evolutionary;

/// <summary>
/// Provides span-based operators for permutation individuals.
/// </summary>
internal static class SpanExtensions
{
    /// <summary>
    /// Swaps the elements at the given positions.
    /// </summary>
    /// <param name="span">The span.</param>
    /// <param name="a">The position of the first element.</param>
    /// <param name="b">The position of the second element.</param>
    public static void Swap(this Span<int> span, int a, int b)
    {
        (span[a], span[b]) = (span[b], span[a]);
    }

    /// <summary>
    /// Rotates the span by one position to the right.
    /// </summary>
    /// <param name="span">The span.</param>
    public static void RightRotate(this Span<int> span)
    {
        var lastIndex = span.Length - 1;

        if (lastIndex < 1)
        {
            return;
        }

        var tmp = span[lastIndex];
        for (var i = lastIndex; i > 0; i--)
        {
            span[i] = span[i - 1];
        }

        span[0] = tmp;
    }

    /// <summary>
    /// Rotates the span by <paramref name="k"/> positions to the right.
    /// Negative values of <paramref name="k"/> rotate to the left.
    /// </summary>
    /// <param name="span">The span.</param>
    /// <param name="k">The number of positions to rotate.</param>
    public static void RightRotate(this Span<int> span, int k)
    {
        if (span.Length < 2)
        {
            return;
        }

        k %= span.Length;

        if (k < 0)
        {
            k = span.Length + k;
        }

        switch (k)
        {
            case 0:
                return;
            case 1:
                span.RightRotate();
                return;
            case 2:
                span.RightRotate();
                span.RightRotate();
                return;
            default:
                span.Reverse();
                span.Slice(0, k).Reverse();
                span.Slice(k).Reverse();
                return;
        }
    }

    /// <summary>
    /// Rotates the span by one position to the left.
    /// </summary>
    /// <param name="span">The span.</param>
    public static void LeftRotate(this Span<int> span)
    {
        if (span.Length < 2)
        {
            return;
        }

        var first = span[0];
        for (var i = 1; i < span.Length; i++)
        {
            span[i - 1] = span[i];
        }

        span[span.Length - 1] = first;
    }

    /// <summary>
    /// Rotates the span by <paramref name="k"/> positions to the left.
    /// Negative values of <paramref name="k"/> rotate to the right.
    /// </summary>
    /// <param name="span">The span.</param>
    /// <param name="k">The number of positions to rotate.</param>
    public static void LeftRotate(this Span<int> span, int k)
    {
        if (span.Length < 2)
        {
            return;
        }

        k %= span.Length;

        switch (k)
        {
            case 0:
                return;
            case 1:
                span.LeftRotate();
                return;
            case 2:
                span.LeftRotate();
                span.LeftRotate();
                return;
            default:
                span.RightRotate(span.Length - k);
                return;
        }
    }

    /// <summary>
    /// Randomly shuffles the elements (Fisher–Yates shuffle).
    /// </summary>
    /// <param name="span">The span.</param>
    /// <param name="random">The random number generator.</param>
    public static void Scramble(this Span<int> span, Random random)
    {
        var n = span.Length;
        for (var i = 0; i < n - 1; i++)
        {
            var j = random.Next(i + 1, n);
            span.Swap(i, j);
        }
    }

    /// <summary>
    /// Performs a cycle crossover (CX) of the two given parents and stores
    /// the offspring in the given span.
    /// </summary>
    /// <param name="p1">The first parent.</param>
    /// <param name="p2">The second parent.</param>
    /// <param name="offspring">The span that receives the offspring. It must
    /// have the same length as the parents.</param>
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

    /// <summary>
    /// Performs a cut and crossfill crossover of the two given parents and
    /// stores the offspring in the given span.
    /// </summary>
    /// <param name="p1">The first parent.</param>
    /// <param name="p2">The second parent.</param>
    /// <param name="position">The number of elements that are copied from
    /// the first parent.</param>
    /// <param name="offspring">The span that receives the offspring. It must
    /// have the same length as the parents.</param>
    public static void CutAndCrossfillCrossover(this ReadOnlySpan<int> p1, ReadOnlySpan<int> p2, int position, Span<int> offspring)
    {
        var span = p1.Slice(0, position);
        span.CopyTo(offspring);

        var j = 0;
        for (var i = position; i < offspring.Length; i++)
        {
            while (j < p2.Length && span.IndexOf(p2[j]) != -1)
            {
                j++;
            }
            offspring[i] = p2[j];
            j++;
        }
    }
}
