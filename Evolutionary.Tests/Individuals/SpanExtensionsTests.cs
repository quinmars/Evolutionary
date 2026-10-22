using System;
using Evolutionary.Individuals;
using NUnit.Framework;
using Troschuetz.Random;

namespace Evolutionary.Tests.Individuals
{
    public class SpanExtensionsTests
    {
        [Test]
        public void Swap()
        {
            var array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            array.AsSpan().Swap(3, 5);

            Assert.That(array, Is.EqualTo(new[] { 1, 2, 3, 6, 5, 4, 7, 8, 9 }));
        }

        [Test]
        public void RightRotate()
        {
            var array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            array.AsSpan(3, 4).RightRotate();

            Assert.That(array, Is.EqualTo(new[] { 1, 2, 3, 7, 4, 5, 6, 8, 9 }));
        }

        [TestCase(0, new int[] { 1, 2, 3, 4, 5 })]
        [TestCase(1, new int[] { 5, 1, 2, 3, 4 })]
        [TestCase(2, new int[] { 4, 5, 1, 2, 3 })]
        [TestCase(3, new int[] { 3, 4, 5, 1, 2 })]
        [TestCase(4, new int[] { 2, 3, 4, 5, 1 })]
        [TestCase(5, new int[] { 1, 2, 3, 4, 5 })]
        [TestCase(6, new int[] { 5, 1, 2, 3, 4 })]
        [TestCase(7, new int[] { 4, 5, 1, 2, 3 })]
        [TestCase(8, new int[] { 3, 4, 5, 1, 2 })]
        [TestCase(-1, new int[] { 2, 3, 4, 5, 1 })]
        [TestCase(-2, new int[] { 3, 4, 5, 1, 2 })]
        [TestCase(-3, new int[] { 4, 5, 1, 2, 3 })]
        [TestCase(-4, new int[] { 5, 1, 2, 3, 4 })]
        [TestCase(-5, new int[] { 1, 2, 3, 4, 5 })]
        [Test]
        public void RightRotateWithArgument(int k, int[] expect)
        {
            var array = new int[] { 1, 2, 3, 4, 5 };
            array.AsSpan().RightRotate(k);

            Assert.That(array, Is.EqualTo(expect));
        }

        [Test]
        public void RightRotate_Single()
        {
            var array = new int[] { 2 };
            array.AsSpan().RightRotate();

            Assert.That(array, Is.EqualTo(new[] { 2 }));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(-1)]
        [Test]
        public void RightRotate_Single_WithArgument(int n)
        {
            var array = new int[] { 2 };
            array.AsSpan().RightRotate(n);

            Assert.That(array, Is.EqualTo(new[] { 2 }));
        }

        [Test]
        public void RightRotate_Empty()
        {
            var array = new int[] { };
            array.AsSpan().RightRotate();

            Assert.That(array, Is.Empty);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(-1)]
        [Test]
        public void RightRotate_Empty_WithArgument(int n)
        {
            var array = new int[] { };
            array.AsSpan().RightRotate(n);

            Assert.That(array, Is.Empty);
        }

        [Test]
        public void LeftRotate()
        {
            var array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            array.AsSpan(3, 4).LeftRotate();

            Assert.That(array, Is.EqualTo(new[] { 1, 2, 3, 5, 6, 7, 4, 8, 9 }));
        }

        [TestCase(0, new int[] { 1, 2, 3, 4, 5 })]
        [TestCase(1, new int[] { 2, 3, 4, 5, 1 })]
        [TestCase(2, new int[] { 3, 4, 5, 1, 2 })]
        [TestCase(3, new int[] { 4, 5, 1, 2, 3 })]
        [TestCase(4, new int[] { 5, 1, 2, 3, 4 })]
        [TestCase(5, new int[] { 1, 2, 3, 4, 5 })]
        [TestCase(6, new int[] { 2, 3, 4, 5, 1 })]
        [TestCase(7, new int[] { 3, 4, 5, 1, 2 })]
        [TestCase(8, new int[] { 4, 5, 1, 2, 3 })]
        [TestCase(-1, new int[] { 5, 1, 2, 3, 4 })]
        [TestCase(-2, new int[] { 4, 5, 1, 2, 3 })]
        [TestCase(-3, new int[] { 3, 4, 5, 1, 2 })]
        [TestCase(-4, new int[] { 2, 3, 4, 5, 1 })]
        [TestCase(-5, new int[] { 1, 2, 3, 4, 5 })]
        [TestCase(-6, new int[] { 5, 1, 2, 3, 4 })]
        [Test]
        public void LeftRotateWithArgument(int k, int[] expect)
        {
            var array = new int[] { 1, 2, 3, 4, 5 };
            array.AsSpan().LeftRotate(k);

            Assert.That(array, Is.EqualTo(expect));
        }

        [Test]
        public void LeftRotate_Single()
        {
            var array = new int[] { 2 };
            array.AsSpan().LeftRotate();

            Assert.That(array, Is.EqualTo(new[] { 2 }));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(-1)]
        [Test]
        public void LeftRotate_Single_WithArgument(int n)
        {
            var array = new int[] { 2 };
            array.AsSpan().RightRotate(n);

            Assert.That(array, Is.EqualTo(new[] { 2 }));
        }

        [Test]
        public void LeftRotate_Empty()
        {
            var array = new int[] { };
            array.AsSpan().RightRotate();

            Assert.That(array, Is.Empty);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(-1)]
        [Test]
        public void LeftRotate_Empty_WithArgument(int n)
        {
            var array = new int[] { };
            array.AsSpan().RightRotate(n);

            Assert.That(array, Is.Empty);
        }

        [Test]
        public void Scrumble()
        {
            var array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            array.AsSpan().Scramble(new TRandom());

            Assert.That(array, Is.EquivalentTo(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }));
        }

        [Test]
        public void PartiallyMappedCrossover1()
        {
            var p1 = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var p2 = new int[] { 9, 3, 7, 8, 2, 6, 5, 1, 4 };
            var offspring = new int[p1.Length];

            SpanExtensions.PartiallyMappedCrossover(p1, p2, 3, 4, offspring);

            Assert.That(offspring, Is.EqualTo(new[] { 9, 3, 2, 4, 5, 6, 7, 1, 8 }));
        }

        [Test]
        public void PartiallyMappedCrossover2()
        {
            var p1 = new int[] { 5, 7, 6, 2, 1, 4, 0, 3 };
            var p2 = new int[] { 4, 6, 5, 0, 2, 1, 7, 3 };
            var offspring = new int[p1.Length];

            SpanExtensions.PartiallyMappedCrossover(p1, p2, 1, 7, offspring);

            Assert.That(offspring, Is.EqualTo(new[] { 5, 7, 6, 2, 1, 4, 0, 3 }));
        }


        [Test]
        public void CutAndCrossfillCrossover()
        {
            var p1 = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var p2 = new int[] { 9, 3, 7, 8, 2, 6, 5, 1, 4 };
            var offspring = new int[p1.Length];

            SpanExtensions.CutAndCrossfillCrossover(p1, p2, 4, offspring);

            Assert.That(offspring, Is.EqualTo(new[] { 1, 2, 3, 4, 9, 7, 8, 6, 5 }));
        }

        [Test]
        public void CycleCrossover()
        {
            var p1 = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var p2 = new int[] { 9, 3, 7, 8, 2, 6, 5, 1, 4 };
            var offspring = new int[p1.Length];

            SpanExtensions.CycleCrossover(p1, p2, offspring);

            Assert.That(offspring, Is.EqualTo(new[] { 1, 3, 7, 4, 2, 6, 5, 8, 9 }));
        }
    }
}
