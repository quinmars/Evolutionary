namespace Evolutionary;

/// <summary>
/// Provides the read-only list interface for the <see cref="BitString"/> struct.
/// </summary>
public readonly partial struct BitString : IReadOnlyList<bool>
{
    /// <summary>
    /// Gets the bit at the given index.
    /// </summary>
    /// <param name="index">The zero-based index of the bit.</param>
    /// <returns>The bit.</returns>
    public bool this[int index] => Bits[index];

    /// <summary>
    /// Gets the number of bits.
    /// </summary>
    public int Count => Bits.Length;

    /// <summary>
    /// Returns an enumerator that iterates through the bits.
    /// </summary>
    /// <returns>The enumerator.</returns>
    public IEnumerator<bool> GetEnumerator()
    {
        return ((IEnumerable<bool>)Bits).GetEnumerator();
    }

    /// <summary>
    /// Returns an enumerator that iterates through the bits.
    /// </summary>
    /// <returns>The enumerator.</returns>
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return Bits.GetEnumerator();
    }
}
