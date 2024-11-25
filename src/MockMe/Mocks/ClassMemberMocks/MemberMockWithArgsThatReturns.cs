using MockMe.Extensions;

namespace MockMe.Mocks.ClassMemberMocks;

public abstract class MemberMockWithArgsThatReturns<TReturn, TSelf, TCallback, TReturnFunc>
    : VoidMemberMockWithArgsBase<TSelf, TCallback>,
        IMemberMockWithArgs<TReturn, TSelf, TCallback, TReturnFunc>,
        IMockCallbackAndReturnCallRetriever<TCallback, TReturnFunc>
    where TSelf : MemberMockWithArgsThatReturns<TReturn, TSelf, TCallback, TReturnFunc>
{
    private readonly ReturnManager<TReturn, TReturnFunc> returnManager;

    internal MemberMockWithArgsThatReturns(
        CallbackManager<TCallback> callbackManager,
        Func<TReturn, TReturnFunc> toReturnCall,
        Func<Exception, TReturnFunc> toThrownException
    )
        : base(callbackManager)
    {
        this.returnManager = new(callbackManager, toReturnCall, toThrownException);
    }

    public TSelf Returns(TReturnFunc returnThis, params TReturnFunc[] thenReturnThese)
    {
        this.returnManager.Returns(returnThis, thenReturnThese);
        return (TSelf)this;
    }

    public TSelf Returns(TReturn returnThis, params TReturn[] thenReturnThese)
    {
        this.returnManager.Returns(returnThis, thenReturnThese);
        return (TSelf)this;
    }

    public void Throws(Exception ex)
    {
        this.returnManager.Throws(ex);
    }

    TReturnFunc? IMockReturnCallRetriever<TReturnFunc>.GetReturnValue() =>
        this.returnManager.GetReturnCall();
}

public class MemberMock<T1, TReturn>
    : MemberMockWithArgsThatReturns<TReturn, MemberMock<T1, TReturn>, Action<T1>, Func<T1, TReturn>>
{
    public MemberMock()
        : base(
            new(ActionUtils.CallbackFunc<T1>()),
            FunctionUtils.ToReturnFunc<T1, TReturn>(),
            ThrowUtils.ToThrow<T1, TReturn>()
        ) { }
}

public class MemberMock<T1, T2, TReturn>
    : MemberMockWithArgsThatReturns<
        TReturn,
        MemberMock<T1, T2, TReturn>,
        Action<T1, T2>,
        Func<T1, T2, TReturn>
    >
{
    public MemberMock()
        : base(
            new(ActionUtils.CallbackFunc<T1, T2>()),
            FunctionUtils.ToReturnFunc<T1, T2, TReturn>(),
            ThrowUtils.ToThrow<T1, T2, TReturn>()
        ) { }
}

public class MemberMock<T1, T2, T3, TReturn>
    : MemberMockWithArgsThatReturns<
        TReturn,
        MemberMock<T1, T2, T3, TReturn>,
        Action<T1, T2, T3>,
        Func<T1, T2, T3, TReturn>
    >
{
    public MemberMock()
        : base(
            new(ActionUtils.CallbackFunc<T1, T2, T3>()),
            FunctionUtils.ToReturnFunc<T1, T2, T3, TReturn>(),
            ThrowUtils.ToThrow<T1, T2, T3, TReturn>()
        ) { }
}

public class MemberMock<T1, T2, T3, T4, TReturn>
    : MemberMockWithArgsThatReturns<
        TReturn,
        MemberMock<T1, T2, T3, T4, TReturn>,
        Action<T1, T2, T3, T4>,
        Func<T1, T2, T3, T4, TReturn>
    >
{
    public MemberMock()
        : base(
            new(ActionUtils.CallbackFunc<T1, T2, T3, T4>()),
            FunctionUtils.ToReturnFunc<T1, T2, T3, T4, TReturn>(),
            ThrowUtils.ToThrow<T1, T2, T3, T4, TReturn>()
        ) { }
}

public class MemberMock<T1, T2, T3, T4, T5, TReturn>
    : MemberMockWithArgsThatReturns<
        TReturn,
        MemberMock<T1, T2, T3, T4, T5, TReturn>,
        Action<T1, T2, T3, T4, T5>,
        Func<T1, T2, T3, T4, T5, TReturn>
    >
{
    public MemberMock()
        : base(
            new(ActionUtils.CallbackFunc<T1, T2, T3, T4, T5>()),
            FunctionUtils.ToReturnFunc<T1, T2, T3, T4, T5, TReturn>(),
            ThrowUtils.ToThrow<T1, T2, T3, T4, T5, TReturn>()
        ) { }
}

public class MemberMock<T1, T2, T3, T4, T5, T6, TReturn>
    : MemberMockWithArgsThatReturns<
        TReturn,
        MemberMock<T1, T2, T3, T4, T5, T6, TReturn>,
        Action<T1, T2, T3, T4, T5, T6>,
        Func<T1, T2, T3, T4, T5, T6, TReturn>
    >
{
    public MemberMock()
        : base(
            new(ActionUtils.CallbackFunc<T1, T2, T3, T4, T5, T6>()),
            FunctionUtils.ToReturnFunc<T1, T2, T3, T4, T5, T6, TReturn>(),
            ThrowUtils.ToThrow<T1, T2, T3, T4, T5, T6, TReturn>()
        ) { }
}

public class MemberMock<T1, T2, T3, T4, T5, T6, T7, TReturn>
    : MemberMockWithArgsThatReturns<
        TReturn,
        MemberMock<T1, T2, T3, T4, T5, T6, T7, TReturn>,
        Action<T1, T2, T3, T4, T5, T6, T7>,
        Func<T1, T2, T3, T4, T5, T6, T7, TReturn>
    >
{
    public MemberMock()
        : base(
            new(ActionUtils.CallbackFunc<T1, T2, T3, T4, T5, T6, T7>()),
            FunctionUtils.ToReturnFunc<T1, T2, T3, T4, T5, T6, T7, TReturn>(),
            ThrowUtils.ToThrow<T1, T2, T3, T4, T5, T6, T7, TReturn>()
        ) { }
}

public class MemberMock<T1, T2, T3, T4, T5, T6, T7, T8, TReturn>
    : MemberMockWithArgsThatReturns<
        TReturn,
        MemberMock<T1, T2, T3, T4, T5, T6, T7, T8, TReturn>,
        Action<T1, T2, T3, T4, T5, T6, T7, T8>,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, TReturn>
    >
{
    public MemberMock()
        : base(
            new(ActionUtils.CallbackFunc<T1, T2, T3, T4, T5, T6, T7, T8>()),
            FunctionUtils.ToReturnFunc<T1, T2, T3, T4, T5, T6, T7, T8, TReturn>(),
            ThrowUtils.ToThrow<T1, T2, T3, T4, T5, T6, T7, T8, TReturn>()
        ) { }
}

public class MemberMock<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn>
    : MemberMockWithArgsThatReturns<
        TReturn,
        MemberMock<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn>,
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9>,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn>
    >
{
    public MemberMock()
        : base(
            new(ActionUtils.CallbackFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9>()),
            FunctionUtils.ToReturnFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn>(),
            ThrowUtils.ToThrow<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn>()
        ) { }
}

public class MemberMock<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn>
    : MemberMockWithArgsThatReturns<
        TReturn,
        MemberMock<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn>,
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn>
    >
{
    public MemberMock()
        : base(
            new(ActionUtils.CallbackFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>()),
            FunctionUtils.ToReturnFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn>(),
            ThrowUtils.ToThrow<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn>()
        ) { }
}

public class MemberMock<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn>
    : MemberMockWithArgsThatReturns<
        TReturn,
        MemberMock<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn>,
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn>
    >
{
    public MemberMock()
        : base(
            new(ActionUtils.CallbackFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>()),
            FunctionUtils.ToReturnFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn>(),
            ThrowUtils.ToThrow<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn>()
        ) { }
}

public class MemberMock<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn>
    : MemberMockWithArgsThatReturns<
        TReturn,
        MemberMock<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn>,
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn>
    >
{
    public MemberMock()
        : base(
            new(ActionUtils.CallbackFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>()),
            FunctionUtils.ToReturnFunc<
                T1,
                T2,
                T3,
                T4,
                T5,
                T6,
                T7,
                T8,
                T9,
                T10,
                T11,
                T12,
                TReturn
            >(),
            ThrowUtils.ToThrow<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn>()
        ) { }
}

public class MemberMock<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn>
    : MemberMockWithArgsThatReturns<
        TReturn,
        MemberMock<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn>,
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn>
    >
{
    public MemberMock()
        : base(
            new(ActionUtils.CallbackFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()),
            FunctionUtils.ToReturnFunc<
                T1,
                T2,
                T3,
                T4,
                T5,
                T6,
                T7,
                T8,
                T9,
                T10,
                T11,
                T12,
                T13,
                TReturn
            >(),
            ThrowUtils.ToThrow<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn>()
        ) { }
}

public class MemberMock<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn>
    : MemberMockWithArgsThatReturns<
        TReturn,
        MemberMock<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn>,
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn>
    >
{
    public MemberMock()
        : base(
            new(
                ActionUtils.CallbackFunc<
                    T1,
                    T2,
                    T3,
                    T4,
                    T5,
                    T6,
                    T7,
                    T8,
                    T9,
                    T10,
                    T11,
                    T12,
                    T13,
                    T14
                >()
            ),
            FunctionUtils.ToReturnFunc<
                T1,
                T2,
                T3,
                T4,
                T5,
                T6,
                T7,
                T8,
                T9,
                T10,
                T11,
                T12,
                T13,
                T14,
                TReturn
            >(),
            ThrowUtils.ToThrow<
                T1,
                T2,
                T3,
                T4,
                T5,
                T6,
                T7,
                T8,
                T9,
                T10,
                T11,
                T12,
                T13,
                T14,
                TReturn
            >()
        ) { }
}

public class MemberMock<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn>
    : MemberMockWithArgsThatReturns<
        TReturn,
        MemberMock<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn>,
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn>
    >
{
    public MemberMock()
        : base(
            new(
                ActionUtils.CallbackFunc<
                    T1,
                    T2,
                    T3,
                    T4,
                    T5,
                    T6,
                    T7,
                    T8,
                    T9,
                    T10,
                    T11,
                    T12,
                    T13,
                    T14,
                    T15
                >()
            ),
            FunctionUtils.ToReturnFunc<
                T1,
                T2,
                T3,
                T4,
                T5,
                T6,
                T7,
                T8,
                T9,
                T10,
                T11,
                T12,
                T13,
                T14,
                T15,
                TReturn
            >(),
            ThrowUtils.ToThrow<
                T1,
                T2,
                T3,
                T4,
                T5,
                T6,
                T7,
                T8,
                T9,
                T10,
                T11,
                T12,
                T13,
                T14,
                T15,
                TReturn
            >()
        ) { }
}
