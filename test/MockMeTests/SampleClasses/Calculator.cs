namespace MockMe.Tests.SampleClasses;

public class Calculator
{
    public int Add(int x, int y) => 99;

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

public class Calculator2
{
    public int Add(int x, int y) => 99;

    public double Multiply(double x, double y) => x * y;

    public CalculatorType CalculatorType { get; set; }

    public void DivideByZero(double numToDivide) =>
        throw new InvalidOperationException("Cannot divide by 0");

    public bool IsOn() => true;

    public void TurnOff() { }

    public T AddUpAllOfThese<T>(T[] values)
    {
        return values.First();
    }

    public TReplacement AddUpAllOfThese2<TReplacement>(
        int hello,
        TReplacement[] values,
        double goodbye
    )
    {
        return (TReplacement)(object)99.0;
    }
}
