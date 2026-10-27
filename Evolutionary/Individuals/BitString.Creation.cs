namespace Evolutionary;

/// <summary>
/// Provides factory methods for random bit strings.
/// </summary>
public readonly partial struct BitString
{
    /// <summary>
    /// Creates a new bit string whose bits are randomly initialized.
    /// </summary>
    /// <param name="n">The number of bits.</param>
    /// <param name="random">The random number generator.</param>
    /// <returns>The random bit string.</returns>
    public static BitString CreateRandomly(int n, Random random)
    {
        var bits = new bool[n];
        for (var i = 0; i < n; i++)
        {
            bits[i] = random.NextBoolean();
        }

        return new BitString(bits);
    }
}
