using MockMe.Mocks;

namespace MockMe;

public interface IArgBag<TArgCollection, TCallback, TReturnCall>
{
    public bool AllArgsSatisfy(TArgCollection args);
    public IMockCallbackAndReturnCallRetriever<TCallback, TReturnCall> Mock { get; }
}

public interface IArgBag<TArgCollection, TCallback, TReturnCall, TSelf>
    : IArgBag<TArgCollection, TCallback, TReturnCall>
{
    public static abstract TSelf Construct(
        TArgCollection collection,
        IMockCallbackAndReturnCallRetriever<TCallback, TReturnCall> mock
    );
}

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
    public static TReturn CallMemberMock<TArg1, TReturn>(
        List<ArgBag<TArg1, TReturn>> mockStore,
        List<TArg1> argStore,
        TArg1 arg1
    )
    {
        return CallMemberMock(
            mockStore,
            argStore,
            arg1,
            (col, callback) => callback(col),
            (col, returnCall) => returnCall(col)
        );
    }

    public static TReturn CallMemberMock<TArg1, TArg2, TReturn>(
        List<ArgBag<TArg1, TArg2, TReturn>> mockStore,
        List<ValueTuple<TArg1, TArg2>> argStore,
        TArg1 arg1,
        TArg2 arg2
    )
    {
        return CallMemberMock(
            mockStore,
            argStore,
            (arg1, arg2),
            (col, callback) => callback(col.Item1, col.Item2),
            (col, returnCall) => returnCall(col.Item1, col.Item2)
        );
    }

    private static TReturn CallMemberMock<TReturn, TArgCollection, TCallback, TReturnCall>(
        IReadOnlyList<IArgBag<TArgCollection, TCallback, TReturnCall>> mockStore,
        List<TArgCollection> argStore,
        TArgCollection argCollection,
        Action<TArgCollection, TCallback> callbackAction,
        Func<TArgCollection, TReturnCall, TReturn> returnCallFunc
    )
    {
        for (int i = mockStore.Count - 1; i >= 0; i--)
        {
            var argBag = mockStore[i];
            if (!argBag.AllArgsSatisfy(argCollection))
            {
                continue;
            }

            argStore.Add(argCollection);
            foreach (TCallback callback in argBag.Mock.GetCallbacksRegisteredBeforeReturnCall())
            {
                callbackAction(argCollection, callback);
            }

            TReturnCall? returnFunc = argBag.Mock.GetReturnValue();
            TReturn? returnVal = returnFunc is not null
                ? returnCallFunc(argCollection, returnFunc)
                : default(TReturn);

            foreach (TCallback callback in argBag.Mock.GetCallbacksRegisteredAfterReturnCall())
            {
                callbackAction(argCollection, callback);
            }
            return returnVal;
        }

        return default;
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
