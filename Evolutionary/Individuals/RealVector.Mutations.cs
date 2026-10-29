namespace Evolutionary;

public static partial class RealVector
{
    /// <summary>
    /// Adds normally distributed noise with the given standard deviation to
    /// every dimension (Gaussian mutation). The noise is generated with the
    /// <see cref="RandomExtensions.NextNormal(Random, double, double)"/>
    /// extension method.
    /// </summary>
    /// <typeparam name="T">The floating point type of the vector elements.
    /// It is inferred from the vector to mutate.</typeparam>
    /// <param name="vector">The vector to mutate.</param>
    /// <param name="random">The random number generator.</param>
    /// <param name="sigma">The standard deviation of the noise.</param>
    /// <returns>The mutated vector.</returns>
    public static RealVector<T> GaussianMutation<T>(RealVector<T> vector, Random random, double sigma)
        where T : IFloatingPoint<T>
    {
        var values = vector.ToArray();

        for (var i = 0; i < values.Length; i++)
        {
            values[i] += T.CreateTruncating(random.NextNormal(0, sigma));
        }

        return new RealVector<T>(values);
    }
}
