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
    }
}
