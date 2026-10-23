namespace Evolutionary.Individuals;

public readonly partial struct Permutation
{
    private (int a, int b) GetRandomIndexPair(Random random)
    {
        var start = IsCircular ? 1 : 0;
        return GetRandomIndexPair(random, _elements.Length, start);
    }

    private static (int a, int b) GetRandomIndexPair(Random random, int n, int start)
    {
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
