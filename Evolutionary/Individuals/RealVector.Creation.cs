namespace Evolutionary;

/// <summary>
/// Provides factory and operator methods for the <see cref="RealVector{T}"/> struct.
/// </summary>
public static partial class RealVector
{
    /// <summary>
    /// Creates a new vector whose values are uniformly distributed between
    /// <paramref name="min"/> (inclusive) and <paramref name="max"/>
    /// (exclusive).
    /// </summary>
    /// <typeparam name="T">The floating point type of the vector elements, e.g.
    /// <see cref="double"/> or <see cref="float"/>. It is inferred from the
    /// bounds of the interval.</typeparam>
    /// <param name="n">The number of dimensions.</param>
    /// <param name="min">The inclusive lower bound of the interval.</param>
    /// <param name="max">The exclusive upper bound of the interval.</param>
    /// <param name="random">The random number generator.</param>
    /// <returns>The random vector.</returns>
    public static RealVector<T> CreateRandomly<T>(int n, T min, T max, Random random)
        where T : IFloatingPoint<T>
    {
        var values = new T[n];

        for (var i = 0; i < n; i++)
        {
            values[i] = min + (max - min) * T.CreateTruncating(random.NextDouble());
        }

        return new RealVector<T>(values);
    }
}
