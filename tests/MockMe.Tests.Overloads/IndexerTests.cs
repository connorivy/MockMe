using Xunit;

namespace MockMe.Tests.Overloads
{
    public class IndexerTests
    {
        [Fact]
        public void IndexerForInt_ReturnsAndCallbackShouldWork()
        {
            var mock = Mock.Me<SealedOverloadsClass>(default(SealedOverloadsClass));

            int numCalls = 0;
            mock.Setup[Arg.AnyOf<int>()].Callback(() => numCalls++).Returns("hello indexer");

            SealedOverloadsClass sealedOverloadsClass = mock;

            var val = sealedOverloadsClass[99];

            Assert.Equal("hello indexer", val);
            Assert.Equal(1, numCalls);
        }

        [Fact]
        public void IndexerForString_ReturnsAndCallbackShouldWork()
        {
            var mock = Mock.Me<SealedOverloadsClass>(default(SealedOverloadsClass));

            int numCalls = 0;
            mock.Setup[Arg.AnyOf<string>()].Callback(() => numCalls++).Returns(99);

            SealedOverloadsClass sealedOverloadsClass = mock;

            var val = sealedOverloadsClass["hello"];

            Assert.Equal(99, val);
            Assert.Equal(1, numCalls);
        }
    }
}
