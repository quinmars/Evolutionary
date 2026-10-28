namespace Evolutionary;

public readonly partial struct Permutation
{
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

        PartiallyMappedCrossover(p1, p2, a, b - a, array);

        return new Permutation(array, p1.IsCircular);
    }

    /// <summary>
    /// Creates a child by a partially mapped crossover (PMX). The
    /// selected segment of the first parent is copied into the child, and
    /// the remaining elements are taken from the second parent, with
    /// duplicates being resolved by the partial mapping.
    /// </summary>
    /// <param name="p1">The first parent.</param>
    /// <param name="p2">The second parent.</param>
    /// <param name="start">The start position of the mapped segment.</param>
    /// <param name="length">The length of the mapped segment.</param>
    /// <returns>The child.</returns>
    /// <exception cref="ArgumentException">Thrown if the element counts or
    /// the circularity of both permutations differ.</exception>
    public static Permutation PartiallyMappedCrossover(Permutation p1, Permutation p2, int start, int length)
    {
        if (p1.Count != p2.Count)
        {
            throw new ArgumentException("The element count differs.", nameof(p2));
        }

        if (p1.IsCircular != p2.IsCircular)
        {
            throw new ArgumentException("The circularity differs.", nameof(p2));
        }

        // TODO: Range checks for start and length

        var array = new int[p1.Count];

        PartiallyMappedCrossover(p1, p2, start, length, array);

        return new Permutation(array, p1.IsCircular);
    }

    private static void PartiallyMappedCrossover(ReadOnlySpan<int> p1, ReadOnlySpan<int> p2, int start, int length, Span<int> offspring)
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
        for (var i = start; i < start + length; i++)
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
        for (var i = 0; i < offspring.Length; i++)
        {
            if (offspring[i] == -1)
            {
                offspring[i] = p2[i];
            }
        }
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
