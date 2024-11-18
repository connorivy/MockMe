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

            mock.Setup.ComputeHashForObjects<int>(Arg.Any).Returns(99);

            ComplexCalculator calc = (ComplexCalculator)mock;

            var result = calc.ComputeHashForObjects(new int[] { 1, 2, 3, 4, 5 });
        }
    }
}
