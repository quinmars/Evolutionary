namespace Evolutionary;

public readonly partial struct Permutation
{
    /// <summary>
    /// Swaps the elements at two randomly selected positions (swap mutation).
    /// </summary>
    /// <param name="permutation">The permutation to mutate.</param>
    /// <param name="random">The random number generator.</param>
    /// <returns>The mutated permutation.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the permutation
    /// is too short for this operation.</exception>
    public static Permutation SwapMutation(Permutation permutation, Random random)
    {
        var array = permutation.ToArray();
        var (a, b) = permutation.GetRandomIndexPair(random);

        array.AsSpan().Swap(a, b);
        return new Permutation(array, permutation.IsCircular);
    }

    /// <summary>
    /// Removes a randomly selected element and reinserts it at another
    /// randomly selected position. The elements in between are shifted
    /// (insertion mutation).
    /// </summary>
    /// <param name="permutation">The permutation to mutate.</param>
    /// <param name="random">The random number generator.</param>
    /// <returns>The mutated permutation.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the permutation
    /// is too short for this operation.</exception>
    public static Permutation InsertMutation(Permutation permutation, Random random)
    {
        var array = permutation.ToArray();
        var (a, b) = permutation.GetRandomIndexPair(random);

        array.AsSpan(a, b - a).RightRotate();
        return new Permutation(array, permutation.IsCircular);
    }

    /// <summary>
    /// Randomly shuffles the elements between two randomly selected positions
    /// (scramble mutation).
    /// </summary>
    /// <param name="permutation">The permutation to mutate.</param>
    /// <param name="random">The random number generator.</param>
    /// <returns>The mutated permutation.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the permutation
    /// is too short for this operation.</exception>
    public static Permutation ScrambleMutation(Permutation permutation, Random random)
    {
        var array = permutation.ToArray();
        var (a, b) = permutation.GetRandomIndexPair(random);

        array.AsSpan(a, b - a).Scramble(random);
        return new Permutation(array, permutation.IsCircular);
    }

    /// <summary>
    /// Reverses the elements between two randomly selected positions
    /// (inversion mutation).
    /// </summary>
    /// <param name="permutation">The permutation to mutate.</param>
    /// <param name="random">The random number generator.</param>
    /// <returns>The mutated permutation.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the permutation
    /// is too short for this operation.</exception>
    public static Permutation InversionMutation(Permutation permutation, Random random)
    {
        var array = permutation.ToArray();
        var (a, b) = permutation.GetRandomIndexPair(random);

        array.AsSpan(a, b - a).Reverse();
        return new Permutation(array, permutation.IsCircular);
    }
}
