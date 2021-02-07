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

        public Offspring<int> NullOffspring { get; } = null;
        public Offspring<int> NonNullOffspring { get; } = Population
            .Create((int x) => Sq(x))
            .SelectParentsByRank();

        [Fact]
        public void New_NullCheck()
        {
            var array = new int[] { };

            var pop = Population.Create((int i) => 0.0);

            Action act1 = () => new Offspring<int>(null, array, array);
            Action act2 = () => new Offspring<int>(pop, null, array);
            Action act3 = () => new Offspring<int>(pop, array, null);

            act1
                .Should().Throw<ArgumentNullException>();
            act2
                .Should().Throw<ArgumentNullException>();
            act3
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
            Func<int, double> fitness = x => Sq(x);

            var offspring = Population.Create(fitness)
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
            Func<int, double> fitness = x => Sq(x);

            var offspring = Population.Create(fitness)
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
            Func<int, double> fitness = x => Sq(x);

            var firstGen = Population.Create(fitness);

            var secondGen = firstGen
                .SelectParentsByRank()
                .WithChildren(new[] { 1, 2, 3 })
                .ToPopulation();

            secondGen.Fitness
                .Should().Be(firstGen.Fitness);

            secondGen.Random
                .Should().Be(firstGen.Random);

            secondGen.Individuals
                .Should().BeEquivalentTo(1, 2, 3);
        }
        
        [Fact]
        public void ToPopulationEagerness()
        {
            Func<int, double> fitness = x => Sq(x);

            int i = 1;
            var population = Population
                .Create(fitness)
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
            Func<int, double> fitness = x => Sq(x);

            var population = Population
                .Create(fitness)
                .AddIndividuals(new[] { 10, 11, 12, 3, 2, 1, 13, 14, 15, 4, 5, 6 });

            var offspring = new Offspring<int>(
                population,
                new int [] { 30, 31, 32, 33, 30, 31 },
                new int [] { 20, 21 })
                .SelectElite(4);

            offspring.Children
                .Should().BeEquivalentTo(1, 2, 3, 4, 20, 21);
        }
        
        [Fact]
        public void Recombine_NullChecks()
        {
            Action act1 = () => NonNullOffspring.Recombine(10, default(Func<int, int, TRandom, int>));
            Action act2 = () => NonNullOffspring.Recombine(10, default(Func<int, int, TRandom, int[]>));
            Action act3 = () => NullOffspring.Recombine(10, (a, b, rnd) => a);
            Action act4 = () => NullOffspring.Recombine(10, (a, b, rnd) => new [] { a, b });
            
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
        public void Recombine1()
        {
            Func<int, double> fitness = x => Sq(x);

            var population = Population
                .Create(fitness)
                .AddIndividuals(new[] { 10, 11, 12, 3, 2, 1, 13, 14, 15, 4, 5, 6 });

            var offspring = new Offspring<int>(
                population,
                new int [] { 30, 31, 32, 33, 30, 31 },
                new int [] { 100 })
                .Recombine(3, (a, b, r) => a + b);

            offspring.Children
                .Should().Equal(100, 60, 62, 64);
        }
        
        [Fact]
        public void Recombine2()
        {
            Func<int, double> fitness = x => Sq(x);

            var population = Population
                .Create(fitness)
                .AddIndividuals(new[] { 10, 11, 12, 3, 2, 1, 13, 14, 15, 4, 5, 6 });

            var offspring = new Offspring<int>(
                population,
                new int [] { 30, 31, 32, 33, 30, 31 },
                new int [] { 100 })
                .Recombine(3, (a, b, r) => new [] { a, b });

            offspring.Children
                .Should().Equal(100, 30, 30, 31);
        }
        
        [Fact]
        public void Mutate_NullChecks()
        {
            Action act1 = () => NonNullOffspring.Mutate(10, default(Func<int, TRandom, int>));
            Action act2 = () => NullOffspring.Mutate(10, (a, rnd) => a);
            
            act1
                .Should().Throw<ArgumentNullException>();
            act2
                .Should().Throw<ArgumentNullException>();
        }
        
        [Fact]
        public void Mutate()
        {
            Func<int, double> fitness = x => Sq(x);

            var population = Population
                .Create(fitness)
                .AddIndividuals(new[] { 10, 11, 12, 3, 2, 1, 13, 14, 15, 4, 5, 6 });

            var offspring = new Offspring<int>(
                population,
                new int [] { 30, 31, 32, 33, 30, 31 },
                new int [] { 100 })
                .Mutate(3, (a, r) => a + 100);

            offspring.Children
                .Should().Equal(100, 130, 131, 132);
        }
        
        [Fact]
        public void SelectSurvivors_NullChecks()
        {
            Action act = () => NullOffspring.SelectSurvivors(10);
            
            act
                .Should().Throw<ArgumentNullException>();
        }
        
        [Fact]
        public void SelectSurvivors()
        {
            Func<int, double> fitness = x => Sq(x);

            var offspring = Population.Create(fitness)
                .SelectParentsByRank()
                .WithChildren(new[] { 6, 5, 3, 1, 8, 2 })
                .SelectSurvivors(3);

            offspring.Children
                .Should().BeEquivalentTo(1, 2, 3);
        }
        
        [Fact]
        public void Eagerly_NullChecks()
        {
            Action act = () => NullOffspring.Eagerly();
            
            act
                .Should().Throw<ArgumentNullException>();
        }
        
        [Fact]
        public void Eagerly_Eagerness()
        {
            Func<int, double> fitness = x => Sq(x);

            int i = 1;
            var offspring = Population
                .Create(fitness)
                .SelectParentsByRank()
                .WithChildren(Enumerable.Repeat(0, 3).Select(_ => i++))
                .Eagerly();

            offspring.Children
                .Should().BeEquivalentTo(1, 2, 3);

            offspring.Children
                .Should().BeEquivalentTo(1, 2, 3);
        }
    }
}
