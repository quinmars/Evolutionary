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
}
