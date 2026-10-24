using System;
using Evolutionary;
using NUnit.Framework;


namespace Evolutionary.Tests
{
    public partial class PermutationTests
    {
        [Test]
        public void CreateRandomly()
        {
            var permutation = Permutation.CreateRandomly(5, new Random());

            Assert.That(permutation, Is.EquivalentTo(new[] { 0, 1, 2, 3, 4 }));
            Assert.That(permutation.IsCircular, Is.False);
        }

        [Test]
        public void CreateRandomlyCircular()
        {
            var permutation = Permutation.CreateCircularRandomly(5, new Random());

            Assert.That(permutation, Is.EquivalentTo(new[] { 0, 1, 2, 3, 4 }));
            Assert.That(permutation.IsCircular, Is.True);
            Assert.That(permutation[0], Is.EqualTo(0));
        }
    }
}
