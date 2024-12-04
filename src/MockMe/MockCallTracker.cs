using MockMe.Extensions;

namespace MockMe;

public interface IArgBag<in TArgCollection>
{
    public bool AllArgsSatisfy(TArgCollection args);
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
    private static TReturn? FindAndCallApplicableMemberMock<
        TOriginalArgCollection,
        TReturn,
        TCallback,
        TReturnCall
    >(
        IReadOnlyList<IArgBagWithMock2<TOriginalArgCollection, TCallback>>? mockStore,
        List<TOriginalArgCollection> argStore,
        TOriginalArgCollection argCollection,
        Action<TOriginalArgCollection, TCallback> callbackAction,
        Func<TOriginalArgCollection, TReturnCall, TReturn>? returnCallFunc
    )
    {
        argStore.Add(argCollection);

        TReturn? returnVal = default;
        bool returnValAssigned = false;

        if (mockStore is not null)
        {
            for (int i = mockStore.Count - 1; i >= 0; i--)
            {
                var argBag = mockStore[i];
                if (!argBag.ArgBag.AllArgsSatisfy(argCollection))
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
                .NotNull()
                .MakeGenericMethod(resultType);
            return (TReturn)
                taskFromResultMethod.Invoke(null, [Activator.CreateInstance(resultType)]).NotNull();
        }
        else if (
            typeof(TReturn).IsGenericType
            && typeof(TReturn).GetGenericTypeDefinition() == typeof(ValueTask<>)
        )
        {
            var resultType = typeof(TReturn).GetGenericArguments()[0];
            var valueTaskConstructor = typeof(ValueTask<>)
                .MakeGenericType(resultType)
                .GetConstructor([resultType]);
            return (TReturn)
                valueTaskConstructor
                    .NotNull()
                    .Invoke([Activator.CreateInstance(resultType)])
                    .NotNull();
        }
        else
        {
            return default;
        }
    }

    public static TReturn? CallMemberMock<TReturn>(
        IMockCallbackAndReturnCallRetriever<Action, Func<TReturn>>? mockStore
    ) =>
        CallMemberMockBase<TReturn, bool, Action, Func<TReturn>>(
            mockStore,
            false,
            static (_, action) => action(),
            static (_, ret) => ret(),
            out _
        );

    public static void CallVoidMemberMock(IMockCallbackRetriever<Action>? mockStore) =>
        CallMemberMockBase<bool, bool, Action, bool>(
            mockStore,
            false,
            static (_, action) => action(),
            static (_, ret) => ret,
            out _
        );

    public static void CallVoidMemberMock<TOriginalArgCollection>(
        IReadOnlyList<
            IArgBagWithMock2<TOriginalArgCollection, Action<TOriginalArgCollection>>
        >? mockStore,
        List<TOriginalArgCollection> argStore,
        TOriginalArgCollection args
    )
    {
        FindAndCallApplicableMemberMock<
            TOriginalArgCollection,
            bool,
            Action<TOriginalArgCollection>,
            Func<TOriginalArgCollection, bool>
        >(
            mockStore,
            argStore,
            args,
            static (collection, callback) => callback(collection),
            static (collection, returnCall) => returnCall(collection)
        );
    }

    public static TReturn? CallMemberMock<TOriginalArgCollection, TReturn>(
        IReadOnlyList<ArgBagWithMock<TOriginalArgCollection>>? mockStore,
        List<TOriginalArgCollection> argStore,
        TOriginalArgCollection args
    )
    {
        return FindAndCallApplicableMemberMock<
            TOriginalArgCollection,
            TReturn,
            Action<TOriginalArgCollection>,
            Func<TOriginalArgCollection, TReturn>
        >(
            mockStore,
            argStore,
            args,
            static (collection, callback) => callback(collection),
            static (collection, returnCall) => returnCall(collection)
        );
    }
}
