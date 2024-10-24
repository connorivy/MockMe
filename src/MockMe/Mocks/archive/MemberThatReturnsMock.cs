namespace MockMe.Mocks.archive;

public class MemberMock<TReturn>
    : IMemberMockWithCallback<MemberMock<TReturn>, Action>,
        IMemberThatReturnsMock<TReturn, MemberMock<TReturn>, TReturn>
{
    List<Action>? IMemberMockWithCallback<MemberMock<TReturn>, Action>.Callbacks { get; set; }
    Queue<TReturn>? IMemberThatReturnsMock<
        TReturn,
        MemberMock<TReturn>,
        TReturn
    >.ReturnCalls { get; set; }
}
