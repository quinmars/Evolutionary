using System;
using System.Linq;
using NUnit.Framework;


namespace Evolutionary.Tests
{
    public class PopulationTests
    {
        public Population<int> NullPopulation { get; } = null!;
        public Population<int> NonNullPopulation { get; } = Population.Create((int x) => Sq(x));

        private static double Sq(double x) => x * x;

        [Test]
        public void New_NullChecks()
        {
            Func<int, double> fitness = (x) => 0.0;

            Action act1 = () => new Population<int>(null!);
            Action act2 = () => new Population<int>(fitness, null!);
            Action act3 = () => new Population<int>(null!, new int[] { });

            Assert.That(act1, Throws.ArgumentNullException);
            Assert.That(act2, Throws.ArgumentNullException);
            Assert.That(act3, Throws.ArgumentNullException);
        }

        [Test]
        public void Create()
        {
            Func<double, double> fitness = x => Sq(x);

            var population = Population.Create(fitness);

            Assert.That(population.Fitness, Is.EqualTo(fitness));
            Assert.That(population.Individuals, Is.Not.Null);
            Assert.That(population.Individuals, Is.Empty);
        }

        [Test]
        public void Create_NullChecks()
        {
            Action act1 = () => Population.Create((Func<int, double>)null!);

            Assert.That(act1, Throws.ArgumentNullException);
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
        public void AddIndividuals3()
        {
            Func<int, double> fitness = x => Sq(x);

            int i = 1;
            var population = Population
                .Create(fitness)
                .AddIndividuals(10, () => i++);

            Assert.That(population.Individuals, Is.EquivalentTo(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }));
        }

        [Test]
        public void AddIndividuals4()
        {
            Func<int, double> fitness = x => Sq(x);

            int i = 1;
            var population = Population
                .Create(fitness)
                .AddIndividuals(4, new Random(), _ => i++)
                .AddIndividuals(6, new Random(), _ => i++);

            Assert.That(population.Individuals, Is.EquivalentTo(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }));
        }

        [Test]
        public void AddIndividuals5()
        {
            Func<int, double> fitness = x => Sq(x);

            var population = Population
                .Create(fitness)
                .AddIndividuals(4, 100, p => p + 1)
                .AddIndividuals(6, 200, p => p + 1);

            Assert.That(population.Individuals, Is.EquivalentTo(new[] { 101, 101, 101, 101, 201, 201, 201, 201, 201, 201 }));
        }

        [Test]
        public void AddIndividuals_NullChecks()
        {
            Action act1 = () => NonNullPopulation.AddIndividuals(null!);
            Action act2 = () => NullPopulation.AddIndividuals(new[] { 1 });
            Action act3 = () => NonNullPopulation.AddIndividuals(10, (Func<int>)null!);
            Action act4 = () => NullPopulation.AddIndividuals(10, () => 0);
            Action act5 = () => NonNullPopulation.AddIndividuals(10, new Random(), (Func<Random, int>)null!);
            Action act6 = () => NullPopulation.AddIndividuals(10, new Random(), rnd => 0);
            Action act7 = () => NonNullPopulation.AddIndividuals(10, new Random(), new Random(), (Func<Random, Random, int>)null!);
            Action act8 = () => NullPopulation.AddIndividuals(10, new Random(), new Random(), (a, b) => 0);

            Assert.That(act1, Throws.ArgumentNullException);
            Assert.That(act2, Throws.ArgumentNullException);
            Assert.That(act3, Throws.ArgumentNullException);
            Assert.That(act4, Throws.ArgumentNullException);
            Assert.That(act5, Throws.ArgumentNullException);
            Assert.That(act6, Throws.ArgumentNullException);
            Assert.That(act7, Throws.ArgumentNullException);
            Assert.That(act8, Throws.ArgumentNullException);
        }

        [Test]
        public void SelectParentsByRank()
        {
            Func<int, double> fitness = x => Sq(x);

            var individuals = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            var parents = Population
                .Create(fitness)
                .AddIndividuals(individuals)
                .SelectParentsByRank(new Random(42))
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
            Action act = () => NullPopulation.SelectParentsByRank(new Random());

            Assert.That(act, Throws.ArgumentNullException);
        }
    }
}
