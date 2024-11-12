using System;

namespace MockMe.Tests.ExampleClasses
{
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
        Graphing,
    }
}
