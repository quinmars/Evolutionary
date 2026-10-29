using System;
using Evolutionary;
using NUnit.Framework;

namespace Evolutionary.Tests
{
    public partial class RealVectorTests
    {
        [Test]
        public void CreateRandomly_ValuesAreWithinRange()
        {
            var vector = RealVector.CreateRandomly(100, -2.5, 7.5, new Random(42));

            Assert.That(vector.Length, Is.EqualTo(100));

            for (var i = 0; i < vector.Length; i++)
            {
                // The upper bound is exclusive, hence the value must be
                // strictly less than max.
                Assert.That(vector[i], Is.InRange(-2.5, 7.5));
                Assert.That(vector[i], Is.LessThan(7.5));
            }
        }

        [Test]
        public void CreateRandomly_IsDeterministicForTheSameSeed()
        {
            var v1 = RealVector.CreateRandomly(10, -5.0, 5.0, new Random(42));
            var v2 = RealVector.CreateRandomly(10, -5.0, 5.0, new Random(42));

            Assert.That(v2.ToArray(), Is.EqualTo(v1.ToArray()));
        }
    }
}
