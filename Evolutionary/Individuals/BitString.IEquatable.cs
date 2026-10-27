namespace Evolutionary;

/// <summary>
/// Provides value equality for the <see cref="BitString"/> struct. Two bit
/// strings are equal if they have the same sequence of bits.
/// </summary>
public readonly partial struct BitString : IEquatable<BitString>
{
    /// <summary>
    /// Indicates whether the current bit string and the given one are equal.
    /// </summary>
    /// <param name="other">The other bit string.</param>
    /// <returns><see langword="true"/> if both bit strings are equal.</returns>
    public bool Equals(BitString other)
        => ReferenceEquals(_bits, other._bits)
        ||
        (
            _bits != null
            && other._bits != null
            && _bits.SequenceEqual(other._bits)
        );

    /// <summary>
    /// Indicates whether two bit strings are equal.
    /// </summary>
    /// <param name="lhs">The first bit string.</param>
    /// <param name="rhs">The second bit string.</param>
    /// <returns><see langword="true"/> if both bit strings are equal.</returns>
    public static bool operator ==(BitString lhs, BitString rhs)
        => lhs.Equals(rhs);

    /// <summary>
    /// Indicates whether two bit strings are not equal.
    /// </summary>
    /// <param name="lhs">The first bit string.</param>
    /// <param name="rhs">The second bit string.</param>
    /// <returns><see langword="true"/> if both bit strings are not equal.</returns>
    public static bool operator !=(BitString lhs, BitString rhs)
        => !lhs.Equals(rhs);

    /// <summary>
    /// Indicates whether the current bit string and the given object are
    /// equal.
    /// </summary>
    /// <param name="obj">The object to compare with.</param>
    /// <returns><see langword="true"/> if both are equal.</returns>
    public override bool Equals(object? obj)
        => obj is BitString other && Equals(other);

    /// <summary>
    /// Returns a hash code for the bit string. Two equal bit strings have
    /// the same hash code.
    /// </summary>
    /// <returns>The hash code.</returns>
    public override int GetHashCode()
    {
        var hash = new HashCode();

        if (_bits != null)
        {
            foreach (var bit in _bits)
            {
                hash.Add(bit);
            }
        }

        return hash.ToHashCode();
    }
}
