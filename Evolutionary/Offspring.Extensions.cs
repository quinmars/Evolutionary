namespace Evolutionary;

public static class Offspring
{
    public static Offspring<TIndividual> WithChildren<TIndividual>(this Offspring<TIndividual> offspring, IEnumerable<TIndividual> children)
    {
        ArgumentNullException.ThrowIfNull(offspring);
        ArgumentNullException.ThrowIfNull(children);

        return new Offspring<TIndividual>(offspring.ParentPopulation, offspring.RandomParents, children);
    }

    public static Offspring<TIndividual> AddChildren<TIndividual>(this Offspring<TIndividual> offspring, IEnumerable<TIndividual> children)
    {
        ArgumentNullException.ThrowIfNull(offspring);
        ArgumentNullException.ThrowIfNull(children);

        var c = offspring
            .Children
            .Concat(children);

        return offspring.WithChildren(c);
    }

    public static Population<TIndividual> ToPopulation<TIndividual>(this Offspring<TIndividual> offspring)
    {
        ArgumentNullException.ThrowIfNull(offspring);

        return new Population<TIndividual>(offspring.ParentPopulation.Fitness, offspring.ParentPopulation.Random, offspring.Children);
    }

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

    public static Offspring<TIndividual> Recombine<TIndividual>(this Offspring<TIndividual> offspring, int count, Func<TIndividual, TIndividual, TRandom, TIndividual> recombine)
    {
        ArgumentNullException.ThrowIfNull(offspring);
        ArgumentNullException.ThrowIfNull(recombine);

        var random = offspring.ParentPopulation.Random;

        var children = offspring.RandomParents
            .Zip(offspring.RandomParents, (a, b) => recombine(a, b, random))
            .Take(count);

        return offspring.AddChildren(children);
    }

    public static Offspring<TIndividual> Recombine<TIndividual>(this Offspring<TIndividual> offspring, int count, Func<TIndividual, TIndividual, TRandom, IEnumerable<TIndividual>> recombine)
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

    public static Offspring<TIndividual> Mutate<TIndividual>(this Offspring<TIndividual> offspring, int count, Func<TIndividual, TRandom, TIndividual> mutate)
    {
        ArgumentNullException.ThrowIfNull(offspring);
        ArgumentNullException.ThrowIfNull(mutate);

        var random = offspring.ParentPopulation.Random;

        var children = offspring.RandomParents
            .Select(p => mutate(p, random))
            .Take(count);

        return offspring.AddChildren(children);
    }

    public static Offspring<TIndividual> SelectSurvivors<TIndividual>(this Offspring<TIndividual> offspring, int count)
    {
        ArgumentNullException.ThrowIfNull(offspring);

        var fitness = offspring.ParentPopulation.Fitness;

        var children = offspring.Children
            .OrderBy(fitness)
            .Take(count);

        return offspring.WithChildren(children);
    }

    public static Offspring<TIndividual> Eagerly<TIndividual>(this Offspring<TIndividual> offspring)
    {
        ArgumentNullException.ThrowIfNull(offspring);

        return offspring.WithChildren(offspring.Children.ToArray());
    }
}
