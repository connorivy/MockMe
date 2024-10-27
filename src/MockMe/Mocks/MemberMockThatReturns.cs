namespace MockMe.Mocks;

public class MemberMock<TReturn>
    : VoidMemberNoArgsBaseMock<MemberMock<TReturn>>,
        IMemberMock<TReturn, MemberMock<TReturn>>
{
    public MemberMock()
        : this(new()) { }

    internal MemberMock(ActionCallbackManager<Action> callbackManager)
        : base(callbackManager)
    {
        this.returnManager = new(callbackManager);
    }

    private readonly ReturnManager<TReturn> returnManager;

    public MemberMock<TReturn> Return(TReturn returnThis, params TReturn[] thenReturnThese)
    {
        returnManager.Returns(returnThis, thenReturnThese);
        return this;
    }
}
