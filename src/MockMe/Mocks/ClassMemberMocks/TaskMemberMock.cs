using MockMe.Extensions;

namespace MockMe.Mocks.ClassMemberMocks;

//public class TaskMemberMock<TReturn> : MemberMock<Task<TReturn>>
//{
//    public TaskMemberMock<TReturn> ReturnsAsync(
//        TReturn returnThis,
//        params TReturn[] thenReturnThese
//    )
//    {
//        this.Returns(
//            Task.FromResult(returnThis),
//            thenReturnThese.Select(ret => Task.FromResult(ret)).ToArray()
//        );
//        return this;
//    }
//}
public class TaskMemberMock : MemberMock<Task>
{
    public TaskMemberMock()
        : base() { }
}

public class TaskMemberMockBase<TReturn, TSelf, TCallback, TReturnFunc>
    : MemberMockWithArgsThatReturns<Task<TReturn>, TSelf, TCallback, TReturnFunc>
    where TSelf : TaskMemberMockBase<TReturn, TSelf, TCallback, TReturnFunc>
{
    internal TaskMemberMockBase(
        CallbackManager<TCallback> callbackManager,
        Func<Task<TReturn>, TReturnFunc> toReturnCall,
        Func<Exception, TReturnFunc> toThrowFunc
    )
        : base(callbackManager, toReturnCall, toThrowFunc) { }

    public TSelf ReturnsAsync(TReturn returnThis, params TReturn[] thenReturnThese)
    {
        this.Returns(
            Task.FromResult(returnThis),
            thenReturnThese.Select(ret => Task.FromResult(ret)).ToArray()
        );
        return (TSelf)this;
    }
}

public class TaskMemberMock<TReturn>
    : TaskMemberMockBase<TReturn, TaskMemberMock<TReturn>, Action, Task<TReturn>>
{
    public TaskMemberMock()
        : base(
            new(ActionUtils.CallbackFunc()),
            static task => task,
            ThrowUtils.ToThrow<Task<TReturn>>()
        ) { }
}

public class TaskMemberMock<T1, TReturn>
    : TaskMemberMockBase<TReturn, TaskMemberMock<T1, TReturn>, Action<T1>, Func<T1, Task<TReturn>>>
{
    public TaskMemberMock()
        : base(
            new(ActionUtils.CallbackFunc<T1>()),
            FunctionUtils.ToReturnFunc<T1, Task<TReturn>>(),
            ThrowUtils.ToThrow<T1, Task<TReturn>>()
        ) { }
}

public class TaskMemberMock<T1, T2, TReturn>
    : TaskMemberMockBase<
        TReturn,
        TaskMemberMock<T1, T2, TReturn>,
        Action<T1, T2>,
        Func<T1, T2, Task<TReturn>>
    >
{
    public TaskMemberMock()
        : base(
            new(ActionUtils.CallbackFunc<T1, T2>()),
            FunctionUtils.ToReturnFunc<T1, T2, Task<TReturn>>(),
            ThrowUtils.ToThrow<T1, T2, Task<TReturn>>()
        ) { }
}

public class TaskMemberMock<T1, T2, T3, TReturn>
    : TaskMemberMockBase<
        TReturn,
        TaskMemberMock<T1, T2, T3, TReturn>,
        Action<T1, T2, T3>,
        Func<T1, T2, T3, Task<TReturn>>
    >
{
    public TaskMemberMock()
        : base(
            new(ActionUtils.CallbackFunc<T1, T2, T3>()),
            FunctionUtils.ToReturnFunc<T1, T2, T3, Task<TReturn>>(),
            ThrowUtils.ToThrow<T1, T2, T3, Task<TReturn>>()
        ) { }
}

public class TaskMemberMock<T1, T2, T3, T4, TReturn>
    : TaskMemberMockBase<
        TReturn,
        TaskMemberMock<T1, T2, T3, T4, TReturn>,
        Action<T1, T2, T3, T4>,
        Func<T1, T2, T3, T4, Task<TReturn>>
    >
{
    public TaskMemberMock()
        : base(
            new(ActionUtils.CallbackFunc<T1, T2, T3, T4>()),
            FunctionUtils.ToReturnFunc<T1, T2, T3, T4, Task<TReturn>>(),
            ThrowUtils.ToThrow<T1, T2, T3, T4, Task<TReturn>>()
        ) { }
}

public class TaskMemberMock<T1, T2, T3, T4, T5, TReturn>
    : TaskMemberMockBase<
        TReturn,
        TaskMemberMock<T1, T2, T3, T4, T5, TReturn>,
        Action<T1, T2, T3, T4, T5>,
        Func<T1, T2, T3, T4, T5, Task<TReturn>>
    >
{
    public TaskMemberMock()
        : base(
            new(ActionUtils.CallbackFunc<T1, T2, T3, T4, T5>()),
            FunctionUtils.ToReturnFunc<T1, T2, T3, T4, T5, Task<TReturn>>(),
            ThrowUtils.ToThrow<T1, T2, T3, T4, T5, Task<TReturn>>()
        ) { }
}

public class TaskMemberMock<T1, T2, T3, T4, T5, T6, TReturn>
    : TaskMemberMockBase<
        TReturn,
        TaskMemberMock<T1, T2, T3, T4, T5, T6, TReturn>,
        Action<T1, T2, T3, T4, T5, T6>,
        Func<T1, T2, T3, T4, T5, T6, Task<TReturn>>
    >
{
    public TaskMemberMock()
        : base(
            new(ActionUtils.CallbackFunc<T1, T2, T3, T4, T5, T6>()),
            FunctionUtils.ToReturnFunc<T1, T2, T3, T4, T5, T6, Task<TReturn>>(),
            ThrowUtils.ToThrow<T1, T2, T3, T4, T5, T6, Task<TReturn>>()
        ) { }
}

public class TaskMemberMock<T1, T2, T3, T4, T5, T6, T7, TReturn>
    : TaskMemberMockBase<
        TReturn,
        TaskMemberMock<T1, T2, T3, T4, T5, T6, T7, TReturn>,
        Action<T1, T2, T3, T4, T5, T6, T7>,
        Func<T1, T2, T3, T4, T5, T6, T7, Task<TReturn>>
    >
{
    public TaskMemberMock()
        : base(
            new(ActionUtils.CallbackFunc<T1, T2, T3, T4, T5, T6, T7>()),
            FunctionUtils.ToReturnFunc<T1, T2, T3, T4, T5, T6, T7, Task<TReturn>>(),
            ThrowUtils.ToThrow<T1, T2, T3, T4, T5, T6, T7, Task<TReturn>>()
        ) { }
}

public class TaskMemberMock<T1, T2, T3, T4, T5, T6, T7, T8, TReturn>
    : TaskMemberMockBase<
        TReturn,
        TaskMemberMock<T1, T2, T3, T4, T5, T6, T7, T8, TReturn>,
        Action<T1, T2, T3, T4, T5, T6, T7, T8>,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, Task<TReturn>>
    >
{
    public TaskMemberMock()
        : base(
            new(ActionUtils.CallbackFunc<T1, T2, T3, T4, T5, T6, T7, T8>()),
            FunctionUtils.ToReturnFunc<T1, T2, T3, T4, T5, T6, T7, T8, Task<TReturn>>(),
            ThrowUtils.ToThrow<T1, T2, T3, T4, T5, T6, T7, T8, Task<TReturn>>()
        ) { }
}

public class TaskMemberMock<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn>
    : TaskMemberMockBase<
        TReturn,
        TaskMemberMock<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn>,
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9>,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, Task<TReturn>>
    >
{
    public TaskMemberMock()
        : base(
            new(ActionUtils.CallbackFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9>()),
            FunctionUtils.ToReturnFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, Task<TReturn>>(),
            ThrowUtils.ToThrow<T1, T2, T3, T4, T5, T6, T7, T8, T9, Task<TReturn>>()
        ) { }
}

public class TaskMemberMock<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn>
    : TaskMemberMockBase<
        TReturn,
        TaskMemberMock<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn>,
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, Task<TReturn>>
    >
{
    public TaskMemberMock()
        : base(
            new(ActionUtils.CallbackFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>()),
            FunctionUtils.ToReturnFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, Task<TReturn>>(),
            ThrowUtils.ToThrow<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, Task<TReturn>>()
        ) { }
}

public class TaskMemberMock<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn>
    : TaskMemberMockBase<
        TReturn,
        TaskMemberMock<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn>,
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, Task<TReturn>>
    >
{
    public TaskMemberMock()
        : base(
            new(ActionUtils.CallbackFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>()),
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
                Task<TReturn>
            >(),
            ThrowUtils.ToThrow<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, Task<TReturn>>()
        ) { }
}

public class TaskMemberMock<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn>
    : TaskMemberMockBase<
        TReturn,
        TaskMemberMock<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn>,
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, Task<TReturn>>
    >
{
    public TaskMemberMock()
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
                Task<TReturn>
            >(),
            ThrowUtils.ToThrow<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, Task<TReturn>>()
        ) { }
}

public class TaskMemberMock<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn>
    : TaskMemberMockBase<
        TReturn,
        TaskMemberMock<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn>,
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, Task<TReturn>>
    >
{
    public TaskMemberMock()
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
                Task<TReturn>
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
                Task<TReturn>
            >()
        ) { }
}

public class TaskMemberMock<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn>
    : TaskMemberMockBase<
        TReturn,
        TaskMemberMock<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn>,
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, Task<TReturn>>
    >
{
    public TaskMemberMock()
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
                Task<TReturn>
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
                Task<TReturn>
            >()
        ) { }
}

public class TaskMemberMock<
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
>
    : TaskMemberMockBase<
        TReturn,
        TaskMemberMock<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn>,
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, Task<TReturn>>
    >
{
    public TaskMemberMock()
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
                Task<TReturn>
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
                Task<TReturn>
            >()
        ) { }
}
