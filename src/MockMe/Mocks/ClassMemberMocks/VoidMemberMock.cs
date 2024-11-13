using MockMe.Extensions;

namespace MockMe.Mocks.ClassMemberMocks;

public abstract class VoidMemberMockWithArgsBase<TSelf, TCallback>
    : IMemberMockWithArgs<TSelf, TCallback>,
        IMockCallbackRetriever<TCallback>
    where TSelf : VoidMemberMockWithArgsBase<TSelf, TCallback>
{
    private readonly CallbackManager<TCallback> callbackManager;

    internal VoidMemberMockWithArgsBase(CallbackManager<TCallback> callbackManager)
    {
        this.callbackManager = callbackManager;
    }

    public TSelf Callback(TCallback callback)
    {
        this.callbackManager.AddCallback(callback);
        return (TSelf)this;
    }

    public TSelf Callback(Action callback)
    {
        this.callbackManager.AddCallback(callback);
        return (TSelf)this;
    }

    IEnumerable<TCallback> IMockCallbackRetriever<TCallback>.GetCallbacksRegisteredAfterReturnCall() =>
        this.callbackManager.GetCallbacksRegisteredAfterReturnCall();

    IEnumerable<TCallback> IMockCallbackRetriever<TCallback>.GetCallbacksRegisteredBeforeReturnCall() =>
        this.callbackManager.GetCallbacksRegisteredBeforeReturnCall();
}

public class VoidMemberMock<TArg1>
    : VoidMemberMockWithArgsBase<VoidMemberMock<TArg1>, Action<TArg1>>
{
    public VoidMemberMock()
        : base(new(ActionExtensions.CallbackFunc<TArg1>())) { }
}

public class VoidMemberMock<TArg1, TArg2>
    : VoidMemberMockWithArgsBase<VoidMemberMock<TArg1, TArg2>, Action<TArg1, TArg2>>
{
    public VoidMemberMock()
        : base(new(ActionExtensions.CallbackFunc<TArg1, TArg2>())) { }
}

public class VoidMemberMock<TArg1, TArg2, TArg3>
    : VoidMemberMockWithArgsBase<VoidMemberMock<TArg1, TArg2, TArg3>, Action<TArg1, TArg2, TArg3>>
{
    public VoidMemberMock()
        : base(new(ActionExtensions.CallbackFunc<TArg1, TArg2, TArg3>())) { }
}

public class VoidMemberMock<TArg1, TArg2, TArg3, TArg4>
    : VoidMemberMockWithArgsBase<
        VoidMemberMock<TArg1, TArg2, TArg3, TArg4>,
        Action<TArg1, TArg2, TArg3, TArg4>
    >
{
    public VoidMemberMock()
        : base(new(ActionExtensions.CallbackFunc<TArg1, TArg2, TArg3, TArg4>())) { }
}

public class VoidMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5>
    : VoidMemberMockWithArgsBase<
        VoidMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5>,
        Action<TArg1, TArg2, TArg3, TArg4, TArg5>
    >
{
    public VoidMemberMock()
        : base(new(ActionExtensions.CallbackFunc<TArg1, TArg2, TArg3, TArg4, TArg5>())) { }
}

public class VoidMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>
    : VoidMemberMockWithArgsBase<
        VoidMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>,
        Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>
    >
{
    public VoidMemberMock()
        : base(new(ActionExtensions.CallbackFunc<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>())) { }
}

public class VoidMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>
    : VoidMemberMockWithArgsBase<
        VoidMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>,
        Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>
    >
{
    public VoidMemberMock()
        : base(
            new(ActionExtensions.CallbackFunc<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>())
        ) { }
}

public class VoidMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>
    : VoidMemberMockWithArgsBase<
        VoidMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>,
        Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>
    >
{
    public VoidMemberMock()
        : base(
            new(
                ActionExtensions.CallbackFunc<
                    TArg1,
                    TArg2,
                    TArg3,
                    TArg4,
                    TArg5,
                    TArg6,
                    TArg7,
                    TArg8
                >()
            )
        ) { }
}

public class VoidMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>
    : VoidMemberMockWithArgsBase<
        VoidMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>,
        Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>
    >
{
    public VoidMemberMock()
        : base(
            new(
                ActionExtensions.CallbackFunc<
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
            )
        ) { }
}

public class VoidMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10>
    : VoidMemberMockWithArgsBase<
        VoidMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10>,
        Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10>
    >
{
    public VoidMemberMock()
        : base(
            new(
                ActionExtensions.CallbackFunc<
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
            )
        ) { }
}

public class VoidMemberMock<
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
>
    : VoidMemberMockWithArgsBase<
        VoidMemberMock<
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
        >,
        Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11>
    >
{
    public VoidMemberMock()
        : base(
            new(
                ActionExtensions.CallbackFunc<
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
            )
        ) { }
}

public class VoidMemberMock<
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
>
    : VoidMemberMockWithArgsBase<
        VoidMemberMock<
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
        >
    >
{
    public VoidMemberMock()
        : base(
            new(
                ActionExtensions.CallbackFunc<
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
            )
        ) { }
}

public class VoidMemberMock<
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
>
    : VoidMemberMockWithArgsBase<
        VoidMemberMock<
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
        >
    >
{
    public VoidMemberMock()
        : base(
            new(
                ActionExtensions.CallbackFunc<
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
            )
        ) { }
}

public class VoidMemberMock<
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
>
    : VoidMemberMockWithArgsBase<
        VoidMemberMock<
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
        >
    >
{
    public VoidMemberMock()
        : base(
            new(
                ActionExtensions.CallbackFunc<
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
            )
        ) { }
}

public class VoidMemberMock<
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
>
    : VoidMemberMockWithArgsBase<
        VoidMemberMock<
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
        >
    >
{
    public VoidMemberMock()
        : base(
            new(
                ActionExtensions.CallbackFunc<
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
            )
        ) { }
}
