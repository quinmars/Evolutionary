namespace Evolutionary;

/// <summary>
/// Provides value equality for the <see cref="RealVector{T}"/> struct. Two
/// vectors are equal if they have the same sequence of values. Note that
/// the values are compared with the default equality comparer of
/// <typeparamref name="T"/>; as with <see cref="double.Equals(double)"/>,
/// NaN values are considered equal.
/// </summary>
public readonly partial struct RealVector<T> : IEquatable<RealVector<T>>
    where T : IFloatingPoint<T>
{
    /// <summary>
    /// Indicates whether the current vector and the given one are equal.
    /// </summary>
    /// <param name="other">The other vector.</param>
    /// <returns><see langword="true"/> if both vectors are equal.</returns>
    public bool Equals(RealVector<T> other)
        => ReferenceEquals(_values, other._values)
        || Values.SequenceEqual(other.Values);

    /// <summary>
    /// Indicates whether two vectors are equal.
    /// </summary>
    /// <param name="lhs">The first vector.</param>
    /// <param name="rhs">The second vector.</param>
    /// <returns><see langword="true"/> if both vectors are equal.</returns>
    public static bool operator ==(RealVector<T> lhs, RealVector<T> rhs)
        => lhs.Equals(rhs);

    /// <summary>
    /// Indicates whether two vectors are not equal.
    /// </summary>
    /// <param name="lhs">The first vector.</param>
    /// <param name="rhs">The second vector.</param>
    /// <returns><see langword="true"/> if both vectors are not equal.</returns>
    public static bool operator !=(RealVector<T> lhs, RealVector<T> rhs)
        => !lhs.Equals(rhs);

    /// <summary>
    /// Indicates whether the current vector and the given object are equal.
    /// </summary>
    /// <param name="obj">The object to compare with.</param>
    /// <returns><see langword="true"/> if both are equal.</returns>
    public override bool Equals(object? obj)
        => obj is RealVector<T> other && Equals(other);

    /// <summary>
    /// Returns a hash code for the vector. Two equal vectors have the same
    /// hash code.
    /// </summary>
    /// <returns>The hash code.</returns>
    public override int GetHashCode()
    {
        var hash = new HashCode();

        foreach (var value in Values)
        {
            hash.Add(value);
        }

        return hash.ToHashCode();
    }
}
