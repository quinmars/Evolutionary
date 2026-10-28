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
}
