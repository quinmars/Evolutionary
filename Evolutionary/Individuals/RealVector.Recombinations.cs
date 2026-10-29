namespace Evolutionary;

public static partial class RealVector
{
    /// <summary>
    /// Creates a child by a uniform crossover. Each value is taken from one
    /// of the parents with equal probability.
    /// </summary>
    /// <typeparam name="T">The floating point type of the vector elements.
    /// It is inferred from the parents.</typeparam>
    /// <param name="p1">The first parent.</param>
    /// <param name="p2">The second parent.</param>
    /// <param name="random">The random number generator.</param>
    /// <returns>The child.</returns>
    /// <exception cref="ArgumentException">Thrown if the lengths of both
    /// vectors differ.</exception>
    public static RealVector<T> UniformCrossover<T>(RealVector<T> p1, RealVector<T> p2, Random random)
        where T : IFloatingPoint<T>
    {
        if (p1.Length != p2.Length)
        {
            throw new ArgumentException("The length differs.", nameof(p2));
        }

        var values = new T[p1.Length];

        for (var i = 0; i < values.Length; i++)
        {
            values[i] = random.NextBoolean() ? p1[i] : p2[i];
        }

        return new RealVector<T>(values);
    }

    /// <summary>
    /// Creates a child by a whole arithmetic crossover. The child is the
    /// weighted mean of both parents, where the weight is a single random
    /// value between 0 and 1. The child may therefore lie outside of the
    /// interval spanned by the parents.
    /// </summary>
    /// <typeparam name="T">The floating point type of the vector elements.
    /// It is inferred from the parents.</typeparam>
    /// <param name="p1">The first parent.</param>
    /// <param name="p2">The second parent.</param>
    /// <param name="random">The random number generator.</param>
    /// <returns>The child.</returns>
    /// <exception cref="ArgumentException">Thrown if the lengths of both
    /// vectors differ.</exception>
    public static RealVector<T> ArithmeticCrossover<T>(RealVector<T> p1, RealVector<T> p2, Random random)
        where T : IFloatingPoint<T>
    {
        if (p1.Length != p2.Length)
        {
            throw new ArgumentException("The length differs.", nameof(p2));
        }

        var alpha = T.CreateTruncating(random.NextDouble());
        var values = new T[p1.Length];

        for (var i = 0; i < values.Length; i++)
        {
            values[i] = alpha * p1[i] + (T.One - alpha) * p2[i];
        }

        return new RealVector<T>(values);
    }
}
