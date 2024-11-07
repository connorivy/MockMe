using System;
using System.Collections.Generic;
using System.Linq;
using MockMe.Tests.NuGet;

namespace MockMe.Tests.ExampleClasses
{
    public class ComplexCalculator
    {
        public int Add(int x, int y) => x + y;

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

        private Dictionary<string, int> hello = new();

        public IReadOnlyDictionary<string, int> GetDict() => hello;

        public T AddUpAllOfThese2<T>(int hello, T[] values, double goodbye)
        {
            return values.First();
        }

        public T AddUpAllOfThese2_New<T>(int hello, T[] values, double goodbye)
        {
            if (TempCalcMockState<ComplexCalculator>.GetStore().TryGetValue(this, out object mock))
            {
                Type mockType = mock.GetType();
                var callTrackerPropInfo = mockType.GetProperty(
                    "CallTracker",
                    System.Reflection.BindingFlags.NonPublic
                        | System.Reflection.BindingFlags.Instance
                );
                var callTracker = callTrackerPropInfo.GetValue(mock);

                var methodPropInfo = callTracker
                    .GetType()
                    .GetMethod(
                        "AddUpAllOfThese2",
                        System.Reflection.BindingFlags.Public
                            | System.Reflection.BindingFlags.Instance
                    )
                    .MakeGenericMethod(typeof(T));
                return (T)
                    methodPropInfo.Invoke(callTracker, new object[] { hello, values, goodbye });
                //return ((dynamic)mock).CallTracker.AddUpAllOfThese2<T>(hello, values, goodbye);
            }
            return values.First();
        }
    }

    public enum CalculatorType
    {
        Standard,
        Scientific,
        Graphing,
    }
}
