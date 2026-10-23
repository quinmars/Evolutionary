namespace Evolutionary;

/// <summary>
/// Provides extension methods for the <see cref="Random"/> class, that are
/// missing in the .NET Base Class Library.
/// </summary>
public static class RandomExtensions
{
    /// <summary>
    /// Returns a standard normally distributed random number, that is a
    /// random number with a mean of 0 and a standard deviation of 1.
    /// </summary>
    /// <param name="random">The random number generator.</param>
    /// <returns>A standard normally distributed random number.</returns>
    public static double NextNormal(this Random random)
    {
        ArgumentNullException.ThrowIfNull(random);

        return NextNormal(random, 0.0, 1.0);
    }

    /// <summary>
    /// Returns a normally distributed random number with the given mean
    /// value <paramref name="mu"/> and the standard deviation
    /// <paramref name="sigma"/>.
    /// </summary>
    /// <remarks>
    /// The method uses the Box-Muller transform to convert two uniformly
    /// distributed random values into one normally distributed random value.
    /// </remarks>
    /// <param name="random">The random number generator.</param>
    /// <param name="mu">The mean of the distribution.</param>
    /// <param name="sigma">The standard deviation of the distribution.</param>
    /// <returns>A normally distributed random number.</returns>
    public static double NextNormal(this Random random, double mu, double sigma)
    {
        ArgumentNullException.ThrowIfNull(random);

        // NextDouble() can return 0, but never 1. By subtracting it from 1.0
        // the logarithm argument is guaranteed to be greater than zero.
        var u1 = 1.0 - random.NextDouble();
        var u2 = random.NextDouble();

        var z = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Cos(2.0 * Math.PI * u2);

        return mu + sigma * z;
    }

    /// <summary>
    /// Returns a uniformly distributed random number within the interval
    /// [<paramref name="min"/>, <paramref name="max"/>). The value
    /// <paramref name="max"/> itself is excluded from the interval.
    /// </summary>
    /// <param name="random">The random number generator.</param>
    /// <param name="min">The inclusive lower bound of the interval.</param>
    /// <param name="max">The exclusive upper bound of the interval.</param>
    /// <returns>A uniformly distributed random number within the given
    /// interval.</returns>
    public static double NextDouble(this Random random, double min, double max)
    {
        ArgumentNullException.ThrowIfNull(random);
        ArgumentOutOfRangeException.ThrowIfLessThan(max, min);

        return min + (max - min) * random.NextDouble();
    }
}
