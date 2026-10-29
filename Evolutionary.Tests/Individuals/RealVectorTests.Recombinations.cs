using System;
using System.Linq;
using Evolutionary;
using NUnit.Framework;

namespace Evolutionary.Tests
{
    public partial class RealVectorTests
    {
        [Test]
        public void UniformCrossover_IsDeterministicForTheSameSeed()
        {
            var p1 = RealVector.CreateRandomly(10, -5.0, 5.0, new Random(42));
            var p2 = RealVector.CreateRandomly(10, -5.0, 5.0, new Random(43));

            var child1 = RealVector.UniformCrossover(p1, p2, new Random(44));
            var child2 = RealVector.UniformCrossover(p1, p2, new Random(44));

            Assert.That(child2.ToArray(), Is.EqualTo(child1.ToArray()));
        }

        [Test]
        public void UniformCrossover_MixesBothParents()
        {
            var p1 = new RealVector<double>(System.Linq.Enumerable.Repeat(1.0, 100).ToArray());
            var p2 = new RealVector<double>(100);

            var child = RealVector.UniformCrossover(p1, p2, new Random(42));

            // With 100 fair coin flips the child is (practically) guaranteed
            // to inherit from both parents.
            var sum = 0.0;
            for (var i = 0; i < child.Length; i++)
            {
                sum += child[i];
            }

            Assert.That(sum, Is.InRange(1, 99));
        }

        [Test]
        public void UniformCrossover_Throws_IfLengthsDiffer()
        {
            var p1 = RealVector.CreateRandomly(3, -5.0, 5.0, new Random(42));
            var p2 = RealVector.CreateRandomly(2, -5.0, 5.0, new Random(43));

            Assert.That(
                () => RealVector.UniformCrossover(p1, p2, new Random(44)),
                Throws.ArgumentException);
        }

        [Test]
        public void ArithmeticCrossover_IsDeterministicForTheSameSeed()
        {
            var p1 = RealVector.CreateRandomly(10, -5.0, 5.0, new Random(42));
            var p2 = RealVector.CreateRandomly(10, -5.0, 5.0, new Random(43));

            var child1 = RealVector.ArithmeticCrossover(p1, p2, new Random(44));
            var child2 = RealVector.ArithmeticCrossover(p1, p2, new Random(44));

            Assert.That(child2.ToArray(), Is.EqualTo(child1.ToArray()));
        }

        [Test]
        public void ArithmeticCrossover_ProducesConvexCombinations()
        {
            var p1 = new RealVector<double>(new[] { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 });
            var p2 = new RealVector<double>(10);

            var child = RealVector.ArithmeticCrossover(p1, p2, new Random(42));

            // alpha is drawn from [0, 1), hence every value is a convex
            // combination of the parent values.
            for (var i = 0; i < child.Length; i++)
            {
                Assert.That(child[i], Is.InRange(0.0, 1.0));
            }
        }

        [Test]
        public void ArithmeticCrossover_Throws_IfLengthsDiffer()
        {
            var p1 = RealVector.CreateRandomly(3, -5.0, 5.0, new Random(42));
            var p2 = RealVector.CreateRandomly(2, -5.0, 5.0, new Random(43));

            Assert.That(
                () => RealVector.ArithmeticCrossover(p1, p2, new Random(44)),
                Throws.ArgumentException);
        }

        [Test]
        public void ArithmeticCrossover_WithEqualParents_ReturnsTheParents()
        {
            var parent = new RealVector<double>(new[] { 0.3, -1.2, 7.7 });

            var child = RealVector.ArithmeticCrossover(parent, parent, new Random(42));

            // Floating point rounding may introduce a tiny error, hence the
            // comparison uses a small tolerance.
            for (var i = 0; i < child.Length; i++)
            {
                Assert.That(child[i], Is.EqualTo(parent[i]).Within(1e-12));
            }
        }
    }
}
