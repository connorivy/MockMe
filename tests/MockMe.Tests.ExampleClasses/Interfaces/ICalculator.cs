namespace MockMe.Tests.ExampleClasses.Interfaces
{
    public interface IAddition
    {
        int Add(int x, int y);
    }

    public interface ISubtraction
    {
        int Subtract(int a, int b);
    }

    public interface IMultiplication
    {
        double Multiply(double x, double y);
    }

    public interface IDivision
    {
        int Divide(int a, int b);
    }

    public interface ICalculator : IAddition, ISubtraction, IMultiplication, IDivision
    {
        CalculatorType CalculatorType { get; set; }
        void DivideByZero(double numToDivide);
        bool IsOn();
        void TurnOff();
    }
}
