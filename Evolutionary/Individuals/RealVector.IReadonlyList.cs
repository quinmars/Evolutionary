namespace Evolutionary;

/// <summary>
/// Provides the read-only list interface for the <see cref="RealVector{T}"/> struct.
/// </summary>
public readonly partial struct RealVector<T> : IReadOnlyList<T>
    where T : IFloatingPoint<T>
{
    /// <summary>
    /// Gets the value at the given dimension.
    /// </summary>
    /// <param name="index">The zero-based index of the dimension.</param>
    /// <returns>The value.</returns>
    public T this[int index] => Values[index];

    /// <summary>
    /// Gets the number of dimensions.
    /// </summary>
    public int Count => Values.Length;

    /// <summary>
    /// Returns an enumerator that iterates through the values.
    /// </summary>
    /// <returns>The enumerator.</returns>
    public IEnumerator<T> GetEnumerator()
    {
        return ((IEnumerable<T>)Values).GetEnumerator();
    }

    /// <summary>
    /// Returns an enumerator that iterates through the values.
    /// </summary>
    /// <returns>The enumerator.</returns>
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return Values.GetEnumerator();
    }
}
