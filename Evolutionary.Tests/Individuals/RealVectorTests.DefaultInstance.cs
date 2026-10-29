using System;
using Evolutionary;
using NUnit.Framework;

namespace Evolutionary.Tests
{
    public partial class RealVectorTests
    {
        [Test]
        public void DefaultInstance_HasZeroLength()
        {
            var vector = default(RealVector<double>);

            Assert.That(vector.Length, Is.EqualTo(0));
            Assert.That(vector.Count, Is.EqualTo(0));
            Assert.That(vector.ToArray(), Is.Empty);
            Assert.That(vector.ToString(), Is.EqualTo("[]"));
            Assert.That(vector.ToString("F2"), Is.EqualTo("[]"));
        }

        [Test]
        public void DefaultInstance_Enumeration_IsEmpty()
        {
            var vector = default(RealVector<double>);

            var enumerated = new List<double>();
            foreach (var value in vector)
            {
                enumerated.Add(value);
            }

            Assert.That(enumerated, Is.Empty);
        }

        [Test]
        public void DefaultInstance_Indexer_Throws()
        {
            var vector = default(RealVector<double>);

            Assert.That(() => _ = vector[0], Throws.TypeOf<IndexOutOfRangeException>());
        }

        [Test]
        public void DefaultInstance_GaussianMutation_ReturnsEmptyVector()
        {
            var mutated = RealVector.GaussianMutation(default(RealVector<double>), new Random(42), 0.5);

            Assert.That(mutated.Length, Is.EqualTo(0));
            Assert.That(mutated, Is.EqualTo(default(RealVector<double>)));
        }

        [Test]
        public void DefaultInstance_UniformCrossover_ReturnsEmptyVector()
        {
            var child = RealVector.UniformCrossover(default(RealVector<double>), default(RealVector<double>), new Random(42));

            Assert.That(child.Length, Is.EqualTo(0));
            Assert.That(child, Is.EqualTo(default(RealVector<double>)));
        }

        [Test]
        public void DefaultInstance_ArithmeticCrossover_ReturnsEmptyVector()
        {
            var child = RealVector.ArithmeticCrossover(default(RealVector<double>), default(RealVector<double>), new Random(42));

            Assert.That(child.Length, Is.EqualTo(0));
            Assert.That(child, Is.EqualTo(default(RealVector<double>)));
        }
    }
}
