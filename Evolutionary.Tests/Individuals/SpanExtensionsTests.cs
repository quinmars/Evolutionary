﻿using System;
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
        public void RightShift()
        {
            var array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            array.AsSpan(3, 4).RightShift();

            array
                .Should().Equal(1, 2, 3, 7, 4, 5, 6, 8, 9);
        }
        
        [Fact]
        public void RightShift_Single()
        {
            var array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            array.AsSpan(3, 1).RightShift();

            array
                .Should().Equal(1, 2, 3, 4, 5, 6, 7, 8, 9);
        }
        
        [Fact]
        public void RightShift_Empty()
        {
            var array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            array.AsSpan(3, 0).RightShift();

            array
                .Should().Equal(1, 2, 3, 4, 5, 6, 7, 8, 9);
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
        public void PartiallyMappedCrossover()
        {
            var p1 = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var p2 = new int[] { 9, 3, 7, 8, 2, 6, 5, 1, 4 };
            var offspring = new int[p1.Length];

            SpanExtensions.PartiallyMappedCrossover(p1, p2, 3, 4, offspring);

            offspring
                .Should().Equal(9, 3, 2, 4, 5, 6, 7, 1, 8);
        }
    }
}