namespace MockMe.Mocks.ClassMemberMocks.Setup;

public partial class MemberMockSetup
{
    public static VoidMemberMock SetupVoidMethod(List<VoidMemberMock> mockAndArgsStore) =>
        SetupMemberMockBase<bool, VoidMemberMock, VoidMemberMock>(
            false,
            mockAndArgsStore,
            static (col, mock) => mock
        );

    public static VoidMemberMock<TArg1> SetupVoidMethod<TArg1>(
        List<ArgBagWithVoidMemberMock<TArg1>> mockAndArgsStore,
        Arg<TArg1> arg1
    ) =>
        SetupMemberMockBase<Arg<TArg1>, VoidMemberMock<TArg1>, ArgBagWithVoidMemberMock<TArg1>>(
            arg1,
            mockAndArgsStore,
            static (col, mock) => new(col, mock)
        );

    public static VoidMemberMock<TArg1, TArg2> SetupVoidMethod<TArg1, TArg2>(
        List<ArgBagWithVoidMemberMock<TArg1, TArg2>> mockAndArgsStore,
        Arg<TArg1> arg1,
        Arg<TArg2> arg2
    ) =>
        SetupMemberMockBase<
            (Arg<TArg1>, Arg<TArg2>),
            VoidMemberMock<TArg1, TArg2>,
            ArgBagWithVoidMemberMock<TArg1, TArg2>
        >((arg1, arg2), mockAndArgsStore, static (col, mock) => new(col.Item1, col.Item2, mock));

    public static VoidMemberMock<TArg1, TArg2, TArg3> SetupVoidMethod<TArg1, TArg2, TArg3>(
        List<ArgBagWithVoidMemberMock<TArg1, TArg2, TArg3>> mockAndArgsStore,
        Arg<TArg1> arg1,
        Arg<TArg2> arg2,
        Arg<TArg3> arg3
    ) =>
        SetupMemberMockBase<
            (Arg<TArg1>, Arg<TArg2>, Arg<TArg3>),
            VoidMemberMock<TArg1, TArg2, TArg3>,
            ArgBagWithVoidMemberMock<TArg1, TArg2, TArg3>
        >(
            (arg1, arg2, arg3),
            mockAndArgsStore,
            static (col, mock) => new(col.Item1, col.Item2, col.Item3, mock)
        );

    public static VoidMemberMock<TArg1, TArg2, TArg3, TArg4> SetupVoidMethod<
        TArg1,
        TArg2,
        TArg3,
        TArg4
    >(
        List<ArgBagWithVoidMemberMock<TArg1, TArg2, TArg3, TArg4>> mockAndArgsStore,
        Arg<TArg1> arg1,
        Arg<TArg2> arg2,
        Arg<TArg3> arg3,
        Arg<TArg4> arg4
    ) =>
        SetupMemberMockBase<
            (Arg<TArg1>, Arg<TArg2>, Arg<TArg3>, Arg<TArg4>),
            VoidMemberMock<TArg1, TArg2, TArg3, TArg4>,
            ArgBagWithVoidMemberMock<TArg1, TArg2, TArg3, TArg4>
        >(
            (arg1, arg2, arg3, arg4),
            mockAndArgsStore,
            static (col, mock) => new(col.Item1, col.Item2, col.Item3, col.Item4, mock)
        );

    public static VoidMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5> SetupVoidMethod<
        TArg1,
        TArg2,
        TArg3,
        TArg4,
        TArg5
    >(
        List<ArgBagWithVoidMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5>> mockAndArgsStore,
        Arg<TArg1> arg1,
        Arg<TArg2> arg2,
        Arg<TArg3> arg3,
        Arg<TArg4> arg4,
        Arg<TArg5> arg5
    ) =>
        SetupMemberMockBase<
            (Arg<TArg1>, Arg<TArg2>, Arg<TArg3>, Arg<TArg4>, Arg<TArg5>),
            VoidMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5>,
            ArgBagWithVoidMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5>
        >(
            (arg1, arg2, arg3, arg4, arg5),
            mockAndArgsStore,
            static (col, mock) => new(col.Item1, col.Item2, col.Item3, col.Item4, col.Item5, mock)
        );

    public static VoidMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6> SetupVoidMethod<
        TArg1,
        TArg2,
        TArg3,
        TArg4,
        TArg5,
        TArg6
    >(
        List<ArgBagWithVoidMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>> mockAndArgsStore,
        Arg<TArg1> arg1,
        Arg<TArg2> arg2,
        Arg<TArg3> arg3,
        Arg<TArg4> arg4,
        Arg<TArg5> arg5,
        Arg<TArg6> arg6
    ) =>
        SetupMemberMockBase<
            (Arg<TArg1>, Arg<TArg2>, Arg<TArg3>, Arg<TArg4>, Arg<TArg5>, Arg<TArg6>),
            VoidMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>,
            ArgBagWithVoidMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>
        >(
            (arg1, arg2, arg3, arg4, arg5, arg6),
            mockAndArgsStore,
            static (col, mock) =>
                new(col.Item1, col.Item2, col.Item3, col.Item4, col.Item5, col.Item6, mock)
        );

    public static VoidMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7> SetupVoidMethod<
        TArg1,
        TArg2,
        TArg3,
        TArg4,
        TArg5,
        TArg6,
        TArg7
    >(
        List<
            ArgBagWithVoidMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>
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
            (
                Arg<TArg1> arg1,
                Arg<TArg2> arg2,
                Arg<TArg3> arg3,
                Arg<TArg4> arg4,
                Arg<TArg5> arg5,
                Arg<TArg6> arg6,
                Arg<TArg7> arg7
            ),
            VoidMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>,
            ArgBagWithVoidMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>
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

    public static VoidMemberMock<
        TArg1,
        TArg2,
        TArg3,
        TArg4,
        TArg5,
        TArg6,
        TArg7,
        TArg8
    > SetupVoidMethod<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(
        List<
            ArgBagWithVoidMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>
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
                Arg<TArg1> arg1,
                Arg<TArg2> arg2,
                Arg<TArg3> arg3,
                Arg<TArg4> arg4,
                Arg<TArg5> arg5,
                Arg<TArg6> arg6,
                Arg<TArg7> arg7,
                Arg<TArg8> arg8
            ),
            VoidMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>,
            ArgBagWithVoidMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>
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

    public static VoidMemberMock<
        TArg1,
        TArg2,
        TArg3,
        TArg4,
        TArg5,
        TArg6,
        TArg7,
        TArg8,
        TArg9
    > SetupVoidMethod<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>(
        List<
            ArgBagWithVoidMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>
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
                Arg<TArg1> arg1,
                Arg<TArg2> arg2,
                Arg<TArg3> arg3,
                Arg<TArg4> arg4,
                Arg<TArg5> arg5,
                Arg<TArg6> arg6,
                Arg<TArg7> arg7,
                Arg<TArg8> arg8,
                Arg<TArg9> arg9
            ),
            VoidMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>,
            ArgBagWithVoidMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>
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

    public static VoidMemberMock<
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
    > SetupVoidMethod<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10>(
        List<
            ArgBagWithVoidMemberMock<
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
            ),
            VoidMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10>,
            ArgBagWithVoidMemberMock<
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

    public static VoidMemberMock<
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
    > SetupVoidMethod<
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
    >(
        List<
            ArgBagWithVoidMemberMock<
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
            ),
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
            ArgBagWithVoidMemberMock<
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

    public static VoidMemberMock<
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
    > SetupVoidMethod<
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
    >(
        List<
            ArgBagWithVoidMemberMock<
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
            ),
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
            ArgBagWithVoidMemberMock<
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

    public static VoidMemberMock<
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
    > SetupVoidMethod<
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
    >(
        List<
            ArgBagWithVoidMemberMock<
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
            ArgBagWithVoidMemberMock<
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

    public static VoidMemberMock<
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
    > SetupVoidMethod<
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
    >(
        List<
            ArgBagWithVoidMemberMock<
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
            ArgBagWithVoidMemberMock<
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

    public static VoidMemberMock<
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
    > SetupVoidMethod<
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
    >(
        List<
            ArgBagWithVoidMemberMock<
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
            ArgBagWithVoidMemberMock<
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
