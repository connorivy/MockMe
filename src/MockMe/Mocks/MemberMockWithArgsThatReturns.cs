using MockMe.Extensions;

namespace MockMe.Mocks;

public abstract class MemberMockWithArgsThatReturns<TReturn, TSelf, TCallback, TReturnFunc>
    : VoidMemberMockWithArgsBase<TSelf, TCallback>,
        IMemberMockWithArgs<TReturn, TSelf, TCallback, TReturnFunc>,
        IMockCallbackAndReturnCallRetriever<TCallback, TReturnFunc>
    where TSelf : MemberMockWithArgsThatReturns<TReturn, TSelf, TCallback, TReturnFunc>
{
    private readonly ReturnManager<TReturn, TReturnFunc> returnManager;

    internal MemberMockWithArgsThatReturns(
        CallbackManager<TCallback> callbackManager,
        Func<TReturn, TReturnFunc> toReturnCall
    )
        : base(callbackManager)
    {
        this.returnManager = new(callbackManager, toReturnCall);
    }

    public TSelf Return(TReturnFunc returnThis, params TReturnFunc[] thenReturnThese)
    {
        returnManager.Returns(returnThis, thenReturnThese);
        return (TSelf)this;
    }

    public TSelf Return(TReturn returnThis, params TReturn[] thenReturnThese)
    {
        returnManager.Returns(returnThis, thenReturnThese);
        return (TSelf)this;
    }

    TReturnFunc? IMockReturnCallRetriever<TReturnFunc>.GetReturnValue() =>
        returnManager.GetReturnCall();
}

public class MemberMock<TArg1, TReturn>
    : MemberMockWithArgsThatReturns<
        TReturn,
        MemberMock<TArg1, TReturn>,
        Action<TArg1>,
        Func<TArg1, TReturn>
    >
{
    public MemberMock()
        : base(
            new(ActionExtensions.CallbackFunc<TArg1>()),
            FunctionUtils.ToReturnFunc<TArg1, TReturn>()
        ) { }
}

public class MemberMock<TArg1, TArg2, TReturn>
    : MemberMockWithArgsThatReturns<
        TReturn,
        MemberMock<TArg1, TArg2, TReturn>,
        Action<TArg1, TArg2>,
        Func<TArg1, TArg2, TReturn>
    >
{
    public MemberMock()
        : base(
            new(ActionExtensions.CallbackFunc<TArg1, TArg2>()),
            FunctionUtils.ToReturnFunc<TArg1, TArg2, TReturn>()
        ) { }
}

public class MemberMock<TArg1, TArg2, TArg3, TReturn>
    : MemberMockWithArgsThatReturns<
        TReturn,
        MemberMock<TArg1, TArg2, TArg3, TReturn>,
        Action<TArg1, TArg2, TArg3>,
        Func<TArg1, TArg2, TArg3, TReturn>
    >
{
    public MemberMock()
        : base(
            new(ActionExtensions.CallbackFunc<TArg1, TArg2, TArg3>()),
            FunctionUtils.ToReturnFunc<TArg1, TArg2, TArg3, TReturn>()
        ) { }
}

public class MemberMock<TArg1, TArg2, TArg3, TArg4, TReturn>
    : MemberMockWithArgsThatReturns<
        TReturn,
        MemberMock<TArg1, TArg2, TArg3, TArg4, TReturn>,
        Action<TArg1, TArg2, TArg3, TArg4>,
        Func<TArg1, TArg2, TArg3, TArg4, TReturn>
    >
{
    public MemberMock()
        : base(
            new(ActionExtensions.CallbackFunc<TArg1, TArg2, TArg3, TArg4>()),
            FunctionUtils.ToReturnFunc<TArg1, TArg2, TArg3, TArg4, TReturn>()
        ) { }
}

public class MemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TReturn>
    : MemberMockWithArgsThatReturns<
        TReturn,
        MemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TReturn>,
        Action<TArg1, TArg2, TArg3, TArg4, TArg5>,
        Func<TArg1, TArg2, TArg3, TArg4, TArg5, TReturn>
    >
{
    public MemberMock()
        : base(
            new(ActionExtensions.CallbackFunc<TArg1, TArg2, TArg3, TArg4, TArg5>()),
            FunctionUtils.ToReturnFunc<TArg1, TArg2, TArg3, TArg4, TArg5, TReturn>()
        ) { }
}
