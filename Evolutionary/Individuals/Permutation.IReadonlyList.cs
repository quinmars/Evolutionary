namespace Evolutionary.Individuals;

public readonly partial struct Permutation : IReadOnlyList<int>
{
    public int this[int index] => _elements[index];

    public int Count => _elements.Length;

    public IEnumerator<int> GetEnumerator()
    {
        return ((IEnumerable<int>)_elements).GetEnumerator();
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return _elements.GetEnumerator();
    }
}
