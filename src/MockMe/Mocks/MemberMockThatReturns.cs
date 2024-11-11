namespace MockMe.Mocks;

public class MemberMock<TReturn>
    : VoidMemberNoArgsBaseMock<MemberMock<TReturn>>,
        IMemberMock<TReturn, MemberMock<TReturn>>,
        IMockCallbackAndReturnCallRetriever<Action, TReturn>
{
    public MemberMock()
        : this(new()) { }

    internal MemberMock(ActionCallbackManager<Action> callbackManager)
        : base(callbackManager)
    {
        this.returnManager = new(callbackManager);
    }

    private readonly ReturnManager<TReturn> returnManager;

    public MemberMock<TReturn> Returns(TReturn returnThis, params TReturn[] thenReturnThese)
    {
        this.returnManager.Returns(returnThis, thenReturnThese);
        return this;
    }

    TReturn? IMockReturnCallRetriever<TReturn>.GetReturnValue() =>
        this.returnManager.GetReturnCall();
}
