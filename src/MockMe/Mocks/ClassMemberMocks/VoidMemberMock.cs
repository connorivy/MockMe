namespace MockMe.Mocks.ClassMemberMocks;

public abstract class VoidMemberMockBase<TSelf, TCallback, TCallbackManager>
    :
    //IMemberMockWithArgs<TSelf, TCallback>,
    IMockCallbackRetriever<TCallback>
    where TSelf : VoidMemberMockBase<TSelf, TCallback, TCallbackManager>
    where TCallbackManager : CallbackManagerBase<TCallback>
{
    internal TCallbackManager CallbackManager { get; }

    internal VoidMemberMockBase(TCallbackManager callbackManager)
    {
        this.CallbackManager = callbackManager;
    }

    public TSelf Callback(TCallback callback)
    {
        this.CallbackManager.AddCallback(callback);
        return (TSelf)this;
    }

    IEnumerable<TCallback> IMockCallbackRetriever<TCallback>.GetCallbacksRegisteredAfterReturnCall() =>
        this.CallbackManager.GetCallbacksRegisteredAfterReturnCall();

    IEnumerable<TCallback> IMockCallbackRetriever<TCallback>.GetCallbacksRegisteredBeforeReturnCall() =>
        this.CallbackManager.GetCallbacksRegisteredBeforeReturnCall();
}

public interface IVoidMemberMockBase<out TSelf, TCallback, out TCallbackManager>
    : IMockCallbackRetriever<TCallback>
    where TSelf : IVoidMemberMockBase<TSelf, TCallback, TCallbackManager>
    where TCallbackManager : CallbackManagerBase<TCallback>
{
    internal TCallbackManager CallbackManager { get; }

    public TSelf Callback(TCallback callback)
    {
        this.CallbackManager.AddCallback(callback);
        return (TSelf)this;
    }

    IEnumerable<TCallback> IMockCallbackRetriever<TCallback>.GetCallbacksRegisteredAfterReturnCall() =>
        this.CallbackManager.GetCallbacksRegisteredAfterReturnCall();

    IEnumerable<TCallback> IMockCallbackRetriever<TCallback>.GetCallbacksRegisteredBeforeReturnCall() =>
        this.CallbackManager.GetCallbacksRegisteredBeforeReturnCall();
}

public interface IVoidMemberMock<out TSelf> : IVoidMemberMockBase<TSelf, Action, CallbackManager>
    where TSelf : IVoidMemberMock<TSelf> { }

public interface IVoidMemberMock<out TSelf, TArgCollection>
    : IVoidMemberMockBase<TSelf, Action<TArgCollection>, CallbackManager<TArgCollection>>
    where TSelf : IVoidMemberMock<TSelf, TArgCollection>
{
    public TSelf Callback(Action callback)
    {
        this.CallbackManager.AddCallback(callback);
        return (TSelf)this;
    }
}

public class VoidMemberMockBase<TSelf> : IMockCallbackRetriever<Action>
    where TSelf : VoidMemberMockBase<TSelf>
{
    public VoidMemberMockBase()
        : this(new CallbackManager()) { }

    internal VoidMemberMockBase(CallbackManager callbackManager)
    {
        this.CallbackManager = callbackManager;
    }

    internal CallbackManager CallbackManager { get; }

    public TSelf Callback(Action callback)
    {
        this.CallbackManager.AddCallback(callback);
        return (TSelf)this;
    }

    IEnumerable<Action> IMockCallbackRetriever<Action>.GetCallbacksRegisteredAfterReturnCall() =>
        this.CallbackManager.GetCallbacksRegisteredAfterReturnCall();

    IEnumerable<Action> IMockCallbackRetriever<Action>.GetCallbacksRegisteredBeforeReturnCall() =>
        this.CallbackManager.GetCallbacksRegisteredBeforeReturnCall();
}

public class VoidMemberMock : VoidMemberMockBase<VoidMemberMock> { }

public class VoidMemberMockBase<TSelf, TArgCollection>
    : IMockCallbackRetriever<Action<TArgCollection>>
    where TSelf : VoidMemberMockBase<TSelf, TArgCollection>
{
    public VoidMemberMockBase()
        : this(new CallbackManager<TArgCollection>()) { }

    internal VoidMemberMockBase(CallbackManager<TArgCollection> callbackManager)
    {
        this.CallbackManager = callbackManager;
    }

    internal CallbackManager<TArgCollection> CallbackManager { get; }

    public TSelf Callback(Action callback)
    {
        this.CallbackManager.AddCallback(callback);
        return (TSelf)this;
    }

    public TSelf Callback(Action<TArgCollection> callback)
    {
        this.CallbackManager.AddCallback(callback);
        return (TSelf)this;
    }

    IEnumerable<Action<TArgCollection>> IMockCallbackRetriever<
        Action<TArgCollection>
    >.GetCallbacksRegisteredAfterReturnCall() =>
        this.CallbackManager.GetCallbacksRegisteredAfterReturnCall();

    IEnumerable<Action<TArgCollection>> IMockCallbackRetriever<
        Action<TArgCollection>
    >.GetCallbacksRegisteredBeforeReturnCall() =>
        this.CallbackManager.GetCallbacksRegisteredBeforeReturnCall();
}

public class VoidMemberMock<TArgCollection>
    : VoidMemberMockBase<VoidMemberMock<TArgCollection>, TArgCollection> { }
