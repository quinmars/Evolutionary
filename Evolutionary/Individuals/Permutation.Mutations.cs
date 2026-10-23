namespace Evolutionary.Individuals;

public readonly partial struct Permutation
{
    /*
     * Mutations
     */
    public static Permutation SwapMutation(Permutation permutation, Random random)
    {
        var array = permutation.ToArray();
        var (a, b) = permutation.GetRandomIndexPair(random);

        array.AsSpan().Swap(a, b);
        return new Permutation(array, permutation.IsCircular);
    }

    public static Permutation InsertMutation(Permutation permutation, Random random)
    {
        var array = permutation.ToArray();
        var (a, b) = permutation.GetRandomIndexPair(random);

        array.AsSpan(a, b - a).RightRotate();
        return new Permutation(array, permutation.IsCircular);
    }

    public static Permutation ScrambleMutation(Permutation permutation, Random random)
    {
        var array = permutation.ToArray();
        var (a, b) = permutation.GetRandomIndexPair(random);

        array.AsSpan(a, b - a).Scramble(random);
        return new Permutation(array, permutation.IsCircular);
    }

    public static Permutation InversionMutation(Permutation permutation, Random random)
    {
        var array = permutation.ToArray();
        var (a, b) = permutation.GetRandomIndexPair(random);

        array.AsSpan(a, b - a).Reverse();
        return new Permutation(array, permutation.IsCircular);
    }
}
