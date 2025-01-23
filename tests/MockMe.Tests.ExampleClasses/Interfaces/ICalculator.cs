namespace MockMe.Tests.ExampleClasses.Interfaces
{
    public interface ISymbolVisitor
    {
        void VisitAddition(IAddition addition);
        void VisitSubtraction(ISubtraction subtraction);
        void VisitMultiplication(IMultiplication multiplication);
        void VisitDivision(IDivision division);
    }

    public interface ISymbolVisitor<out T>
    {
        T VisitAddition(IAddition addition);
        T VisitSubtraction(ISubtraction subtraction);
        T VisitMultiplication(IMultiplication multiplication);
        T VisitDivision(IDivision division);
    }

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

        void Accept(ISymbolVisitor visitor);
        T Accept<T>(ISymbolVisitor<T> visitor);
    }
}
