namespace Evolutionary;

/// <summary>
/// Provides recombination (crossover) operators for the
/// <see cref="BitString"/> struct.
/// </summary>
public readonly partial struct BitString
{
    /// <summary>
    /// Creates a child by a uniform crossover. Each bit is taken from one of
    /// the parents with equal probability.
    /// </summary>
    /// <param name="p1">The first parent.</param>
    /// <param name="p2">The second parent.</param>
    /// <param name="random">The random number generator.</param>
    /// <returns>The child.</returns>
    /// <exception cref="ArgumentException">Thrown if the lengths of both bit
    /// strings differ.</exception>
    public static BitString UniformCrossover(BitString p1, BitString p2, Random random)
    {
        if (p1.Length != p2.Length)
        {
            throw new ArgumentException("The length differs.", nameof(p2));
        }

        var bits = new bool[p1.Length];
        for (var i = 0; i < bits.Length; i++)
        {
            bits[i] = random.NextBoolean() ? p1.Bits[i] : p2.Bits[i];
        }

        return new BitString(bits);
    }

    /// <summary>
    /// Creates a child by a one-point crossover. The bits before a randomly
    /// selected cut position are taken from the first parent and the
    /// remaining bits from the second parent.
    /// </summary>
    /// <param name="p1">The first parent.</param>
    /// <param name="p2">The second parent.</param>
    /// <param name="random">The random number generator.</param>
    /// <returns>The child.</returns>
    /// <exception cref="ArgumentException">Thrown if the lengths of both bit
    /// strings differ.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the bit string
    /// is too short for this operation.</exception>
    public static BitString OnePointCrossover(BitString p1, BitString p2, Random random)
    {
        if (p1.Length != p2.Length)
        {
            throw new ArgumentException("The length differs.", nameof(p2));
        }

        if (p1.Length == 0)
        {
            throw new InvalidOperationException("The bit string is too short for this operation.");
        }

        var cut = random.Next(1, p1.Length);
        var bits = new bool[p1.Length];

        for (var i = 0; i < cut; i++)
        {
            bits[i] = p1.Bits[i];
        }

        for (var i = cut; i < bits.Length; i++)
        {
            bits[i] = p2.Bits[i];
        }

        return new BitString(bits);
    }
}
