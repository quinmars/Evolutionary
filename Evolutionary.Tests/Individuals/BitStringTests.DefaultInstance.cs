using System;
using Evolutionary;
using NUnit.Framework;

namespace Evolutionary.Tests
{
    public partial class BitStringTests
    {
        [Test]
        public void DefaultInstance_HasZeroLength()
        {
            var bitString = default(BitString);

            Assert.That(bitString.Length, Is.EqualTo(0));
            Assert.That(bitString.Count, Is.EqualTo(0));
            Assert.That(bitString.ToArray(), Is.Empty);
            Assert.That(bitString.PopCount(), Is.EqualTo(0));
        }

        [Test]
        public void DefaultInstance_ToString_IsEmptyBinaryLiteral()
        {
            var bitString = default(BitString);

            Assert.That(bitString.ToString(), Is.EqualTo("0b"));
        }

        [Test]
        public void DefaultInstance_Enumeration_IsEmpty()
        {
            var bitString = default(BitString);

            var enumerated = new List<bool>();
            foreach (var bit in bitString)
            {
                enumerated.Add(bit);
            }

            Assert.That(enumerated, Is.Empty);
        }

        [Test]
        public void DefaultInstance_Indexer_Throws()
        {
            var bitString = default(BitString);

            Assert.That(() => _ = bitString[0], Throws.TypeOf<IndexOutOfRangeException>());
        }

        [Test]
        public void DefaultInstance_FlipMutation_ReturnsEmptyBitString()
        {
            var mutated = BitString.FlipMutation(default(BitString), new Random(42), 0.5);

            Assert.That(mutated.Length, Is.EqualTo(0));
            Assert.That(mutated, Is.EqualTo(default(BitString)));
        }

        [Test]
        public void DefaultInstance_UniformCrossover_ReturnsEmptyBitString()
        {
            var child = BitString.UniformCrossover(default(BitString), default(BitString), new Random(42));

            Assert.That(child.Length, Is.EqualTo(0));
            Assert.That(child, Is.EqualTo(default(BitString)));
        }

        [Test]
        public void DefaultInstance_OnePointCrossover_Throws()
        {
            Assert.That(
                () => BitString.OnePointCrossover(default(BitString), default(BitString), new Random(42)),
                Throws.InvalidOperationException);
        }
    }
}
