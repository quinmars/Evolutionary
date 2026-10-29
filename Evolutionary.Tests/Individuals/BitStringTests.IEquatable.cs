using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Evolutionary.Tests
{
    public partial class BitStringTests
    {
        public static IEnumerable<Func<BitString>> BitStrings { get; } = new Func<BitString>[]
        {
            () => new BitString(),
            () => new BitString([true]),
            () => new BitString([false]),
            () => new BitString([true, false]),
            () => new BitString([false, true]),
            () => new BitString([true, true, true]),
        };

        public static IEnumerable<object[]> ValueEqualityData =>
            from a in BitStrings.Select((f, i) => (id: f(), index: i))
            from b in BitStrings.Select((f, i) => (id: f(), index: i))
            select new object[] { a.id, b.id, a.index == b.index };

        public static IEnumerable<object[]> ReferenceEqualityData
        {
            get
            {
                var list = BitStrings.Select((f, i) => (id: f(), index: i)).ToList();
                return from a in list
                       from b in list
                       select new object[] { a.id, b.id, a.index == b.index };
            }
        }

        public static IEnumerable<object[]> DefaultValueEqualityData => new object[][]
        {
            [new BitString(), default(BitString), true],
            [new BitString(), new BitString(0), true],
            [new BitString(Array.Empty<bool>()), default(BitString), true],
            [new BitString(new bool[] { }), default(BitString), true],
            [new BitString(new bool[] { }), new BitString(Array.Empty<bool>()), true],
            [new BitString(0), new BitString(Array.Empty<bool>()), true]
        };

        [TestCaseSource(nameof(ValueEqualityData))]
        [TestCaseSource(nameof(ReferenceEqualityData))]
        [TestCaseSource(nameof(DefaultValueEqualityData))]
        [Test]
        public void Equality(BitString a, BitString b, bool shouldBeEqual)
        {
            if (shouldBeEqual)
            {
                // Test Equal(object)
                Assert.That(a.Equals((object)b), Is.True);
                Assert.That(a.Equals("string"), Is.False);

                // Test Equal(Permutation)
                Assert.That(a.Equals(b), Is.True);

                // operator
                Assert.That(a == b, Is.True);
                Assert.That(a != b, Is.False);

                Assert.That(a.GetHashCode(), Is.EqualTo(b.GetHashCode()));
            }
            else
            {
                // Test Equal(object)
                Assert.That(a.Equals((object)b), Is.False);
                Assert.That(a.Equals("string"), Is.False);

                // Test Equal(Permutation)
                Assert.That(a.Equals(b), Is.False);

                // operator
                Assert.That(a != b, Is.True);
                Assert.That(a == b, Is.False);

                // This is technically not required but the current
                // implementation fulfills this. If this should ever
                // fail it could be bad luck or the the implementation
                // is really broken.
                Assert.That(a.GetHashCode(), Is.Not.EqualTo(b.GetHashCode()));
            }
        }

        public static IEnumerable<object[]> EqualityNullData =>
            BitStrings.Select(f => new object[] { f() });

        [TestCaseSource(nameof(EqualityNullData))]
        [Test]
        public void EqualityNull(BitString? val)
        {
            Assert.That(val == null, Is.False);
            Assert.That(val != null, Is.True);
            Assert.That(null == val, Is.False);
            Assert.That(null != val, Is.True);

            // This is using Equals(object)
            Assert.That(val, Is.Not.Null);
        }
    }
}
