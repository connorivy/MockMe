namespace MockMe.Extensions;

internal static class ThrowUtils
{
    public static Func<Exception, TReturn> ToThrow<TReturn>() => val => throw val;

    public static Func<Exception, Func<T1, TReturn>> ToThrow<T1, TReturn>() =>
        val => (_) => throw val;

    public static Func<Exception, Func<T1, T2, TReturn>> ToThrow<T1, T2, TReturn>() =>
        val => (_, _) => throw val;

    public static Func<Exception, Func<T1, T2, T3, TReturn>> ToThrow<T1, T2, T3, TReturn>() =>
        val => (_, _, _) => throw val;

    public static Func<Exception, Func<T1, T2, T3, T4, TReturn>> ToThrow<
        T1,
        T2,
        T3,
        T4,
        TReturn
    >() => val => (_, _, _, _) => throw val;

    public static Func<Exception, Func<T1, T2, T3, T4, T5, TReturn>> ToThrow<
        T1,
        T2,
        T3,
        T4,
        T5,
        TReturn
    >() => val => (_, _, _, _, _) => throw val;

    public static Func<Exception, Func<T1, T2, T3, T4, T5, T6, TReturn>> ToThrow<
        T1,
        T2,
        T3,
        T4,
        T5,
        T6,
        TReturn
    >() => val => (_, _, _, _, _, _) => throw val;

    public static Func<Exception, Func<T1, T2, T3, T4, T5, T6, T7, TReturn>> ToThrow<
        T1,
        T2,
        T3,
        T4,
        T5,
        T6,
        T7,
        TReturn
    >() => val => (_, _, _, _, _, _, _) => throw val;

    public static Func<Exception, Func<T1, T2, T3, T4, T5, T6, T7, T8, TReturn>> ToThrow<
        T1,
        T2,
        T3,
        T4,
        T5,
        T6,
        T7,
        T8,
        TReturn
    >() => val => (_, _, _, _, _, _, _, _) => throw val;

    public static Func<Exception, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn>> ToThrow<
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
    >() => val => (_, _, _, _, _, _, _, _, _) => throw val;

    public static Func<Exception, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn>> ToThrow<
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
        TReturn
    >() => val => (_, _, _, _, _, _, _, _, _, _) => throw val;

    public static Func<
        Exception,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn>
    > ToThrow<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn>() =>
        val => (_, _, _, _, _, _, _, _, _, _, _) => throw val;

    public static Func<
        Exception,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn>
    > ToThrow<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn>() =>
        val => (_, _, _, _, _, _, _, _, _, _, _, _) => throw val;

    public static Func<
        Exception,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn>
    > ToThrow<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn>() =>
        val => (_, _, _, _, _, _, _, _, _, _, _, _, _) => throw val;

    public static Func<
        Exception,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn>
    > ToThrow<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn>() =>
        val => (_, _, _, _, _, _, _, _, _, _, _, _, _, _) => throw val;

    public static Func<
        Exception,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn>
    > ToThrow<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn>() =>
        val => (_, _, _, _, _, _, _, _, _, _, _, _, _, _, _) => throw val;
}
