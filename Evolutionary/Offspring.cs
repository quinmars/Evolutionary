namespace Evolutionary;

/// <summary>
/// The offspring class.
/// </summary>
/// <typeparam name="TIndividual">The individuals type.</typeparam>
public class Offspring<TIndividual>
{
    /// <summary>
    /// The parent population.
    /// </summary>
    public Population<TIndividual> ParentPopulation { get; }

    /// <summary>
    /// Gets the random number generator that is used by the operators.
    /// </summary>
    public Random Random { get; }

    /// <summary>
    /// A never ending enumeration of a random selection of the parents.
    /// </summary>
    public IEnumerable<TIndividual> RandomParents { get; }

    /// <summary>
    /// The list of children of the generation.
    /// </summary>
    public IEnumerable<TIndividual> Children { get; }

    /// <summary>
    /// Creates a new offspring.
    /// </summary>
    /// <param name="parentPopulation">The parent population.</param>
    /// <param name="random">The random generator that is used by the operators.</param>
    /// <param name="randomParents">A never ending random enumeration over the parents.</param>
    /// <param name="children">The children.</param>
    public Offspring(Population<TIndividual> parentPopulation, Random random, IEnumerable<TIndividual> randomParents, IEnumerable<TIndividual> children)
    {
        ArgumentNullException.ThrowIfNull(parentPopulation);
        ArgumentNullException.ThrowIfNull(random);
        ArgumentNullException.ThrowIfNull(randomParents);
        ArgumentNullException.ThrowIfNull(children);

        ParentPopulation = parentPopulation;
        Random = random;
        RandomParents = randomParents;
        Children = children;
    }
}
