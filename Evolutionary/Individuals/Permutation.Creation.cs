namespace Evolutionary.Individuals;

public readonly partial struct Permutation
{
    public static Permutation CreateRandomly(int n, Random random)
    {
        var permutation = new Permutation(n, false);
        var array = permutation.ToArray();

        array.AsSpan().Scramble(random);
        return new Permutation(array, permutation.IsCircular);
    }

    public static Permutation CreateCircularRandomly(int n, Random random)
    {
        var permutation = new Permutation(n, true);
        var array = permutation.ToArray();

        array.AsSpan(1).Scramble(random);
        return new Permutation(array, permutation.IsCircular);
    }
}
