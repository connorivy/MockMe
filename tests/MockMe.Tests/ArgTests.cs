using MockMe.Asserters;
using MockMe.Exceptions;
using MockMe.Tests.ExampleClasses;
using Xunit;

namespace MockMe.Tests
{
    public class ArgTests
    {
        [Fact]
        public void ArgAny_ShouldCoverAnyArgument()
        {
            var calculatorMock = Mock.Me(default(Calculator));

            Calculator calculator = calculatorMock;

            calculator.Add(0, 0);
            calculator.Add(1, -1);
            calculator.Add(-234, 2134586);
            calculator.Add(-234, -89345373);
            calculator.Add(-234, 567);
            calculator.Add(int.MaxValue, int.MinValue);

            calculatorMock.Assert.Add(Arg.Any(), Arg.Any()).WasCalled(NumTimes.Exactly(6));
            Assert.ThrowsAny<MockMeException>(
                () => calculatorMock.Assert.Add(Arg.Any(), Arg.Any()).WasCalled(NumTimes.Exactly(5))
            );

            calculatorMock.Assert.Add(-234, Arg.Any()).WasCalled(NumTimes.Exactly(3));
            Assert.ThrowsAny<MockMeException>(
                () => calculatorMock.Assert.Add(-234, Arg.Any()).WasCalled(NumTimes.Exactly(5))
            );
        }
    }
}
