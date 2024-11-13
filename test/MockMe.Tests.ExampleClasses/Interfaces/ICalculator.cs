namespace MockMe.Tests.ExampleClasses.Interfaces
{
    public interface ICalculator
    {
        //CalculatorType CalculatorType { get; set; }

        int Add(int x, int y);
        void DivideByZero(double numToDivide);
        bool IsOn();
        double Multiply(double x, double y);
        void TurnOff();
    }
}
