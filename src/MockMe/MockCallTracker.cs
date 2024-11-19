using System.Reflection.Metadata;
using MockMe.Extensions;
using MockMe.Mocks.ClassMemberMocks;

namespace MockMe;

public interface IArgBag<TArgCollection>
{
    public bool AllArgsSatisfy(TArgCollection args);
}

public interface IArgBagWithMock<TArgCollection, out TMock> : IArgBag<TArgCollection>
{
    public TMock Mock { get; }
}

public interface IArgBag<TArgCollection, TCallback>
    : IArgBagWithMock<TArgCollection, IMockCallbackRetriever<TCallback>> { }

public interface IArgBag<TArgCollection, TCallback, TReturnCall>
    : IArgBagWithMock<TArgCollection, IMockCallbackAndReturnCallRetriever<TCallback, TReturnCall>>,
        IArgBag<TArgCollection, TCallback> { }

//public interface IArgBag<TArgCollection, TCallback, TReturnCall, TSelf>
//    : IArgBag<TArgCollection, TCallback, TReturnCall>
//{
//    public static abstract TSelf Construct(
//        TArgCollection collection,
//        IMockCallbackAndReturnCallRetriever<TCallback, TReturnCall> mock
//    );
//}

public interface IMockCallbackRetriever<TCallback>
{
    internal IEnumerable<TCallback> GetCallbacksRegisteredBeforeReturnCall();
    internal IEnumerable<TCallback> GetCallbacksRegisteredAfterReturnCall();
}

public interface IMockReturnCallRetriever<TReturnCall>
{
    internal TReturnCall? GetReturnValue();
}

public interface IMockCallbackAndReturnCallRetriever<TCallback, TReturnCall>
    : IMockCallbackRetriever<TCallback>,
        IMockReturnCallRetriever<TReturnCall> { }

public class MockCallTracker
{
    internal static TReturn? FindAndCallApplicableMemberMock<
        TReturn,
        TArgCollection,
        TCallback,
        TReturnCall
    >(
        IReadOnlyList<IArgBag<TArgCollection, TCallback>>? mockStore,
        List<TArgCollection> argStore,
        TArgCollection argCollection,
        Action<TArgCollection, TCallback> callbackAction,
        Func<TArgCollection, TReturnCall, TReturn>? returnCallFunc = null
    )
    {
        argStore.Add(argCollection);

        TReturn? returnVal = default;
        bool returnValAssigned = false;

        for (int i = (mockStore?.Count ?? 0) - 1; i >= 0; i--)
        {
            var argBag = mockStore[i];
            if (!argBag.AllArgsSatisfy(argCollection))
            {
                continue;
            }

            var localReturn = CallMemberMockBase(
                argBag.Mock,
                argCollection,
                callbackAction,
                returnCallFunc,
                out bool retValIsUserConfigured
            );

            if (!returnValAssigned)
            {
                returnVal = localReturn;
            }

            if (retValIsUserConfigured)
            {
                returnValAssigned = true;
            }
        }

        if (returnValAssigned)
        {
            return returnVal;
        }

        return GetDefaultOrCompletedTask<TReturn>();
    }

    internal static TReturn? CallMemberMockBase<TReturn, TArgCollection, TCallback, TReturnCall>(
        IMockCallbackRetriever<TCallback>? mock,
        TArgCollection argCollection,
        Action<TArgCollection, TCallback> callbackAction,
        Func<TArgCollection, TReturnCall, TReturn>? returnCallFunc,
        out bool retValIsUserConfigured
    )
    {
        retValIsUserConfigured = false;
        TReturn? returnVal = default;

        if (mock is null)
        {
            return GetDefaultOrCompletedTask<TReturn>();
        }

        foreach (TCallback callback in mock.GetCallbacksRegisteredBeforeReturnCall())
        {
            callbackAction(argCollection, callback);
        }

        if (
            returnCallFunc is not null
            && mock is IMockReturnCallRetriever<TReturnCall> mockWithReturn
            && mockWithReturn.GetReturnValue() is TReturnCall returnFunc
        )
        {
            //TReturnCall? returnFunc = mockWithReturn.GetReturnValue();
            //returnVal = returnFunc is not null
            //    ? returnCallFunc(argCollection, returnFunc)
            //    : default;
            returnVal = returnCallFunc(argCollection, returnFunc);
            retValIsUserConfigured = true;
        }

        foreach (TCallback callback in mock.GetCallbacksRegisteredAfterReturnCall())
        {
            callbackAction(argCollection, callback);
        }

        if (retValIsUserConfigured)
        {
            return returnVal;
        }

        return GetDefaultOrCompletedTask<TReturn>();
    }

    private static TReturn? GetDefaultOrCompletedTask<TReturn>()
    {
        if (typeof(TReturn) == typeof(Task))
        {
            return (TReturn)(object)Task.CompletedTask;
        }
        else if (
            typeof(TReturn).IsGenericType
            && typeof(TReturn).GetGenericTypeDefinition() == typeof(Task<>)
        )
        {
            var resultType = typeof(TReturn).GetGenericArguments()[0];
            var taskFromResultMethod = typeof(Task)
                .GetMethod(nameof(Task.FromResult))
                .MakeGenericMethod(resultType);
            return (TReturn)
                taskFromResultMethod.Invoke(null, new[] { Activator.CreateInstance(resultType) });
        }
        else if (
            typeof(TReturn).IsGenericType
            && typeof(TReturn).GetGenericTypeDefinition() == typeof(ValueTask<>)
        )
        {
            var resultType = typeof(TReturn).GetGenericArguments()[0];
            var valueTaskConstructor = typeof(ValueTask<>)
                .MakeGenericType(resultType)
                .GetConstructor(new[] { resultType });
            return (TReturn)
                valueTaskConstructor.Invoke(new[] { Activator.CreateInstance(resultType) });
        }
        else
        {
            return default;
        }
    }

    public static TReturn? CallMemberMock<TReturn>(
        IMockCallbackAndReturnCallRetriever<Action, TReturn>? mockStore
    ) =>
        CallMemberMockBase<TReturn, bool, Action, TReturn>(
            mockStore,
            false,
            static (_, action) => action(),
            static (_, ret) => ret,
            out _
        );

    public static TReturn? CallMemberMock<T1, TReturn>(
        List<ArgBagWithMemberMock<T1, TReturn>>? mockStore,
        List<T1> argStore,
        T1 arg1
    )
    {
        return FindAndCallApplicableMemberMock(
            mockStore,
            argStore,
            arg1,
            ActionUtils.CallbackAction<T1>(),
            FunctionUtils.ToReturnCallFunc<T1, TReturn>()
        );
    }

    public static TReturn? CallMemberMock<T1, T2, TReturn>(
        List<ArgBagWithMemberMock<T1, T2, TReturn>>? mockStore,
        List<ValueTuple<T1, T2>> argStore,
        T1 arg1,
        T2 arg2
    )
    {
        return FindAndCallApplicableMemberMock(
            mockStore,
            argStore,
            (arg1, arg2),
            ActionUtils.CallbackAction<T1, T2>(),
            FunctionUtils.ToReturnCallFunc<T1, T2, TReturn>()
        );
    }

    public static TReturn? CallMemberMock<T1, T2, T3, TReturn>(
        List<ArgBagWithMemberMock<T1, T2, T3, TReturn>>? mockStore,
        List<ValueTuple<T1, T2, T3>> argStore,
        T1 arg1,
        T2 arg2,
        T3 arg3
    )
    {
        return FindAndCallApplicableMemberMock(
            mockStore,
            argStore,
            (arg1, arg2, arg3),
            ActionUtils.CallbackAction<T1, T2, T3>(),
            FunctionUtils.ToReturnCallFunc<T1, T2, T3, TReturn>()
        );
    }

    public static TReturn? CallMemberMock<T1, T2, T3, T4, TReturn>(
        List<ArgBagWithMemberMock<T1, T2, T3, T4, TReturn>>? mockStore,
        List<(T1, T2, T3, T4)> argStore,
        T1 arg1,
        T2 arg2,
        T3 arg3,
        T4 arg4
    ) =>
        FindAndCallApplicableMemberMock(
            mockStore,
            argStore,
            (arg1, arg2, arg3, arg4),
            ActionUtils.CallbackAction<T1, T2, T3, T4>(),
            FunctionUtils.ToReturnCallFunc<T1, T2, T3, T4, TReturn>()
        );

    public static TReturn? CallMemberMock<T1, T2, T3, T4, T5, TReturn>(
        List<ArgBagWithMemberMock<T1, T2, T3, T4, T5, TReturn>>? mockStore,
        List<(T1, T2, T3, T4, T5)> argStore,
        T1 arg1,
        T2 arg2,
        T3 arg3,
        T4 arg4,
        T5 arg5
    ) =>
        FindAndCallApplicableMemberMock(
            mockStore,
            argStore,
            (arg1, arg2, arg3, arg4, arg5),
            ActionUtils.CallbackAction<T1, T2, T3, T4, T5>(),
            FunctionUtils.ToReturnCallFunc<T1, T2, T3, T4, T5, TReturn>()
        );

    public static TReturn? CallMemberMock<T1, T2, T3, T4, T5, T6, TReturn>(
        List<ArgBagWithMemberMock<T1, T2, T3, T4, T5, T6, TReturn>>? mockStore,
        List<(T1, T2, T3, T4, T5, T6)> argStore,
        T1 arg1,
        T2 arg2,
        T3 arg3,
        T4 arg4,
        T5 arg5,
        T6 arg6
    ) =>
        FindAndCallApplicableMemberMock(
            mockStore,
            argStore,
            (arg1, arg2, arg3, arg4, arg5, arg6),
            ActionUtils.CallbackAction<T1, T2, T3, T4, T5, T6>(),
            FunctionUtils.ToReturnCallFunc<T1, T2, T3, T4, T5, T6, TReturn>()
        );

    public static TReturn? CallMemberMock<T1, T2, T3, T4, T5, T6, T7, TReturn>(
        List<ArgBagWithMemberMock<T1, T2, T3, T4, T5, T6, T7, TReturn>>? mockStore,
        List<(T1, T2, T3, T4, T5, T6, T7)> argStore,
        T1 arg1,
        T2 arg2,
        T3 arg3,
        T4 arg4,
        T5 arg5,
        T6 arg6,
        T7 arg7
    ) =>
        FindAndCallApplicableMemberMock(
            mockStore,
            argStore,
            (arg1, arg2, arg3, arg4, arg5, arg6, arg7),
            ActionUtils.CallbackAction<T1, T2, T3, T4, T5, T6, T7>(),
            FunctionUtils.ToReturnCallFunc<T1, T2, T3, T4, T5, T6, T7, TReturn>()
        );

    public static TReturn? CallMemberMock<T1, T2, T3, T4, T5, T6, T7, T8, TReturn>(
        List<ArgBagWithMemberMock<T1, T2, T3, T4, T5, T6, T7, T8, TReturn>>? mockStore,
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
        FindAndCallApplicableMemberMock(
            mockStore,
            argStore,
            (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8),
            ActionUtils.CallbackAction<T1, T2, T3, T4, T5, T6, T7, T8>(),
            FunctionUtils.ToReturnCallFunc<T1, T2, T3, T4, T5, T6, T7, T8, TReturn>()
        );

    public static TReturn? CallMemberMock<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn>(
        List<ArgBagWithMemberMock<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn>>? mockStore,
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
        FindAndCallApplicableMemberMock(
            mockStore,
            argStore,
            (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9),
            ActionUtils.CallbackAction<T1, T2, T3, T4, T5, T6, T7, T8, T9>(),
            FunctionUtils.ToReturnCallFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn>()
        );

    public static TReturn? CallMemberMock<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn>(
        List<ArgBagWithMemberMock<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn>>? mockStore,
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
        FindAndCallApplicableMemberMock(
            mockStore,
            argStore,
            (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10),
            ActionUtils.CallbackAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(),
            FunctionUtils.ToReturnCallFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn>()
        );

    public static TReturn? CallMemberMock<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn>(
        List<
            ArgBagWithMemberMock<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn>
        >? mockStore,
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
        FindAndCallApplicableMemberMock(
            mockStore,
            argStore,
            (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11),
            ActionUtils.CallbackAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(),
            FunctionUtils.ToReturnCallFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn>()
        );

    public static TReturn? CallMemberMock<
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
        TReturn
    >(
        List<
            ArgBagWithMemberMock<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn>
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
        FindAndCallApplicableMemberMock(
            mockStore,
            argStore,
            (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12),
            ActionUtils.CallbackAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(),
            FunctionUtils.ToReturnCallFunc<
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
                TReturn
            >()
        );

    public static TReturn? CallMemberMock<
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
        TReturn
    >(
        List<
            ArgBagWithMemberMock<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn>
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
        FindAndCallApplicableMemberMock(
            mockStore,
            argStore,
            (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13),
            ActionUtils.CallbackAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(),
            FunctionUtils.ToReturnCallFunc<
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
                TReturn
            >()
        );

    public static TReturn? CallMemberMock<
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
        TReturn
    >(
        List<
            ArgBagWithMemberMock<
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
                TReturn
            >
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
        FindAndCallApplicableMemberMock(
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
            ActionUtils.CallbackAction<
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
            >(),
            FunctionUtils.ToReturnCallFunc<
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
                TReturn
            >()
        );

    public static TReturn? CallMemberMock<
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
    >(
        List<
            ArgBagWithMemberMock<
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
        FindAndCallApplicableMemberMock(
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
            ActionUtils.CallbackAction<
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
            >(),
            FunctionUtils.ToReturnCallFunc<
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
            >()
        );
}
