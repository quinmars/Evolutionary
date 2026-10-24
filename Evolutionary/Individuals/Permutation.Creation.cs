namespace Evolutionary;

public readonly partial struct Permutation
{
    /// <summary>
    /// Creates a new linear permutation whose elements are shuffled randomly.
    /// </summary>
    /// <param name="n">The number of elements.</param>
    /// <param name="random">The random number generator.</param>
    /// <returns>The random permutation.</returns>
    public static Permutation CreateRandomly(int n, Random random)
    {
        var permutation = new Permutation(n, false);
        var array = permutation.ToArray();

        array.AsSpan().Scramble(random);
        return new Permutation(array, permutation.IsCircular);
    }

    /// <summary>
    /// Creates a new circular permutation whose elements are shuffled
    /// randomly. The first element is always 0, as the circular permutation
    /// has no well-defined first element.
    /// </summary>
    /// <param name="n">The number of elements.</param>
    /// <param name="random">The random number generator.</param>
    /// <returns>The random circular permutation.</returns>
    public static Permutation CreateCircularRandomly(int n, Random random)
    {
        var permutation = new Permutation(n, true);
        var array = permutation.ToArray();

        array.AsSpan(1).Scramble(random);
        return new Permutation(array, permutation.IsCircular);
    }
}
