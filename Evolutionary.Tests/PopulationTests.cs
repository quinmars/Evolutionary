using FluentAssertions;
using System;
using System.Linq;
using Troschuetz.Random;
using Xunit;

namespace Evolutionary.Tests
{
    public class PopulationTests
    {
        public Population<int> NullPopulation { get; } = null;
        public Population<int> NonNullPopulation { get; } = Population.Create((int x) => Sq(x));

        private static double Sq(double x) => x * x;
        
        [Fact]
        public void New_NullChecks()
        {
            Func<int, double> fitness = (x) => 0.0;
            var random = new TRandom();

            Action act1 = () => new Population<int>(null, random);
            Action act2 = () => new Population<int>(fitness, null);
            Action act3 = () => new Population<int>(fitness, random, null);
            Action act4 = () => new Population<int>(fitness, null, new int[] { });
            Action act5 = () => new Population<int>(null, random, new int[] { });

            act1
                .Should().Throw<ArgumentNullException>();
            act2
                .Should().Throw<ArgumentNullException>();
            act3
                .Should().Throw<ArgumentNullException>();
            act4
                .Should().Throw<ArgumentNullException>();
            act5
                .Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Create1()
        {
            Func<double, double> fitness = x => Sq(x);
            var random = new TRandom();

            var population = Population.Create(fitness, random);

            population.Fitness
                .Should().NotBeNull();

            population.Random
                .Should().NotBeNull();

            population.Individuals
                .Should().NotBeNull();

            population.Individuals
                .Should().BeEmpty();
        }
        
        [Fact]
        public void Create2()
        {
            Func<double, double> fitness = x => Sq(x);

            var population = Population.Create(fitness);

            population.Fitness
                .Should().Be(fitness);

            population.Random
                .Should().NotBeNull();

            population.Individuals
                .Should().NotBeNull()
                .And.BeEmpty();

            population.Individuals
                .Should().BeEmpty();
        }
        
        [Fact]
        public void Create_NullChecks()
        {
            Action act1 = () => Population.Create(default(Func<int,double>), new TRandom());
            Action act2 = () => Population.Create((int a) => 0.0, null);
            Action act3 = () => Population.Create(default(Func<int,double>));

            act1
                .Should().Throw<ArgumentNullException>();
            act2
                .Should().Throw<ArgumentNullException>();
            act3
                .Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void AddIndividuals1()
        {
            Func<int, double> fitness = x => Sq(x);

            var population = Population
                .Create(fitness)
                .AddIndividuals(new[] { 1, 2, 3, 4, 5, 6, 7, 9, 8, 10 });

            population.Individuals
                .Should().BeEquivalentTo(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
        }
        
        [Fact]
        public void AddIndividuals2()
        {
            Func<int, double> fitness = x => Sq(x);

            var population = Population
                .Create(fitness)
                .AddIndividuals(new[] { 8, 10, 9 })
                .AddIndividuals(new[] { 1, 2, 3, 4, 5, 6, 7 });

            population.Individuals
                .Should().BeEquivalentTo(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
        }
        
        [Fact]
        public void AddIndividuals_NullChecks()
        {
            Action act1 = () => NonNullPopulation.AddIndividuals(null);
            Action act2 = () => NullPopulation.AddIndividuals(new[] { 1 });

            act1
                .Should().Throw<ArgumentNullException>();
            act2
                .Should().Throw<ArgumentNullException>();
        }
        
        [Fact]
        public void AddRandomIndividuals1()
        {
            Func<int, double> fitness = x => Sq(x);

            int i = 1;
            var population = Population
                .Create(fitness)
                .AddRandomIndividuals(10, _ => i++);

            population.Individuals
                .Should().BeEquivalentTo(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
        }
        
        [Fact]
        public void AddRandomIndividuals2()
        {
            Func<int, double> fitness = x => Sq(x);

            int i = 1;
            var population = Population
                .Create(fitness)
                .AddRandomIndividuals(4, _ => i++)
                .AddRandomIndividuals(6, _ => i++);

            population.Individuals
                .Should().BeEquivalentTo(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
        }
        
        [Fact]
        public void AddRandomIndividuals_NullChecks()
        {
            Action act1 = () => NonNullPopulation.AddRandomIndividuals(200, null);
            Action act2 = () => NullPopulation.AddRandomIndividuals(200, rnd => 0);

            act1
                .Should().Throw<ArgumentNullException>();
            act2
                .Should().Throw<ArgumentNullException>();
        }
        
        [Fact]
        public void SelectParentsByRank()
        {
            Func<int, double> fitness = x => Sq(x);

            var individuals = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            var parents = Population
                .Create(fitness)
                .AddIndividuals(individuals)
                .SelectParentsByRank()
                .RandomParents;

            var count = individuals.Length;
            var n = count * (count + 1) / 2;
            var N = 100000;
            var q = n / (double)N;

            var stats = parents
                .Take(N)
                .GroupBy(x => x)
                .OrderBy(x => x.Key)
                .Select((x, i) => new { Value = x.Key, Count = x.Count(), Index = i })
                .ToArray();

            stats
                .Should().HaveCount(10);

            foreach (var s in stats)
            {
                var calc = s.Count / (count - (double)s.Index) * q;

                calc
                    .Should().BeApproximately(1.0, 0.1);
            }
        }
        
        [Fact]
        public void AddRandomIndividualsNulSelectParentsByRank_NulllChecks()
        {
            Action act = () => NullPopulation.SelectParentsByRank();

            act
                .Should().Throw<ArgumentNullException>();
        }
    }
}
