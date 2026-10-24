namespace Evolutionary.Individuals;

public readonly partial struct Permutation : IEquatable<Permutation>
{
    /// <summary>
    /// Indicates whether the current permutation and the given one are
    /// equal. Two permutations are equal if they have the same circularity
    /// and the same sequence of elements.
    /// </summary>
    /// <param name="other">The other permutation.</param>
    /// <returns><see langword="true"/> if both permutations are equal.</returns>
    public bool Equals(Permutation other)
        => IsCircular == other.IsCircular
        &&
        (
            ReferenceEquals(_elements, other._elements)
            ||
            (
                _elements != null
                && other._elements != null
                && _elements.SequenceEqual(other._elements)
            )
        );

    /// <summary>
    /// Indicates whether two permutations are equal.
    /// </summary>
    /// <param name="lhs">The first permutation.</param>
    /// <param name="rhs">The second permutation.</param>
    /// <returns><see langword="true"/> if both permutations are equal.</returns>
    public static bool operator ==(Permutation lhs, Permutation rhs)
        => lhs.Equals(rhs);

    /// <summary>
    /// Indicates whether two permutations are not equal.
    /// </summary>
    /// <param name="lhs">The first permutation.</param>
    /// <param name="rhs">The second permutation.</param>
    /// <returns><see langword="true"/> if both permutations are not equal.</returns>
    public static bool operator !=(Permutation lhs, Permutation rhs)
        => !lhs.Equals(rhs);

    /// <summary>
    /// Indicates whether the current permutation and the given object are
    /// equal.
    /// </summary>
    /// <param name="obj">The object to compare with.</param>
    /// <returns><see langword="true"/> if both are equal.</returns>
    public override bool Equals(object? obj)
        => obj is Permutation other && Equals(other);

    /// <summary>
    /// Returns a hash code for the permutation. Two equal permutations have
    /// the same hash code.
    /// </summary>
    /// <returns>The hash code.</returns>
    public override int GetHashCode()
    {
        var hash = new HashCode();

        hash.Add(IsCircular);

        if (_elements != null)
        {
            hash.Add(0);
            foreach (var e in _elements)
                hash.Add(e);
        }

        return hash.ToHashCode();
    }
}
