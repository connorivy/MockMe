namespace MockMe.Extensions;

internal static class FunctionUtils
{
    public static Func<TReturn, Func<TReturn>> ToReturnFunc<TReturn>() => val => () => val;

    public static Func<TReturn, Func<T1, TReturn>> ToReturnFunc<T1, TReturn>() => val => (_) => val;

    public static Func<TReturn, Func<T1, T2, TReturn>> ToReturnFunc<T1, T2, TReturn>() =>
        val => (_, _) => val;

    public static Func<TReturn, Func<T1, T2, T3, TReturn>> ToReturnFunc<T1, T2, T3, TReturn>() =>
        val => (_, _, _) => val;

    public static Func<TReturn, Func<T1, T2, T3, T4, TReturn>> ToReturnFunc<
        T1,
        T2,
        T3,
        T4,
        TReturn
    >() => val => (_, _, _, _) => val;

    public static Func<TReturn, Func<T1, T2, T3, T4, T5, TReturn>> ToReturnFunc<
        T1,
        T2,
        T3,
        T4,
        T5,
        TReturn
    >() => val => (_, _, _, _, _) => val;

    public static Func<TReturn, Func<T1, T2, T3, T4, T5, T6, TReturn>> ToReturnFunc<
        T1,
        T2,
        T3,
        T4,
        T5,
        T6,
        TReturn
    >() => val => (_, _, _, _, _, _) => val;

    public static Func<TReturn, Func<T1, T2, T3, T4, T5, T6, T7, TReturn>> ToReturnFunc<
        T1,
        T2,
        T3,
        T4,
        T5,
        T6,
        T7,
        TReturn
    >() => val => (_, _, _, _, _, _, _) => val;

    public static Func<TReturn, Func<T1, T2, T3, T4, T5, T6, T7, T8, TReturn>> ToReturnFunc<
        T1,
        T2,
        T3,
        T4,
        T5,
        T6,
        T7,
        T8,
        TReturn
    >() => val => (_, _, _, _, _, _, _, _) => val;

    public static Func<TReturn, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn>> ToReturnFunc<
        T1,
        T2,
        T3,
        T4,
        T5,
        T6,
        T7,
        T8,
        T9,
        TReturn
    >() => val => (_, _, _, _, _, _, _, _, _) => val;

    public static Func<
        TReturn,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn>
    > ToReturnFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn>() =>
        val => (_, _, _, _, _, _, _, _, _, _) => val;

    public static Func<
        TReturn,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn>
    > ToReturnFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn>() =>
        val => (_, _, _, _, _, _, _, _, _, _, _) => val;

    public static Func<
        TReturn,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn>
    > ToReturnFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn>() =>
        val => (_, _, _, _, _, _, _, _, _, _, _, _) => val;

    public static Func<
        TReturn,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn>
    > ToReturnFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn>() =>
        val => (_, _, _, _, _, _, _, _, _, _, _, _, _) => val;

    public static Func<
        TReturn,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn>
    > ToReturnFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn>() =>
        val => (_, _, _, _, _, _, _, _, _, _, _, _, _, _) => val;

    public static Func<
        TReturn,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn>
    > ToReturnFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn>() =>
        val => (_, _, _, _, _, _, _, _, _, _, _, _, _, _, _) => val;

    public static Func<T1, Func<T1, TReturn>, TReturn> ToReturnCallFunc<T1, TReturn>() =>
        static (col, returnCall) => returnCall(col);

    public static Func<(T1, T2), Func<T1, T2, TReturn>, TReturn> ToReturnCallFunc<
        T1,
        T2,
        TReturn
    >() => static (col, returnCall) => returnCall(col.Item1, col.Item2);

    public static Func<(T1, T2, T3), Func<T1, T2, T3, TReturn>, TReturn> ToReturnCallFunc<
        T1,
        T2,
        T3,
        TReturn
    >() => static (col, returnCall) => returnCall(col.Item1, col.Item2, col.Item3);

    public static Func<(T1, T2, T3, T4), Func<T1, T2, T3, T4, TReturn>, TReturn> ToReturnCallFunc<
        T1,
        T2,
        T3,
        T4,
        TReturn
    >() => static (col, returnCall) => returnCall(col.Item1, col.Item2, col.Item3, col.Item4);

    public static Func<
        (T1, T2, T3, T4, T5),
        Func<T1, T2, T3, T4, T5, TReturn>,
        TReturn
    > ToReturnCallFunc<T1, T2, T3, T4, T5, TReturn>() =>
        static (col, returnCall) =>
            returnCall(col.Item1, col.Item2, col.Item3, col.Item4, col.Item5);

    public static Func<
        (T1, T2, T3, T4, T5, T6),
        Func<T1, T2, T3, T4, T5, T6, TReturn>,
        TReturn
    > ToReturnCallFunc<T1, T2, T3, T4, T5, T6, TReturn>() =>
        static (col, returnCall) =>
            returnCall(col.Item1, col.Item2, col.Item3, col.Item4, col.Item5, col.Item6);

    public static Func<
        (T1, T2, T3, T4, T5, T6, T7),
        Func<T1, T2, T3, T4, T5, T6, T7, TReturn>,
        TReturn
    > ToReturnCallFunc<T1, T2, T3, T4, T5, T6, T7, TReturn>() =>
        static (col, returnCall) =>
            returnCall(col.Item1, col.Item2, col.Item3, col.Item4, col.Item5, col.Item6, col.Item7);

    public static Func<
        (T1, T2, T3, T4, T5, T6, T7, T8),
        Func<T1, T2, T3, T4, T5, T6, T7, T8, TReturn>,
        TReturn
    > ToReturnCallFunc<T1, T2, T3, T4, T5, T6, T7, T8, TReturn>() =>
        static (col, returnCall) =>
            returnCall(
                col.Item1,
                col.Item2,
                col.Item3,
                col.Item4,
                col.Item5,
                col.Item6,
                col.Item7,
                col.Item8
            );

    public static Func<
        (T1, T2, T3, T4, T5, T6, T7, T8, T9),
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn>,
        TReturn
    > ToReturnCallFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn>() =>
        static (col, returnCall) =>
            returnCall(
                col.Item1,
                col.Item2,
                col.Item3,
                col.Item4,
                col.Item5,
                col.Item6,
                col.Item7,
                col.Item8,
                col.Item9
            );

    public static Func<
        (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10),
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn>,
        TReturn
    > ToReturnCallFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn>() =>
        static (col, returnCall) =>
            returnCall(
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
            );

    public static Func<
        (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11),
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn>,
        TReturn
    > ToReturnCallFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn>() =>
        static (col, returnCall) =>
            returnCall(
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
            );

    public static Func<
        (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12),
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn>,
        TReturn
    > ToReturnCallFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn>() =>
        static (col, returnCall) =>
            returnCall(
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
            );

    public static Func<
        (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13),
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn>,
        TReturn
    > ToReturnCallFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn>() =>
        static (col, returnCall) =>
            returnCall(
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
            );

    public static Func<
        (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14),
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn>,
        TReturn
    > ToReturnCallFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn>() =>
        static (col, returnCall) =>
            returnCall(
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
            );

    public static Func<
        (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15),
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn>,
        TReturn
    > ToReturnCallFunc<
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
    >() =>
        static (col, returnCall) =>
            returnCall(
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
            );
}
