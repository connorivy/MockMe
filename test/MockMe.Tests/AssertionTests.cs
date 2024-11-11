using MockMe.Exceptions;
using MockMe.Tests.ExampleClasses;
using Xunit;

namespace MockMe.Tests
{
    public class AssertionTests
    {
        [Fact]
        public void TestWasCalled_ForMethodWithNoArgsAndNoReturnVal()
        {
            var calculatorMock = Mock.Me<Calculator>();

            Assert.ThrowsAny<MockMeException>(() => calculatorMock.Assert.TurnOff().WasCalled());

            Calculator calculator = (Calculator)calculatorMock;
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
            var calculatorMock = Mock.Me<Calculator>();

            Assert.ThrowsAny<MockMeException>(
                () =>
                    calculatorMock
                        .Assert.set_CalculatorType(ExampleClasses.CalculatorType.Scientific)
                        .WasCalled()
            );

            Calculator calculator = (Calculator)calculatorMock;

            calculator.CalculatorType = ExampleClasses.CalculatorType.Scientific;

            calculatorMock
                .Assert.set_CalculatorType(ExampleClasses.CalculatorType.Scientific)
                .WasCalled();

            Assert.ThrowsAny<MockMeException>(
                () =>
                    calculatorMock
                        .Assert.set_CalculatorType(ExampleClasses.CalculatorType.Graphing)
                        .WasCalled()
            );
        }
    }
}
