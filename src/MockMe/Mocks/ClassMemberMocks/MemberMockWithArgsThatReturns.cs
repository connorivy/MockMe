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
        Func<TReturn, TReturnFunc> toReturnCall
    )
        : base(callbackManager)
    {
        this.returnManager = new(callbackManager, toReturnCall);
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

    TReturnFunc? IMockReturnCallRetriever<TReturnFunc>.GetReturnValue() =>
        this.returnManager.GetReturnCall();
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
        : base(new(ActionUtils.CallbackFunc<TArg1>()), FunctionUtils.ToReturnFunc<TArg1, TReturn>())
    { }
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
            new(ActionUtils.CallbackFunc<TArg1, TArg2>()),
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
            new(ActionUtils.CallbackFunc<TArg1, TArg2, TArg3>()),
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
            new(ActionUtils.CallbackFunc<TArg1, TArg2, TArg3, TArg4>()),
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
            new(ActionUtils.CallbackFunc<TArg1, TArg2, TArg3, TArg4, TArg5>()),
            FunctionUtils.ToReturnFunc<TArg1, TArg2, TArg3, TArg4, TArg5, TReturn>()
        ) { }
}

public class MemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TReturn>
    : MemberMockWithArgsThatReturns<
        TReturn,
        MemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TReturn>,
        Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>,
        Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TReturn>
    >
{
    public MemberMock()
        : base(
            new(ActionUtils.CallbackFunc<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>()),
            FunctionUtils.ToReturnFunc<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TReturn>()
        ) { }
}

public class MemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TReturn>
    : MemberMockWithArgsThatReturns<
        TReturn,
        MemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TReturn>,
        Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>,
        Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TReturn>
    >
{
    public MemberMock()
        : base(
            new(ActionUtils.CallbackFunc<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>()),
            FunctionUtils.ToReturnFunc<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TReturn>()
        ) { }
}

public class MemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TReturn>
    : MemberMockWithArgsThatReturns<
        TReturn,
        MemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TReturn>,
        Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>,
        Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TReturn>
    >
{
    public MemberMock()
        : base(
            new(ActionUtils.CallbackFunc<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>()),
            FunctionUtils.ToReturnFunc<
                TArg1,
                TArg2,
                TArg3,
                TArg4,
                TArg5,
                TArg6,
                TArg7,
                TArg8,
                TReturn
            >()
        ) { }
}

public class MemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TReturn>
    : MemberMockWithArgsThatReturns<
        TReturn,
        MemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TReturn>,
        Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>,
        Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TReturn>
    >
{
    public MemberMock()
        : base(
            new(
                ActionUtils.CallbackFunc<
                    TArg1,
                    TArg2,
                    TArg3,
                    TArg4,
                    TArg5,
                    TArg6,
                    TArg7,
                    TArg8,
                    TArg9
                >()
            ),
            FunctionUtils.ToReturnFunc<
                TArg1,
                TArg2,
                TArg3,
                TArg4,
                TArg5,
                TArg6,
                TArg7,
                TArg8,
                TArg9,
                TReturn
            >()
        ) { }
}

public class MemberMock<
    TArg1,
    TArg2,
    TArg3,
    TArg4,
    TArg5,
    TArg6,
    TArg7,
    TArg8,
    TArg9,
    TArg10,
    TReturn
>
    : MemberMockWithArgsThatReturns<
        TReturn,
        MemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TReturn>,
        Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10>,
        Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TReturn>
    >
{
    public MemberMock()
        : base(
            new(
                ActionUtils.CallbackFunc<
                    TArg1,
                    TArg2,
                    TArg3,
                    TArg4,
                    TArg5,
                    TArg6,
                    TArg7,
                    TArg8,
                    TArg9,
                    TArg10
                >()
            ),
            FunctionUtils.ToReturnFunc<
                TArg1,
                TArg2,
                TArg3,
                TArg4,
                TArg5,
                TArg6,
                TArg7,
                TArg8,
                TArg9,
                TArg10,
                TReturn
            >()
        ) { }
}

public class MemberMock<
    TArg1,
    TArg2,
    TArg3,
    TArg4,
    TArg5,
    TArg6,
    TArg7,
    TArg8,
    TArg9,
    TArg10,
    TArg11,
    TReturn
>
    : MemberMockWithArgsThatReturns<
        TReturn,
        MemberMock<
            TArg1,
            TArg2,
            TArg3,
            TArg4,
            TArg5,
            TArg6,
            TArg7,
            TArg8,
            TArg9,
            TArg10,
            TArg11,
            TReturn
        >,
        Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11>,
        Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TReturn>
    >
{
    public MemberMock()
        : base(
            new(
                ActionUtils.CallbackFunc<
                    TArg1,
                    TArg2,
                    TArg3,
                    TArg4,
                    TArg5,
                    TArg6,
                    TArg7,
                    TArg8,
                    TArg9,
                    TArg10,
                    TArg11
                >()
            ),
            FunctionUtils.ToReturnFunc<
                TArg1,
                TArg2,
                TArg3,
                TArg4,
                TArg5,
                TArg6,
                TArg7,
                TArg8,
                TArg9,
                TArg10,
                TArg11,
                TReturn
            >()
        ) { }
}

public class MemberMock<
    TArg1,
    TArg2,
    TArg3,
    TArg4,
    TArg5,
    TArg6,
    TArg7,
    TArg8,
    TArg9,
    TArg10,
    TArg11,
    TArg12,
    TReturn
>
    : MemberMockWithArgsThatReturns<
        TReturn,
        MemberMock<
            TArg1,
            TArg2,
            TArg3,
            TArg4,
            TArg5,
            TArg6,
            TArg7,
            TArg8,
            TArg9,
            TArg10,
            TArg11,
            TArg12,
            TReturn
        >,
        Action<
            TArg1,
            TArg2,
            TArg3,
            TArg4,
            TArg5,
            TArg6,
            TArg7,
            TArg8,
            TArg9,
            TArg10,
            TArg11,
            TArg12
        >,
        Func<
            TArg1,
            TArg2,
            TArg3,
            TArg4,
            TArg5,
            TArg6,
            TArg7,
            TArg8,
            TArg9,
            TArg10,
            TArg11,
            TArg12,
            TReturn
        >
    >
{
    public MemberMock()
        : base(
            new(
                ActionUtils.CallbackFunc<
                    TArg1,
                    TArg2,
                    TArg3,
                    TArg4,
                    TArg5,
                    TArg6,
                    TArg7,
                    TArg8,
                    TArg9,
                    TArg10,
                    TArg11,
                    TArg12
                >()
            ),
            FunctionUtils.ToReturnFunc<
                TArg1,
                TArg2,
                TArg3,
                TArg4,
                TArg5,
                TArg6,
                TArg7,
                TArg8,
                TArg9,
                TArg10,
                TArg11,
                TArg12,
                TReturn
            >()
        ) { }
}

public class MemberMock<
    TArg1,
    TArg2,
    TArg3,
    TArg4,
    TArg5,
    TArg6,
    TArg7,
    TArg8,
    TArg9,
    TArg10,
    TArg11,
    TArg12,
    TArg13,
    TReturn
>
    : MemberMockWithArgsThatReturns<
        TReturn,
        MemberMock<
            TArg1,
            TArg2,
            TArg3,
            TArg4,
            TArg5,
            TArg6,
            TArg7,
            TArg8,
            TArg9,
            TArg10,
            TArg11,
            TArg12,
            TArg13,
            TReturn
        >,
        Action<
            TArg1,
            TArg2,
            TArg3,
            TArg4,
            TArg5,
            TArg6,
            TArg7,
            TArg8,
            TArg9,
            TArg10,
            TArg11,
            TArg12,
            TArg13
        >,
        Func<
            TArg1,
            TArg2,
            TArg3,
            TArg4,
            TArg5,
            TArg6,
            TArg7,
            TArg8,
            TArg9,
            TArg10,
            TArg11,
            TArg12,
            TArg13,
            TReturn
        >
    >
{
    public MemberMock()
        : base(
            new(
                ActionUtils.CallbackFunc<
                    TArg1,
                    TArg2,
                    TArg3,
                    TArg4,
                    TArg5,
                    TArg6,
                    TArg7,
                    TArg8,
                    TArg9,
                    TArg10,
                    TArg11,
                    TArg12,
                    TArg13
                >()
            ),
            FunctionUtils.ToReturnFunc<
                TArg1,
                TArg2,
                TArg3,
                TArg4,
                TArg5,
                TArg6,
                TArg7,
                TArg8,
                TArg9,
                TArg10,
                TArg11,
                TArg12,
                TArg13,
                TReturn
            >()
        ) { }
}

public class MemberMock<
    TArg1,
    TArg2,
    TArg3,
    TArg4,
    TArg5,
    TArg6,
    TArg7,
    TArg8,
    TArg9,
    TArg10,
    TArg11,
    TArg12,
    TArg13,
    TArg14,
    TReturn
>
    : MemberMockWithArgsThatReturns<
        TReturn,
        MemberMock<
            TArg1,
            TArg2,
            TArg3,
            TArg4,
            TArg5,
            TArg6,
            TArg7,
            TArg8,
            TArg9,
            TArg10,
            TArg11,
            TArg12,
            TArg13,
            TArg14,
            TReturn
        >,
        Action<
            TArg1,
            TArg2,
            TArg3,
            TArg4,
            TArg5,
            TArg6,
            TArg7,
            TArg8,
            TArg9,
            TArg10,
            TArg11,
            TArg12,
            TArg13,
            TArg14
        >,
        Func<
            TArg1,
            TArg2,
            TArg3,
            TArg4,
            TArg5,
            TArg6,
            TArg7,
            TArg8,
            TArg9,
            TArg10,
            TArg11,
            TArg12,
            TArg13,
            TArg14,
            TReturn
        >
    >
{
    public MemberMock()
        : base(
            new(
                ActionUtils.CallbackFunc<
                    TArg1,
                    TArg2,
                    TArg3,
                    TArg4,
                    TArg5,
                    TArg6,
                    TArg7,
                    TArg8,
                    TArg9,
                    TArg10,
                    TArg11,
                    TArg12,
                    TArg13,
                    TArg14
                >()
            ),
            FunctionUtils.ToReturnFunc<
                TArg1,
                TArg2,
                TArg3,
                TArg4,
                TArg5,
                TArg6,
                TArg7,
                TArg8,
                TArg9,
                TArg10,
                TArg11,
                TArg12,
                TArg13,
                TArg14,
                TReturn
            >()
        ) { }
}

public class MemberMock<
    TArg1,
    TArg2,
    TArg3,
    TArg4,
    TArg5,
    TArg6,
    TArg7,
    TArg8,
    TArg9,
    TArg10,
    TArg11,
    TArg12,
    TArg13,
    TArg14,
    TArg15,
    TReturn
>
    : MemberMockWithArgsThatReturns<
        TReturn,
        MemberMock<
            TArg1,
            TArg2,
            TArg3,
            TArg4,
            TArg5,
            TArg6,
            TArg7,
            TArg8,
            TArg9,
            TArg10,
            TArg11,
            TArg12,
            TArg13,
            TArg14,
            TArg15,
            TReturn
        >,
        Action<
            TArg1,
            TArg2,
            TArg3,
            TArg4,
            TArg5,
            TArg6,
            TArg7,
            TArg8,
            TArg9,
            TArg10,
            TArg11,
            TArg12,
            TArg13,
            TArg14,
            TArg15
        >,
        Func<
            TArg1,
            TArg2,
            TArg3,
            TArg4,
            TArg5,
            TArg6,
            TArg7,
            TArg8,
            TArg9,
            TArg10,
            TArg11,
            TArg12,
            TArg13,
            TArg14,
            TArg15,
            TReturn
        >
    >
{
    public MemberMock()
        : base(
            new(
                ActionUtils.CallbackFunc<
                    TArg1,
                    TArg2,
                    TArg3,
                    TArg4,
                    TArg5,
                    TArg6,
                    TArg7,
                    TArg8,
                    TArg9,
                    TArg10,
                    TArg11,
                    TArg12,
                    TArg13,
                    TArg14,
                    TArg15
                >()
            ),
            FunctionUtils.ToReturnFunc<
                TArg1,
                TArg2,
                TArg3,
                TArg4,
                TArg5,
                TArg6,
                TArg7,
                TArg8,
                TArg9,
                TArg10,
                TArg11,
                TArg12,
                TArg13,
                TArg14,
                TArg15,
                TReturn
            >()
        ) { }
}
