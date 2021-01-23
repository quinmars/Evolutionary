using FluentAssertions;
using System;
using System.Linq;
using Troschuetz.Random;
using Xunit;

namespace Evolutionary.Tests
{
    public class PopulationTests
    {
        public Population<int, int> NullPopulation { get; } = null;
        public Population<int, int> NonNullPopulation { get; } = Population.Create(32, (int x, int d) => Sq(x));

        private static double Sq(double x) => x * x;
        
        [Fact]
        public void New_NullChecks()
        {
            var env = new Enviroment<int, int>(0, (x, d) => 0.0, new TRandom());

            Action act1 = () => new Population<int, int>(null);
            Action act2 = () => new Population<int, int>(env, null);
            Action act3 = () => new Population<int, int>(null, new[] { 0 });

            act1
                .Should().Throw<ArgumentNullException>();
            act2
                .Should().Throw<ArgumentNullException>();
            act3
                .Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Create1()
        {
            int dataSet = 13;
            Func<double, int, double> fitness = (x, _) => Sq(x);
            var random = new TRandom();

            var population = Population.Create(dataSet, fitness, random);

            population.Enviroment
                .Should().NotBeNull();

            population.Enviroment.DataSet
                .Should().Be(dataSet);

            population.Enviroment.Fitness
                .Should().Be(fitness);

            population.Enviroment.Random
                .Should().Be(random);

            population.Individuals
                .Should().NotBeNull();

            population.Individuals
                .Should().BeEmpty();
        }
        
        [Fact]
        public void Create2()
        {
            int dataSet = 13;
            Func<double, int, double> fitness = (x, _) => Sq(x);

            var population = Population.Create(dataSet, fitness);

            population.Enviroment
                .Should().NotBeNull();

            population.Enviroment.DataSet
                .Should().Be(dataSet);

            population.Enviroment.Fitness
                .Should().Be(fitness);

            population.Enviroment.Random
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
            Action act1 = () => Population.Create(0, default(Func<int,int,double>), new TRandom());
            Action act2 = () => Population.Create(0, (int a, int b) => 0.0, null);
            Action act3 = () => Population.Create(0, default(Func<int,int,double>));

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
            int dataSet = 1;
            Func<int, int, double> fitness = (x, d) => Sq(x - d);

            var population = Population
                .Create(dataSet, fitness)
                .AddIndividuals(new[] { 1, 2, 3, 4, 5, 6, 7, 9, 8, 10 });

            population.Individuals
                .Should().BeEquivalentTo(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
        }
        
        [Fact]
        public void AddIndividuals2()
        {
            int dataSet = 1;
            Func<int, int, double> fitness = (x, d) => Sq(x - d);

            var population = Population
                .Create(dataSet, fitness)
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
            int dataSet = 1;
            Func<int, int, double> fitness = (x, d) => Sq(x - d);

            int i = 1;
            var population = Population
                .Create(dataSet, fitness)
                .AddRandomIndividuals(10, (_, __) => i++);

            population.Individuals
                .Should().BeEquivalentTo(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
        }
        
        [Fact]
        public void AddRandomIndividuals2()
        {
            int dataSet = 1;
            Func<int, int, double> fitness = (x, d) => Sq(x - d);

            int i = 1;
            var population = Population
                .Create(dataSet, fitness)
                .AddRandomIndividuals(4, (_, __) => i++)
                .AddRandomIndividuals(6, (_, __) => i++);

            population.Individuals
                .Should().BeEquivalentTo(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
        }
        
        [Fact]
        public void AddRandomIndividuals_NullChecks()
        {
            Action act1 = () => NonNullPopulation.AddRandomIndividuals(200, null);
            Action act2 = () => NullPopulation.AddRandomIndividuals(200, (a, rnd) => 0);

            act1
                .Should().Throw<ArgumentNullException>();
            act2
                .Should().Throw<ArgumentNullException>();
        }
        
        [Fact]
        public void SelectParentsByRank()
        {
            double dataSet = 0.5;
            Func<int, double, double> fitness = (x, d) => Sq(x - d);

            var individuals = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            var parents = Population
                .Create(dataSet, fitness)
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
