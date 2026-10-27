using System;
using System.Linq;
using Evolutionary;
using NUnit.Framework;

namespace Evolutionary.Tests
{
    public partial class BitStringTests
    {
        [Test]
        public void UniformCrossover_IsDeterministicForTheSameSeed()
        {
            var p1 = new BitString(new[] { true, false, true, false });
            var p2 = new BitString(new[] { false, true, false, true });

            var child1 = BitString.UniformCrossover(p1, p2, new Random(42));
            var child2 = BitString.UniformCrossover(p1, p2, new Random(42));

            Assert.That(child2.ToArray(), Is.EqualTo(child1.ToArray()));
        }

        [Test]
        public void UniformCrossover_MixesBothParents()
        {
            var p1 = new BitString(Enumerable.Repeat(true, 100).ToArray());
            var p2 = new BitString(100);

            var child = BitString.UniformCrossover(p1, p2, new Random(42));

            // With 100 fair coin flips the child is (practically) guaranteed
            // to inherit from both parents.
            Assert.That(child.PopCount(), Is.InRange(1, 99));
        }

        [Test]
        public void UniformCrossover_Throws_IfLengthsDiffer()
        {
            var p1 = new BitString(new[] { true, false, true });
            var p2 = new BitString(new[] { true, false });

            Assert.That(
                () => BitString.UniformCrossover(p1, p2, new Random()),
                Throws.ArgumentException);
        }

        [Test]
        public void OnePointCrossover_PrefixFromFirstParentSuffixFromSecond()
        {
            var p1 = new BitString(new[] { true, false, true, false, true, false });
            var p2 = new BitString(new[] { false, true, false, true, false, true });

            var child = BitString.OnePointCrossover(p1, p2, new Random(42));

            // The parents are complementary, hence the first position where
            // the child differs from the first parent is the cut position.
            var cut = 0;
            while (cut < p1.Length && child[cut] == p1[cut])
            {
                cut++;
            }

            for (var i = 0; i < child.Length; i++)
            {
                var expected = i < cut ? p1[i] : p2[i];
                Assert.That(child[i], Is.EqualTo(expected));
            }

            Assert.That(cut, Is.InRange(1, p1.Length - 1));
        }

        [Test]
        public void OnePointCrossover_Throws_IfLengthsDiffer()
        {
            var p1 = new BitString(new[] { true, false, true });
            var p2 = new BitString(new[] { true, false });

            Assert.That(
                () => BitString.OnePointCrossover(p1, p2, new Random()),
                Throws.ArgumentException);
        }
    }
}
