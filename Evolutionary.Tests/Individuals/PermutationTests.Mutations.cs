using Evolutionary.Individuals;
using NUnit.Framework;
using Troschuetz.Random;

namespace Evolutionary.Tests.Individuals
{
    public partial class PermutationTests
    {
        [Test]
        public void SwapMutation()
        {
            var array = new int[] { 1, 2, 3, 4, 5, 6 };
            var permutation = new Permutation(array, false);

            var offspring = Permutation.SwapMutation(permutation, new TRandom());

            Assert.That(offspring, Is.EquivalentTo(array));
        }

        [Test]
        public void SwapMutation_Circular()
        {
            var array = new int[] { 1, 2, 3, 4, 5, 6 };
            var permutation = new Permutation(array, true);

            var offspring = Permutation.SwapMutation(permutation, new TRandom());

            Assert.That(offspring, Is.EquivalentTo(array));
            Assert.That(offspring[0], Is.EqualTo(1));
        }

        [Test]
        public void InsertMutation()
        {
            var array = new int[] { 1, 2, 3, 4, 5, 6 };
            var permutation = new Permutation(array, false);

            var offspring = Permutation.InsertMutation(permutation, new TRandom());

            Assert.That(offspring, Is.EquivalentTo(array));
        }

        [Test]
        public void InsertMutation_Circular()
        {
            var array = new int[] { 1, 2, 3, 4, 5, 6 };
            var permutation = new Permutation(array, true);

            var offspring = Permutation.InsertMutation(permutation, new TRandom());

            Assert.That(offspring, Is.EquivalentTo(array));
            Assert.That(offspring[0], Is.EqualTo(1));
        }

        [Test]
        public void ScrambleMutation()
        {
            var array = new int[] { 1, 2, 3, 4, 5, 6 };
            var permutation = new Permutation(array, false);

            var offspring = Permutation.ScrambleMutation(permutation, new TRandom());

            Assert.That(offspring, Is.EquivalentTo(array));
        }

        [Test]
        public void ScrambleMutation_Circular()
        {
            var array = new int[] { 1, 2, 3, 4, 5, 6 };
            var permutation = new Permutation(array, true);

            var offspring = Permutation.ScrambleMutation(permutation, new TRandom());

            Assert.That(offspring, Is.EquivalentTo(array));
            Assert.That(offspring[0], Is.EqualTo(1));
        }

        [Test]
        public void InversionMutation()
        {
            var array = new int[] { 1, 2, 3, 4, 5, 6 };
            var permutation = new Permutation(array, false);

            var offspring = Permutation.InversionMutation(permutation, new TRandom());

            Assert.That(offspring, Is.EquivalentTo(array));
        }

        [Test]
        public void InversionMutation_Circular()
        {
            var array = new int[] { 1, 2, 3, 4, 5, 6 };
            var permutation = new Permutation(array, true);

            var offspring = Permutation.InversionMutation(permutation, new TRandom());

            Assert.That(offspring, Is.EquivalentTo(array));
            Assert.That(offspring[0], Is.EqualTo(1));
        }
    }
}
