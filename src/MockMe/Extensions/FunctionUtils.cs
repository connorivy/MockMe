namespace MockMe.Extensions;

internal static class FunctionUtils
{
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
}

internal static class ActionExtensions
{
    public static Func<Action, Action<T1>> CallbackFunc<T1>() => action => _ => action();

    public static Func<Action, Action<T1, T2>> CallbackFunc<T1, T2>() =>
        action => (_, _) => action();

    public static Func<Action, Action<T1, T2, T3>> CallbackFunc<T1, T2, T3>() =>
        action => (_, _, _) => action();

    public static Func<Action, Action<T1, T2, T3, T4>> CallbackFunc<T1, T2, T3, T4>() =>
        action => (_, _, _, _) => action();

    public static Func<Action, Action<T1, T2, T3, T4, T5>> CallbackFunc<T1, T2, T3, T4, T5>() =>
        action => (_, _, _, _, _) => action();
}
