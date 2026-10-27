using System;
using Evolutionary;
using NUnit.Framework;

namespace Evolutionary.Tests
{
    public partial class BitStringTests
    {
        [Test]
        public void FlipMutation_ProbabilityZero_Unchanged()
        {
            var bitString = new BitString(new[] { true, false, true });

            var mutated = BitString.FlipMutation(bitString, new Random(42), 0.0);

            Assert.That(mutated.ToArray(), Is.EqualTo(new[] { true, false, true }));
        }

        [Test]
        public void FlipMutation_ProbabilityOne_AllBitsFlipped()
        {
            var bitString = new BitString(new[] { true, false, true, false });

            var mutated = BitString.FlipMutation(bitString, new Random(42), 1.0);

            Assert.That(mutated.ToArray(), Is.EqualTo(new[] { false, true, false, true }));
        }

        [Test]
        public void FlipMutation_DoesNotChangeTheOriginal()
        {
            var bitString = new BitString(new[] { true, false, true });
            var original = bitString.ToArray();

            _ = BitString.FlipMutation(bitString, new Random(42), 0.5);

            Assert.That(bitString.ToArray(), Is.EqualTo(original));
        }

        [Test]
        public void FlipMutation_IsDeterministicForTheSameSeed()
        {
            var bitString = new BitString(new[] { true, false, true, false, true });

            var mutated1 = BitString.FlipMutation(bitString, new Random(42), 0.3);
            var mutated2 = BitString.FlipMutation(bitString, new Random(42), 0.3);

            Assert.That(mutated2.ToArray(), Is.EqualTo(mutated1.ToArray()));
        }
    }
}
