using System.Runtime.CompilerServices;
using MockMe.Tests.ExampleClasses;
using Xunit;

namespace MockMe.Tests
{
    public class PotentiallyInlinedMethods
    {
        public int AddNormal(int x, int y) => x + y;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public int AddNoInline(int x, int y) => x + y;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int AddAggressiveInline(int x, int y) => x + y;
    }

    public class InlineTestCaller
    {
        private readonly PotentiallyInlinedMethods potentiallyInlinedMethods;

        public InlineTestCaller(PotentiallyInlinedMethods potentiallyInlinedMethods)
        {
            this.potentiallyInlinedMethods = potentiallyInlinedMethods;
        }

        public int AddNormal(int x, int y) => this.potentiallyInlinedMethods.AddNormal(x, y);

        public int AddNoInline(int x, int y) => this.potentiallyInlinedMethods.AddNoInline(x, y);

        public int AddAggressiveInline(int x, int y) =>
            this.potentiallyInlinedMethods.AddAggressiveInline(x, y);
    }

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

        [Fact]
        public void AddMethod_ShouldReturnConfiguredValue()
        {
            var inlineMock = Mock.Me<PotentiallyInlinedMethods>();

            inlineMock.Setup.AddNoInline(Arg.Any, Arg.Any).Returns(99);
            inlineMock.Setup.AddNormal(Arg.Any, Arg.Any).Returns(999);
            inlineMock.Setup.AddAggressiveInline(Arg.Any, Arg.Any).Returns(9999);

            InlineTestCaller caller = new(inlineMock);

            Assert.Equal(99, caller.AddNoInline(1, 1));
            Assert.Equal(999, caller.AddNormal(1, 1));
            Assert.Equal(9999, caller.AddAggressiveInline(1, 1));
        }
    }
}
