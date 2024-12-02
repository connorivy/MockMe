using Xunit;

namespace MockMe.Tests.Overloads;

public class ArgumentModifierTests
{
    [Fact]
    public void OutKeyword()
    {
        var mock = Mock.Me<AllOverloads>(default(AllOverloads));

        mock.Setup.OutArgument(out _)
            .Returns(args =>
            {
                args.arg = 55;
                return 99;
            });

        AllOverloads allOverloads = mock.MockedObject;
        var result = allOverloads.OutArgument(out var outArg);

        Assert.Equal(55, outArg);
        Assert.Equal(99, result);
        mock.Assert.OutArgument(out _).WasCalled();
    }
}
