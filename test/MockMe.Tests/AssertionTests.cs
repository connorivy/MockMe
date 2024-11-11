using MockMe.Exceptions;
using MockMe.Tests.SampleClasses;
using Xunit;

namespace MockMe.Tests
{
    public class AssertionTests
    {
        [Fact]
        public void TestWasCalled_ForMethodWithNoArgsAndNoReturnVal()
        {
            var calculatorMock = Mock.Me<SimpleCalculator>();

            Assert.ThrowsAny<MockMeException>(() => calculatorMock.Assert.TurnOff().WasCalled());

            SimpleCalculator calculator = (SimpleCalculator)calculatorMock;
            calculator.TurnOff();
            calculator.TurnOff();
            calculator.TurnOff();
            calculator.TurnOff();
            calculator.TurnOff();

            calculatorMock.Assert.TurnOff().WasCalled();
            calculatorMock.Assert.TurnOff().WasCalled(NumTimes.AtLeast, 5);
            calculatorMock.Assert.TurnOff().WasCalled(NumTimes.AtMost, 5);
            calculatorMock.Assert.TurnOff().WasCalled(NumTimes.Exactly, 5);
            Assert.ThrowsAny<MockMeException>(
                () => calculatorMock.Assert.TurnOff().WasCalled(NumTimes.Exactly, 4)
            );
            Assert.ThrowsAny<MockMeException>(
                () => calculatorMock.Assert.TurnOff().WasCalled(NumTimes.AtLeast, 6)
            );
            Assert.ThrowsAny<MockMeException>(
                () => calculatorMock.Assert.TurnOff().WasCalled(NumTimes.AtMost, 4)
            );
        }

        [Fact]
        public void SetCalculatorType_PropertySetterWasCalled()
        {
            var calculatorMock = Mock.Me<SimpleCalculator>();

            Assert.ThrowsAny<MockMeException>(
                () =>
                    calculatorMock
                        .Assert.set_CalculatorType(SampleClasses.CalculatorType.Scientific)
                        .WasCalled()
            );

            SimpleCalculator calculator = (SimpleCalculator)calculatorMock;

            calculator.CalculatorType = SampleClasses.CalculatorType.Scientific;

            calculatorMock
                .Assert.set_CalculatorType(SampleClasses.CalculatorType.Scientific)
                .WasCalled();

            Assert.ThrowsAny<MockMeException>(
                () =>
                    calculatorMock
                        .Assert.set_CalculatorType(SampleClasses.CalculatorType.Graphing)
                        .WasCalled()
            );
        }
    }
}
