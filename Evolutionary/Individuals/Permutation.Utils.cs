namespace Evolutionary;

public readonly partial struct Permutation
{
    private (int a, int b) GetRandomIndexPair(Random random)
    {
        var start = IsCircular ? 1 : 0;
        return GetRandomIndexPair(random, Elements.Length, start);
    }

    private static (int a, int b) GetRandomIndexPair(Random random, int n, int start)
    {
        if (n - start < 2)
        {
            throw new InvalidOperationException("The permutation is too short for this operation.");
        }

        var a = random.Next(start, n);

        int b;
        do
        {
            b = random.Next(start, n);
        }
        while (a == b);

        return a < b ? (a, b) : (b, a);
    }

    /// <summary>
    /// Swaps the elements at the given positions.
    /// </summary>
    /// <param name="span">The span.</param>
    /// <param name="a">The position of the first element.</param>
    /// <param name="b">The position of the second element.</param>
    internal static void Swap(Span<int> span, int a, int b)
    {
        (span[a], span[b]) = (span[b], span[a]);
    }

    /// <summary>
    /// Rotates the span by one position to the right.
    /// </summary>
    /// <param name="span">The span.</param>
    internal static void RightRotate(Span<int> span)
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
    internal static void RightRotate(Span<int> span, int k)
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
                RightRotate(span);
                return;
            case 2:
                RightRotate(span);
                RightRotate(span);
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
    internal static void LeftRotate(Span<int> span)
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
    internal static void LeftRotate(Span<int> span, int k)
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
                LeftRotate(span);
                return;
            case 2:
                LeftRotate(span);
                LeftRotate(span);
                return;
            default:
                RightRotate(span, span.Length - k);
                return;
        }
    }

    /// <summary>
    /// Randomly shuffles the elements (Fisher–Yates shuffle).
    /// </summary>
    /// <param name="span">The span.</param>
    /// <param name="random">The random number generator.</param>
    internal static void Scramble(Span<int> span, Random random)
    {
        var n = span.Length;
        for (var i = 0; i < n - 1; i++)
        {
            var j = random.Next(i + 1, n);
            Swap(span, i, j);
        }
    }
}
