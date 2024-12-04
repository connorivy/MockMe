using MockMe.Asserters;
using MockMe.Exceptions;
using MockMe.Tests.ExampleClasses;
using MockMe.Tests.ExampleClasses.Interfaces;
using Xunit;

namespace MockMe.Tests
{
    public class AssertionTests
    {
        [Fact]
        public void TestWasCalled_ForMethodWithNoArgsAndNoReturnVal()
        {
            var calculatorMock = Mock.Me(default(Calculator));

            Assert.ThrowsAny<MockMeException>(() => calculatorMock.Assert.TurnOff().WasCalled());

            Calculator calculator = (Calculator)calculatorMock;
            calculator.TurnOff();
            calculator.TurnOff();
            calculator.TurnOff();
            calculator.TurnOff();
            calculator.TurnOff();

            calculatorMock.Assert.TurnOff().WasCalled();
            calculatorMock.Assert.TurnOff().WasCalled(NumTimes.AtLeast(5));
            calculatorMock.Assert.TurnOff().WasCalled(NumTimes.AtMost(5));
            calculatorMock.Assert.TurnOff().WasCalled(NumTimes.Exactly(5));
            calculatorMock.Assert.TurnOff().WasCalled(NumTimes.Between(4, 5));
            calculatorMock.Assert.TurnOff().WasCalled(NumTimes.Between(5, 6));
            Assert.ThrowsAny<MockMeException>(
                () => calculatorMock.Assert.TurnOff().WasCalled(NumTimes.Exactly(4))
            );
            Assert.ThrowsAny<MockMeException>(
                () => calculatorMock.Assert.TurnOff().WasCalled(NumTimes.AtLeast(6))
            );
            Assert.ThrowsAny<MockMeException>(
                () => calculatorMock.Assert.TurnOff().WasCalled(NumTimes.AtMost(4))
            );
            Assert.ThrowsAny<MockMeException>(
                () => calculatorMock.Assert.TurnOff().WasCalled(NumTimes.Between(1, 4))
            );
            Assert.ThrowsAny<MockMeException>(
                () => calculatorMock.Assert.TurnOff().WasCalled(NumTimes.Between(6, 10))
            );
        }

        [Fact]
        public void SetCalculatorType_PropertySetterWasCalled()
        {
            var calculatorMock = Mock.Me(default(Calculator));

            Assert.ThrowsAny<MockMeException>(
                () =>
                    calculatorMock.Assert.CalculatorType.Set(CalculatorType.Scientific).WasCalled()
            );

            Calculator calculator = calculatorMock;

            calculator.CalculatorType = CalculatorType.Scientific;

            calculatorMock.Assert.CalculatorType.Set(CalculatorType.Scientific).WasCalled();

            Assert.ThrowsAny<MockMeException>(
                () => calculatorMock.Assert.CalculatorType.Set(CalculatorType.Graphing).WasCalled()
            );
        }

        [Fact]
        public void SetICalculatorType_PropertySetterWasCalled()
        {
            var calculatorMock = Mock.Me(default(ICalculator));

            Assert.ThrowsAny<MockMeException>(
                () =>
                    calculatorMock.Assert.CalculatorType.Set(CalculatorType.Scientific).WasCalled()
            );

            ICalculator calculator = calculatorMock.MockedObject;

            calculator.CalculatorType = CalculatorType.Scientific;

            calculatorMock.Assert.CalculatorType.Set(CalculatorType.Scientific).WasCalled();

            Assert.ThrowsAny<MockMeException>(
                () => calculatorMock.Assert.CalculatorType.Set(CalculatorType.Graphing).WasCalled()
            );
        }
    }
}
