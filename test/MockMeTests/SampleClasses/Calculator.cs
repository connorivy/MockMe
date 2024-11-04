using MockMe.Abstractions;
using MockMe.Tests.SampleClasses;

[assembly: GenericMethodDefinition(
    "MockMe.SampleMocks",
    "MockMe.SampleMocks.CalculatorSample.Calculator",
    "AddUpAllOfThese2",
    "MockMe.Tests",
    "MockMe.Tests.SampleClasses.Calculator2",
    "AddUpAllOfThese2"
)]

namespace MockMe.Tests.SampleClasses
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

        public T AddUpAllOfThese2<T>(int hello, T[] values, double goodbye)
        {
            return (T)(object)99.0;
        }
    }
}

namespace MockMe
{
    public static partial class Mock
    {
        public static List<MockReplacementInfo> GenericTypes() =>
            new()
            {
                new(
                    typeToReplace: new GenericMethodInfo(
                        assemblyName: "MockMe.SampleMocks",
                        typeFullName: "MockMe.SampleMocks.CalculatorSample.Calculator",
                        methodName: "AddUpAllOfThese2"
                    ),
                    sourceType: new GenericMethodInfo(
                        assemblyName: typeof(Calculator2).Assembly.FullName,
                        typeFullName: typeof(Calculator2).FullName,
                        methodName: nameof(Calculator2.AddUpAllOfThese2)
                    )
                )
            };
    }
}
