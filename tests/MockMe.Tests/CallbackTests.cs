using MockMe.Tests.ExampleClasses;
using Xunit;

namespace MockMe.Tests;

public class CallbackTests
{
    [Fact]
    public void Callback_IsCalled()
    {
        var calculatorMock = Mock.Me(default(Calculator));

        int numTimesCalled = 0;
        calculatorMock.Setup.Add(Arg.Any(), Arg.Any()).Callback(() => numTimesCalled++);

        Calculator calculator = calculatorMock;

        calculator.Add(1, 1);

        Assert.Equal(1, numTimesCalled);
    }

    [Fact]
    public void Callback_IsCalledWithCorrectArgs()
    {
        var calculatorMock = Mock.Me(default(Calculator));

        int multResult = 0;
        calculatorMock
            .Setup.Add(Arg.Any(), Arg.Any())
            .Callback(args => multResult = args.x * args.y);

        Calculator calculator = calculatorMock;

        calculator.Add(5, 5);

        Assert.Equal(25, multResult);
    }

    [Fact]
    public void MultipleCallbacks_AreCalledInOrder()
    {
        var calculatorMock = Mock.Me(default(Calculator));

        int? first = null;
        int? second = null;
        int? third = null;
        int? fourth = null;
        calculatorMock
            .Setup.Add(5, 5)
            .Callback(args =>
            {
                Assert.Null(first);
                Assert.Null(second);
                Assert.Null(third);
                Assert.Null(fourth);
                first = 0;
            })
            .Callback(args =>
            {
                Assert.NotNull(first);
                Assert.Null(second);
                Assert.Null(third);
                Assert.Null(fourth);
                second = 0;
            })
            .Callback(args =>
            {
                Assert.NotNull(first);
                Assert.NotNull(second);
                Assert.Null(third);
                Assert.Null(fourth);
                third = 0;
            })
            .Callback(args =>
            {
                Assert.NotNull(first);
                Assert.NotNull(second);
                Assert.NotNull(third);
                Assert.Null(fourth);
                fourth = 0;
            })
            .Callback(args =>
            {
                Assert.NotNull(first);
                Assert.NotNull(second);
                Assert.NotNull(third);
                Assert.NotNull(fourth);
            });

        Calculator calculator = calculatorMock;

        calculator.Add(5, 5);
    }

    [Fact]
    public void CallbacksBeforeAndAfterReturn_AreCalledInOrder()
    {
        var calculatorMock = Mock.Me(default(Calculator));

        bool callback1Called = false;
        bool returnCalled = false;
        bool callback2Called = false;
        calculatorMock
            .Setup.Add(5, 5)
            .Callback(() =>
            {
                Assert.False(returnCalled);
                callback1Called = true;
            })
            .Returns(() =>
            {
                returnCalled = true;
                return 99;
            })
            .Callback(() =>
            {
                Assert.True(returnCalled);
                callback2Called = true;
            });

        Calculator calculator = calculatorMock;

        var result = calculator.Add(5, 5);

        Assert.Equal(99, result);
        Assert.True(callback1Called);
        Assert.True(callback2Called);
    }
}
