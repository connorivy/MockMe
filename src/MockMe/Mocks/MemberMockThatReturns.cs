namespace MockMe.Mocks;

public class MemberMock<TReturn>
    : VoidMemberNoArgsBaseMock<MemberMock<TReturn>>,
        IMemberMock<TReturn, MemberMock<TReturn>>
{
    private readonly ReturnManager<TReturn> returnManager = new();

    public MemberMock<TReturn> Return(TReturn returnThis, params TReturn[] thenReturnThese)
    {
        returnManager.Returns(returnThis, thenReturnThese);
        return this;
    }
}
