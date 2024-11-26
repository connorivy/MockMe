using MockMe.Exceptions;
using Xunit;

namespace MockMe.Tests.Overloads
{
    public class IndexerTests
    {
        [Fact]
        public void IndexerGetForInt_ReturnsAndCallbackShouldWork()
        {
            var mock = Mock.Me<OverloadsClass>(default(OverloadsClass));

            int numCalls = 0;
            mock.Setup[Arg.Any<int>()].Get().Callback(() => numCalls++).Returns("hello indexer");

            OverloadsClass OverloadsClass = mock.MockedObject;

            var val = OverloadsClass[99];

            Assert.Equal("hello indexer", val);
            Assert.Equal(1, numCalls);
            mock.Assert[Arg.Any<int>()].Get().WasCalled();
            Assert.ThrowsAny<MockMeException>(
                () => mock.Assert[new Arg<int>(98)].Get().WasCalled()
            );
        }

        [Fact]
        public void IndexerSetForInt_ReturnsAndCallbackShouldWork()
        {
            var mock = Mock.Me<OverloadsClass>(default(OverloadsClass));

            int numCalls = 0;
            mock.Setup[Arg.Any<int>()].Set(Arg.Any()).Callback(() => numCalls++);

            OverloadsClass OverloadsClass = mock.MockedObject;

            OverloadsClass[99] = "hello indexer";

            Assert.Equal(1, numCalls);
            mock.Assert[Arg.Any<int>()].Set(Arg.Any()).WasCalled();
            mock.Assert[new Arg<int>(99)].Set("hello indexer").WasCalled();
            Assert.ThrowsAny<MockMeException>(
                () => mock.Assert[new Arg<int>(98)].Set(Arg.Any()).WasCalled()
            );
        }

        [Fact]
        public void IndexerGetForString_ReturnsAndCallbackShouldWork()
        {
            var mock = Mock.Me<OverloadsClass>(default(OverloadsClass));

            int numCalls = 0;
            mock.Setup[Arg.Any<string>()].Get().Callback(() => numCalls++).Returns(99);

            OverloadsClass OverloadsClass = mock.MockedObject;

            var val = OverloadsClass["hello"];

            Assert.Equal(99, val);
            Assert.Equal(1, numCalls);
            mock.Assert[Arg.Any<string>()].Get().WasCalled();
            Assert.ThrowsAny<MockMeException>(() => mock.Assert["goodbye"].Get().WasCalled());
        }

        [Fact]
        public void SetOnlyIndexer_AssertAndCallbackShouldWork()
        {
            var mock = Mock.Me<OverloadsClass>(default(OverloadsClass));

            int numCalls = 0;
            mock.Setup[Arg.Any<double>()].Set(Arg.Any()).Callback(() => numCalls++);

            OverloadsClass OverloadsClass = mock.MockedObject;

            OverloadsClass[9.9] = 9.9;

            Assert.Equal(1, numCalls);
            mock.Assert[Arg.Any<double>()].Set(Arg.Any()).WasCalled();
            mock.Assert[9.9].Set(9.9).WasCalled();

            Assert.ThrowsAny<MockMeException>(() => mock.Assert[9.8].Set(Arg.Any()).WasCalled());
        }
    }
}
