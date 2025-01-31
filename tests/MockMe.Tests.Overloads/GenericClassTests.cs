using System;
using Xunit;

namespace MockMe.Tests.Overloads
{
    public class GenericClassTests
    {
        [Fact]
        public void MultipleValueTypeOverloads_ShouldReturnConfiguredValues()
        {
            var mock = Mock.Me(default(GenericType<int, double, char>));

            mock.Setup.UsesSingleClassScopedGeneric(Arg.Any()).Returns(99);
            var mockedObject = mock.MockedObject;
            Assert.Equal(99, mockedObject.UsesSingleClassScopedGeneric(0));

            mock.Setup.UsesMultipleClassScopedGeneric(1, 1.0, '1').Returns(1);
            Assert.Equal(1, mockedObject.UsesMultipleClassScopedGeneric(1, 1.0, '1'));

            var randomDateTime = DateTime.Now;
            mock.Setup.UsesMultipleMethodScopedGeneric<float, char, DateTime>(
                    50,
                    'a',
                    randomDateTime
                )
                .Returns(50);

            Assert.Equal(
                50,
                mockedObject.UsesMultipleMethodScopedGeneric((float)50, 'a', randomDateTime)
            );

            mock.Setup.MixesClassAndMethodScopedGeneric<DateTime>(1, Arg.Any(), '1')
                .Returns(randomDateTime);

            Assert.Equal(
                randomDateTime,
                mockedObject.MixesClassAndMethodScopedGeneric(1, DateTime.Now, '1')
            );
        }
    }
}
