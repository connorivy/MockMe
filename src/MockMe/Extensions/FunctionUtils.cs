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
}

internal static class ActionExtensions
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
}
