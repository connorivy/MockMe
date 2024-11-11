using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//using MockMe.Tests.NuGet;

namespace MockMe.Tests.ExampleClasses
{
    public class ComplexCalculator : Calculator
    {
        public int ComputeHashForObjects<T>(T[] values)
        {
            const int seed = 487;
            const int modifier = 31;
            return values.Aggregate(
                seed,
                (current, type) => (current * modifier) + type.GetHashCode()
            );
        }

        public T AddUpAllOfThese2<T>(int hello, T[] values, double goodbye)
        {
            return values.First();
        }

        public T GetItemWithNearestHashCodeToProductOfTwoNums<T>(int num1, double num2, T[] values)
        {
            return values.First();
        }

        public Task<double> MultiplyAsync(double num1, double num2)
        {
            return Task.FromResult(num1 * num2);
        }
    }
}
