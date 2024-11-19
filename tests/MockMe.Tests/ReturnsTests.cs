using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MockMe.Tests.ExampleClasses;
using MockMe.Tests.ExampleClasses.Interfaces;
using Xunit;

namespace MockMe.Tests
{
    public class ReturnsTests
    {
        [Fact]
        public void CalculatorAdd_ShouldReturnConfiguredValue()
        {
            var calculatorMock = Mock.Me<Calculator>();

            calculatorMock.Setup.Add(1, 2).Returns(9999);

            Assert.Equal(9999, calculatorMock.MockedObject.Add(1, 2));
        }

        [Fact]
        public void ICalculatorAdd_ShouldReturnConfiguredValue()
        {
            var calculatorMock = Mock.Me<ICalculator>();

            calculatorMock.Setup.Add(1, 2).Returns(9999);

            Assert.Equal(9999, calculatorMock.MockedObject.Add(1, 2));
        }

        [Fact]
        public void CalculatorAdd_WhenCalledByANonMock_ShouldCallOriginalCode()
        {
            var calculatorMock = Mock.Me<Calculator>();

            calculatorMock.Setup.Add(1, 2).Returns(9999);

            Assert.Equal(9999, calculatorMock.MockedObject.Add(1, 2));

            Assert.Equal(3, new Calculator().Add(1, 2));
            Assert.Equal(10, new Calculator().Add(6, 4));
            Assert.Equal(2, new Calculator().Add(6, -4));
        }

        [Fact]
        public void CalculatorAdd_WhenReturnIsCalledManyTimes_ShouldReturnConfiguredValue()
        {
            var calculatorMock = Mock.Me<Calculator>();

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
            var calculatorMock = Mock.Me<Calculator>();

            calculatorMock.Setup.Add(1, 2).Returns(9, 99, 999, 9999, 99999);

            Assert.Equal(9, calculatorMock.MockedObject.Add(1, 2));
            Assert.Equal(99, calculatorMock.MockedObject.Add(1, 2));
            Assert.Equal(999, calculatorMock.MockedObject.Add(1, 2));
            Assert.Equal(9999, calculatorMock.MockedObject.Add(1, 2));
            Assert.Equal(99999, calculatorMock.MockedObject.Add(1, 2));
        }

        [Fact]
        public void CalculatorType_ShouldReturnConfiguredPropertyValue()
        {
            var calculatorMock = Mock.Me<Calculator>();

            calculatorMock.Setup.get_CalculatorType().Returns(CalculatorType.Graphing);

            Assert.Equal(CalculatorType.Graphing, calculatorMock.MockedObject.CalculatorType);
        }

        [Fact]
        public void ICalculatorType_ShouldReturnConfiguredPropertyValue()
        {
            var calculatorMock = Mock.Me<ICalculator>();

            calculatorMock.Setup.get_CalculatorType().Returns(CalculatorType.Graphing);

            Assert.Equal(CalculatorType.Graphing, calculatorMock.MockedObject.CalculatorType);
        }

        [Fact]
        public async Task CalculatorAddAsync_ShouldReturnConfiguredPropertyValue()
        {
            var calculatorMock = Mock.Me<ComplexCalculator>((ComplexCalculator)default);

            calculatorMock.Setup.MultiplyAsync(1, 1).ReturnsAsync(9);

            var taskOf99 = Task.FromResult(99.0);
            calculatorMock.Setup.MultiplyAsync(5, 5).Returns(taskOf99);

            ComplexCalculator calc = (ComplexCalculator)calculatorMock;

            Assert.Equal(9, await calc.MultiplyAsync(1, 1));
            Assert.Equal(taskOf99, calc.MultiplyAsync(5, 5));
        }

        [Fact]
        public async Task CalculatorAddAsync_WhenReturnIsNotConfigured_ShouldReturnCompletedTask()
        {
            var calculatorMock = Mock.Me<ComplexCalculator>((ComplexCalculator)default);

            ComplexCalculator calc = calculatorMock;

            double result = await calc.MultiplyAsync(1, 1);

            Assert.Equal(0, result);
        }

        [Fact]
        public async Task CalculatorWaitAsync_WhenReturnIsNotConfigured_ShouldReturnCompletedTask()
        {
            var calculatorMock = Mock.Me<ComplexCalculator>((ComplexCalculator)default);

            ComplexCalculator calc = calculatorMock;

            await calc.WaitForOperationsToFinish();

            calculatorMock.Assert.WaitForOperationsToFinish().WasCalled();
        }
    }
}
