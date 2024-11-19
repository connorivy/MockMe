namespace MockMe.Mocks.ClassMemberMocks.Setup;

public partial class MemberMockSetup
{
    internal static TMock SetupMemberMockBase<TArgCollection, TMock, TArgBag>(
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

    protected static MemberMock<TReturn> SetupMethod<TReturn>(
        List<MemberMock<TReturn>> mockAndArgsStore
    ) =>
        SetupMemberMockBase<bool, MemberMock<TReturn>, MemberMock<TReturn>>(
            false,
            mockAndArgsStore,
            static (col, mock) => mock
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

    public static MemberMock<TArg1, TArg2, TArg3, TArg4, TReturn> SetupMethod<
        TArg1,
        TArg2,
        TArg3,
        TArg4,
        TReturn
    >(
        List<ArgBagWithMemberMock<TArg1, TArg2, TArg3, TArg4, TReturn>> mockAndArgsStore,
        Arg<TArg1> arg1,
        Arg<TArg2> arg2,
        Arg<TArg3> arg3,
        Arg<TArg4> arg4
    ) =>
        SetupMemberMockBase<
            (Arg<TArg1>, Arg<TArg2>, Arg<TArg3>, Arg<TArg4>),
            MemberMock<TArg1, TArg2, TArg3, TArg4, TReturn>,
            ArgBagWithMemberMock<TArg1, TArg2, TArg3, TArg4, TReturn>
        >(
            (arg1, arg2, arg3, arg4),
            mockAndArgsStore,
            static (col, mock) => new(col.Item1, col.Item2, col.Item3, col.Item4, mock)
        );

    public static MemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TReturn> SetupMethod<
        TArg1,
        TArg2,
        TArg3,
        TArg4,
        TArg5,
        TReturn
    >(
        List<ArgBagWithMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TReturn>> mockAndArgsStore,
        Arg<TArg1> arg1,
        Arg<TArg2> arg2,
        Arg<TArg3> arg3,
        Arg<TArg4> arg4,
        Arg<TArg5> arg5
    ) =>
        SetupMemberMockBase<
            (Arg<TArg1>, Arg<TArg2>, Arg<TArg3>, Arg<TArg4>, Arg<TArg5>),
            MemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TReturn>,
            ArgBagWithMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TReturn>
        >(
            (arg1, arg2, arg3, arg4, arg5),
            mockAndArgsStore,
            static (col, mock) => new(col.Item1, col.Item2, col.Item3, col.Item4, col.Item5, mock)
        );

    public static MemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TReturn> SetupMethod<
        TArg1,
        TArg2,
        TArg3,
        TArg4,
        TArg5,
        TArg6,
        TReturn
    >(
        List<
            ArgBagWithMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TReturn>
        > mockAndArgsStore,
        Arg<TArg1> arg1,
        Arg<TArg2> arg2,
        Arg<TArg3> arg3,
        Arg<TArg4> arg4,
        Arg<TArg5> arg5,
        Arg<TArg6> arg6
    ) =>
        SetupMemberMockBase<
            (Arg<TArg1>, Arg<TArg2>, Arg<TArg3>, Arg<TArg4>, Arg<TArg5>, Arg<TArg6>),
            MemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TReturn>,
            ArgBagWithMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TReturn>
        >(
            (arg1, arg2, arg3, arg4, arg5, arg6),
            mockAndArgsStore,
            static (col, mock) =>
                new(col.Item1, col.Item2, col.Item3, col.Item4, col.Item5, col.Item6, mock)
        );

    public static MemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TReturn> SetupMethod<
        TArg1,
        TArg2,
        TArg3,
        TArg4,
        TArg5,
        TArg6,
        TArg7,
        TReturn
    >(
        List<
            ArgBagWithMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TReturn>
        > mockAndArgsStore,
        Arg<TArg1> arg1,
        Arg<TArg2> arg2,
        Arg<TArg3> arg3,
        Arg<TArg4> arg4,
        Arg<TArg5> arg5,
        Arg<TArg6> arg6,
        Arg<TArg7> arg7
    ) =>
        SetupMemberMockBase<
            (Arg<TArg1>, Arg<TArg2>, Arg<TArg3>, Arg<TArg4>, Arg<TArg5>, Arg<TArg6>, Arg<TArg7>),
            MemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TReturn>,
            ArgBagWithMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TReturn>
        >(
            (arg1, arg2, arg3, arg4, arg5, arg6, arg7),
            mockAndArgsStore,
            static (col, mock) =>
                new(
                    col.Item1,
                    col.Item2,
                    col.Item3,
                    col.Item4,
                    col.Item5,
                    col.Item6,
                    col.Item7,
                    mock
                )
        );

    public static MemberMock<
        TArg1,
        TArg2,
        TArg3,
        TArg4,
        TArg5,
        TArg6,
        TArg7,
        TArg8,
        TReturn
    > SetupMethod<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TReturn>(
        List<
            ArgBagWithMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TReturn>
        > mockAndArgsStore,
        Arg<TArg1> arg1,
        Arg<TArg2> arg2,
        Arg<TArg3> arg3,
        Arg<TArg4> arg4,
        Arg<TArg5> arg5,
        Arg<TArg6> arg6,
        Arg<TArg7> arg7,
        Arg<TArg8> arg8
    ) =>
        SetupMemberMockBase<
            (
                Arg<TArg1>,
                Arg<TArg2>,
                Arg<TArg3>,
                Arg<TArg4>,
                Arg<TArg5>,
                Arg<TArg6>,
                Arg<TArg7>,
                Arg<TArg8>
            ),
            MemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TReturn>,
            ArgBagWithMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TReturn>
        >(
            (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8),
            mockAndArgsStore,
            static (col, mock) =>
                new(
                    col.Item1,
                    col.Item2,
                    col.Item3,
                    col.Item4,
                    col.Item5,
                    col.Item6,
                    col.Item7,
                    col.Item8,
                    mock
                )
        );

    public static MemberMock<
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
    > SetupMethod<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TReturn>(
        List<
            ArgBagWithMemberMock<
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
            >
        > mockAndArgsStore,
        Arg<TArg1> arg1,
        Arg<TArg2> arg2,
        Arg<TArg3> arg3,
        Arg<TArg4> arg4,
        Arg<TArg5> arg5,
        Arg<TArg6> arg6,
        Arg<TArg7> arg7,
        Arg<TArg8> arg8,
        Arg<TArg9> arg9
    ) =>
        SetupMemberMockBase<
            (
                Arg<TArg1>,
                Arg<TArg2>,
                Arg<TArg3>,
                Arg<TArg4>,
                Arg<TArg5>,
                Arg<TArg6>,
                Arg<TArg7>,
                Arg<TArg8>,
                Arg<TArg9>
            ),
            MemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TReturn>,
            ArgBagWithMemberMock<
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
            >
        >(
            (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9),
            mockAndArgsStore,
            static (col, mock) =>
                new(
                    col.Item1,
                    col.Item2,
                    col.Item3,
                    col.Item4,
                    col.Item5,
                    col.Item6,
                    col.Item7,
                    col.Item8,
                    col.Item9,
                    mock
                )
        );

    public static MemberMock<
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
    > SetupMethod<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TReturn>(
        List<
            ArgBagWithMemberMock<
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
        > mockAndArgsStore,
        Arg<TArg1> arg1,
        Arg<TArg2> arg2,
        Arg<TArg3> arg3,
        Arg<TArg4> arg4,
        Arg<TArg5> arg5,
        Arg<TArg6> arg6,
        Arg<TArg7> arg7,
        Arg<TArg8> arg8,
        Arg<TArg9> arg9,
        Arg<TArg10> arg10
    ) =>
        SetupMemberMockBase<
            (
                Arg<TArg1>,
                Arg<TArg2>,
                Arg<TArg3>,
                Arg<TArg4>,
                Arg<TArg5>,
                Arg<TArg6>,
                Arg<TArg7>,
                Arg<TArg8>,
                Arg<TArg9>,
                Arg<TArg10>
            ),
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
                TReturn
            >,
            ArgBagWithMemberMock<
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
        >(
            (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10),
            mockAndArgsStore,
            static (col, mock) =>
                new(
                    col.Item1,
                    col.Item2,
                    col.Item3,
                    col.Item4,
                    col.Item5,
                    col.Item6,
                    col.Item7,
                    col.Item8,
                    col.Item9,
                    col.Item10,
                    mock
                )
        );

    public static MemberMock<
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
    > SetupMethod<
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
    >(
        List<
            ArgBagWithMemberMock<
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
        > mockAndArgsStore,
        Arg<TArg1> arg1,
        Arg<TArg2> arg2,
        Arg<TArg3> arg3,
        Arg<TArg4> arg4,
        Arg<TArg5> arg5,
        Arg<TArg6> arg6,
        Arg<TArg7> arg7,
        Arg<TArg8> arg8,
        Arg<TArg9> arg9,
        Arg<TArg10> arg10,
        Arg<TArg11> arg11
    ) =>
        SetupMemberMockBase<
            (
                Arg<TArg1>,
                Arg<TArg2>,
                Arg<TArg3>,
                Arg<TArg4>,
                Arg<TArg5>,
                Arg<TArg6>,
                Arg<TArg7>,
                Arg<TArg8>,
                Arg<TArg9>,
                Arg<TArg10>,
                Arg<TArg11>
            ),
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
            ArgBagWithMemberMock<
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
        >(
            (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11),
            mockAndArgsStore,
            static (col, mock) =>
                new(
                    col.Item1,
                    col.Item2,
                    col.Item3,
                    col.Item4,
                    col.Item5,
                    col.Item6,
                    col.Item7,
                    col.Item8,
                    col.Item9,
                    col.Item10,
                    col.Item11,
                    mock
                )
        );

    public static MemberMock<
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
    > SetupMethod<
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
    >(
        List<
            ArgBagWithMemberMock<
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
        > mockAndArgsStore,
        Arg<TArg1> arg1,
        Arg<TArg2> arg2,
        Arg<TArg3> arg3,
        Arg<TArg4> arg4,
        Arg<TArg5> arg5,
        Arg<TArg6> arg6,
        Arg<TArg7> arg7,
        Arg<TArg8> arg8,
        Arg<TArg9> arg9,
        Arg<TArg10> arg10,
        Arg<TArg11> arg11,
        Arg<TArg12> arg12
    ) =>
        SetupMemberMockBase<
            (
                Arg<TArg1>,
                Arg<TArg2>,
                Arg<TArg3>,
                Arg<TArg4>,
                Arg<TArg5>,
                Arg<TArg6>,
                Arg<TArg7>,
                Arg<TArg8>,
                Arg<TArg9>,
                Arg<TArg10>,
                Arg<TArg11>,
                Arg<TArg12>
            ),
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
            ArgBagWithMemberMock<
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
        >(
            (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12),
            mockAndArgsStore,
            static (col, mock) =>
                new(
                    col.Item1,
                    col.Item2,
                    col.Item3,
                    col.Item4,
                    col.Item5,
                    col.Item6,
                    col.Item7,
                    col.Item8,
                    col.Item9,
                    col.Item10,
                    col.Item11,
                    col.Item12,
                    mock
                )
        );

    public static MemberMock<
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
    > SetupMethod<
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
    >(
        List<
            ArgBagWithMemberMock<
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
        > mockAndArgsStore,
        Arg<TArg1> arg1,
        Arg<TArg2> arg2,
        Arg<TArg3> arg3,
        Arg<TArg4> arg4,
        Arg<TArg5> arg5,
        Arg<TArg6> arg6,
        Arg<TArg7> arg7,
        Arg<TArg8> arg8,
        Arg<TArg9> arg9,
        Arg<TArg10> arg10,
        Arg<TArg11> arg11,
        Arg<TArg12> arg12,
        Arg<TArg13> arg13
    ) =>
        SetupMemberMockBase<
            (
                Arg<TArg1> arg1,
                Arg<TArg2> arg2,
                Arg<TArg3> arg3,
                Arg<TArg4> arg4,
                Arg<TArg5> arg5,
                Arg<TArg6> arg6,
                Arg<TArg7> arg7,
                Arg<TArg8> arg8,
                Arg<TArg9> arg9,
                Arg<TArg10> arg10,
                Arg<TArg11> arg11,
                Arg<TArg12> arg12,
                Arg<TArg13> arg13
            ),
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
            ArgBagWithMemberMock<
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
        >(
            (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13),
            mockAndArgsStore,
            static (col, mock) =>
                new(
                    col.Item1,
                    col.Item2,
                    col.Item3,
                    col.Item4,
                    col.Item5,
                    col.Item6,
                    col.Item7,
                    col.Item8,
                    col.Item9,
                    col.Item10,
                    col.Item11,
                    col.Item12,
                    col.Item13,
                    mock
                )
        );

    public static MemberMock<
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
    > SetupMethod<
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
    >(
        List<
            ArgBagWithMemberMock<
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
        > mockAndArgsStore,
        Arg<TArg1> arg1,
        Arg<TArg2> arg2,
        Arg<TArg3> arg3,
        Arg<TArg4> arg4,
        Arg<TArg5> arg5,
        Arg<TArg6> arg6,
        Arg<TArg7> arg7,
        Arg<TArg8> arg8,
        Arg<TArg9> arg9,
        Arg<TArg10> arg10,
        Arg<TArg11> arg11,
        Arg<TArg12> arg12,
        Arg<TArg13> arg13,
        Arg<TArg14> arg14
    ) =>
        SetupMemberMockBase<
            (
                Arg<TArg1> arg1,
                Arg<TArg2> arg2,
                Arg<TArg3> arg3,
                Arg<TArg4> arg4,
                Arg<TArg5> arg5,
                Arg<TArg6> arg6,
                Arg<TArg7> arg7,
                Arg<TArg8> arg8,
                Arg<TArg9> arg9,
                Arg<TArg10> arg10,
                Arg<TArg11> arg11,
                Arg<TArg12> arg12,
                Arg<TArg13> arg13,
                Arg<TArg14> arg14
            ),
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
            ArgBagWithMemberMock<
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
        >(
            (
                arg1,
                arg2,
                arg3,
                arg4,
                arg5,
                arg6,
                arg7,
                arg8,
                arg9,
                arg10,
                arg11,
                arg12,
                arg13,
                arg14
            ),
            mockAndArgsStore,
            static (col, mock) =>
                new(
                    col.Item1,
                    col.Item2,
                    col.Item3,
                    col.Item4,
                    col.Item5,
                    col.Item6,
                    col.Item7,
                    col.Item8,
                    col.Item9,
                    col.Item10,
                    col.Item11,
                    col.Item12,
                    col.Item13,
                    col.Item14,
                    mock
                )
        );

    public static MemberMock<
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
    > SetupMethod<
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
    >(
        List<
            ArgBagWithMemberMock<
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
        > mockAndArgsStore,
        Arg<TArg1> arg1,
        Arg<TArg2> arg2,
        Arg<TArg3> arg3,
        Arg<TArg4> arg4,
        Arg<TArg5> arg5,
        Arg<TArg6> arg6,
        Arg<TArg7> arg7,
        Arg<TArg8> arg8,
        Arg<TArg9> arg9,
        Arg<TArg10> arg10,
        Arg<TArg11> arg11,
        Arg<TArg12> arg12,
        Arg<TArg13> arg13,
        Arg<TArg14> arg14,
        Arg<TArg15> arg15
    ) =>
        SetupMemberMockBase<
            (
                Arg<TArg1> arg1,
                Arg<TArg2> arg2,
                Arg<TArg3> arg3,
                Arg<TArg4> arg4,
                Arg<TArg5> arg5,
                Arg<TArg6> arg6,
                Arg<TArg7> arg7,
                Arg<TArg8> arg8,
                Arg<TArg9> arg9,
                Arg<TArg10> arg10,
                Arg<TArg11> arg11,
                Arg<TArg12> arg12,
                Arg<TArg13> arg13,
                Arg<TArg14> arg14,
                Arg<TArg15> arg15
            ),
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
            ArgBagWithMemberMock<
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
        >(
            (
                arg1,
                arg2,
                arg3,
                arg4,
                arg5,
                arg6,
                arg7,
                arg8,
                arg9,
                arg10,
                arg11,
                arg12,
                arg13,
                arg14,
                arg15
            ),
            mockAndArgsStore,
            static (col, mock) =>
                new(
                    col.Item1,
                    col.Item2,
                    col.Item3,
                    col.Item4,
                    col.Item5,
                    col.Item6,
                    col.Item7,
                    col.Item8,
                    col.Item9,
                    col.Item10,
                    col.Item11,
                    col.Item12,
                    col.Item13,
                    col.Item14,
                    col.Item15,
                    mock
                )
        );
}
