using MockMe.Mocks;

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
    public static void CallVoidMemberMock(VoidMemberMock? mockStore)
    {
        if (mockStore is null)
        {
            return;
        }

        _ = CallMemberMock<bool, bool, Action, bool>(
            mockStore,
            false,
            static (_, action) => action()
        );
    }

    public static void CallVoidMemberMock<TArg1>(
        List<ArgBagWithVoidMemberMock<TArg1>>? mockStore,
        List<TArg1> argStore,
        TArg1 arg1
    )
    {
        CallVoidMemberMock(mockStore, argStore, arg1, (col, callback) => callback(col));
    }

    public static TReturn? CallMemberMock<TReturn>(MemberMock<TReturn>? mockStore)
    {
        if (mockStore is null)
        {
            return default;
        }

        return CallMemberMock<TReturn, bool, Action, Func<TReturn>>(
            mockStore,
            false,
            static (_, action) => action(),
            static (_, func) => func()
        );
    }

    public static TReturn? CallMemberMock<TArg1, TReturn>(
        List<ArgBagWithMemberMock<TArg1, TReturn>>? mockStore,
        List<TArg1> argStore,
        TArg1 arg1
    )
    {
        return FindAndCallApplicableMemberMock<TReturn, TArg1, Action<TArg1>, Func<TArg1, TReturn>>(
            mockStore,
            argStore,
            arg1,
            (col, callback) => callback(col),
            (col, returnCall) => returnCall(col)
        );
    }

    public static TReturn? CallMemberMock<TArg1, TArg2, TReturn>(
        List<ArgBagWithMemberMock<TArg1, TArg2, TReturn>>? mockStore,
        List<ValueTuple<TArg1, TArg2>> argStore,
        TArg1 arg1,
        TArg2 arg2
    )
    {
        return FindAndCallApplicableMemberMock<
            TReturn,
            ValueTuple<TArg1, TArg2>,
            Action<TArg1, TArg2>,
            Func<TArg1, TArg2, TReturn>
        >(
            mockStore,
            argStore,
            (arg1, arg2),
            (col, callback) => callback(col.Item1, col.Item2),
            (col, returnCall) => returnCall(col.Item1, col.Item2)
        );
    }

    //private static TReturn? CallMemberMock<TReturn, TArgCollection, TCallback, TReturnCall>(
    //    IReadOnlyList<IArgBag<TArgCollection, TCallback, TReturnCall>>? mockStore,
    //    List<TArgCollection> argStore,
    //    TArgCollection argCollection,
    //    Action<TArgCollection, TCallback> callbackAction,
    //    Func<TArgCollection, TReturnCall, TReturn> returnCallFunc
    //)
    //{
    //    if (mockStore is null)
    //    {
    //        return default;
    //    }

    //    for (int i = mockStore.Count - 1; i >= 0; i--)
    //    {
    //        var argBag = mockStore[i];
    //        if (!argBag.AllArgsSatisfy(argCollection))
    //        {
    //            continue;
    //        }

    //        argStore.Add(argCollection);
    //        foreach (TCallback callback in argBag.Mock.GetCallbacksRegisteredBeforeReturnCall())
    //        {
    //            callbackAction(argCollection, callback);
    //        }

    //        TReturnCall? returnFunc = argBag.Mock.GetReturnValue();
    //        TReturn? returnVal = returnFunc is not null
    //            ? returnCallFunc(argCollection, returnFunc)
    //            : default;

    //        foreach (TCallback callback in argBag.Mock.GetCallbacksRegisteredAfterReturnCall())
    //        {
    //            callbackAction(argCollection, callback);
    //        }
    //        return returnVal;
    //    }

    //    return default;
    //}

    private static void CallVoidMemberMock<TArgCollection, TCallback>(
        IReadOnlyList<IArgBag<TArgCollection, TCallback>>? mockStore,
        List<TArgCollection> argStore,
        TArgCollection argCollection,
        Action<TArgCollection, TCallback> callbackAction
    ) =>
        FindAndCallApplicableMemberMock<bool, TArgCollection, TCallback, bool>(
            mockStore,
            argStore,
            argCollection,
            callbackAction
        );

    private static TReturn? FindAndCallApplicableMemberMock<
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
        if (mockStore is null)
        {
            return default;
        }

        for (int i = mockStore.Count - 1; i >= 0; i--)
        {
            var argBag = mockStore[i];
            if (!argBag.AllArgsSatisfy(argCollection))
            {
                continue;
            }
            argStore.Add(argCollection);

            return CallMemberMock(argBag.Mock, argCollection, callbackAction, returnCallFunc);
        }

        return default;
    }

    private static TReturn? CallMemberMock<TReturn, TArgCollection, TCallback, TReturnCall>(
        IMockCallbackRetriever<TCallback> mock,
        TArgCollection argCollection,
        Action<TArgCollection, TCallback> callbackAction,
        Func<TArgCollection, TReturnCall, TReturn>? returnCallFunc = null
    )
    {
        TReturn? returnVal = default;
        foreach (TCallback callback in mock.GetCallbacksRegisteredBeforeReturnCall())
        {
            callbackAction(argCollection, callback);
        }

        if (
            returnCallFunc is not null
            && mock is IMockReturnCallRetriever<TReturnCall> mockWithReturn
        )
        {
            TReturnCall? returnFunc = mockWithReturn.GetReturnValue();
            returnVal = returnFunc is not null
                ? returnCallFunc(argCollection, returnFunc)
                : default;
        }

        foreach (TCallback callback in mock.GetCallbacksRegisteredAfterReturnCall())
        {
            callbackAction(argCollection, callback);
        }
        return returnVal;
    }

    //protected static TReturn CallMemberMock<TArg1, TReturn>(
    //    List<ArgBag<TArg1, MemberMock<TArg1, TReturn>>> mockStore,
    //    List<TArg1> argStore,
    //    TArg1 arg1
    //)
    //{
    //    for (int i = mockStore.Count - 1; i >= 0; i--)
    //    {
    //        var argBag = mockStore[i];
    //        if (!argBag.AllArgsSatisfy(arg1))
    //        {
    //            continue;
    //        }

    //        argStore.Add(arg1);
    //        foreach (var callback in argBag.Mock.GetCallbacksRegisteredBeforeReturnCall())
    //        {
    //            callback(arg1);
    //        }

    //        var returnFunc = argBag.Mock.GetReturnValue();
    //        TReturn? returnVal = returnFunc is not null ? returnFunc(arg1) : default(TReturn);

    //        foreach (var callback in argBag.Mock.GetCallbacksRegisteredAfterReturnCall())
    //        {
    //            callback(arg1);
    //        }
    //        return returnVal;
    //    }

    //    return default;
    //}

    //protected static TReturn CallMemberMock<TArg1, TArg2, TReturn>(
    //    List<ArgBag<TArg1, TArg2, MemberMock<TArg1, TArg2, TReturn>>> mockStore,
    //    List<ValueTuple<TArg1, TArg2>> argStore,
    //    TArg1 arg1,
    //    TArg2 arg2
    //)
    //{
    //    //for (int i = mockStore.Count - 1; i >= 0; i--)
    //    //{
    //    //    var argBag = mockStore[i];
    //    //    if (!argBag.AllArgsSatisfy(arg1, arg2))
    //    //    {
    //    //        continue;
    //    //    }

    //    //    argStore.Add((arg1, arg2));
    //    //    var returnVal = argBag.Mock.ReturnVal;

    //    //    if (argBag.Mock.GenericActions is not null)
    //    //    {
    //    //        foreach (Action action in argBag.Mock.GenericActions)
    //    //        {
    //    //            action.Invoke();
    //    //        }
    //    //    }
    //    //    if (argBag.Mock.Actions is not null)
    //    //    {
    //    //        foreach (var action in argBag.Mock.Actions)
    //    //        {
    //    //            action.Invoke(arg1, arg2);
    //    //        }
    //    //    }
    //    //    return returnVal;
    //    //}

    //    return default;
    //}
}
