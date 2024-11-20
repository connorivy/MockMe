//using MockMe.Tests.SampleClasses;
using System.Collections.Generic;
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

        [Fact]
        public void GenericClass_ShouldReturnConfiguredValue()
        {
            //var mock = Mock.Me<List<int>>();

            var stringMock = Mock.Me<List<string>>();

            //mock.Setup.Contains(5).Returns(true);

            stringMock.Setup.Contains("hello").Returns(true);

            //List<int> list = mock;

            //list.set_Item(5)

            //var result = list.Contains(5);

            var x = stringMock.MockedObject.Contains("hello");
        }
    }
}
