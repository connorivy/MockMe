namespace MockMe.Mocks.ClassMemberMocks;

public interface IMemberMock<TReturn, TSelf> : IVoidMemberMock<TSelf>
{
    public TSelf Returns(TReturn returnThis, params TReturn[] thenReturnThese);
}

public interface IVoidMemberMock<TSelf>
{
    public TSelf Callback(Action callback);
}

public interface IMemberMockWithArgs<TSelf, TCallback> : IVoidMemberMock<TSelf>
{
    public TSelf Callback(TCallback callback);
}

public interface IMemberMockWithArgs<TReturn, TSelf, TCallback, TReturnCall>
    : IMemberMock<TReturn, TSelf>
{
    public TSelf Returns(TReturnCall returnThis, params TReturnCall[] thenReturnThese);
    public TSelf Callback(TCallback callback);
}
