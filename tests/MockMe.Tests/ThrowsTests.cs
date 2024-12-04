using System;
using MockMe.Tests.ExampleClasses;
using Xunit;

namespace MockMe.Tests
{
    public class ThrowsTests
    {
        [Fact]
        public void Throws_ShouldThrowException()
        {
            var mock = Mock.Me(default(Calculator));

            mock.Setup.Add(Arg.Any(), Arg.Any()).Throws(new InvalidCastException()); // throw random exception

            Calculator calc = mock;

            Assert.Throws<InvalidCastException>(() => calc.Add(1, 1));
        }

        [Fact]
        public void ReturnFollowedByThrows_ShouldReturnThenThrowException()
        {
            var mock = Mock.Me(default(Calculator));

            mock.Setup.Add(Arg.Any(), Arg.Any())
                .Returns(9, 99, 999)
                .Throws(new InvalidCastException()); // throw random exception

            Calculator calc = mock;

            Assert.Equal(9, calc.Add(1, 1));
            Assert.Equal(99, calc.Add(1, 1));
            Assert.Equal(999, calc.Add(1, 1));
            Assert.Throws<InvalidCastException>(() => calc.Add(1, 1));
        }

        [Fact]
        public void Throws_ShouldGiveAccurateAssertInfo()
        {
            var mock = Mock.Me(default(Calculator));

            mock.Setup.Add(Arg.Any(), Arg.Any()).Throws(new InvalidCastException()); // throw random exception

            Calculator calc = mock;

            Assert.Throws<InvalidCastException>(() => calc.Add(1, 1));

            mock.Assert.Add(1, 1).WasCalled();
            mock.Assert.Add(1, 2).WasNotCalled();
        }
    }
}
