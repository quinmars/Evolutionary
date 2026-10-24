namespace Evolutionary.Individuals;

public readonly partial struct Permutation
{
    /*
     * Recombinations
     */

    /// <summary>
    /// Creates a child by a partially mapped crossover (PMX). A randomly
    /// selected segment of the first parent is copied into the child, and
    /// the remaining elements are taken from the second parent, with
    /// duplicates being resolved by the partial mapping.
    /// </summary>
    /// <param name="p1">The first parent.</param>
    /// <param name="p2">The second parent.</param>
    /// <param name="random">The random number generator.</param>
    /// <returns>The child.</returns>
    /// <exception cref="ArgumentException">Thrown if the element counts or
    /// the circularity of both permutations differ.</exception>
    public static Permutation PartiallyMappedCrossover(Permutation p1, Permutation p2, Random random)
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
        var (a, b) = p1.GetRandomIndexPair(random);

        SpanExtensions.PartiallyMappedCrossover(p1, p2, a, b - a, array);

        return new Permutation(array, p1.IsCircular);
    }

    /// <summary>
    /// Creates a child by a cut and crossfill crossover. The prefix of the
    /// first parent is copied into the child, and the child is completed by
    /// the remaining elements of the second parent in their original order.
    /// </summary>
    /// <param name="p1">The first parent.</param>
    /// <param name="p2">The second parent.</param>
    /// <param name="random">The random number generator.</param>
    /// <returns>The child.</returns>
    /// <exception cref="ArgumentException">Thrown if the element counts or
    /// the circularity of both permutations differ.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the permutation
    /// is too short for this operation.</exception>
    public static Permutation CutAndCrossfillCrossover(Permutation p1, Permutation p2, Random random)
    {
        if (p1.Count != p2.Count)
        {
            throw new ArgumentException("The element count differs.", nameof(p2));
        }

        if (p1.IsCircular != p2.IsCircular)
        {
            throw new ArgumentException("The circularity differs.", nameof(p2));
        }

        var min = (p1.IsCircular ? 1 : 0) + 1;
        var max = p1.Count - 1;

        if (max < min)
        {
            throw new InvalidOperationException("The permutation is to short for this operation.");
        }

        var position = (max == min)
            ? min
            : random.Next(min, max);

        var array = new int[p1.Count];

        SpanExtensions.CutAndCrossfillCrossover(p1, p2, position, array);

        return new Permutation(array, p1.IsCircular);
    }

    /// <summary>
    /// Creates a child by a cycle crossover (CX). The elements at cycle
    /// positions are taken from the first parent and the others from the
    /// second parent.
    /// </summary>
    /// <param name="p1">The first parent.</param>
    /// <param name="p2">The second parent.</param>
    /// <returns>The child.</returns>
    /// <exception cref="ArgumentException">Thrown if the element counts or
    /// the circularity of both permutations differ.</exception>
    public static Permutation CycleCrossover(Permutation p1, Permutation p2)
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

        SpanExtensions.CycleCrossover(p1, p2, array);

        return new Permutation(array, p1.IsCircular);
    }

    /// <summary>
    /// Creates a child by a cycle crossover (CX). The random number generator
    /// is not used; the overload exists for API consistency with the other
    /// recombination operators.
    /// </summary>
    /// <param name="p1">The first parent.</param>
    /// <param name="p2">The second parent.</param>
    /// <param name="random">The random number generator (unused).</param>
    /// <returns>The child.</returns>
    /// <exception cref="ArgumentException">Thrown if the element counts or
    /// the circularity of both permutations differ.</exception>
    public static Permutation CycleCrossover(Permutation p1, Permutation p2, Random random)
        => CycleCrossover(p1, p2);
}
