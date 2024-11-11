using MockMe.Extensions;

namespace MockMe.Mocks.ClassMemberMocks;

public class TaskMemberMock<TReturn> : MemberMock<Task<TReturn>>
{
    public TaskMemberMock<TReturn> ReturnsAsync(
        TReturn returnThis,
        params TReturn[] thenReturnThese
    )
    {
        this.Returns(
            Task.FromResult(returnThis),
            thenReturnThese.Select(ret => Task.FromResult(ret)).ToArray()
        );
        return this;
    }
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
            new(ActionExtensions.CallbackFunc<TArg1>()),
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
            new(ActionExtensions.CallbackFunc<TArg1, TArg2>()),
            FunctionUtils.ToReturnFunc<TArg1, TArg2, Task<TReturn>>()
        ) { }
}
