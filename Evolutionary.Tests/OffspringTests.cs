using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Troschuetz.Random;
using Xunit;

namespace Evolutionary.Tests
{
    public class OffspringTests
    {
        private static double Sq(double x) => x * x;

        public Offspring<int, int> NullOffspring { get; } = null;
        public Offspring<int, int> NonNullOffspring { get; } = Population
            .Create(43, (int x, int _) => Sq(x))
            .SelectParentsByRank();

        [Fact]
        public void New_NullCheck()
        {
            int dataSet = 13;
            Func<int, int, double> fitness = (x, _) => Sq(x);
            var rnd = new TRandom();
            var array = new int[] { };

            var env = new Enviroment<int, int>(dataSet, fitness, rnd);

            Action act1 = () => new Offspring<int, int>(null, array, array, array);
            Action act2 = () => new Offspring<int, int>(env, null, array, array);
            Action act3 = () => new Offspring<int, int>(env, array, null, array);
            Action act4 = () => new Offspring<int, int>(env, array, array, null);

            act1
                .Should().Throw<ArgumentNullException>();
            act2
                .Should().Throw<ArgumentNullException>();
            act3
                .Should().Throw<ArgumentNullException>();
            act4
                .Should().Throw<ArgumentNullException>();
        }
        
        [Fact]
        public void WithChildren_NullChecks()
        {
            Action act1 = () => NonNullOffspring.WithChildren(null);
            Action act2 = () => NullOffspring.WithChildren(new [] { 0, 1, 2 });
            
            act1
                .Should().Throw<ArgumentNullException>();
            act2
                .Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void WithChildren()
        {
            int dataSet = 13;
            Func<int, int, double> fitness = (x, _) => Sq(x);

            var offspring = Population.Create(dataSet, fitness)
                .SelectParentsByRank()
                .WithChildren(new[] { 1, 2, 3 });

            offspring.Children
                .Should().BeEquivalentTo(1, 2, 3);
        }
        
        [Fact]
        public void AddChildren_NullChecks()
        {
            Action act1 = () => NonNullOffspring.AddChildren(null);
            Action act2 = () => NullOffspring.AddChildren(new [] { 0, 1, 2 });
            
            act1
                .Should().Throw<ArgumentNullException>();
            act2
                .Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void AddChildren()
        {
            int dataSet = 13;
            Func<int, int, double> fitness = (x, _) => Sq(x);

            var offspring = Population.Create(dataSet, fitness)
                .SelectParentsByRank()
                .WithChildren(new[] { 1, 2, 3 })
                .AddChildren(new[] { 7, 8, 9 });

            offspring.Children
                .Should().BeEquivalentTo(1, 2, 3, 7, 8, 9);
        }

        [Fact]
        public void ToPopulation_NullChecks()
        {
            Action act = () => NullOffspring.ToPopulation();
            
            act
                .Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void ToPopulation()
        {
            int dataSet = 13;
            Func<int, int, double> fitness = (x, _) => Sq(x);

            var firstGen = Population.Create(dataSet, fitness);

            var secondGen = firstGen
                .SelectParentsByRank()
                .WithChildren(new[] { 1, 2, 3 })
                .ToPopulation();

            secondGen.Enviroment
                .Should().Be(firstGen.Enviroment);

            secondGen.Individuals
                .Should().BeEquivalentTo(1, 2, 3);
        }
        
        [Fact]
        public void ToPopulationEagerness()
        {
            int dataSet = 13;
            Func<int, int, double> fitness = (x, _) => Sq(x);

            int i = 1;
            var population = Population
                .Create(dataSet, fitness)
                .SelectParentsByRank()
                .WithChildren(Enumerable.Repeat(0, 3).Select(_ => i++))
                .ToPopulation();

            population.Individuals
                .Should().BeEquivalentTo(1, 2, 3);

            population.Individuals
                .Should().BeEquivalentTo(1, 2, 3);
        }
        
        [Fact]
        public void SelectElite_NullChecks()
        {
            Action act = () => NullOffspring.SelectElite(2);
            
            act
                .Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void SelectElite()
        {
            int dataSet = 13;
            Func<int, int, double> fitness = (x, _) => Sq(x);
            TRandom rnd = new TRandom();

            var offspring = new Offspring<int, int>(
                new Enviroment<int, int>(dataSet, fitness, rnd),
                new[] { 10, 11, 12, 3, 2, 1, 13, 14, 15, 4, 5, 6 },
                new int [] { 30, 31, 32, 33, 30, 31 },
                new int [] { 20, 21 })
                .SelectElite(4);

            offspring.Children
                .Should().BeEquivalentTo(1, 2, 3, 4, 20, 21);
        }
        
        [Fact]
        public void Recombine_NullChecks()
        {
            Action act1 = () => NonNullOffspring.Recombine(10, default(Func<int, int, int, TRandom, int>));
            Action act2 = () => NonNullOffspring.Recombine(10, default(Func<int, int, int, TRandom, int[]>));
            Action act3 = () => NullOffspring.Recombine(10, (a, b, d, rnd) => a);
            Action act4 = () => NullOffspring.Recombine(10, (a, b, d, rnd) => new [] { a, b });
            
            act1
                .Should().Throw<ArgumentNullException>();
            act2
                .Should().Throw<ArgumentNullException>();
            act3
                .Should().Throw<ArgumentNullException>();
            act4
                .Should().Throw<ArgumentNullException>();
        }
        
        [Fact]
        public void Mutate_NullChecks()
        {
            Action act1 = () => NonNullOffspring.Mutate(10, default(Func<int, int, TRandom, int>));
            Action act2 = () => NullOffspring.Mutate(10, (a, d, rnd) => a);
            
            act1
                .Should().Throw<ArgumentNullException>();
            act2
                .Should().Throw<ArgumentNullException>();
        }
        
        [Fact]
        public void SelectSurvivors_NullChecks()
        {
            Action act = () => NullOffspring.SelectSurvivors(10);
            
            act
                .Should().Throw<ArgumentNullException>();
        }
    }
}
