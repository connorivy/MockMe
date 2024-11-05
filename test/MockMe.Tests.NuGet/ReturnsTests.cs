namespace MockMe.Tests.NuGet;

public class ReturnsTests
{
    //[Fact]
    //public void CalculatorAdd_ShouldReturnConfiguredValue()
    //{
    //    var calculatorMock = Mock.Me<Calculator>();

    //    calculatorMock.Setup.Add(1, 2).Returns(9999);

    //    Assert.Equal(9999, calculatorMock.Value.Add(1, 2));
    //}
}

public class Calculator
{
    public int Add(int x, int y) => x + y;

    public double Multiply(double x, double y) => x * y;

    public CalculatorType CalculatorType { get; set; }

    public void DivideByZero(double numToDivide) =>
        throw new InvalidOperationException("Cannot divide by 0");

    public bool IsOn() => true;

    public void TurnOff() { }
}

public enum CalculatorType
{
    Standard,
    Scientific,
    Graphing
}
