namespace Evolutionary;

/// <summary>
/// Provides mutation operators for the <see cref="BitString"/> struct.
/// </summary>
public readonly partial struct BitString
{
    /// <summary>
    /// Flips each bit independently with the given probability (bit-flip
    /// mutation). A common choice for the probability is 1/Length.
    /// </summary>
    /// <param name="bitString">The bit string to mutate.</param>
    /// <param name="random">The random number generator.</param>
    /// <param name="probability">The probability to flip a single bit.</param>
    /// <returns>The mutated bit string.</returns>
    public static BitString FlipMutation(BitString bitString, Random random, double probability)
    {
        var bits = bitString.ToArray();
        for (var i = 0; i < bits.Length; i++)
        {
            if (random.NextDouble() < probability)
            {
                bits[i] = !bits[i];
            }
        }

        return new BitString(bits);
    }
}
