using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockMe.Extensions;

internal static class FunctionExtensions
{
    public static Func<T2, T> ToFunc<T, T2>(this T t) => new(_ => t);
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
