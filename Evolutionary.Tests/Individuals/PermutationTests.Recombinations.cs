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
        public void PartiallyMappedCrossover_ArgumentChecks()
        {
            var a1 = new int[] { 1, 2, 3, 4, 5, 6 };
            var a2 = new int[] { 1, 2, 3, 4, 5 };
            var a3 = new int[] { 1, 2, 3, 4, 6, 5 };

            var p1 = new Permutation(a1, false);
            var p2 = new Permutation(a2, false);
            var p3 = new Permutation(a1, true);
            var p4 = new Permutation(a3, false);

            Action act1 = () => Permutation.PartiallyMappedCrossover(p1, p2, new TRandom());
            Action act2 = () => Permutation.PartiallyMappedCrossover(p3, p4, new TRandom());

            act1
                .Should().Throw<ArgumentException>();
            act2
                .Should().Throw<ArgumentException>();
        }
        
        [Fact]
        public void PartiallyMappedCrossover()
        {
            var a1 = new int[] { 1, 2, 3, 4, 5, 6 };
            var a2 = new int[] { 6, 5, 3, 4, 2, 1 };
            var p1 = new Permutation(a1, false);
            var p2 = new Permutation(a2, false);

            var offspring = Permutation.PartiallyMappedCrossover(p1, p2, new TRandom());

            offspring
                .Should().BeEquivalentTo(a1);
        }
        
        [Fact]
        public void PartiallyMappedCrossover_Circular()
        {
            var a1 = new int[] { 1, 2, 3, 4, 5, 6 };
            var a2 = new int[] { 1, 5, 3, 4, 2, 6 };
            var p1 = new Permutation(a1, true);
            var p2 = new Permutation(a2, true);

            var offspring = Permutation.PartiallyMappedCrossover(p1, p2, new TRandom());

            offspring
                .Should().BeEquivalentTo(a1);
        }
        
        [InlineData(true)]
        [InlineData(false)]
        [Theory]
        public void PartiallyMappedCrossover_Identity(bool circular)
        {
            var a1 = new int[] { 1, 2, 3, 4, 5, 6 };
            var p1 = new Permutation(a1, circular);

            var offspring = Permutation.PartiallyMappedCrossover(p1, p1, new TRandom());

            offspring
                .Should().Equal(a1);
        }
        
        [Fact]
        public void CycleCrossover()
        {
            var a1 = new int[] { 1, 2, 3, 4, 5, 6 };
            var a2 = new int[] { 6, 5, 3, 4, 2, 1 };
            var p1 = new Permutation(a1, false);
            var p2 = new Permutation(a2, false);

            var offspring = Permutation.CycleCrossover(p1, p2, new TRandom());

            offspring
                .Should().BeEquivalentTo(a1);
        }
        
        [Fact]
        public void CycleCrossover_Circular()
        {
            var a1 = new int[] { 1, 2, 3, 4, 5, 6 };
            var a2 = new int[] { 1, 5, 3, 4, 2, 6 };
            var p1 = new Permutation(a1, true);
            var p2 = new Permutation(a2, true);

            var offspring = Permutation.CycleCrossover(p1, p2, new TRandom());

            offspring
                .Should().BeEquivalentTo(a1);
        }
        
        [Fact]
        public void CycleCrossover_ArgumentChecks()
        {
            var a1 = new int[] { 1, 2, 3, 4, 5, 6 };
            var a2 = new int[] { 1, 2, 3, 4, 5 };
            var a3 = new int[] { 1, 2, 3, 4, 6, 5 };

            var p1 = new Permutation(a1, false);
            var p2 = new Permutation(a2, false);
            var p3 = new Permutation(a1, true);
            var p4 = new Permutation(a3, false);

            Action act1 = () => Permutation.CycleCrossover(p1, p2, new TRandom());
            Action act2 = () => Permutation.CycleCrossover(p3, p4, new TRandom());

            act1
                .Should().Throw<ArgumentException>();
            act2
                .Should().Throw<ArgumentException>();
        }
        
        [InlineData(true)]
        [InlineData(false)]
        [Theory]
        public void CycleCrossover_Identity(bool circular)
        {
            var a1 = new int[] { 1, 2, 3, 4, 5, 6 };
            var p1 = new Permutation(a1, circular);

            var offspring = Permutation.CycleCrossover(p1, p1, new TRandom());

            offspring
                .Should().Equal(a1);
        }

        
        [Fact]
        public void CutAndCrossfillCrossover()
        {
            var a1 = new int[] { 1, 2, 3, 4, 5, 6 };
            var a2 = new int[] { 6, 5, 3, 4, 2, 1 };
            var p1 = new Permutation(a1, false);
            var p2 = new Permutation(a2, false);

            var offspring = Permutation.CutAndCrossfillCrossover(p1, p2, new TRandom());

            offspring
                .Should().BeEquivalentTo(a1);
        }
        
        [Fact]
        public void CutAndCrossfillCrossover_Circular()
        {
            var a1 = new int[] { 1, 2, 3, 4, 5, 6 };
            var a2 = new int[] { 1, 5, 3, 4, 2, 6 };
            var p1 = new Permutation(a1, true);
            var p2 = new Permutation(a2, true);

            var offspring = Permutation.CutAndCrossfillCrossover(p1, p2, new TRandom());

            offspring
                .Should().BeEquivalentTo(a1);
        }
        
        [Fact]
        public void CutAndCrossfillCrossover_ArgumentChecks()
        {
            var a1 = new int[] { 1, 2, 3, 4, 5, 6 };
            var a2 = new int[] { 1, 2, 3, 4, 5 };
            var a3 = new int[] { 1, 2, 3, 4, 6, 5 };
            var a4 = new int[] { 2, 3 };

            var p1 = new Permutation(a1, false);
            var p2 = new Permutation(a2, false);
            var p3 = new Permutation(a1, true);
            var p4 = new Permutation(a3, false);
            var p5 = new Permutation(a4, true);

            Action act1 = () => Permutation.CutAndCrossfillCrossover(p1, p2, new TRandom());
            Action act2 = () => Permutation.CutAndCrossfillCrossover(p3, p4, new TRandom());
            Action act3 = () => Permutation.CutAndCrossfillCrossover(p5, p5, new TRandom());

            act1
                .Should().Throw<ArgumentException>();
            act2
                .Should().Throw<ArgumentException>();
            act3
                .Should().Throw<InvalidOperationException>();
        }
        
        [InlineData(true)]
        [InlineData(false)]
        [Theory]
        public void CutAndCrossfillCrossover_Identity(bool circular)
        {
            var a1 = new int[] { 1, 2, 3, 4, 5, 6 };
            var p1 = new Permutation(a1, circular);

            var offspring = Permutation.CutAndCrossfillCrossover(p1, p1, new TRandom());

            offspring
                .Should().Equal(a1);
        }
    }
}
