using FluentAssertions;
using System;
using System.Linq;
using Troschuetz.Random;
using Xunit;

namespace Evolutionary.Tests
{
    public class EnviromentTests
    {
        [Fact]
        public void New_NullCheck()
        {
            Action act1 = () => new Enviroment<int, int>(0, null, new TRandom());
            Action act2 = () => new Enviroment<int, int>(0, (a, d) => a, null);

            act1
                .Should().Throw<ArgumentNullException>();
            act2
                .Should().Throw<ArgumentNullException>();
        }
    }
}
