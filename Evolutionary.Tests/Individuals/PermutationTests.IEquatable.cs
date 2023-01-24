using Evolutionary.Individuals;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Troschuetz.Random;
using Xunit;

namespace Evolutionary.Tests.Individuals
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

        [MemberData(nameof(ValueEqualityData))]
        [MemberData(nameof(ReferenceEqualityData))]
        [Theory]
        public void Equality(Permutation a, Permutation b, bool shouldBeEqual)
        {
            if (shouldBeEqual)
            {
                // Test Equal(object)
                a.Equals((object)b)
                    .Should().BeTrue();
                a.Equals("string")
                    .Should().BeFalse();

                // Test Equal(Permutation)
                a.Equals(b)
                    .Should().BeTrue();

                // operator
                (a == b)
                    .Should().BeTrue();
                (a != b)
                    .Should().BeFalse();

                a.GetHashCode()
                    .Should().Be(b.GetHashCode());
            }
            else
            {
                // Test Equal(object)
                a.Equals((object)b)
                    .Should().BeFalse();
                a.Equals("string")
                    .Should().BeFalse();

                // Test Equal(Permutation)
                a.Equals(b)
                    .Should().BeFalse();

                // operator
                (a != b)
                    .Should().BeTrue();
                (a == b)
                    .Should().BeFalse();

                // This is technically not required but the current
                // implementation fulfills this. If this should ever
                // fail it could be bad luck or the the implementation
                // is really broken.
                a.GetHashCode()
                    .Should().NotBe(b.GetHashCode());
            }
        }

        public static IEnumerable<object[]> EqualityNullData =>
            Permutations.Select(f => new object[] { f() });

        [MemberData(nameof(EqualityNullData))]
        [Theory]
        public void EqualityNull(Permutation? val)
        {
            (val == null)
                .Should().BeFalse();
            (val != null)
                .Should().BeTrue();
            (null == val)
                .Should().BeFalse();
            (null != val)
                .Should().BeTrue();

            // This is using Equals(object)
            val.Should()
                .NotBeNull();
        }
    }
}
