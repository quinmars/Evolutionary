using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Evolutionary.Tests
{
    public partial class PermutationTests
    {
        public static IEnumerable<Func<Permutation>> Permutations { get; } = new Func<Permutation>[]
        {
            () => new Permutation(),
            () => new Permutation(Array.Empty<int>(), true),
            () => new Permutation(Array.Empty<int>(), false),
            () => new Permutation(new int[]{ 2, 1 }, true),
            () => new Permutation(new int[]{ 1, 2 }, true),
            () => new Permutation(new int[]{ 2, 1 }, false),
            () => new Permutation(new int[]{ 1, 2 }, false),
            () => new Permutation(new int[]{ 2, 1, 0 }, true),
            () => new Permutation(new int[]{ 1, 2, 0 }, true),
            () => new Permutation(new int[]{ 2, 1, 0 }, false),
            () => new Permutation(new int[]{ 1, 2, 0 }, false),
        };

        public static IEnumerable<object[]> ValueEqualityData =>
            from a in Permutations.Select((f, i) => (id: f(), index: i))
            from b in Permutations.Select((f, i) => (id: f(), index: i))
            select new object[] { a.id, b.id, a.index == b.index };

        public static IEnumerable<object[]> ReferenceEqualityData
        {
            get
            {
                var list = Permutations.Select((f, i) => (id: f(), index: i)).ToList();
                return from a in list
                       from b in list
                       select new object[] { a.id, b.id, a.index == b.index };
            }
        }

        [TestCaseSource(nameof(ValueEqualityData))]
        [TestCaseSource(nameof(ReferenceEqualityData))]
        [Test]
        public void Equality(Permutation a, Permutation b, bool shouldBeEqual)
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
            Permutations.Select(f => new object[] { f() });

        [TestCaseSource(nameof(EqualityNullData))]
        [Test]
        public void EqualityNull(Permutation? val)
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
