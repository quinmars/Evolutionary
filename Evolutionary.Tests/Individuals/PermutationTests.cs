using Evolutionary.Individuals;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Troschuetz.Random;
using Xunit;

namespace Evolutionary.Tests.Individuals
{
    public class PermutationTests
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

        [Fact]
        public void SwapMutation()
        {
            var array = new int[] { 1, 2, 3, 4, 5, 6 };
            var permutation = new Permutation(array, false);

            var offspring = Permutation.SwapMutation(permutation, new TRandom());

            offspring
                .Should().BeEquivalentTo(array);
        }
        
        [Fact]
        public void SwapMutation_Circular()
        {
            var array = new int[] { 1, 2, 3, 4, 5, 6 };
            var permutation = new Permutation(array, true);

            var offspring = Permutation.SwapMutation(permutation, new TRandom());

            offspring
                .Should().BeEquivalentTo(array);
            offspring
                .Should().HaveElementAt(0, 1);
        }
        
        [Fact]
        public void InsertMutation()
        {
            var array = new int[] { 1, 2, 3, 4, 5, 6 };
            var permutation = new Permutation(array, false);

            var offspring = Permutation.InsertMutation(permutation, new TRandom());

            offspring
                .Should().BeEquivalentTo(array);
        }
        
        [Fact]
        public void InsertMutation_Circular()
        {
            var array = new int[] { 1, 2, 3, 4, 5, 6 };
            var permutation = new Permutation(array, true);

            var offspring = Permutation.InsertMutation(permutation, new TRandom());

            offspring
                .Should().BeEquivalentTo(array);
            offspring
                .Should().HaveElementAt(0, 1);
        }
        
        [Fact]
        public void ScrambleMutation()
        {
            var array = new int[] { 1, 2, 3, 4, 5, 6 };
            var permutation = new Permutation(array, false);

            var offspring = Permutation.ScrambleMutation(permutation, new TRandom());

            offspring
                .Should().BeEquivalentTo(array);
        }
        
        [Fact]
        public void ScrambleMutation_Circular()
        {
            var array = new int[] { 1, 2, 3, 4, 5, 6 };
            var permutation = new Permutation(array, true);

            var offspring = Permutation.ScrambleMutation(permutation, new TRandom());

            offspring
                .Should().BeEquivalentTo(array);
            offspring
                .Should().HaveElementAt(0, 1);
        }
        
        [Fact]
        public void InversionMutation()
        {
            var array = new int[] { 1, 2, 3, 4, 5, 6 };
            var permutation = new Permutation(array, false);

            var offspring = Permutation.InversionMutation(permutation, new TRandom());

            offspring
                .Should().BeEquivalentTo(array);
        }
        
        [Fact]
        public void InversionMutation_Circular()
        {
            var array = new int[] { 1, 2, 3, 4, 5, 6 };
            var permutation = new Permutation(array, true);

            var offspring = Permutation.InversionMutation(permutation, new TRandom());

            offspring
                .Should().BeEquivalentTo(array);
            offspring
                .Should().HaveElementAt(0, 1);
        }
        
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
