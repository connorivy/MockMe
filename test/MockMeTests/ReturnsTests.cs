using MockMe.Tests.SampleClasses;

namespace MockMe.Tests;

public class ReturnsTests
{
    [Fact]
    public void CalculatorAdd_ShouldReturnConfiguredValue()
    {
        var calculatorMock = Mock.Me<Calculator>();

        calculatorMock.Setup.Add(3, 5).Returns(9999);

        Assert.Equal(8, new Calculator().Add(3, 5));
        Assert.Equal(9999, calculatorMock.Value.Add(3, 5));
        calculatorMock.Assert.Add(3, 5).WasCalled();
    }
}
