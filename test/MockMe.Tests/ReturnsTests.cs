using MockMe.Tests.SampleClasses;
using Xunit;

namespace MockMe.Tests
{
    public class ReturnsTests
    {
        [Fact]
        public void CalculatorAdd_ShouldReturnConfiguredValue()
        {
            var calculatorMock = Mock.Me<SimpleCalculator>();

            calculatorMock.Setup.Add(1, 2).Returns(9999);

            Assert.Equal(9999, calculatorMock.MockedObject.Add(1, 2));
        }

        [Fact]
        public void CalculatorAdd_WhenCalledByANonMock_ShouldCallOriginalCode()
        {
            var calculatorMock = Mock.Me<SimpleCalculator>();

            calculatorMock.Setup.Add(1, 2).Returns(9999);

            Assert.Equal(9999, calculatorMock.MockedObject.Add(1, 2));

            Assert.Equal(3, new SimpleCalculator().Add(1, 2));
            Assert.Equal(10, new SimpleCalculator().Add(6, 4));
            Assert.Equal(2, new SimpleCalculator().Add(6, -4));
        }

        [Fact]
        public void CalculatorAdd_WhenReturnIsCalledManyTimes_ShouldReturnConfiguredValue()
        {
            var calculatorMock = Mock.Me<SimpleCalculator>();

            calculatorMock.Setup.Add(1, 2).Returns(9999);

            Assert.Equal(9999, calculatorMock.MockedObject.Add(1, 2));
            Assert.Equal(9999, calculatorMock.MockedObject.Add(1, 2));
            Assert.Equal(9999, calculatorMock.MockedObject.Add(1, 2));
            Assert.Equal(9999, calculatorMock.MockedObject.Add(1, 2));
            Assert.Equal(9999, calculatorMock.MockedObject.Add(1, 2));
        }

        [Fact]
        public void CalculatorAdd_WhenReturnIsCalledManyTimesWithDifferentValues_ShouldReturnConfiguredValue()
        {
            var calculatorMock = Mock.Me<SimpleCalculator>();

            calculatorMock.Setup.Add(1, 2).Returns(9, 99, 999, 9999, 99999);

            Assert.Equal(9, calculatorMock.MockedObject.Add(1, 2));
            Assert.Equal(99, calculatorMock.MockedObject.Add(1, 2));
            Assert.Equal(999, calculatorMock.MockedObject.Add(1, 2));
            Assert.Equal(9999, calculatorMock.MockedObject.Add(1, 2));
            Assert.Equal(99999, calculatorMock.MockedObject.Add(1, 2));
        }
    }
}
