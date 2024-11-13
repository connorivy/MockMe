namespace MockMe.Mocks.ClassMemberMocks;

public abstract class VoidMemberNoArgsBaseMock<TSelf>
    : IVoidMemberMock<TSelf>,
        IMockCallbackRetriever<Action>
    where TSelf : VoidMemberNoArgsBaseMock<TSelf>
{
    private readonly ActionCallbackManager<Action> callbackManager;

    internal VoidMemberNoArgsBaseMock(ActionCallbackManager<Action> callbackManager)
    {
        this.callbackManager = callbackManager;
    }

    public TSelf Callback(Action callback)
    {
        this.callbackManager.AddCallback(callback);
        return (TSelf)this;
    }

    IEnumerable<Action> IMockCallbackRetriever<Action>.GetCallbacksRegisteredAfterReturnCall() =>
        this.callbackManager.GetCallbacksRegisteredAfterReturnCall();

    IEnumerable<Action> IMockCallbackRetriever<Action>.GetCallbacksRegisteredBeforeReturnCall() =>
        this.callbackManager.GetCallbacksRegisteredBeforeReturnCall();
}
