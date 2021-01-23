using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Troschuetz.Random;

namespace Evolutionary
{
    public static class Offspring
    {
        public static Offspring<TIndividual, TDataSet> WithChildren<TIndividual, TDataSet>(this Offspring<TIndividual, TDataSet> offspring, IEnumerable<TIndividual> children)
        {
            if (offspring is null)
                throw new ArgumentNullException(nameof(offspring));
            if (children is null)
                throw new ArgumentNullException(nameof(children));

            return new Offspring<TIndividual, TDataSet>(offspring.Enviroment, offspring.Parents, offspring.RandomParents, children);
        }
        
        public static Offspring<TIndividual, TDataSet> AddChildren<TIndividual, TDataSet>(this Offspring<TIndividual, TDataSet> offspring, IEnumerable<TIndividual> children)
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

        public static Population<TIndividual, TDataSet> ToPopulation<TIndividual, TDataSet>(this Offspring<TIndividual, TDataSet> offspring)
        {
            if (offspring is null)
                throw new ArgumentNullException(nameof(offspring));

            return new Population<TIndividual, TDataSet>(offspring.Enviroment, offspring.Children);
        }

        public static Offspring<TIndividual, TDataSet> SelectElite<TIndividual, TDataSet>(this Offspring<TIndividual, TDataSet> offspring, int count)
        {
            if (offspring is null)
                throw new ArgumentNullException(nameof(offspring));

            var children = offspring
                .Parents
                .OrderBy(x => offspring.Enviroment.Fitness(x, offspring.Enviroment.DataSet))
                .Take(count);

            return offspring.AddChildren(children);
        }

        public static Offspring<TIndividual, TDataSet> Recombine<TIndividual, TDataSet>(this Offspring<TIndividual, TDataSet> offspring, int count, Func<TIndividual, TIndividual, TDataSet, TRandom, TIndividual> recombine)
        {
            if (offspring is null)
                throw new ArgumentNullException(nameof(offspring));
            if (recombine is null)
                throw new ArgumentNullException(nameof(recombine));

            var children = offspring.RandomParents
                .Zip(offspring.RandomParents, (a, b) => recombine(a, b, offspring.Enviroment.DataSet, offspring.Enviroment.Random))
                .Take(count);
            
            return offspring.AddChildren(children);
        }

        public static Offspring<TIndividual, TDataSet> Recombine<TIndividual, TDataSet>(this Offspring<TIndividual, TDataSet> offspring, int count, Func<TIndividual, TIndividual, TDataSet, TRandom, IEnumerable<TIndividual>> recombine)
        {
            if (offspring is null)
                throw new ArgumentNullException(nameof(offspring));
            if (recombine is null)
                throw new ArgumentNullException(nameof(recombine));

            var children = offspring.RandomParents
                .Zip(offspring.RandomParents, (a, b) => recombine(a, b, offspring.Enviroment.DataSet, offspring.Enviroment.Random))
                .SelectMany(x => x)
                .Take(count);
            
            return offspring.AddChildren(children);
        }

        public static Offspring<TIndividual, TDataSet> Mutate<TIndividual, TDataSet>(this Offspring<TIndividual, TDataSet> offspring, int count, Func<TIndividual, TDataSet, TRandom, TIndividual> mutate)
        {
            if (offspring is null)
                throw new ArgumentNullException(nameof(offspring));
            if (mutate is null)
                throw new ArgumentNullException(nameof(mutate));

            var children = offspring.RandomParents
                .Select(p => mutate(p, offspring.Enviroment.DataSet, offspring.Enviroment.Random))
                .Take(count);
            
            return offspring.AddChildren(children);
        }

        public static Offspring<TIndividual, TDataSet> SelectSurvivors<TIndividual, TDataSet>(this Offspring<TIndividual, TDataSet> offspring, int count)
        {
            if (offspring is null)
                throw new ArgumentNullException(nameof(offspring));

            var env = offspring.Enviroment;
            var children = offspring.Children
                .OrderBy(c => env.Fitness(c, env.DataSet))
                .Take(count);
            
            return offspring.WithChildren(children);
        }
        
        public static Offspring<TIndividual, TDataSet> Eagerly<TIndividual, TDataSet>(this Offspring<TIndividual, TDataSet> offspring)
        {
            if (offspring is null)
                throw new ArgumentNullException(nameof(offspring));

            return offspring.WithChildren(offspring.Children.ToArray());
        }
    }
}
