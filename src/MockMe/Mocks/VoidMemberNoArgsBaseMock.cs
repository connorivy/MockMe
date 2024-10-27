using MockMe.Extensions;

namespace MockMe.Mocks;

public abstract class VoidMemberNoArgsBaseMock<TSelf> : IVoidMemberMock<TSelf>
    where TSelf : VoidMemberNoArgsBaseMock<TSelf>
{
    private readonly ActionCallbackManager<Action> callbackManager;

    internal VoidMemberNoArgsBaseMock(ActionCallbackManager<Action> callbackManager)
    {
        this.callbackManager = callbackManager;
    }

    public TSelf Callback(Action callback)
    {
        callbackManager.AddCallback(callback);
        return (TSelf)this;
    }
}

public class VoidMemberMock : VoidMemberNoArgsBaseMock<VoidMemberMock>
{
    public VoidMemberMock()
        : this(new()) { }

    internal VoidMemberMock(ActionCallbackManager<Action> callbackManager)
        : base(callbackManager) { }
}

public abstract class VoidMemberMockWithArgsBase<TSelf, TCallback>
    : IMemberMockWithArgs<TSelf, TCallback>,
        IMockCallbackRetriever<TCallback>
    where TSelf : VoidMemberMockWithArgsBase<TSelf, TCallback>
{
    private readonly CallbackManager<TCallback> callbackManager;

    internal VoidMemberMockWithArgsBase(CallbackManager<TCallback> callbackManager)
    {
        this.callbackManager = callbackManager;
    }

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

    IEnumerable<TCallback> IMockCallbackRetriever<TCallback>.GetCallbacksRegisteredAfterReturnCall() =>
        callbackManager.GetCallbacksRegisteredAfterReturnCall();

    IEnumerable<TCallback> IMockCallbackRetriever<TCallback>.GetCallbacksRegisteredBeforeReturnCall() =>
        callbackManager.GetCallbacksRegisteredBeforeReturnCall();
}

public class VoidMemberMock<TArg1>
    : VoidMemberMockWithArgsBase<VoidMemberMock<TArg1>, Action<TArg1>>
{
    public VoidMemberMock()
        : base(new(ActionExtensions.CallbackFunc<TArg1>())) { }
}

public class VoidMemberMock<TArg1, TArg2>
    : VoidMemberMockWithArgsBase<VoidMemberMock<TArg1, TArg2>, Action<TArg1, TArg2>>
{
    public VoidMemberMock()
        : base(new(ActionExtensions.CallbackFunc<TArg1, TArg2>())) { }
}

public class VoidMemberMock<TArg1, TArg2, TArg3>
    : VoidMemberMockWithArgsBase<VoidMemberMock<TArg1, TArg2, TArg3>, Action<TArg1, TArg2, TArg3>>
{
    public VoidMemberMock()
        : base(new(ActionExtensions.CallbackFunc<TArg1, TArg2, TArg3>())) { }
}

public class VoidMemberMock<TArg1, TArg2, TArg3, TArg4>
    : VoidMemberMockWithArgsBase<
        VoidMemberMock<TArg1, TArg2, TArg3, TArg4>,
        Action<TArg1, TArg2, TArg3, TArg4>
    >
{
    public VoidMemberMock()
        : base(new(ActionExtensions.CallbackFunc<TArg1, TArg2, TArg3, TArg4>())) { }
}

public class VoidMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5>
    : VoidMemberMockWithArgsBase<
        VoidMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5>,
        Action<TArg1, TArg2, TArg3, TArg4, TArg5>
    >
{
    public VoidMemberMock()
        : base(new(ActionExtensions.CallbackFunc<TArg1, TArg2, TArg3, TArg4, TArg5>())) { }
}
