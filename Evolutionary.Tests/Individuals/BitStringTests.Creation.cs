using System;
using Evolutionary;
using NUnit.Framework;


namespace Evolutionary.Tests
{
    public partial class BitStringTests
    {
        [Test]
        public void CreateRandomly()
        {
            var bitString = BitString.CreateRandomly(5, new Random());

            Assert.That(bitString, Has.Length.EqualTo(5));
        }
    }
}
