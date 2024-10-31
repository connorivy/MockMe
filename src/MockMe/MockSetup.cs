using MockMe.Mocks;

namespace MockMe;

public class MockSetup
{
    protected static VoidMemberMock SetupVoidMethod(List<VoidMemberMock> mockAndArgsStore) =>
        SetupMemberMockBase<int, VoidMemberMock, VoidMemberMock>(
            0,
            mockAndArgsStore,
            static (col, mock) => mock
        );

    protected static MemberMock<TReturn> SetupMethod<TReturn>(
        List<MemberMock<TReturn>> mockAndArgsStore
    ) =>
        SetupMemberMockBase<int, MemberMock<TReturn>, MemberMock<TReturn>>(
            0,
            mockAndArgsStore,
            static (col, mock) => mock
        );

    protected static VoidMemberMock<TArg1> SetupVoidMethod<TArg1>(
        List<ArgBagWithVoidMemberMock<TArg1>> mockAndArgsStore,
        Arg<TArg1> arg1
    ) =>
        SetupMemberMockBase<Arg<TArg1>, VoidMemberMock<TArg1>, ArgBagWithVoidMemberMock<TArg1>>(
            arg1,
            mockAndArgsStore,
            static (col, mock) => new(col, mock)
        );

    protected static MemberMock<TArg1, TReturn> SetupMethod<TArg1, TReturn>(
        List<ArgBagWithMemberMock<TArg1, TReturn>> mockAndArgsStore,
        Arg<TArg1> arg1
    ) =>
        SetupMemberMockBase<
            Arg<TArg1>,
            MemberMock<TArg1, TReturn>,
            ArgBagWithMemberMock<TArg1, TReturn>
        >(arg1, mockAndArgsStore, static (col, mock) => new(col, mock));

    //protected static VoidMemberMock<TArg1, TArg2> SetupVoidMethod<TArg1, TArg2>(
    //    List<VoidArgBag<TArg1, TArg2>> mockAndArgsStore,
    //    Arg<TArg1> arg1
    //) =>
    //    SetupMemberMockBase<Arg<TArg1>, VoidMemberMock<TArg1, TArg2>, VoidArgBag<TArg1, TArg2>>(
    //        arg1,
    //        mockAndArgsStore,
    //        (col, mock) => new(col, mock)
    //    );

    protected static MemberMock<TArg1, TArg2, TReturn> SetupMethod<TArg1, TArg2, TReturn>(
        List<ArgBagWithMemberMock<TArg1, TArg2, TReturn>> mockAndArgsStore,
        Arg<TArg1> arg1,
        Arg<TArg2> arg2
    ) =>
        SetupMemberMockBase<
            ValueTuple<Arg<TArg1>, Arg<TArg2>>,
            MemberMock<TArg1, TArg2, TReturn>,
            ArgBagWithMemberMock<TArg1, TArg2, TReturn>
        >((arg1, arg2), mockAndArgsStore, static (col, mock) => new(col.Item1, col.Item2, mock));

    protected static MemberMock<TArg1, TArg2, TArg3, TReturn> SetupMethod<
        TArg1,
        TArg2,
        TArg3,
        TReturn
    >(
        List<ArgBagWithMemberMock<TArg1, TArg2, TArg3, TReturn>> mockAndArgsStore,
        Arg<TArg1> arg1,
        Arg<TArg2> arg2,
        Arg<TArg3> arg3
    ) =>
        SetupMemberMockBase<
            ValueTuple<Arg<TArg1>, Arg<TArg2>, Arg<TArg3>>,
            MemberMock<TArg1, TArg2, TArg3, TReturn>,
            ArgBagWithMemberMock<TArg1, TArg2, TArg3, TReturn>
        >(
            (arg1, arg2, arg3),
            mockAndArgsStore,
            static (col, mock) => new(col.Item1, col.Item2, col.Item3, mock)
        );

    private static TMock SetupMemberMockBase<TArgCollection, TMock, TArgBag>(
        TArgCollection argCollection,
        ICollection<TArgBag> mockAndArgsStore,
        Func<TArgCollection, TMock, TArgBag> argBagFactory
    )
        where TMock : new()
    {
        TMock memberMock = new();
        mockAndArgsStore.Add(argBagFactory(argCollection, memberMock));
        return memberMock;
    }
}
