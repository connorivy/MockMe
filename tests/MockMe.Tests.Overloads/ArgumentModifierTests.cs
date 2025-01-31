using MockMe.Generated.MockMe.Tests.Overloads;
using Xunit;

namespace MockMe.Tests.Overloads;

public class ArgumentModifierTests
{
    [Fact]
    public void OutKeyword_WhenConfiguredInReturnCall_ShouldSetTheCorrectValue()
    {
        var mock = Mock.Me(default(AllOverloads));

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

    [Fact]
    public void OutKeyword_WhenConfiguredInCallbackCall_ShouldSetTheCorrectValue()
    {
        var mock = Mock.Me(default(AllOverloads));

        mock.Setup.OutArgument(out _).Callback(args => args.arg = 55).Returns(99);

        AllOverloads allOverloads = mock.MockedObject;
        var result = allOverloads.OutArgument(out var outArg);

        Assert.Equal(55, outArg);
        Assert.Equal(99, result);
        mock.Assert.OutArgument(out _).WasCalled();
    }

    /// <summary>
    /// Regression test for this issue
    /// https://github.com/connorivy/MockMe/issues/43
    /// </summary>
    [Fact]
    public void OutKeyword_ForReferenceType_ShouldSetTheCorrectValue()
    {
        var mock = Mock.Me(default(AllOverloads));

        mock.Setup.OutStringArgument(out _).Callback(args => args.arg = "hello").Returns(1);

        AllOverloads allOverloads = mock.MockedObject;
        _ = allOverloads.OutStringArgument(out var outArg);

        Assert.Equal("hello", outArg);
        mock.Assert.OutStringArgument(out _).WasCalled();
    }

    [Fact]
    public void ParametersNotPassedByReference_ShouldNotHaveASetter()
    {
        var mock = Mock.Me(default(AllOverloads));

        var outParamType = typeof(AllOverloadsMockSetup.OutArgument_OutInt32Collection);
        var regularParamType = typeof(AllOverloadsMockSetup.OutArgument_Int32Collection);

        var outParamGetter = outParamType.GetMethod("get_arg");
        var outParamSetter = outParamType.GetMethod("set_arg");
        Assert.NotNull(outParamGetter);
        Assert.NotNull(outParamSetter);

        var regularParamGetter = regularParamType.GetMethod("get_arg");
        var regularParamSetter = regularParamType.GetMethod("set_arg");
        Assert.NotNull(regularParamGetter);
        Assert.Null(regularParamSetter); // this type should NOT have a setter because that wouldn't make sense
    }
}
