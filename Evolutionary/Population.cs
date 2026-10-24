namespace Evolutionary;

/// <summary>
/// The population class.
/// </summary>
/// <typeparam name="TIndividual">The individuals type.</typeparam>
public class Population<TIndividual>
{
    private static readonly IReadOnlyList<TIndividual> _emptyIndividuals = [];

    /// <summary>
    /// Gets the fitness function that is used to rank the individuals.
    /// </summary>
    public Func<TIndividual, double> Fitness { get; }

    /// <summary>
    /// Gets the random number generator that is used by the operators.
    /// </summary>
    public Random Random { get; }

    /// <summary>
    /// The individuals of the population. The individuals are order by their fitness increasingly.
    /// Hence the first element is always the best fitting individual.
    /// </summary>
    public IReadOnlyList<TIndividual> Individuals { get; }

    /// <summary>
    /// Creates a new population with zero individuals.
    /// </summary>
    /// <param name="fitness">The fitness function.</param>
    /// <param name="random">The random generator.</param>
    public Population(Func<TIndividual, double> fitness, Random random)
    {
        ArgumentNullException.ThrowIfNull(fitness);
        ArgumentNullException.ThrowIfNull(random);

        Fitness = fitness;
        Random = random;
        Individuals = _emptyIndividuals;
    }

    /// <summary>
    /// Creates a new population with some individuals.
    /// </summary>
    /// <param name="fitness">The fitness function.</param>
    /// <param name="random">The random generator.</param>
    /// <param name="individuals">The individuals.</param>
    public Population(Func<TIndividual, double> fitness, Random random, IEnumerable<TIndividual> individuals)
    {
        ArgumentNullException.ThrowIfNull(fitness);
        ArgumentNullException.ThrowIfNull(random);
        ArgumentNullException.ThrowIfNull(individuals);

        Fitness = fitness;
        Random = random;
        Individuals = individuals
            .OrderBy(fitness)
            .ToList();
    }
}
