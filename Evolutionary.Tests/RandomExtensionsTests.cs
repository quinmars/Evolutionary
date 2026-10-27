using System;
using System.Linq;
using Evolutionary;
using NUnit.Framework;

namespace Evolutionary.Tests
{
    public class RandomExtensionsTests
    {
        private const int N = 1000000;

        [Test]
        public void NextBoolean_NullChecks()
        {
            Random rnd = null!;

            Action act = () => rnd.NextBoolean();

            Assert.That(act, Throws.ArgumentNullException);
        }

        [Test]
        public void NextBoolean_IsFair()
        {
            var rnd = new Random(42);
            const int count = 10000;

            var trues = 0;
            for (var i = 0; i < count; i++)
            {
                if (rnd.NextBoolean())
                {
                    trues++;
                }
            }

            Assert.That(trues / (double)count, Is.EqualTo(0.5).Within(0.02));
        }

        [Test]
        public void NextStandardNormal_NullChecks()
        {
            Random rnd = null!;

            Action act = () => rnd.NextNormal();

            Assert.That(act, Throws.ArgumentNullException);
        }

        [Test]
        public void NextNormal_NullChecks()
        {
            Random rnd = null!;

            Action act = () => rnd.NextNormal(0.0, 1.0);

            Assert.That(act, Throws.ArgumentNullException);
        }

        [Test]
        public void NextStandardNormal_MeanAndStandardDeviation()
        {
            var rnd = new Random(42);

            var values = Enumerable
                .Range(0, N)
                .Select(_ => rnd.NextNormal())
                .ToArray();

            var mean = values.Average();
            var stdDev = Math.Sqrt(values.Select(v => (v - mean) * (v - mean)).Average());

            Assert.That(mean, Is.EqualTo(0.0).Within(0.01));
            Assert.That(stdDev, Is.EqualTo(1.0).Within(0.01));
        }

        [Test]
        public void NextNormal_MeanAndStandardDeviation()
        {
            var rnd = new Random(42);
            const double mu = 10.0;
            const double sigma = 3.5;

            var values = Enumerable
                .Range(0, N)
                .Select(_ => rnd.NextNormal(mu, sigma))
                .ToArray();

            var mean = values.Average();
            var stdDev = Math.Sqrt(values.Select(v => (v - mean) * (v - mean)).Average());

            Assert.That(mean, Is.EqualTo(mu).Within(0.01));
            Assert.That(stdDev, Is.EqualTo(sigma).Within(0.01));
        }

        [Test]
        public void NextNormal_DistributionIsSymmetric()
        {
            var rnd = new Random(42);

            var values = Enumerable
                .Range(0, N)
                .Select(_ => rnd.NextNormal())
                .ToArray();

            var lower = values.Count(v => v < 0);
            var upper = values.Count(v => v >= 0);

            // For a normal distribution we expect a 50:50 split (within a
            // small statistical tolerance).
            Assert.That(lower / (double)N, Is.EqualTo(0.5).Within(0.01));
            Assert.That(upper / (double)N, Is.EqualTo(0.5).Within(0.01));
        }

        [Test]
        public void NextNormal_QuantilesMatchNormalDistribution()
        {
            var rnd = new Random(42);

            var values = Enumerable
                .Range(0, N)
                .Select(_ => rnd.NextNormal())
                .ToArray();

            // Theoretical probabilities of the normal distribution:
            // P(|z| < 1) = 68.27%, P(|z| < 2) = 95.45%, P(|z| < 3) = 99.73%
            Assert.That(values.Count(v => Math.Abs(v) < 1.0) / (double)N, Is.EqualTo(0.6827).Within(0.005));
            Assert.That(values.Count(v => Math.Abs(v) < 2.0) / (double)N, Is.EqualTo(0.9545).Within(0.005));
            Assert.That(values.Count(v => Math.Abs(v) < 3.0) / (double)N, Is.EqualTo(0.9973).Within(0.002));
        }

        [Test]
        public void NextDouble_RangeChecks()
        {
            Random rnd = new Random();

            Action act1 = () => rnd.NextDouble(1.0, 0.0);

            Assert.That(act1, Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void NextDouble_ValuesAreWithinRange()
        {
            var rnd = new Random(42);
            const double min = -3.0;
            const double max = 5.5;

            var values = Enumerable
                .Range(0, N)
                .Select(_ => rnd.NextDouble(min, max))
                .ToArray();

            // For a random sample, the exact boundaries are statistically
            // unlikely to be hit. We therefore verify that all values lie
            // strictly within the interval.
            Assert.That(values, Is.All.GreaterThanOrEqualTo(min));
            Assert.That(values, Is.All.LessThan(max));
        }

        [Test]
        public void NextDouble_DistributionIsUniform()
        {
            var rnd = new Random(42);
            const double min = 10.0;
            const double max = 20.0;

            // The theoretical mean of a uniform distribution is (min + max) / 2.
            var values = Enumerable
                .Range(0, N)
                .Select(_ => rnd.NextDouble(min, max))
                .ToArray();

            var mean = values.Average();

            Assert.That(mean, Is.EqualTo((min + max) / 2.0).Within(0.01));
        }
    }
}
