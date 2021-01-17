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
        private double Sq(double x) => x * x;

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
    }
}
