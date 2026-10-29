namespace Evolutionary;

/// <summary>
/// Represents an immutable bit string individual for binary-encoded
/// evolutionary algorithms.
/// </summary>
/// <remarks>
/// A bit string is the natural representation for selection problems like
/// the OneMax problem or the knapsack problem, where each bit encodes a
/// yes/no decision. The default instance of the struct is an empty bit
/// string.
/// </remarks>
public readonly partial struct BitString
{
    private readonly bool[] _bits;

    // The default instance of the struct has a null backing array. To
    // always behave like an empty bit string, the following property
    // must be used instead of the raw field.
    private bool[] Bits => _bits ?? Array.Empty<bool>();

    /// <summary>
    /// Gets the number of bits.
    /// </summary>
    public int Length => Bits.Length;

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
        foreach (var bit in Bits)
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
    public bool[] ToArray() => (bool[])Bits.Clone();

    /// <summary>
    /// Returns the bits as a string in binary literal notation prefixed
    /// with '0b', e.g. <c>0b101</c>.
    /// </summary>
    /// <returns>The string representation.</returns>
    public override string ToString() => "0b" + new string(Bits.Select(b => b ? '1' : '0').ToArray());
}
