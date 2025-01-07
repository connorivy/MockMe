using System;
using System.Threading.Tasks;
using Xunit;

namespace MockMe.Tests;

public class NewClass
{
    public string Name => throw new NotImplementedException();

    public double GetRandomNum() => throw new NotImplementedException();

    public Task ExpensiveOp(int num1, int num2) => throw new NotImplementedException();
}

public class TestWithFullyQualifiedNamespace
{
    [Fact]
    public void TestWithMockMe()
    {
#pragma warning disable IDE0002 // name can be simplified
        var mock = MockMe.Mock.Me(default(NewClass));
#pragma warning restore IDE0002

        mock.Setup.Name.Get().Returns("New Class Name");
        mock.Setup.GetRandomNum().Returns(99.0);

        NewClass newClass = mock;

        Assert.Equal("New Class Name", newClass.Name);
        Assert.Equal(99.0, newClass.GetRandomNum());
    }
}
