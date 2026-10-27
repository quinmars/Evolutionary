namespace Evolutionary;

/// <summary>
/// Represents an immutable bit string individual for binary-encoded
/// evolutionary algorithms.
/// </summary>
/// <remarks>
/// A bit string is the natural representation for selection problems like
/// the OneMax problem or the knapsack problem, where each bit encodes a
/// yes/no decision.
/// </remarks>
public readonly partial struct BitString
{
    private readonly bool[] _bits;

    /// <summary>
    /// Gets the number of bits.
    /// </summary>
    public int Length => _bits.Length;

    /// <summary>
    /// Initializes a new bit string with all bits set to
    /// <see langword="false"/>.
    /// </summary>
    /// <param name="n">The number of bits.</param>
    public BitString(int n)
    {
        _bits = new bool[n];
    }

    /// <summary>
    /// Initializes a new bit string from the given array. The array is not
    /// copied and not validated.
    /// </summary>
    /// <param name="bits">The bits of the bit string.</param>
    public BitString(bool[] bits)
    {
        _bits = bits;
    }

    /// <summary>
    /// Counts the bits that are set (the population count), in analogy to
    /// System.Numerics.BitOperations.PopCount.
    /// </summary>
    /// <returns>The number of bits that are set.</returns>
    public int PopCount()
    {
        var count = 0;
        foreach (var bit in _bits)
        {
            if (bit)
            {
                count++;
            }
        }

        return count;
    }

    /// <summary>
    /// Returns a copy of the bits as array.
    /// </summary>
    /// <returns>The bits.</returns>
    public bool[] ToArray() => (bool[])_bits.Clone();

    /// <summary>
    /// Returns the bits as a string in binary literal notation prefixed
    /// with '0b', e.g. <c>0b101</c>.
    /// </summary>
    /// <returns>The string representation.</returns>
    public override string ToString() => "0b" + new string(_bits.Select(b => b ? '1' : '0').ToArray());
}
