using System;
using Evolutionary.Individuals;
using NUnit.Framework;

namespace Evolutionary.Tests.Individuals
{
    public partial class PermutationTests
    {
        [Test]
        public void New_Count()
        {
            var permutation = new Permutation(5);

            Assert.That(permutation, Is.EqualTo(new[] { 0, 1, 2, 3, 4 }));
            Assert.That(permutation.IsCircular, Is.False);
        }

        [Test]
        public void New_Count_Circular()
        {
            var permutation = new Permutation(5, true);

            Assert.That(permutation, Is.EqualTo(new[] { 0, 1, 2, 3, 4 }));
            Assert.That(permutation.IsCircular, Is.True);
        }

        [Test]
        public void New_Array()
        {
            var permutation = new Permutation(new int[] { 3, 4, 1, 2}, false);

            Assert.That(permutation, Is.EqualTo(new[] { 3, 4, 1, 2 }));
            Assert.That(permutation.IsCircular, Is.False);
        }

        [Test]
        public void New_Array_Circular()
        {
            var permutation = new Permutation(new int[] { 5, 2, 3, 4}, true);

            Assert.That(permutation, Is.EqualTo(new[] { 5, 2, 3, 4 }));
            Assert.That(permutation.IsCircular, Is.True);
        }
    }
}
