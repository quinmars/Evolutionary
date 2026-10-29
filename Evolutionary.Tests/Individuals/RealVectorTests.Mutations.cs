using System;
using Evolutionary;
using NUnit.Framework;

namespace Evolutionary.Tests
{
    public partial class RealVectorTests
    {
        [Test]
        public void GaussianMutation_IsDeterministicForTheSameSeed()
        {
            var vector = RealVector.CreateRandomly(10, -5.0, 5.0, new Random(42));

            var mutated1 = RealVector.GaussianMutation(vector, new Random(42), 0.25);
            var mutated2 = RealVector.GaussianMutation(vector, new Random(42), 0.25);

            Assert.That(mutated2.ToArray(), Is.EqualTo(mutated1.ToArray()));
        }

        [Test]
        public void GaussianMutation_DoesNotChangeTheOriginal()
        {
            var vector = RealVector.CreateRandomly(10, -5.0, 5.0, new Random(42));
            var original = vector.ToArray();

            _ = RealVector.GaussianMutation(vector, new Random(42), 0.25);

            Assert.That(vector.ToArray(), Is.EqualTo(original));
        }

        [Test]
        public void GaussianMutation_ZeroSigma_Unchanged()
        {
            var vector = RealVector.CreateRandomly(10, -5.0, 5.0, new Random(42));

            var mutated = RealVector.GaussianMutation(vector, new Random(42), 0.0);

            Assert.That(mutated.ToArray(), Is.EqualTo(vector.ToArray()));
        }

        [Test]
        public void GaussianMutation_ChangesValues()
        {
            var vector = RealVector.CreateRandomly(10, -5.0, 5.0, new Random(42));

            var mutated = RealVector.GaussianMutation(vector, new Random(42), 1.0);

            Assert.That(mutated.ToArray(), Is.Not.EqualTo(vector.ToArray()));
        }
    }
}
