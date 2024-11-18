using MockMe.Exceptions;
using Xunit;

namespace MockMe.Tests.MethodOverloads
{
    public class MethodOverloadsTests
    {
        [Fact]
        public void NoReturnNoParams_ReturnsAndAssertShouldWork()
        {
            var mock = Mock.Me<SealedOverloadsClass>(null);

            int numCalls = 0;
            mock.Setup.VoidReturn().Callback(() => numCalls++);

            SealedOverloadsClass sealedClass = mock;

            Assert.ThrowsAny<MockMeException>(() => mock.Assert.VoidReturn().WasCalled());

            sealedClass.VoidReturn();

            Assert.Equal(1, numCalls);
            mock.Assert.VoidReturn().WasCalled();
        }
    }
}
