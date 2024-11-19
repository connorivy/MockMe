namespace MockMe.Extensions;

internal static class ActionUtils
{
    public static Func<Action, Action> CallbackFunc() => action => action;

    public static Func<Action, Action<T1>> CallbackFunc<T1>() => action => _ => action();

    public static Func<Action, Action<T1, T2>> CallbackFunc<T1, T2>() =>
        action => (_, _) => action();

    public static Func<Action, Action<T1, T2, T3>> CallbackFunc<T1, T2, T3>() =>
        action => (_, _, _) => action();

    public static Func<Action, Action<T1, T2, T3, T4>> CallbackFunc<T1, T2, T3, T4>() =>
        action => (_, _, _, _) => action();

    public static Func<Action, Action<T1, T2, T3, T4, T5>> CallbackFunc<T1, T2, T3, T4, T5>() =>
        action => (_, _, _, _, _) => action();

    public static Func<Action, Action<T1, T2, T3, T4, T5, T6>> CallbackFunc<
        T1,
        T2,
        T3,
        T4,
        T5,
        T6
    >() => action => (_, _, _, _, _, _) => action();

    public static Func<Action, Action<T1, T2, T3, T4, T5, T6, T7>> CallbackFunc<
        T1,
        T2,
        T3,
        T4,
        T5,
        T6,
        T7
    >() => action => (_, _, _, _, _, _, _) => action();

    public static Func<Action, Action<T1, T2, T3, T4, T5, T6, T7, T8>> CallbackFunc<
        T1,
        T2,
        T3,
        T4,
        T5,
        T6,
        T7,
        T8
    >() => action => (_, _, _, _, _, _, _, _) => action();

    public static Func<Action, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9>> CallbackFunc<
        T1,
        T2,
        T3,
        T4,
        T5,
        T6,
        T7,
        T8,
        T9
    >() => action => (_, _, _, _, _, _, _, _, _) => action();

    public static Func<Action, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>> CallbackFunc<
        T1,
        T2,
        T3,
        T4,
        T5,
        T6,
        T7,
        T8,
        T9,
        T10
    >() => action => (_, _, _, _, _, _, _, _, _, _) => action();

    public static Func<Action, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>> CallbackFunc<
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
        T11
    >() => action => (_, _, _, _, _, _, _, _, _, _, _) => action();

    public static Func<
        Action,
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>
    > CallbackFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>() =>
        action => (_, _, _, _, _, _, _, _, _, _, _, _) => action();

    public static Func<
        Action,
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>
    > CallbackFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>() =>
        action => (_, _, _, _, _, _, _, _, _, _, _, _, _) => action();

    public static Func<
        Action,
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>
    > CallbackFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>() =>
        action => (_, _, _, _, _, _, _, _, _, _, _, _, _, _) => action();

    public static Func<
        Action,
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>
    > CallbackFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>() =>
        action => (_, _, _, _, _, _, _, _, _, _, _, _, _, _, _) => action();

    public static Action<T1, Action<T1>> CallbackAction<T1>() =>
        static (col, callback) => callback(col);

    public static Action<(T1, T2), Action<T1, T2>> CallbackAction<T1, T2>() =>
        static (col, callback) => callback(col.Item1, col.Item2);

    public static Action<(T1, T2, T3), Action<T1, T2, T3>> CallbackAction<T1, T2, T3>() =>
        static (col, callback) => callback(col.Item1, col.Item2, col.Item3);

    public static Action<(T1, T2, T3, T4), Action<T1, T2, T3, T4>> CallbackAction<
        T1,
        T2,
        T3,
        T4
    >() => static (col, callback) => callback(col.Item1, col.Item2, col.Item3, col.Item4);

    public static Action<(T1, T2, T3, T4, T5), Action<T1, T2, T3, T4, T5>> CallbackAction<
        T1,
        T2,
        T3,
        T4,
        T5
    >() =>
        static (col, callback) => callback(col.Item1, col.Item2, col.Item3, col.Item4, col.Item5);

    public static Action<(T1, T2, T3, T4, T5, T6), Action<T1, T2, T3, T4, T5, T6>> CallbackAction<
        T1,
        T2,
        T3,
        T4,
        T5,
        T6
    >() =>
        static (col, callback) =>
            callback(col.Item1, col.Item2, col.Item3, col.Item4, col.Item5, col.Item6);

    public static Action<
        (T1, T2, T3, T4, T5, T6, T7),
        Action<T1, T2, T3, T4, T5, T6, T7>
    > CallbackAction<T1, T2, T3, T4, T5, T6, T7>() =>
        static (col, callback) =>
            callback(col.Item1, col.Item2, col.Item3, col.Item4, col.Item5, col.Item6, col.Item7);

    public static Action<
        (T1, T2, T3, T4, T5, T6, T7, T8),
        Action<T1, T2, T3, T4, T5, T6, T7, T8>
    > CallbackAction<T1, T2, T3, T4, T5, T6, T7, T8>() =>
        static (col, callback) =>
            callback(
                col.Item1,
                col.Item2,
                col.Item3,
                col.Item4,
                col.Item5,
                col.Item6,
                col.Item7,
                col.Item8
            );

    public static Action<
        (T1, T2, T3, T4, T5, T6, T7, T8, T9),
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9>
    > CallbackAction<T1, T2, T3, T4, T5, T6, T7, T8, T9>() =>
        static (col, callback) =>
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
            );

    public static Action<
        (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10),
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>
    > CallbackAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>() =>
        static (col, callback) =>
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
            );

    public static Action<
        (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11),
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>
    > CallbackAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>() =>
        static (col, callback) =>
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
            );

    public static Action<
        (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12),
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>
    > CallbackAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>() =>
        static (col, callback) =>
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
            );

    public static Action<
        (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13),
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>
    > CallbackAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>() =>
        static (col, callback) =>
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
            );

    public static Action<
        (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14),
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>
    > CallbackAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>() =>
        static (col, callback) =>
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
            );

    public static Action<
        (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15),
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>
    > CallbackAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>() =>
        static (col, callback) =>
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
            );
}
