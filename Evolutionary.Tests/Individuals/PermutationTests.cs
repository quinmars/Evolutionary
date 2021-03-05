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
        public void New_Count()
        {
            var permutation = new Permutation(5);

            permutation
                .Should().Equal(0, 1, 2, 3, 4);

            permutation.IsCircular
                .Should().BeFalse();
        }
        
        [Fact]
        public void New_Count_Circular()
        {
            var permutation = new Permutation(5, true);

            permutation
                .Should().Equal(0, 1, 2, 3, 4);

            permutation.IsCircular
                .Should().BeTrue();
        }
        
        [Fact]
        public void New_Array()
        {
            var permutation = new Permutation(new int[] { 3, 4, 1, 2}, false);

            permutation
                .Should().Equal(3, 4, 1, 2);
            permutation.IsCircular
                .Should().BeFalse();
        }
        
        [Fact]
        public void New_Array_Circular()
        {
            var permutation = new Permutation(new int[] { 5, 2, 3, 4}, true);

            permutation
                .Should().Equal(5, 2, 3, 4);
            permutation.IsCircular
                .Should().BeTrue();
        }
    }
}
