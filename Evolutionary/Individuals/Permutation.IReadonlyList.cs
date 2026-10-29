namespace Evolutionary;

public readonly partial struct Permutation : IReadOnlyList<int>
{
    /// <summary>
    /// Gets the element at the given index.
    /// </summary>
    /// <param name="index">The zero-based index of the element.</param>
    /// <returns>The element.</returns>
    public int this[int index] => Elements[index];

    /// <summary>
    /// Gets the number of elements.
    /// </summary>
    public int Count => Elements.Length;

    /// <summary>
    /// Returns an enumerator that iterates through the elements.
    /// </summary>
    /// <returns>The enumerator.</returns>
    public IEnumerator<int> GetEnumerator()
    {
        return ((IEnumerable<int>)Elements).GetEnumerator();
    }

    /// <summary>
    /// Returns an enumerator that iterates through the elements.
    /// </summary>
    /// <returns>The enumerator.</returns>
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return Elements.GetEnumerator();
    }
}
