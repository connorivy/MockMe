namespace MockMe.Mocks.ClassMemberMocks.Setup;

public partial class MemberMockSetup
{
    protected static TaskMemberMock<TReturn> SetupTaskMethod<TReturn>(
        List<TaskMemberMock<TReturn>> mockAndArgsStore
    ) =>
        SetupMemberMockBase<bool, TaskMemberMock<TReturn>, TaskMemberMock<TReturn>>(
            false,
            mockAndArgsStore,
            static (col, mock) => mock
        );

    protected static TaskMemberMock<TArg1, TReturn> SetupTaskMethod<TArg1, TReturn>(
        List<ArgBagWithMemberMock<TArg1, Task<TReturn>>> mockAndArgsStore,
        Arg<TArg1> arg1
    ) =>
        SetupMemberMockBase<
            Arg<TArg1>,
            TaskMemberMock<TArg1, TReturn>,
            ArgBagWithMemberMock<TArg1, Task<TReturn>>
        >(arg1, mockAndArgsStore, static (col, mock) => new(col, mock));

    protected static TaskMemberMock<TArg1, TArg2, TReturn> SetupTaskMethod<TArg1, TArg2, TReturn>(
        List<ArgBagWithMemberMock<TArg1, TArg2, Task<TReturn>>> mockAndArgsStore,
        Arg<TArg1> arg1,
        Arg<TArg2> arg2
    ) =>
        SetupMemberMockBase<
            ValueTuple<Arg<TArg1>, Arg<TArg2>>,
            TaskMemberMock<TArg1, TArg2, TReturn>,
            ArgBagWithMemberMock<TArg1, TArg2, Task<TReturn>>
        >((arg1, arg2), mockAndArgsStore, static (col, mock) => new(col.Item1, col.Item2, mock));
}
