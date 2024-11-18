namespace MockMe.Mocks.ClassMemberMocks.CallTracker;

public static class VoidMockCallTracker
{
    public static void CallVoidMemberMock(VoidMemberMock? mockStore)
    {
        if (mockStore is null)
        {
            return;
        }

        _ = MockCallTracker.CallMemberMockBase<bool, bool, Action, bool>(
            mockStore,
            false,
            static (_, action) => action()
        );
    }

    private static void CallVoidMemberMockBase<TCollection, TCallback>(
        IReadOnlyList<IArgBag<TCollection, TCallback>>? mockStore,
        List<TCollection> argStore,
        TCollection argCollection,
        Action<TCollection, TCallback> callbackAction
    ) =>
        MockCallTracker.FindAndCallApplicableMemberMock<bool, TCollection, TCallback, bool>(
            mockStore,
            argStore,
            argCollection,
            callbackAction
        );

    public static void CallVoidMemberMock<T1>(
        List<ArgBagWithVoidMemberMock<T1>>? mockStore,
        List<T1> argStore,
        T1 arg1
    ) => CallVoidMemberMockBase(mockStore, argStore, arg1, (col, callback) => callback(col));

    public static void CallVoidMemberMock<T1, T2>(
        List<ArgBagWithVoidMemberMock<T1, T2>>? mockStore,
        List<(T1, T2)> argStore,
        T1 arg1,
        T2 arg2
    ) =>
        CallVoidMemberMockBase(
            mockStore,
            argStore,
            (arg1, arg2),
            (col, callback) => callback(col.Item1, col.Item2)
        );

    public static void CallVoidMemberMock<T1, T2, T3>(
        List<ArgBagWithVoidMemberMock<T1, T2, T3>>? mockStore,
        List<(T1, T2, T3)> argStore,
        T1 arg1,
        T2 arg2,
        T3 arg3
    ) =>
        CallVoidMemberMockBase(
            mockStore,
            argStore,
            (arg1, arg2, arg3),
            (col, callback) => callback(col.Item1, col.Item2, col.Item3)
        );

    public static void CallVoidMemberMock<T1, T2, T3, T4>(
        List<ArgBagWithVoidMemberMock<T1, T2, T3, T4>>? mockStore,
        List<(T1, T2, T3, T4)> argStore,
        T1 arg1,
        T2 arg2,
        T3 arg3,
        T4 arg4
    ) =>
        CallVoidMemberMockBase(
            mockStore,
            argStore,
            (arg1, arg2, arg3, arg4),
            (col, callback) => callback(col.Item1, col.Item2, col.Item3, col.Item4)
        );

    public static void CallVoidMemberMock<T1, T2, T3, T4, T5>(
        List<ArgBagWithVoidMemberMock<T1, T2, T3, T4, T5>>? mockStore,
        List<(T1, T2, T3, T4, T5)> argStore,
        T1 arg1,
        T2 arg2,
        T3 arg3,
        T4 arg4,
        T5 arg5
    ) =>
        CallVoidMemberMockBase(
            mockStore,
            argStore,
            (arg1, arg2, arg3, arg4, arg5),
            (col, callback) => callback(col.Item1, col.Item2, col.Item3, col.Item4, col.Item5)
        );

    public static void CallVoidMemberMock<T1, T2, T3, T4, T5, T6>(
        List<ArgBagWithVoidMemberMock<T1, T2, T3, T4, T5, T6>>? mockStore,
        List<(T1, T2, T3, T4, T5, T6)> argStore,
        T1 arg1,
        T2 arg2,
        T3 arg3,
        T4 arg4,
        T5 arg5,
        T6 arg6
    ) =>
        CallVoidMemberMockBase(
            mockStore,
            argStore,
            (arg1, arg2, arg3, arg4, arg5, arg6),
            (col, callback) =>
                callback(col.Item1, col.Item2, col.Item3, col.Item4, col.Item5, col.Item6)
        );

    public static void CallVoidMemberMock<T1, T2, T3, T4, T5, T6, T7>(
        List<ArgBagWithVoidMemberMock<T1, T2, T3, T4, T5, T6, T7>>? mockStore,
        List<(T1, T2, T3, T4, T5, T6, T7)> argStore,
        T1 arg1,
        T2 arg2,
        T3 arg3,
        T4 arg4,
        T5 arg5,
        T6 arg6,
        T7 arg7
    ) =>
        CallVoidMemberMockBase(
            mockStore,
            argStore,
            (arg1, arg2, arg3, arg4, arg5, arg6, arg7),
            (col, callback) =>
                callback(
                    col.Item1,
                    col.Item2,
                    col.Item3,
                    col.Item4,
                    col.Item5,
                    col.Item6,
                    col.Item7
                )
        );

    public static void CallVoidMemberMock<T1, T2, T3, T4, T5, T6, T7, T8>(
        List<ArgBagWithVoidMemberMock<T1, T2, T3, T4, T5, T6, T7, T8>>? mockStore,
        List<(T1, T2, T3, T4, T5, T6, T7, T8)> argStore,
        T1 arg1,
        T2 arg2,
        T3 arg3,
        T4 arg4,
        T5 arg5,
        T6 arg6,
        T7 arg7,
        T8 arg8
    ) =>
        CallVoidMemberMockBase(
            mockStore,
            argStore,
            (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8),
            (col, callback) =>
                callback(
                    col.Item1,
                    col.Item2,
                    col.Item3,
                    col.Item4,
                    col.Item5,
                    col.Item6,
                    col.Item7,
                    col.Item8
                )
        );

    public static void CallVoidMemberMock<T1, T2, T3, T4, T5, T6, T7, T8, T9>(
        List<ArgBagWithVoidMemberMock<T1, T2, T3, T4, T5, T6, T7, T8, T9>>? mockStore,
        List<(T1, T2, T3, T4, T5, T6, T7, T8, T9)> argStore,
        T1 arg1,
        T2 arg2,
        T3 arg3,
        T4 arg4,
        T5 arg5,
        T6 arg6,
        T7 arg7,
        T8 arg8,
        T9 arg9
    ) =>
        CallVoidMemberMockBase(
            mockStore,
            argStore,
            (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9),
            (col, callback) =>
                callback(
                    col.Item1,
                    col.Item2,
                    col.Item3,
                    col.Item4,
                    col.Item5,
                    col.Item6,
                    col.Item7,
                    col.Item8,
                    col.Item9
                )
        );

    public static void CallVoidMemberMock<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(
        List<ArgBagWithVoidMemberMock<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>>? mockStore,
        List<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)> argStore,
        T1 arg1,
        T2 arg2,
        T3 arg3,
        T4 arg4,
        T5 arg5,
        T6 arg6,
        T7 arg7,
        T8 arg8,
        T9 arg9,
        T10 arg10
    ) =>
        CallVoidMemberMockBase(
            mockStore,
            argStore,
            (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10),
            (col, callback) =>
                callback(
                    col.Item1,
                    col.Item2,
                    col.Item3,
                    col.Item4,
                    col.Item5,
                    col.Item6,
                    col.Item7,
                    col.Item8,
                    col.Item9,
                    col.Item10
                )
        );

    public static void CallVoidMemberMock<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(
        List<ArgBagWithVoidMemberMock<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>>? mockStore,
        List<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11)> argStore,
        T1 arg1,
        T2 arg2,
        T3 arg3,
        T4 arg4,
        T5 arg5,
        T6 arg6,
        T7 arg7,
        T8 arg8,
        T9 arg9,
        T10 arg10,
        T11 arg11
    ) =>
        CallVoidMemberMockBase(
            mockStore,
            argStore,
            (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11),
            (col, callback) =>
                callback(
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
                    col.Item11
                )
        );

    public static void CallVoidMemberMock<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(
        List<
            ArgBagWithVoidMemberMock<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>
        >? mockStore,
        List<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12)> argStore,
        T1 arg1,
        T2 arg2,
        T3 arg3,
        T4 arg4,
        T5 arg5,
        T6 arg6,
        T7 arg7,
        T8 arg8,
        T9 arg9,
        T10 arg10,
        T11 arg11,
        T12 arg12
    ) =>
        CallVoidMemberMockBase(
            mockStore,
            argStore,
            (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12),
            (col, callback) =>
                callback(
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
                    col.Item12
                )
        );

    public static void CallVoidMemberMock<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(
        List<
            ArgBagWithVoidMemberMock<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>
        >? mockStore,
        List<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13)> argStore,
        T1 arg1,
        T2 arg2,
        T3 arg3,
        T4 arg4,
        T5 arg5,
        T6 arg6,
        T7 arg7,
        T8 arg8,
        T9 arg9,
        T10 arg10,
        T11 arg11,
        T12 arg12,
        T13 arg13
    ) =>
        CallVoidMemberMockBase(
            mockStore,
            argStore,
            (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13),
            (col, callback) =>
                callback(
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
                    col.Item13
                )
        );

    public static void CallVoidMemberMock<
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
    >(
        List<
            ArgBagWithVoidMemberMock<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>
        >? mockStore,
        List<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)> argStore,
        T1 arg1,
        T2 arg2,
        T3 arg3,
        T4 arg4,
        T5 arg5,
        T6 arg6,
        T7 arg7,
        T8 arg8,
        T9 arg9,
        T10 arg10,
        T11 arg11,
        T12 arg12,
        T13 arg13,
        T14 arg14
    ) =>
        CallVoidMemberMockBase(
            mockStore,
            argStore,
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
            (col, callback) =>
                callback(
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
                    col.Item14
                )
        );

    public static void CallVoidMemberMock<
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
    >(
        List<
            ArgBagWithVoidMemberMock<
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
            >
        >? mockStore,
        List<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15)> argStore,
        T1 arg1,
        T2 arg2,
        T3 arg3,
        T4 arg4,
        T5 arg5,
        T6 arg6,
        T7 arg7,
        T8 arg8,
        T9 arg9,
        T10 arg10,
        T11 arg11,
        T12 arg12,
        T13 arg13,
        T14 arg14,
        T15 arg15
    ) =>
        CallVoidMemberMockBase(
            mockStore,
            argStore,
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
            (col, callback) =>
                callback(
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
                    col.Item15
                )
        );
}
