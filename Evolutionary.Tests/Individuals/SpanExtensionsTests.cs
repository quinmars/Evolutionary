using System;
using Evolutionary;
using NUnit.Framework;


namespace Evolutionary.Tests
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
            array.AsSpan().Scramble(new Random());

            Assert.That(array, Is.EquivalentTo(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }));
        }
    }
}
