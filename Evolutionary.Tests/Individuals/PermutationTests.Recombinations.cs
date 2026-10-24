using System;
using Evolutionary;
using NUnit.Framework;


namespace Evolutionary.Tests
{
    public partial class PermutationTests
    {
        [Test]
        public void PartiallyMappedCrossover_ArgumentChecks()
        {
            var a1 = new int[] { 1, 2, 3, 4, 5, 6 };
            var a2 = new int[] { 1, 2, 3, 4, 5 };
            var a3 = new int[] { 1, 2, 3, 4, 6, 5 };

            var p1 = new Permutation(a1, false);
            var p2 = new Permutation(a2, false);
            var p3 = new Permutation(a1, true);
            var p4 = new Permutation(a3, false);

            Action act1 = () => Permutation.PartiallyMappedCrossover(p1, p2, new Random());
            Action act2 = () => Permutation.PartiallyMappedCrossover(p3, p4, new Random());

            Assert.That(act1, Throws.ArgumentException);
            Assert.That(act2, Throws.ArgumentException);
        }

        [Test]
        public void PartiallyMappedCrossover()
        {
            var a1 = new int[] { 1, 2, 3, 4, 5, 6 };
            var a2 = new int[] { 6, 5, 3, 4, 2, 1 };
            var p1 = new Permutation(a1, false);
            var p2 = new Permutation(a2, false);

            var offspring = Permutation.PartiallyMappedCrossover(p1, p2, new Random());

            Assert.That(offspring, Is.EquivalentTo(a1));
        }

        [Test]
        public void PartiallyMappedCrossover_Circular()
        {
            var a1 = new int[] { 1, 2, 3, 4, 5, 6 };
            var a2 = new int[] { 1, 5, 3, 4, 2, 6 };
            var p1 = new Permutation(a1, true);
            var p2 = new Permutation(a2, true);

            var offspring = Permutation.PartiallyMappedCrossover(p1, p2, new Random());

            Assert.That(offspring, Is.EquivalentTo(a1));
        }

        [TestCase(true)]
        [TestCase(false)]
        [Test]
        public void PartiallyMappedCrossover_Identity(bool circular)
        {
            var a1 = new int[] { 1, 2, 3, 4, 5, 6 };
            var p1 = new Permutation(a1, circular);

            var offspring = Permutation.PartiallyMappedCrossover(p1, p1, new Random());

            Assert.That(offspring, Is.EqualTo(a1));
        }

        [Test]
        public void CycleCrossover()
        {
            var a1 = new int[] { 1, 2, 3, 4, 5, 6 };
            var a2 = new int[] { 6, 5, 3, 4, 2, 1 };
            var p1 = new Permutation(a1, false);
            var p2 = new Permutation(a2, false);

            var offspring = Permutation.CycleCrossover(p1, p2, new Random());

            Assert.That(offspring, Is.EquivalentTo(a1));
        }

        [Test]
        public void CycleCrossover_Circular()
        {
            var a1 = new int[] { 1, 2, 3, 4, 5, 6 };
            var a2 = new int[] { 1, 5, 3, 4, 2, 6 };
            var p1 = new Permutation(a1, true);
            var p2 = new Permutation(a2, true);

            var offspring = Permutation.CycleCrossover(p1, p2, new Random());

            Assert.That(offspring, Is.EquivalentTo(a1));
        }

        [Test]
        public void CycleCrossover_ArgumentChecks()
        {
            var a1 = new int[] { 1, 2, 3, 4, 5, 6 };
            var a2 = new int[] { 1, 2, 3, 4, 5 };
            var a3 = new int[] { 1, 2, 3, 4, 6, 5 };

            var p1 = new Permutation(a1, false);
            var p2 = new Permutation(a2, false);
            var p3 = new Permutation(a1, true);
            var p4 = new Permutation(a3, false);

            Action act1 = () => Permutation.CycleCrossover(p1, p2, new Random());
            Action act2 = () => Permutation.CycleCrossover(p3, p4, new Random());

            Assert.That(act1, Throws.ArgumentException);
            Assert.That(act2, Throws.ArgumentException);
        }

        [TestCase(true)]
        [TestCase(false)]
        [Test]
        public void CycleCrossover_Identity(bool circular)
        {
            var a1 = new int[] { 1, 2, 3, 4, 5, 6 };
            var p1 = new Permutation(a1, circular);

            var offspring = Permutation.CycleCrossover(p1, p1, new Random());

            Assert.That(offspring, Is.EqualTo(a1));
        }


        [Test]
        public void CutAndCrossfillCrossover()
        {
            var a1 = new int[] { 1, 2, 3, 4, 5, 6 };
            var a2 = new int[] { 6, 5, 3, 4, 2, 1 };
            var p1 = new Permutation(a1, false);
            var p2 = new Permutation(a2, false);

            var offspring = Permutation.CutAndCrossfillCrossover(p1, p2, new Random());

            Assert.That(offspring, Is.EquivalentTo(a1));
        }

        [Test]
        public void CutAndCrossfillCrossover_Circular()
        {
            var a1 = new int[] { 1, 2, 3, 4, 5, 6 };
            var a2 = new int[] { 1, 5, 3, 4, 2, 6 };
            var p1 = new Permutation(a1, true);
            var p2 = new Permutation(a2, true);

            var offspring = Permutation.CutAndCrossfillCrossover(p1, p2, new Random());

            Assert.That(offspring, Is.EquivalentTo(a1));
        }

        [Test]
        public void CutAndCrossfillCrossover_ArgumentChecks()
        {
            var a1 = new int[] { 1, 2, 3, 4, 5, 6 };
            var a2 = new int[] { 1, 2, 3, 4, 5 };
            var a3 = new int[] { 1, 2, 3, 4, 6, 5 };
            var a4 = new int[] { 2, 3 };

            var p1 = new Permutation(a1, false);
            var p2 = new Permutation(a2, false);
            var p3 = new Permutation(a1, true);
            var p4 = new Permutation(a3, false);
            var p5 = new Permutation(a4, true);

            Action act1 = () => Permutation.CutAndCrossfillCrossover(p1, p2, new Random());
            Action act2 = () => Permutation.CutAndCrossfillCrossover(p3, p4, new Random());
            Action act3 = () => Permutation.CutAndCrossfillCrossover(p5, p5, new Random());

            Assert.That(act1, Throws.ArgumentException);
            Assert.That(act2, Throws.ArgumentException);
            Assert.That(act3, Throws.InvalidOperationException);
        }

        [TestCase(true)]
        [TestCase(false)]
        [Test]
        public void CutAndCrossfillCrossover_Identity(bool circular)
        {
            var a1 = new int[] { 1, 2, 3, 4, 5, 6 };
            var p1 = new Permutation(a1, circular);

            var offspring = Permutation.CutAndCrossfillCrossover(p1, p1, new Random());

            Assert.That(offspring, Is.EqualTo(a1));
        }
    }
}
