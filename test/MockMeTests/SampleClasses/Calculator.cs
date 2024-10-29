namespace MockMe.Tests.SampleClasses;

public class Calculator
{
    public int Add(int x, int y) => x + y;

    public double Multiply(double x, double y) => x * y;

    public int RandomNumber() => 17;

    public CalculatorType CalculatorType { get; set; }
}

public enum CalculatorType
{
    Standard,
    Scientific,
    Graphing
}
