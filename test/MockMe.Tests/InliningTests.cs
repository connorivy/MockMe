using System.Runtime.CompilerServices;
using MockMe.Tests.SampleClasses;
using Xunit;

namespace MockMe.Tests
{
    public class InliningTests
    {
        public class CalculatorManager
        {
            private readonly Calculator calculator;

            public CalculatorManager(Calculator calculator)
            {
                this.calculator = calculator;
            }

            public int AddNormal(int x, int y) => this.calculator.Add(x, y);

            [MethodImpl(MethodImplOptions.NoInlining)]
            public int AddNoInline(int x, int y) => this.calculator.Add(x, y);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public int AddAggressiveInline(int x, int y) => this.calculator.Add(x, y);
        }

        [Fact]
        public void CalculatorAdd_ShouldReturnConfiguredValue()
        {
            var calculatorMock = Mock.Me<Calculator>();
            calculatorMock.Setup.Add(3, 5).Returns(99, 999, 9999);
            CalculatorManager calculatorManager = new(calculatorMock);

            Assert.Equal(99, calculatorManager.AddNoInline(3, 5));
            Assert.Equal(999, calculatorManager.AddNormal(3, 5));
            Assert.Equal(9999, calculatorManager.AddAggressiveInline(3, 5));
        }

        //[Fact]
        //public void CalculatorRandomNumber_ShouldReturnConfiguredValue()
        //{
        //    var calculatorMock = Mock.Me<Calculator>();
        //    calculatorMock.Setup.Add(3, 5).Returns(9999, 9999, 9999);
        //    CalculatorManager calculatorManager = new(calculatorMock);

        //    Assert.Equal(9999, calculatorManager.AddNoInline(3, 5));
        //    Assert.Equal(9999, calculatorManager.AddNormal(3, 5));
        //    Assert.Equal(9999, calculatorManager.AddAggressiveInline(3, 5));
        //}
    }
}
