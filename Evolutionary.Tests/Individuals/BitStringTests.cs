using System;
using System.Collections.Generic;
using Evolutionary;
using NUnit.Framework;

namespace Evolutionary.Tests
{
    public partial class BitStringTests
    {
        [Test]
        public void New_IntCtor_AllBitsFalse()
        {
            var bitString = new BitString(5);

            Assert.That(bitString.Length, Is.EqualTo(5));
            Assert.That(bitString.Count, Is.EqualTo(5));
            Assert.That(bitString.ToArray(), Is.All.EqualTo(false));
        }

        [Test]
        public void New_ArrayCtor_StoresBits()
        {
            var bitString = new BitString(new[] { true, false, true });

            Assert.That(bitString.Length, Is.EqualTo(3));
            Assert.That(bitString[0], Is.True);
            Assert.That(bitString[1], Is.False);
            Assert.That(bitString[2], Is.True);
        }

        [Test]
        public void ToArray_ReturnsCopy()
        {
            var bitString = new BitString(new[] { true, false });

            var array = bitString.ToArray();
            array[0] = false;

            Assert.That(bitString[0], Is.True);
        }

        [Test]
        public void PopCount()
        {
            var bitString = new BitString(new[] { true, false, true, true, false });

            Assert.That(bitString.PopCount(), Is.EqualTo(3));
        }

        [Test]
        public void ToString_ShowsBinaryLiteral()
        {
            var bitString = new BitString(new[] { true, false, true });

            Assert.That(bitString.ToString(), Is.EqualTo("0b101"));
        }

        [Test]
        public void Enumeration_YieldsBits()
        {
            var bitString = new BitString(new[] { true, false, true });

            var enumerated = new List<bool>();
            foreach (var bit in bitString)
            {
                enumerated.Add(bit);
            }

            Assert.That(enumerated, Is.EqualTo(new[] { true, false, true }));
        }

        [Test]
        public void IReadOnlyList_Interface()
        {
            IReadOnlyList<bool> list = new BitString(new[] { true, false, true });

            Assert.That(list.Count, Is.EqualTo(3));
            Assert.That(list[1], Is.False);
        }

        [Test]
        public void Equality_SameBits_AreEqual()
        {
            var a = new BitString(new[] { true, false, true });
            var b = new BitString(new[] { true, false, true });

            Assert.That(a == b, Is.True);
            Assert.That(a != b, Is.False);
            Assert.That(a.Equals(b), Is.True);
            Assert.That(a.GetHashCode(), Is.EqualTo(b.GetHashCode()));
        }

        [Test]
        public void Equality_DifferentBits_AreNotEqual()
        {
            var a = new BitString(new[] { true, false });
            var b = new BitString(new[] { false, true });

            Assert.That(a == b, Is.False);
            Assert.That(a != b, Is.True);
            Assert.That(a.Equals(b), Is.False);
        }

        [Test]
        public void Equality_DifferentLength_AreNotEqual()
        {
            var a = new BitString(new[] { true });
            var b = new BitString(new[] { true, true });

            Assert.That(a.Equals(b), Is.False);
        }

        [Test]
        public void Equals_NullObject_IsFalse()
        {
            var bitString = new BitString(new[] { true });

            Assert.That(bitString.Equals(null), Is.False);
        }
    }
}
