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
        public void PartiallyMappedCrossover_Indexed1()
        {
            var a1 = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var a2 = new int[] { 9, 3, 7, 8, 2, 6, 5, 1, 4 };
            var p1 = new Permutation(a1, true);
            var p2 = new Permutation(a2, true);

            var offspring = Permutation.PartiallyMappedCrossover(p1, p2, 3, 4);

            Assert.That(offspring.ToArray(), Is.EqualTo(new[] { 9, 3, 2, 4, 5, 6, 7, 1, 8 }));
        }

        [Test]
        public void PartiallyMappedCrossover_Indexed2()
        {
            var a1 = new int[] { 5, 7, 6, 2, 1, 4, 0, 3 };
            var a2 = new int[] { 4, 6, 5, 0, 2, 1, 7, 3 };
            var p1 = new Permutation(a1, true);
            var p2 = new Permutation(a2, true);

            var offspring = Permutation.PartiallyMappedCrossover(p1, p2, 1, 7);

            Assert.That(offspring.ToArray(), Is.EqualTo(new[] { 5, 7, 6, 2, 1, 4, 0, 3 }));
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
        public void CycleCrossover_Offspring()
        {
            var a1 = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var a2 = new int[] { 9, 3, 7, 8, 2, 6, 5, 1, 4 };
            var p1 = new Permutation(a1, false);
            var p2 = new Permutation(a2, false);

            var offspring = Permutation.CycleCrossover(p1, p2);

            Assert.That(offspring.ToArray(), Is.EqualTo(new[] { 1, 3, 7, 4, 2, 6, 5, 8, 9 }));
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

        [Test]
        public void CutAndCrossfillCrossover_Indexed1()
        {
            var a1 = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var a2 = new int[] { 9, 3, 7, 8, 2, 6, 5, 1, 4 };
            var p1 = new Permutation(a1, true);
            var p2 = new Permutation(a2, true);

            var offspring = Permutation.CutAndCrossfillCrossover(p1, p2, 4);

            Assert.That(offspring.ToArray(), Is.EqualTo(new[] { 1, 2, 3, 4, 9, 7, 8, 6, 5 }));
        }

        [Test]
        public void CutAndCrossfillCrossover_Indexed2()
        {
            var a1 = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var a2 = new int[] { 9, 3, 7, 8, 2, 6, 5, 1, 4 };
            var p1 = new Permutation(a1, false);
            var p2 = new Permutation(a2, false);

            var offspring = Permutation.CutAndCrossfillCrossover(p1, p2, 8);

            Assert.That(offspring.ToArray(), Is.EqualTo(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }));
        }

        [Test]
        public void CutAndCrossfillCrossover_Indexed_ArgumentChecks()
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

            Action act1 = () => Permutation.CutAndCrossfillCrossover(p1, p2, 3);
            Action act2 = () => Permutation.CutAndCrossfillCrossover(p3, p4, 3);
            Action act3 = () => Permutation.CutAndCrossfillCrossover(p5, p5, 1);
            Action act4 = () => Permutation.CutAndCrossfillCrossover(p1, p1, 0);
            Action act5 = () => Permutation.CutAndCrossfillCrossover(p1, p1, 6);

            Assert.That(act1, Throws.ArgumentException);
            Assert.That(act2, Throws.ArgumentException);
            Assert.That(act3, Throws.InvalidOperationException);
            Assert.That(act4, Throws.TypeOf<ArgumentOutOfRangeException>());
            Assert.That(act5, Throws.TypeOf<ArgumentOutOfRangeException>());
        }
    }
}
