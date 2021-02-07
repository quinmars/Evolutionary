using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Troschuetz.Random;

namespace Evolutionary
{
    public static class Offspring
    {
        public static Offspring<TIndividual> WithChildren<TIndividual>(this Offspring<TIndividual> offspring, IEnumerable<TIndividual> children)
        {
            if (offspring is null)
                throw new ArgumentNullException(nameof(offspring));
            if (children is null)
                throw new ArgumentNullException(nameof(children));

            return new Offspring<TIndividual>(offspring.ParentPopulation, offspring.RandomParents, children);
        }
        
        public static Offspring<TIndividual> AddChildren<TIndividual>(this Offspring<TIndividual> offspring, IEnumerable<TIndividual> children)
        {
            if (offspring is null)
                throw new ArgumentNullException(nameof(offspring));
            if (children is null)
                throw new ArgumentNullException(nameof(children));

            var c = offspring
                .Children
                .Concat(children);

            return offspring.WithChildren(c);
        }

        public static Population<TIndividual> ToPopulation<TIndividual>(this Offspring<TIndividual> offspring)
        {
            if (offspring is null)
                throw new ArgumentNullException(nameof(offspring));

            return new Population<TIndividual>(offspring.ParentPopulation.Fitness, offspring.ParentPopulation.Random, offspring.Children);
        }

        public static Offspring<TIndividual> SelectElite<TIndividual>(this Offspring<TIndividual> offspring, int count)
        {
            if (offspring is null)
                throw new ArgumentNullException(nameof(offspring));

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
            if (offspring is null)
                throw new ArgumentNullException(nameof(offspring));
            if (recombine is null)
                throw new ArgumentNullException(nameof(recombine));

            var random = offspring.ParentPopulation.Random;

            var children = offspring.RandomParents
                .Zip(offspring.RandomParents, (a, b) => recombine(a, b, random))
                .Take(count);
            
            return offspring.AddChildren(children);
        }

        public static Offspring<TIndividual> Recombine<TIndividual>(this Offspring<TIndividual> offspring, int count, Func<TIndividual, TIndividual, TRandom, IEnumerable<TIndividual>> recombine)
        {
            if (offspring is null)
                throw new ArgumentNullException(nameof(offspring));
            if (recombine is null)
                throw new ArgumentNullException(nameof(recombine));

            var random = offspring.ParentPopulation.Random;

            var children = offspring.RandomParents
                .Zip(offspring.RandomParents, (a, b) => recombine(a, b, random))
                .SelectMany(x => x)
                .Take(count);
            
            return offspring.AddChildren(children);
        }

        public static Offspring<TIndividual> Mutate<TIndividual>(this Offspring<TIndividual> offspring, int count, Func<TIndividual, TRandom, TIndividual> mutate)
        {
            if (offspring is null)
                throw new ArgumentNullException(nameof(offspring));
            if (mutate is null)
                throw new ArgumentNullException(nameof(mutate));

            var random = offspring.ParentPopulation.Random;

            var children = offspring.RandomParents
                .Select(p => mutate(p, random))
                .Take(count);
            
            return offspring.AddChildren(children);
        }

        public static Offspring<TIndividual> SelectSurvivors<TIndividual>(this Offspring<TIndividual> offspring, int count)
        {
            if (offspring is null)
                throw new ArgumentNullException(nameof(offspring));

            var fitness = offspring.ParentPopulation.Fitness;

            var children = offspring.Children
                .OrderBy(fitness)
                .Take(count);
            
            return offspring.WithChildren(children);
        }
        
        public static Offspring<TIndividual> Eagerly<TIndividual>(this Offspring<TIndividual> offspring)
        {
            if (offspring is null)
                throw new ArgumentNullException(nameof(offspring));

            return offspring.WithChildren(offspring.Children.ToArray());
        }
    }
}
