using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Evolutionary.Individuals;
using FluentAssertions;
using Troschuetz.Random;

namespace Evolutionary.Tests.Individuals
{
    public class SpanExtensionsTests
    {
        [Fact]
        public void Swap()
        {
            var array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            array.AsSpan().Swap(3, 5);

            array
                .Should().Equal(1, 2, 3, 6, 5, 4, 7, 8, 9);
        }
        
        [Fact]
        public void RightRotate()
        {
            var array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            array.AsSpan(3, 4).RightRotate();

            array
                .Should().Equal(1, 2, 3, 7, 4, 5, 6, 8, 9);
        }
        
        [InlineData(0, new int[] { 1, 2, 3, 4, 5 })]
        [InlineData(1, new int[] { 5, 1, 2, 3, 4 })]
        [InlineData(2, new int[] { 4, 5, 1, 2, 3 })]
        [InlineData(3, new int[] { 3, 4, 5, 1, 2 })]
        [InlineData(4, new int[] { 2, 3, 4, 5, 1 })]
        [InlineData(5, new int[] { 1, 2, 3, 4, 5 })]
        [InlineData(6, new int[] { 5, 1, 2, 3, 4 })]
        [InlineData(7, new int[] { 4, 5, 1, 2, 3 })]
        [InlineData(8, new int[] { 3, 4, 5, 1, 2 })]
        [InlineData(-1, new int[] { 2, 3, 4, 5, 1 })]
        [InlineData(-2, new int[] { 3, 4, 5, 1, 2 })]
        [InlineData(-3, new int[] { 4, 5, 1, 2, 3 })]
        [InlineData(-4, new int[] { 5, 1, 2, 3, 4 })]
        [InlineData(-5, new int[] { 1, 2, 3, 4, 5 })]
        [Theory]
        public void RightRotateWithArgument(int k, int[] expect)
        {
            var array = new int[] { 1, 2, 3, 4, 5 };
            array.AsSpan().RightRotate(k);

            array
                .Should().Equal(expect);
        }
        
        [Fact]
        public void RightRotate_Single()
        {
            var array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            array.AsSpan(3, 1).RightRotate();

            array
                .Should().Equal(1, 2, 3, 4, 5, 6, 7, 8, 9);
        }
        
        [Fact]
        public void RightRotate_Empty()
        {
            var array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            array.AsSpan(3, 0).RightRotate();

            array
                .Should().Equal(1, 2, 3, 4, 5, 6, 7, 8, 9);
        }
        
        [Fact]
        public void LeftRotate()
        {
            var array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            array.AsSpan(3, 4).LeftRotate();

            array
                .Should().Equal(1, 2, 3, 5, 6, 7, 4, 8, 9);
        }
        
        [InlineData(0, new int[] { 1, 2, 3, 4, 5 })]
        [InlineData(1, new int[] { 2, 3, 4, 5, 1 })]
        [InlineData(2, new int[] { 3, 4, 5, 1, 2 })]
        [InlineData(3, new int[] { 4, 5, 1, 2, 3 })]
        [InlineData(4, new int[] { 5, 1, 2, 3, 4 })]
        [InlineData(5, new int[] { 1, 2, 3, 4, 5 })]
        [InlineData(6, new int[] { 2, 3, 4, 5, 1 })]
        [InlineData(7, new int[] { 3, 4, 5, 1, 2 })]
        [InlineData(8, new int[] { 4, 5, 1, 2, 3 })]
        [InlineData(-1, new int[] { 5, 1, 2, 3, 4 })]
        [InlineData(-2, new int[] { 4, 5, 1, 2, 3 })]
        [InlineData(-3, new int[] { 3, 4, 5, 1, 2 })]
        [InlineData(-4, new int[] { 2, 3, 4, 5, 1 })]
        [InlineData(-5, new int[] { 1, 2, 3, 4, 5 })]
        [InlineData(-6, new int[] { 5, 1, 2, 3, 4 })]
        [Theory]
        public void LeftRotateWithArgument(int k, int[] expect)
        {
            var array = new int[] { 1, 2, 3, 4, 5 };
            array.AsSpan().LeftRotate(k);

            array
                .Should().Equal(expect);
        }

        [Fact]
        public void Scrumble()
        {
            var array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            array.AsSpan().Scramble(new TRandom());

            array
                .Should().BeEquivalentTo(1, 2, 3, 4, 5, 6, 7, 8, 9);
        }
        
        [Fact]
        public void PartiallyMappedCrossover1()
        {
            var p1 = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var p2 = new int[] { 9, 3, 7, 8, 2, 6, 5, 1, 4 };
            var offspring = new int[p1.Length];

            SpanExtensions.PartiallyMappedCrossover(p1, p2, 3, 4, offspring);

            offspring
                .Should().Equal(9, 3, 2, 4, 5, 6, 7, 1, 8);
        }

        [Fact]
        public void PartiallyMappedCrossover2()
        {
            var p1 = new int[] { 5, 7, 6, 2, 1, 4, 0, 3 };
            var p2 = new int[] { 4, 6, 5, 0, 2, 1, 7, 3 };
            var offspring = new int[p1.Length];

            SpanExtensions.PartiallyMappedCrossover(p1, p2, 1, 7, offspring);

            offspring
                .Should().Equal(5, 7, 6, 2, 1, 4, 0, 3);
        }
        
        
        [Fact]
        public void CutAndCrossfillCrossover()
        {
            var p1 = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var p2 = new int[] { 9, 3, 7, 8, 2, 6, 5, 1, 4 };
            var offspring = new int[p1.Length];

            SpanExtensions.CutAndCrossfillCrossover(p1, p2, 4, offspring);

            offspring
                .Should().Equal(1, 2, 3, 4, 9, 7, 8, 6, 5);
        }
        
        [Fact]
        public void CycleCrossover()
        {
            var p1 = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var p2 = new int[] { 9, 3, 7, 8, 2, 6, 5, 1, 4 };
            var offspring = new int[p1.Length];

            SpanExtensions.CycleCrossover(p1, p2, offspring);

            offspring
                .Should().Equal(1, 3, 7, 4, 2, 6, 5, 8, 9);
        }
    }
}
