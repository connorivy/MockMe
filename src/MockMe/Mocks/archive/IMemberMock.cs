namespace MockMe.Mocks.archive;

public interface IMemberMock<TSelf>
{
    protected TSelf This => (TSelf)this;
}

public abstract class MemberMockWithCallback<TSelf>
    where TSelf : MemberMockWithCallback<TSelf>
{
    protected TSelf This => (TSelf)this;
}

public abstract class MemberMockWithCallback<TSelf, TCallback> : MemberMockWithCallback<TSelf>
    where TSelf : MemberMockWithCallback<TSelf, TCallback>
{
    internal List<TCallback>? Callbacks { get; set; }

    public TSelf Callback(TCallback callback)
    {
        Callbacks ??= [];
        Callbacks.Add(callback);
        return This;
    }
}

public abstract class MemberThatReturnsMock<TSelf, TCallback, TReturnCallType>
    : MemberMockWithCallback<TSelf, TCallback>
    where TSelf : MemberThatReturnsMock<TSelf, TCallback, TReturnCallType>
{
    internal Queue<TReturnCallType>? ReturnCalls { get; set; }

    public TSelf Returns(TReturnCallType returnThis, params TReturnCallType[]? thenReturnThese)
    {
        ReturnCalls ??= [];
        ReturnCalls.Enqueue(returnThis);
        if (thenReturnThese is not null)
        {
            foreach (var returnVal in thenReturnThese)
            {
                ReturnCalls.Enqueue(returnVal);
            }
        }
        return This;
    }
}

public abstract class MemberWithArgsThatReturnsMock<TSelf, TCallback, TReturnCallType>
    : MemberMockWithCallback<TSelf, TCallback>
    where TSelf : MemberThatReturnsMock<TSelf, TCallback, TReturnCallType>
{
    internal Queue<TReturnCallType>? ReturnCalls { get; set; }

    public TSelf Returns(TReturnCallType returnThis, params TReturnCallType[]? thenReturnThese)
    {
        ReturnCalls ??= [];
        ReturnCalls.Enqueue(returnThis);
        if (thenReturnThese is not null)
        {
            foreach (var returnVal in thenReturnThese)
            {
                ReturnCalls.Enqueue(returnVal);
            }
        }
        return This;
    }
}

public interface IMemberMockWithCallback<TSelf, TCallback> : IMemberMock<TSelf>
{
    internal List<TCallback>? Callbacks { get; set; }
    public TSelf Callback(TCallback callback)
    {
        Callbacks ??= [];
        Callbacks.Add(callback);
        return This;
    }
}

public interface IMemberThatReturnsMock<TReturn, TSelf, TReturnCallType> : IMemberMock<TSelf>
{
    internal Queue<TReturnCallType>? ReturnCalls { get; set; }

    //public TSelf Returns(TReturnCallType returnThis, params TReturnCallType[]? thenReturnThese);

    public TSelf Returns(TReturnCallType returnThis, params TReturnCallType[]? thenReturnThese)
    {
        ReturnCalls ??= [];
        ReturnCalls.Enqueue(returnThis);
        if (thenReturnThese is not null)
        {
            foreach (var returnVal in thenReturnThese)
            {
                ReturnCalls.Enqueue(returnVal);
            }
        }
        return This;
    }
}
