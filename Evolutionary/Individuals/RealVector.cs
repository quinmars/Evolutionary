using System.Globalization;

namespace Evolutionary;

/// <summary>
/// Represents an immutable real-valued vector individual for continuous
/// evolutionary algorithms.
/// </summary>
/// <typeparam name="T">The floating point type of the vector elements, e.g.
/// <see cref="double"/> or <see cref="float"/>.</typeparam>
/// <remarks>
/// A real-valued vector is the natural representation for continuous
/// optimization problems like the minimization of benchmark functions. The
/// default instance of the struct is an empty vector. The values are
/// formatted with the invariant culture unless a format provider is
/// specified - mathematical output is usually easier to read with a fixed
/// culture.
/// </remarks>
public readonly partial struct RealVector<T> : IFormattable
    where T : IFloatingPoint<T>
{
    private readonly T[] _values;

    // The default instance of the struct has a null backing array. To
    // always behave like an empty vector, the following property must be
    // used instead of the raw field.
    private T[] Values => _values ?? Array.Empty<T>();

    /// <summary>
    /// Gets the number of dimensions.
    /// </summary>
    public int Length => Values.Length;

    /// <summary>
    /// Initializes a new vector with all values set to zero.
    /// </summary>
    /// <param name="n">The number of dimensions.</param>
    public RealVector(int n)
    {
        _values = new T[n];
    }

    /// <summary>
    /// Initializes a new vector from the given array. The array is not
    /// copied and not validated.
    /// </summary>
    /// <param name="values">The values of the vector.</param>
    public RealVector(T[] values)
    {
        _values = values;
    }

    /// <summary>
    /// Returns a copy of the values as array.
    /// </summary>
    /// <returns>The values.</returns>
    public T[] ToArray() => (T[])Values.Clone();

    /// <summary>
    /// Returns the values as string, e.g. <c>[0.5, -1.25]</c>. The values
    /// are formatted with the invariant culture.
    /// </summary>
    /// <returns>The string representation.</returns>
    public override string ToString() => ToString(null, CultureInfo.InvariantCulture);

    /// <summary>
    /// Returns the values as string, e.g. <c>[1.50, -1.25]</c>. The values
    /// are formatted with the given format string and the invariant
    /// culture.
    /// </summary>
    /// <param name="format">The format to use for every value, or
    /// <see langword="null"/> to use the default format.</param>
    /// <returns>The string representation.</returns>
    public string ToString(string? format) => ToString(format, CultureInfo.InvariantCulture);

    /// <summary>
    /// Returns the values as string, e.g. <c>[1.50, -1.25]</c>. The values
    /// are formatted with the given format string.
    /// </summary>
    /// <param name="format">The format to use for every value, or
    /// <see langword="null"/> to use the default format.</param>
    /// <param name="formatProvider">The format provider to use, or
    /// <see langword="null"/> to use the invariant culture.</param>
    /// <returns>The string representation.</returns>
    public string ToString(string? format, IFormatProvider? formatProvider)
        => "[" + string.Join(", ", Values.Select(v => v.ToString(format, formatProvider ?? CultureInfo.InvariantCulture))) + "]";
}
