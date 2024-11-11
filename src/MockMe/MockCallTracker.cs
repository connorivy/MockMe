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

        return CallMemberMock<TReturn, bool, Action, TReturn>(
            mockStore,
            false,
            static (_, action) => action(),
            static (_, ret) => ret
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

    public static TReturn? CallMemberMock<TArg1, TArg2, TArg3, TReturn>(
        List<ArgBagWithMemberMock<TArg1, TArg2, TArg3, TReturn>>? mockStore,
        List<ValueTuple<TArg1, TArg2, TArg3>> argStore,
        TArg1 arg1,
        TArg2 arg2,
        TArg3 arg3
    )
    {
        return FindAndCallApplicableMemberMock<
            TReturn,
            ValueTuple<TArg1, TArg2, TArg3>,
            Action<TArg1, TArg2, TArg3>,
            Func<TArg1, TArg2, TArg3, TReturn>
        >(
            mockStore,
            argStore,
            (arg1, arg2, arg3),
            (col, callback) => callback(col.Item1, col.Item2, col.Item3),
            (col, returnCall) => returnCall(col.Item1, col.Item2, col.Item3)
        );
    }

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
        argStore.Add(argCollection);
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

    protected static List<TArg1> GetGenericCallStore<TArg1>(
        Dictionary<int, object> argStore,
        int hashCode
    ) => GetGenericCallStoreBase<TArg1>(argStore, hashCode);

    protected static List<ValueTuple<TArg1, TArg2>> GetGenericCallStore<TArg1, TArg2>(
        Dictionary<int, object> argStore,
        int hashCode
    ) => GetGenericCallStoreBase<ValueTuple<TArg1, TArg2>>(argStore, hashCode);

    protected static List<ValueTuple<TArg1, TArg2, TArg3>> GetGenericCallStore<TArg1, TArg2, TArg3>(
        Dictionary<int, object> argStore,
        int hashCode
    ) => GetGenericCallStoreBase<ValueTuple<TArg1, TArg2, TArg3>>(argStore, hashCode);

    private static List<TArgCollection> GetGenericCallStoreBase<TArgCollection>(
        Dictionary<int, object> argStore,
        int hashCode
    )
    {
        if (!argStore.TryGetValue(hashCode, out object? specificStore))
        {
            specificStore = new List<TArgCollection>();
            argStore.Add(hashCode, specificStore);
        }
        return (List<TArgCollection>)specificStore;
    }
}
