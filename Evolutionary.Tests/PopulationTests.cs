using System;
using System.Linq;
using NUnit.Framework;


namespace Evolutionary.Tests
{
    public class PopulationTests
    {
        public Population<int> NullPopulation { get; } = null;
        public Population<int> NonNullPopulation { get; } = Population.Create((int x) => Sq(x));

        private static double Sq(double x) => x * x;

        [Test]
        public void New_NullChecks()
        {
            Func<int, double> fitness = (x) => 0.0;
            var random = new Random();

            Action act1 = () => new Population<int>(null, random);
            Action act2 = () => new Population<int>(fitness, null);
            Action act3 = () => new Population<int>(fitness, random, null);
            Action act4 = () => new Population<int>(fitness, null, new int[] { });
            Action act5 = () => new Population<int>(null, random, new int[] { });

            Assert.That(act1, Throws.ArgumentNullException);
            Assert.That(act2, Throws.ArgumentNullException);
            Assert.That(act3, Throws.ArgumentNullException);
            Assert.That(act4, Throws.ArgumentNullException);
            Assert.That(act5, Throws.ArgumentNullException);
        }

        [Test]
        public void Create1()
        {
            Func<double, double> fitness = x => Sq(x);
            var random = new Random();

            var population = Population.Create(fitness, random);

            Assert.That(population.Fitness, Is.Not.Null);
            Assert.That(population.Random, Is.Not.Null);
            Assert.That(population.Individuals, Is.Not.Null);
            Assert.That(population.Individuals, Is.Empty);
        }

        [Test]
        public void Create2()
        {
            Func<double, double> fitness = x => Sq(x);

            var population = Population.Create(fitness);

            Assert.That(population.Fitness, Is.EqualTo(fitness));
            Assert.That(population.Random, Is.Not.Null);
            Assert.That(population.Individuals, Is.Not.Null);
            Assert.That(population.Individuals, Is.Empty);
        }

        [Test]
        public void Create_NullChecks()
        {
            Action act1 = () => Population.Create(default(Func<int,double>), new Random());
            Action act2 = () => Population.Create((int a) => 0.0, null);
            Action act3 = () => Population.Create(default(Func<int,double>));

            Assert.That(act1, Throws.ArgumentNullException);
            Assert.That(act2, Throws.ArgumentNullException);
            Assert.That(act3, Throws.ArgumentNullException);
        }

        [Test]
        public void AddIndividuals1()
        {
            Func<int, double> fitness = x => Sq(x);

            var population = Population
                .Create(fitness)
                .AddIndividuals(new[] { 1, 2, 3, 4, 5, 6, 7, 9, 8, 10 });

            Assert.That(population.Individuals, Is.EquivalentTo(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }));
        }

        [Test]
        public void AddIndividuals2()
        {
            Func<int, double> fitness = x => Sq(x);

            var population = Population
                .Create(fitness)
                .AddIndividuals(new[] { 8, 10, 9 })
                .AddIndividuals(new[] { 1, 2, 3, 4, 5, 6, 7 });

            Assert.That(population.Individuals, Is.EquivalentTo(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }));
        }

        [Test]
        public void AddIndividuals_NullChecks()
        {
            Action act1 = () => NonNullPopulation.AddIndividuals(null);
            Action act2 = () => NullPopulation.AddIndividuals(new[] { 1 });

            Assert.That(act1, Throws.ArgumentNullException);
            Assert.That(act2, Throws.ArgumentNullException);
        }

        [Test]
        public void AddRandomIndividuals1()
        {
            Func<int, double> fitness = x => Sq(x);

            int i = 1;
            var population = Population
                .Create(fitness)
                .AddRandomIndividuals(10, _ => i++);

            Assert.That(population.Individuals, Is.EquivalentTo(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }));
        }

        [Test]
        public void AddRandomIndividuals2()
        {
            Func<int, double> fitness = x => Sq(x);

            int i = 1;
            var population = Population
                .Create(fitness)
                .AddRandomIndividuals(4, _ => i++)
                .AddRandomIndividuals(6, _ => i++);

            Assert.That(population.Individuals, Is.EquivalentTo(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }));
        }

        [Test]
        public void AddRandomIndividuals_NullChecks()
        {
            Action act1 = () => NonNullPopulation.AddRandomIndividuals(200, null);
            Action act2 = () => NullPopulation.AddRandomIndividuals(200, rnd => 0);

            Assert.That(act1, Throws.ArgumentNullException);
            Assert.That(act2, Throws.ArgumentNullException);
        }

        [Test]
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

            Assert.That(stats, Has.Length.EqualTo(10));

            foreach (var s in stats)
            {
                var calc = s.Count / (count - (double)s.Index) * q;

                Assert.That(calc, Is.EqualTo(1.0).Within(0.1));
            }
        }

        [Test]
        public void SelectParentsByRank_NullChecks()
        {
            Action act = () => NullPopulation.SelectParentsByRank();

            Assert.That(act, Throws.ArgumentNullException);
        }
    }
}
