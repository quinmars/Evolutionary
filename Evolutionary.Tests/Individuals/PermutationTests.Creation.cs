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
        [Fact]
        public void CreateRandomly()
        {
            var permutation = Permutation.CreateRandomly(5, new TRandom());

            permutation
                .Should().BeEquivalentTo(0, 1, 2, 3, 4);
            permutation.IsCircular
                .Should().BeFalse();
        }
        
        [Fact]
        public void CreateRandomlyCircular()
        {
            var permutation = Permutation.CreateCircularRandomly(5, new TRandom());

            permutation
                .Should().BeEquivalentTo(0, 1, 2, 3, 4);
            permutation.IsCircular
                .Should().BeTrue();
            permutation
                .Should().HaveElementAt(0, 0);
        }
    }
}
