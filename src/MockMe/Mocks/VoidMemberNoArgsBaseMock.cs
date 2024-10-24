using MockMe.Extensions;

namespace MockMe.Mocks;

public abstract class VoidMemberNoArgsBaseMock<TSelf> : IVoidMemberMock<TSelf>
    where TSelf : VoidMemberNoArgsBaseMock<TSelf>
{
    private readonly ActionCallbackManager<Action> callbackManager = new();

    public TSelf Callback(Action callback)
    {
        callbackManager.AddCallback(callback);
        return (TSelf)this;
    }
}

public class VoidMemberMock : VoidMemberNoArgsBaseMock<VoidMemberMock> { }

public abstract class VoidMemberMockWithArgsBase<TSelf, TCallback>(
    Func<Action, TCallback> toCallback
) : IMemberMockWithArgs<TSelf, TCallback>
    where TSelf : VoidMemberMockWithArgsBase<TSelf, TCallback>
{
    private readonly CallbackManager<TCallback> callbackManager = new(toCallback);

    public TSelf Callback(TCallback callback)
    {
        callbackManager.AddCallback(callback);
        return (TSelf)this;
    }

    public TSelf Callback(Action callback)
    {
        callbackManager.AddCallback(callback);
        return (TSelf)this;
    }
}

public class VoidMemberMock<TArg1>
    : VoidMemberMockWithArgsBase<VoidMemberMock<TArg1>, Action<TArg1>>
{
    public VoidMemberMock()
        : base(ActionExtensions.CallbackFunc<TArg1>()) { }
}

public class VoidMemberMock<TArg1, TArg2>
    : VoidMemberMockWithArgsBase<VoidMemberMock<TArg1, TArg2>, Action<TArg1, TArg2>>
{
    public VoidMemberMock()
        : base(ActionExtensions.CallbackFunc<TArg1, TArg2>()) { }
}

public class VoidMemberMock<TArg1, TArg2, TArg3>
    : VoidMemberMockWithArgsBase<VoidMemberMock<TArg1, TArg2, TArg3>, Action<TArg1, TArg2, TArg3>>
{
    public VoidMemberMock()
        : base(ActionExtensions.CallbackFunc<TArg1, TArg2, TArg3>()) { }
}

public class VoidMemberMock<TArg1, TArg2, TArg3, TArg4>
    : VoidMemberMockWithArgsBase<
        VoidMemberMock<TArg1, TArg2, TArg3, TArg4>,
        Action<TArg1, TArg2, TArg3, TArg4>
    >
{
    public VoidMemberMock()
        : base(ActionExtensions.CallbackFunc<TArg1, TArg2, TArg3, TArg4>()) { }
}

public class VoidMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5>
    : VoidMemberMockWithArgsBase<
        VoidMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5>,
        Action<TArg1, TArg2, TArg3, TArg4, TArg5>
    >
{
    public VoidMemberMock()
        : base(ActionExtensions.CallbackFunc<TArg1, TArg2, TArg3, TArg4, TArg5>()) { }
}
