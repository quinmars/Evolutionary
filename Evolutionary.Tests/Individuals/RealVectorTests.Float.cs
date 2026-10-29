using System;
using Evolutionary;
using NUnit.Framework;

namespace Evolutionary.Tests
{
    public partial class RealVectorTests
    {
        // The generic vector is not limited to double. These tests verify
        // that the full operator set also works for other floating point
        // types, e.g. float.

        [Test]
        public void Float_RealVector_SupportsTheOperatorSet()
        {
            var p1 = RealVector.CreateRandomly(4, -1f, 1f, new Random(42));
            var p2 = RealVector.CreateRandomly(4, -1f, 1f, new Random(43));

            var child = RealVector.ArithmeticCrossover(p1, p2, new Random(44));
            var mutated = RealVector.GaussianMutation(child, new Random(45), 0.1);

            Assert.That(child.Length, Is.EqualTo(4));
            Assert.That(mutated.Length, Is.EqualTo(4));

            for (var i = 0; i < mutated.Length; i++)
            {
                Assert.That(mutated[i], Is.InRange(-5f, 5f));
            }
        }

        [Test]
        public void Float_RealVector_IsDeterministicForTheSameSeed()
        {
            var v1 = RealVector.CreateRandomly(4, -1f, 1f, new Random(42));
            var v2 = RealVector.CreateRandomly(4, -1f, 1f, new Random(42));

            Assert.That(v2.ToArray(), Is.EqualTo(v1.ToArray()));
        }
    }
}
