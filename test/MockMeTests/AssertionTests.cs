using MockMe.Exceptions;
using MockMe.Tests.SampleClasses;

namespace MockMe.Tests;

public class AssertionTests
{
    [Fact]
    public void TestWasCalled_ForMethodWithNoArgsAndNoReturnVal()
    {
        var calculatorMock = Mock.Me<Calculator>();

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
}