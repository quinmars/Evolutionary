using Evolutionary.Individuals;
using NUnit.Framework;
using Troschuetz.Random;

namespace Evolutionary.Tests.Individuals
{
    public partial class PermutationTests
    {
        [Test]
        public void CreateRandomly()
        {
            var permutation = Permutation.CreateRandomly(5, new TRandom());

            Assert.That(permutation, Is.EquivalentTo(new[] { 0, 1, 2, 3, 4 }));
            Assert.That(permutation.IsCircular, Is.False);
        }

        [Test]
        public void CreateRandomlyCircular()
        {
            var permutation = Permutation.CreateCircularRandomly(5, new TRandom());

            Assert.That(permutation, Is.EquivalentTo(new[] { 0, 1, 2, 3, 4 }));
            Assert.That(permutation.IsCircular, Is.True);
            Assert.That(permutation[0], Is.EqualTo(0));
        }
    }
}
