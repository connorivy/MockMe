using Xunit;

namespace MockMe.Tests.MethodOverloads
{
    public class MethodOverloadsTests
    {
        [Fact]
        public void NoReturnNoParams_ReturnsAndAssertShouldWork()
        {
            var mock = Mock.Me<SealedOverloadsClass>(null);
        }
    }
}
