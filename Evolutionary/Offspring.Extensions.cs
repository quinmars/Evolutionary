namespace Evolutionary;

/// <summary>
/// Provides operators for the <see cref="Offspring{TIndividual}"/> class.
/// </summary>
public static class Offspring
{
    /// <summary>
    /// Returns a new offspring with the given children. The previous
    /// children are replaced.
    /// </summary>
    /// <typeparam name="TIndividual">The individuals type.</typeparam>
    /// <param name="offspring">The offspring.</param>
    /// <param name="children">The children.</param>
    /// <returns>The modified offspring.</returns>
    public static Offspring<TIndividual> WithChildren<TIndividual>(this Offspring<TIndividual> offspring, IEnumerable<TIndividual> children)
    {
        ArgumentNullException.ThrowIfNull(offspring);
        ArgumentNullException.ThrowIfNull(children);

        return new Offspring<TIndividual>(offspring.ParentPopulation, offspring.RandomParents, children);
    }

    /// <summary>
    /// Returns a new offspring with the given children appended to the
    /// existing children.
    /// </summary>
    /// <typeparam name="TIndividual">The individuals type.</typeparam>
    /// <param name="offspring">The offspring.</param>
    /// <param name="children">The children to add.</param>
    /// <returns>The modified offspring.</returns>
    public static Offspring<TIndividual> AddChildren<TIndividual>(this Offspring<TIndividual> offspring, IEnumerable<TIndividual> children)
    {
        ArgumentNullException.ThrowIfNull(offspring);
        ArgumentNullException.ThrowIfNull(children);

        var c = offspring
            .Children
            .Concat(children);

        return offspring.WithChildren(c);
    }

    /// <summary>
    /// Converts the offspring into a new population. The individuals of the
    /// new population are sorted by their fitness.
    /// </summary>
    /// <typeparam name="TIndividual">The individuals type.</typeparam>
    /// <param name="offspring">The offspring.</param>
    /// <returns>The new population.</returns>
    public static Population<TIndividual> ToPopulation<TIndividual>(this Offspring<TIndividual> offspring)
    {
        ArgumentNullException.ThrowIfNull(offspring);

        return new Population<TIndividual>(offspring.ParentPopulation.Fitness, offspring.ParentPopulation.Random, offspring.Children);
    }

    /// <summary>
    /// Adds the <paramref name="count"/> best individuals of the parent
    /// population to the children (elitism). The best individuals are never
    /// lost by the following selection.
    /// </summary>
    /// <typeparam name="TIndividual">The individuals type.</typeparam>
    /// <param name="offspring">The offspring.</param>
    /// <param name="count">The number of individuals to preserve.</param>
    /// <returns>The modified offspring.</returns>
    public static Offspring<TIndividual> SelectElite<TIndividual>(this Offspring<TIndividual> offspring, int count)
    {
        ArgumentNullException.ThrowIfNull(offspring);

        var fitness = offspring.ParentPopulation.Fitness;

        var children = offspring
            .ParentPopulation
            .Individuals
            .OrderBy(fitness)
            .Take(count);

        return offspring.AddChildren(children);
    }

    /// <summary>
    /// Adds <paramref name="count"/> children that are created by recombining
    /// pairs of randomly selected parents.
    /// </summary>
    /// <typeparam name="TIndividual">The individuals type.</typeparam>
    /// <param name="offspring">The offspring.</param>
    /// <param name="count">The number of children to create.</param>
    /// <param name="recombine">A function that receives two parents and the
    /// random number generator, and returns a new child.</param>
    /// <returns>The modified offspring.</returns>
    public static Offspring<TIndividual> Recombine<TIndividual>(this Offspring<TIndividual> offspring, int count, Func<TIndividual, TIndividual, Random, TIndividual> recombine)
    {
        ArgumentNullException.ThrowIfNull(offspring);
        ArgumentNullException.ThrowIfNull(recombine);

        var random = offspring.ParentPopulation.Random;

        var children = offspring.RandomParents
            .Zip(offspring.RandomParents, (a, b) => recombine(a, b, random))
            .Take(count);

        return offspring.AddChildren(children);
    }

    /// <summary>
    /// Adds <paramref name="count"/> children that are created by recombining
    /// pairs of randomly selected parents.
    /// </summary>
    /// <typeparam name="TIndividual">The individuals type.</typeparam>
    /// <param name="offspring">The offspring.</param>
    /// <param name="count">The number of children to create.</param>
    /// <param name="recombine">A function that receives two parents and the
    /// random number generator, and returns one or more children.</param>
    /// <returns>The modified offspring.</returns>
    public static Offspring<TIndividual> Recombine<TIndividual>(this Offspring<TIndividual> offspring, int count, Func<TIndividual, TIndividual, Random, IEnumerable<TIndividual>> recombine)
    {
        ArgumentNullException.ThrowIfNull(offspring);
        ArgumentNullException.ThrowIfNull(recombine);

        var random = offspring.ParentPopulation.Random;

        var children = offspring.RandomParents
            .Zip(offspring.RandomParents, (a, b) => recombine(a, b, random))
            .SelectMany(x => x)
            .Take(count);

        return offspring.AddChildren(children);
    }

    /// <summary>
    /// Adds <paramref name="count"/> children that are created by mutating
    /// randomly selected parents.
    /// </summary>
    /// <typeparam name="TIndividual">The individuals type.</typeparam>
    /// <param name="offspring">The offspring.</param>
    /// <param name="count">The number of children to create.</param>
    /// <param name="mutate">A function that receives a parent and the random
    /// number generator, and returns a mutated child.</param>
    /// <returns>The modified offspring.</returns>
    public static Offspring<TIndividual> Mutate<TIndividual>(this Offspring<TIndividual> offspring, int count, Func<TIndividual, Random, TIndividual> mutate)
    {
        ArgumentNullException.ThrowIfNull(offspring);
        ArgumentNullException.ThrowIfNull(mutate);

        var random = offspring.ParentPopulation.Random;

        var children = offspring.RandomParents
            .Select(p => mutate(p, random))
            .Take(count);

        return offspring.AddChildren(children);
    }

    /// <summary>
    /// Reduces the children to the <paramref name="count"/> best individuals.
    /// The children are sorted by their fitness.
    /// </summary>
    /// <typeparam name="TIndividual">The individuals type.</typeparam>
    /// <param name="offspring">The offspring.</param>
    /// <param name="count">The number of children that survive.</param>
    /// <returns>The modified offspring.</returns>
    public static Offspring<TIndividual> SelectSurvivors<TIndividual>(this Offspring<TIndividual> offspring, int count)
    {
        ArgumentNullException.ThrowIfNull(offspring);

        var fitness = offspring.ParentPopulation.Fitness;

        var children = offspring.Children
            .OrderBy(fitness)
            .Take(count);

        return offspring.WithChildren(children);
    }

    /// <summary>
    /// Forces the children to be evaluated eagerly. By default the children
    /// are evaluated lazily; hence repeating an operation on the children
    /// would also repeat all the preceding operations.
    /// </summary>
    /// <typeparam name="TIndividual">The individuals type.</typeparam>
    /// <param name="offspring">The offspring.</param>
    /// <returns>The modified offspring.</returns>
    public static Offspring<TIndividual> Eagerly<TIndividual>(this Offspring<TIndividual> offspring)
    {
        ArgumentNullException.ThrowIfNull(offspring);

        return offspring.WithChildren(offspring.Children.ToArray());
    }
}
