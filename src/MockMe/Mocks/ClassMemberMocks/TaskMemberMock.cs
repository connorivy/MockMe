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
        Func<Task<TReturn>, TReturnFunc> toReturnCall
    )
        : base(callbackManager, toReturnCall) { }

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
        : base(new(ActionUtils.CallbackFunc()), static task => task) { }
}

public class TaskMemberMock<TArg1, TReturn>
    : TaskMemberMockBase<
        TReturn,
        TaskMemberMock<TArg1, TReturn>,
        Action<TArg1>,
        Func<TArg1, Task<TReturn>>
    >
{
    public TaskMemberMock()
        : base(
            new(ActionUtils.CallbackFunc<TArg1>()),
            FunctionUtils.ToReturnFunc<TArg1, Task<TReturn>>()
        ) { }
}

public class TaskMemberMock<TArg1, TArg2, TReturn>
    : TaskMemberMockBase<
        TReturn,
        TaskMemberMock<TArg1, TArg2, TReturn>,
        Action<TArg1, TArg2>,
        Func<TArg1, TArg2, Task<TReturn>>
    >
{
    public TaskMemberMock()
        : base(
            new(ActionUtils.CallbackFunc<TArg1, TArg2>()),
            FunctionUtils.ToReturnFunc<TArg1, TArg2, Task<TReturn>>()
        ) { }
}

public class TaskMemberMock<TArg1, TArg2, TArg3, TReturn>
    : TaskMemberMockBase<
        TReturn,
        TaskMemberMock<TArg1, TArg2, TArg3, TReturn>,
        Action<TArg1, TArg2, TArg3>,
        Func<TArg1, TArg2, TArg3, Task<TReturn>>
    >
{
    public TaskMemberMock()
        : base(
            new(ActionUtils.CallbackFunc<TArg1, TArg2, TArg3>()),
            FunctionUtils.ToReturnFunc<TArg1, TArg2, TArg3, Task<TReturn>>()
        ) { }
}

public class TaskMemberMock<TArg1, TArg2, TArg3, TArg4, TReturn>
    : TaskMemberMockBase<
        TReturn,
        TaskMemberMock<TArg1, TArg2, TArg3, TArg4, TReturn>,
        Action<TArg1, TArg2, TArg3, TArg4>,
        Func<TArg1, TArg2, TArg3, TArg4, Task<TReturn>>
    >
{
    public TaskMemberMock()
        : base(
            new(ActionUtils.CallbackFunc<TArg1, TArg2, TArg3, TArg4>()),
            FunctionUtils.ToReturnFunc<TArg1, TArg2, TArg3, TArg4, Task<TReturn>>()
        ) { }
}

public class TaskMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TReturn>
    : TaskMemberMockBase<
        TReturn,
        TaskMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TReturn>,
        Action<TArg1, TArg2, TArg3, TArg4, TArg5>,
        Func<TArg1, TArg2, TArg3, TArg4, TArg5, Task<TReturn>>
    >
{
    public TaskMemberMock()
        : base(
            new(ActionUtils.CallbackFunc<TArg1, TArg2, TArg3, TArg4, TArg5>()),
            FunctionUtils.ToReturnFunc<TArg1, TArg2, TArg3, TArg4, TArg5, Task<TReturn>>()
        ) { }
}

public class TaskMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TReturn>
    : TaskMemberMockBase<
        TReturn,
        TaskMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TReturn>,
        Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>,
        Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, Task<TReturn>>
    >
{
    public TaskMemberMock()
        : base(
            new(ActionUtils.CallbackFunc<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>()),
            FunctionUtils.ToReturnFunc<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, Task<TReturn>>()
        ) { }
}

public class TaskMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TReturn>
    : TaskMemberMockBase<
        TReturn,
        TaskMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TReturn>,
        Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>,
        Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, Task<TReturn>>
    >
{
    public TaskMemberMock()
        : base(
            new(ActionUtils.CallbackFunc<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>()),
            FunctionUtils.ToReturnFunc<
                TArg1,
                TArg2,
                TArg3,
                TArg4,
                TArg5,
                TArg6,
                TArg7,
                Task<TReturn>
            >()
        ) { }
}

public class TaskMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TReturn>
    : TaskMemberMockBase<
        TReturn,
        TaskMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TReturn>,
        Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>,
        Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, Task<TReturn>>
    >
{
    public TaskMemberMock()
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
                Task<TReturn>
            >()
        ) { }
}

public class TaskMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TReturn>
    : TaskMemberMockBase<
        TReturn,
        TaskMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TReturn>,
        Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>,
        Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, Task<TReturn>>
    >
{
    public TaskMemberMock()
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
                Task<TReturn>
            >()
        ) { }
}

public class TaskMemberMock<
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
    : TaskMemberMockBase<
        TReturn,
        TaskMemberMock<
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
        >,
        Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10>,
        Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, Task<TReturn>>
    >
{
    public TaskMemberMock()
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
                Task<TReturn>
            >()
        ) { }
}

public class TaskMemberMock<
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
    : TaskMemberMockBase<
        TReturn,
        TaskMemberMock<
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
            Task<TReturn>
        >
    >
{
    public TaskMemberMock()
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
                Task<TReturn>
            >()
        ) { }
}

public class TaskMemberMock<
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
    : TaskMemberMockBase<
        TReturn,
        TaskMemberMock<
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
            Task<TReturn>
        >
    >
{
    public TaskMemberMock()
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
                Task<TReturn>
            >()
        ) { }
}

public class TaskMemberMock<
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
    : TaskMemberMockBase<
        TReturn,
        TaskMemberMock<
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
            Task<TReturn>
        >
    >
{
    public TaskMemberMock()
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
                Task<TReturn>
            >()
        ) { }
}

public class TaskMemberMock<
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
    : TaskMemberMockBase<
        TReturn,
        TaskMemberMock<
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
            Task<TReturn>
        >
    >
{
    public TaskMemberMock()
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
                Task<TReturn>
            >()
        ) { }
}

public class TaskMemberMock<
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
    : TaskMemberMockBase<
        TReturn,
        TaskMemberMock<
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
            Task<TReturn>
        >
    >
{
    public TaskMemberMock()
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
                Task<TReturn>
            >()
        ) { }
}
