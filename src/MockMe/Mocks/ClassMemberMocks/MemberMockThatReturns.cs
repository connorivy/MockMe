namespace MockMe.Mocks.ClassMemberMocks;

public class MemberMockBase<TSelf, TReturn>
    : VoidMemberMockBase<TSelf>,
        IMockCallbackAndReturnCallRetriever<Action, Func<TReturn>>
    where TSelf : MemberMockBase<TSelf, TReturn>
{
    private readonly ReturnManager<TReturn> returnManager;

    public MemberMockBase()
        : this(new()) { }

    internal MemberMockBase(CallbackManager callbackManager)
        : base(callbackManager)
    {
        this.returnManager = new(callbackManager);
    }

    public TSelf Returns(TReturn returnThis, params TReturn[] thenReturnThese)
    {
        this.returnManager.Returns(returnThis, thenReturnThese);
        return (TSelf)this;
    }

    public TSelf Returns(Func<TReturn> returnThis)
    {
        this.returnManager.Returns(returnThis);
        return (TSelf)this;
    }

    public void Throws(Exception ex)
    {
        this.returnManager.Throws(ex);
    }

    Func<TReturn>? IMockReturnCallRetriever<Func<TReturn>>.GetReturnValue() =>
        this.returnManager.GetReturnCall();
}

public class MemberMock<TReturn> : MemberMockBase<MemberMock<TReturn>, TReturn> { }

public class MemberMockBase<TSelf, TArgCollection, TReturn>
    : VoidMemberMockBase<TSelf, TArgCollection>,
        IMockCallbackAndReturnCallRetriever<Action<TArgCollection>, Func<TArgCollection, TReturn>>
    where TSelf : MemberMockBase<TSelf, TArgCollection, TReturn>
{
    private readonly ReturnManager<TArgCollection, TReturn> returnManager;

    public MemberMockBase()
        : this(new()) { }

    internal MemberMockBase(CallbackManager<TArgCollection> callbackManager)
        : base(callbackManager)
    {
        this.returnManager = new(callbackManager);
    }

    public TSelf Returns(TReturn returnThis, params TReturn[] thenReturnThese)
    {
        this.returnManager.Returns(returnThis, thenReturnThese);
        return (TSelf)this;
    }

    public TSelf Returns(Func<TReturn> returnThis)
    {
        this.returnManager.Returns(returnThis);
        return (TSelf)this;
    }

    public TSelf Returns(Func<TArgCollection, TReturn> returnThis)
    {
        this.returnManager.Returns(returnThis);
        return (TSelf)this;
    }

    public void Throws(Exception ex)
    {
        this.returnManager.Throws(ex);
    }

    Func<TArgCollection, TReturn>? IMockReturnCallRetriever<
        Func<TArgCollection, TReturn>
    >.GetReturnValue() => this.returnManager.GetReturnCall();
}

public class MemberMock<TArgCollection, TReturn>
    : MemberMockBase<MemberMock<TArgCollection, TReturn>, TArgCollection, TReturn> { }
