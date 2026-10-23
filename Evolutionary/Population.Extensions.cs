namespace Evolutionary;

public static class Population
{
    /// <summary>
    /// Creates a new population with zero individuals.
    /// </summary>
    /// <typeparam name="TIndividual">The individuals type.</typeparam>
    /// <param name="fitness">The fitness function.</param>
    /// <param name="random">The random generator.</param>
    /// <returns>The population.</returns>
    public static Population<TIndividual> Create<TIndividual>(Func<TIndividual, double> fitness, Random random)
    {
        ArgumentNullException.ThrowIfNull(fitness);

        return new Population<TIndividual>(fitness, random);
    }

    /// <summary>
    /// Creates a new population with zero individuals.
    /// </summary>
    /// <typeparam name="TIndividual">The individuals type.</typeparam>
    /// <param name="fitness">The fitness function.</param>
    /// <returns>The population.</returns>
    public static Population<TIndividual> Create<TIndividual>(Func<TIndividual, double> fitness)
    {
        ArgumentNullException.ThrowIfNull(fitness);

        return new Population<TIndividual>(fitness, new Random());
    }

    /// <summary>
    /// Generates an initial offspring generation. The random parents are selected by
    /// rank lineary.
    /// </summary>
    /// <typeparam name="TIndividual">The individuals type.</typeparam>
    /// <param name="population">The parent generation.</param>
    /// <returns>The offspring.</returns>
    public static Offspring<TIndividual> SelectParentsByRank<TIndividual>(this Population<TIndividual> population)
    {
        ArgumentNullException.ThrowIfNull(population);

        var random = population.Random;
        var randomParents = population.GetRandomIndividualsByRank(random);

        return new Offspring<TIndividual>(population, randomParents, Enumerable.Empty<TIndividual>());
    }

    /// <summary>
    /// Adds some individuals to the population.
    /// </summary>
    /// <typeparam name="TIndividual">The individuals type.</typeparam>
    /// <param name="population">The populations.</param>
    /// <param name="individuals">The individuals.</param>
    /// <returns>The extended population.</returns>
    public static Population<TIndividual> AddIndividuals<TIndividual>(this Population<TIndividual> population, IEnumerable<TIndividual> individuals)
    {
        ArgumentNullException.ThrowIfNull(population);
        ArgumentNullException.ThrowIfNull(individuals);

        var allIndividuals = population.Individuals.Concat(individuals);

        return new Population<TIndividual>(population.Fitness, population.Random, allIndividuals);
    }

    /// <summary>
    /// Adds some random individuals to the population.
    /// </summary>
    /// <typeparam name="TIndividual">The individuals type.</typeparam>
    /// <param name="population">The populations.</param>
    /// <param name="count">The count of individuals to add.</param>
    /// <param name="randomIndividual">The function to generate the individuals.</param>
    /// <returns>The extended population.</returns>
    public static Population<TIndividual> AddRandomIndividuals<TIndividual>(this Population<TIndividual> population, int count, Func<Random, TIndividual> randomIndividual)
    {
        ArgumentNullException.ThrowIfNull(population);
        ArgumentNullException.ThrowIfNull(randomIndividual);

        var random = population.Random;

        var individuals = Enumerable
            .Repeat(0, count)
            .Select(_ => randomIndividual(random));

        return population.AddIndividuals(individuals);
    }

    private static IEnumerable<TIndividual> GetRandomIndividualsByRank<TIndividual>(this Population<TIndividual> population, Random random)
    {
        var individuals = population.Individuals;

        while (true)
        {
            yield return GetRandomIndividual(individuals, random);
        }
    }

    private static TIndividual GetRandomIndividual<TIndividual>(IReadOnlyList<TIndividual> individuals, Random rnd)
    {
        var count = (double)individuals.Count;
        double p(int index)
        {
            var rank = count - index;
            return 2 * rank / (count * (count + 1.0));
        }

        var r = rnd.NextDouble();

        var minP = 0.0;
        var i = 0;

        foreach (var individual in individuals)
        {
            var maxP = p(i) + minP;

            if (r >= minP && r <= maxP)
            {
                return individual;
            }
            i++;
            minP = maxP;
        }

        return individuals.Last();
    }
}
