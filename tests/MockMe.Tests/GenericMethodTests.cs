//using MockMe.Tests.SampleClasses;
using MockMe.Tests.ExampleClasses;
using Xunit;

namespace MockMe.Tests
{
    public class GenericMethodTests
    {
        [Fact]
        public void GenericMethod_ShouldReturnConfiguredValue()
        {
            var mock = Mock.Me<ComplexCalculator>();

            mock.Setup.ComputeHashForObjects<int>(Arg.Any()).Returns(99);

            ComplexCalculator calc = (ComplexCalculator)mock;

            var result = calc.ComputeHashForObjects(new int[] { 1, 2, 3, 4, 5 });
            Assert.Equal(99, result);
        }

        [Fact]
        public void GenericMethodWithMultipleGenericArgs_ShouldReturnConfiguredValue()
        {
            var mock = Mock.Me<ClassWithGenericMethods>();

            mock.Setup.ThreeGenericTypes<string, int, double>(Arg.Any(), Arg.Any(), Arg.Any())
                .Returns("asdf");

            ClassWithGenericMethods obj = mock;

            var result = obj.ThreeGenericTypes("Hello", 1, 9.9);
            Assert.Equal("asdf", result);
        }
    }
}
