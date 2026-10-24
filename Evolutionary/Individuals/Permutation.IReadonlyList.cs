namespace Evolutionary;

public readonly partial struct Permutation : IReadOnlyList<int>
{
    /// <summary>
    /// Gets the element at the given index.
    /// </summary>
    /// <param name="index">The zero-based index of the element.</param>
    /// <returns>The element.</returns>
    public int this[int index] => _elements[index];

    /// <summary>
    /// Gets the number of elements.
    /// </summary>
    public int Count => _elements.Length;

    /// <summary>
    /// Returns an enumerator that iterates through the elements.
    /// </summary>
    /// <returns>The enumerator.</returns>
    public IEnumerator<int> GetEnumerator()
    {
        return ((IEnumerable<int>)_elements).GetEnumerator();
    }

    /// <summary>
    /// Returns an enumerator that iterates through the elements.
    /// </summary>
    /// <returns>The enumerator.</returns>
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return _elements.GetEnumerator();
    }
}
