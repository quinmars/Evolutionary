namespace Evolutionary;

/// <summary>
/// Represents a permutation of the numbers 0 to n-1. The permutation is an
/// immutable individual for permutation-based evolutionary algorithms.
/// </summary>
/// <remarks>
/// A permutation can either be linear or circular. A circular permutation
/// treats the first and the last element as adjacent. The default instance
/// of the struct is an empty linear permutation.
/// </remarks>
public readonly partial struct Permutation
{
    private readonly int[] _elements;

    // The default instance of the struct has a null backing array. To
    // always behave like an empty linear permutation, the following
    // property must be used instead of the raw field.
    private int[] Elements => _elements ?? Array.Empty<int>();

    /// <summary>
    /// Gets a value indicating whether the permutation is circular.
    /// </summary>
    public bool IsCircular { get; }

    /// <summary>
    /// Initializes a new linear permutation of the numbers 0 to
    /// <paramref name="n"/>-1.
    /// </summary>
    /// <param name="n">The number of elements.</param>
    public Permutation(int n)
    {
        var array = new int[n];

        for (var i = 0; i < n; i++)
        {
            array[i] = i;
        }

        _elements = array;
        IsCircular = false;
    }

    /// <summary>
    /// Initializes a new permutation of the numbers 0 to
    /// <paramref name="n"/>-1.
    /// </summary>
    /// <param name="n">The number of elements.</param>
    /// <param name="isCircular">Indicates whether the permutation is circular.</param>
    public Permutation(int n, bool isCircular)
    {
        var array = new int[n];

        for (var i = 0; i < n; i++)
        {
            array[i] = i;
        }

        _elements = array;
        IsCircular = isCircular;
    }

    /// <summary>
    /// Initializes a new permutation from the given array. The array is not
    /// copied and not validated.
    /// </summary>
    /// <param name="array">The elements of the permutation.</param>
    /// <param name="isCircular">Indicates whether the permutation is circular.</param>
    public Permutation(int[] array, bool isCircular)
    {
        _elements = array;
        IsCircular = isCircular;
    }

    /// <summary>
    /// Returns a copy of the permutation as array.
    /// </summary>
    /// <returns>The elements of the permutation.</returns>
    public int[] ToArray()
    {
        var elements = Elements;
        var ret = new int[elements.Length];
        elements.CopyTo(ret.AsSpan());

        return ret;
    }

    /// <summary>
    /// Converts the permutation into a read-only span of its elements.
    /// </summary>
    /// <param name="permutation">The permutation.</param>
    public static implicit operator ReadOnlySpan<int>(Permutation permutation)
    {
        return permutation.Elements;
    }
}
