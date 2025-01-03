using MockMe;
using MockMe.Tests.ExampleClasses;
using Xunit;

namespace MockMe_Tests_With_Path_Spaces;

public class PathSpacesTests
{
    /// <summary>
    /// Regression test for this issue building projects when the project path contained a space
    /// https://github.com/connorivy/MockMe/issues/20
    /// </summary>
    [Fact]
    public void AssemblyWithPathSpaces_ThatUsesGenericMethodMock_ShouldWork()
    {
        var mock = Mock.Me(default(ClassWithGenericMethods));

        mock.Setup.ThreeGenericTypes<string, int, double>(Arg.Any(), Arg.Any(), Arg.Any())
            .Returns("asdf");

        ClassWithGenericMethods obj = mock;

        var result = obj.ThreeGenericTypes("Hello", 1, 9.9);
        Assert.Equal("asdf", result);
    }
}
