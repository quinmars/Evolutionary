using System;
using System.Collections.Generic;
using Evolutionary;
using NUnit.Framework;

namespace Evolutionary.Tests
{
    public partial class PermutationTests
    {
        [Test]
        public void DefaultInstance_HasZeroCount()
        {
            var permutation = default(Permutation);

            Assert.That(permutation.Count, Is.EqualTo(0));
            Assert.That(permutation.IsCircular, Is.False);
            Assert.That(permutation.ToArray(), Is.Empty);
        }

        [Test]
        public void DefaultInstance_Enumeration_IsEmpty()
        {
            var permutation = default(Permutation);

            var enumerated = new List<int>();
            foreach (var element in permutation)
            {
                enumerated.Add(element);
            }

            Assert.That(enumerated, Is.Empty);
        }

        [Test]
        public void DefaultInstance_Indexer_Throws()
        {
            var permutation = default(Permutation);

            Assert.That(() => _ = permutation[0], Throws.TypeOf<IndexOutOfRangeException>());
        }

        [Test]
        public void DefaultInstance_Mutations_Throw()
        {
            var permutation = default(Permutation);

            Assert.That(() => Permutation.SwapMutation(permutation, new Random(42)), Throws.InvalidOperationException);
            Assert.That(() => Permutation.InsertMutation(permutation, new Random(42)), Throws.InvalidOperationException);
            Assert.That(() => Permutation.ScrambleMutation(permutation, new Random(42)), Throws.InvalidOperationException);
            Assert.That(() => Permutation.InversionMutation(permutation, new Random(42)), Throws.InvalidOperationException);
        }

        [Test]
        public void Mutations_TooShortPermutations_Throw()
        {
            // A linear permutation needs at least two elements and a circular
            // permutation needs at least three elements to be mutable.
            var linear = new Permutation(new int[] { 1 }, false);
            var circular = new Permutation(new int[] { 1, 2 }, true);

            Assert.That(() => Permutation.SwapMutation(linear, new Random(42)), Throws.InvalidOperationException);
            Assert.That(() => Permutation.InsertMutation(linear, new Random(42)), Throws.InvalidOperationException);
            Assert.That(() => Permutation.ScrambleMutation(linear, new Random(42)), Throws.InvalidOperationException);
            Assert.That(() => Permutation.InversionMutation(linear, new Random(42)), Throws.InvalidOperationException);

            Assert.That(() => Permutation.SwapMutation(circular, new Random(42)), Throws.InvalidOperationException);
            Assert.That(() => Permutation.InsertMutation(circular, new Random(42)), Throws.InvalidOperationException);
            Assert.That(() => Permutation.ScrambleMutation(circular, new Random(42)), Throws.InvalidOperationException);
            Assert.That(() => Permutation.InversionMutation(circular, new Random(42)), Throws.InvalidOperationException);
        }

        [Test]
        public void DefaultInstance_CycleCrossover_ReturnsEmptyPermutation()
        {
            var child = Permutation.CycleCrossover(default(Permutation), default(Permutation));

            Assert.That(child.Count, Is.EqualTo(0));
            Assert.That(child, Is.EqualTo(default(Permutation)));
        }

        [Test]
        public void DefaultInstance_PartiallyMappedCrossover_Throws()
        {
            Assert.That(
                () => Permutation.PartiallyMappedCrossover(default(Permutation), default(Permutation), new Random(42)),
                Throws.InvalidOperationException);
        }

        [Test]
        public void DefaultInstance_CutAndCrossfillCrossover_Throws()
        {
            Assert.That(
                () => Permutation.CutAndCrossfillCrossover(default(Permutation), default(Permutation), new Random(42)),
                Throws.InvalidOperationException);
        }
    }
}
