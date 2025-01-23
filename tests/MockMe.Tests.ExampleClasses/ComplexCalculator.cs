using System;
using System.Linq;
using System.Threading.Tasks;
using MockMe.Tests.ExampleClasses.Interfaces;

namespace MockMe.Tests.ExampleClasses
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

    public class ComplexCalculator
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

        public Task WaitForOperationsToFinish()
        {
            throw new NotImplementedException();
        }

        public void Accept(ISymbolVisitor visitor) => throw new NotImplementedException();

        public T Accept<T>(ISymbolVisitor<T> visitor) => throw new NotImplementedException();
    }
}
