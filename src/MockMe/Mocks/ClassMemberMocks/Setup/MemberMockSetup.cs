namespace MockMe.Mocks.ClassMemberMocks.Setup;

public partial class MemberMockSetup
{
    protected static MemberMock<TReturn> SetupMethod<TReturn>(
        List<MemberMock<TReturn>> mockAndArgsStore
    ) =>
        SetupMemberMockBase<bool, MemberMock<TReturn>, MemberMock<TReturn>>(
            false,
            mockAndArgsStore,
            static (col, mock) => mock
        );

    protected static MemberMock<TArg1, TReturn> SetupMethod<TArg1, TReturn>(
        List<ArgBagWithMemberMock<TArg1, TReturn>> mockAndArgsStore,
        Arg<TArg1> arg1
    ) =>
        SetupMemberMockBase<
            Arg<TArg1>,
            MemberMock<TArg1, TReturn>,
            ArgBagWithMemberMock<TArg1, TReturn>
        >(arg1, mockAndArgsStore, static (col, mock) => new(col, mock));

    protected static MemberMock<TArg1, TArg2, TReturn> SetupMethod<TArg1, TArg2, TReturn>(
        List<ArgBagWithMemberMock<TArg1, TArg2, TReturn>> mockAndArgsStore,
        Arg<TArg1> arg1,
        Arg<TArg2> arg2
    ) =>
        SetupMemberMockBase<
            ValueTuple<Arg<TArg1>, Arg<TArg2>>,
            MemberMock<TArg1, TArg2, TReturn>,
            ArgBagWithMemberMock<TArg1, TArg2, TReturn>
        >((arg1, arg2), mockAndArgsStore, static (col, mock) => new(col.Item1, col.Item2, mock));

    protected static MemberMock<TArg1, TArg2, TArg3, TReturn> SetupMethod<
        TArg1,
        TArg2,
        TArg3,
        TReturn
    >(
        List<ArgBagWithMemberMock<TArg1, TArg2, TArg3, TReturn>> mockAndArgsStore,
        Arg<TArg1> arg1,
        Arg<TArg2> arg2,
        Arg<TArg3> arg3
    ) =>
        SetupMemberMockBase<
            ValueTuple<Arg<TArg1>, Arg<TArg2>, Arg<TArg3>>,
            MemberMock<TArg1, TArg2, TArg3, TReturn>,
            ArgBagWithMemberMock<TArg1, TArg2, TArg3, TReturn>
        >(
            (arg1, arg2, arg3),
            mockAndArgsStore,
            static (col, mock) => new(col.Item1, col.Item2, col.Item3, mock)
        );

    internal static TMock SetupMemberMockBase<TArgCollection, TMock, TArgBag>(
        TArgCollection argCollection,
        ICollection<TArgBag> mockAndArgsStore,
        Func<TArgCollection, TMock, TArgBag> argBagFactory
    )
        where TMock : new()
    {
        TMock memberMock = new();
        mockAndArgsStore.Add(argBagFactory(argCollection, memberMock));
        return memberMock;
    }

    protected static List<ArgBagWithMemberMock<TArg1, TReturn>> SetupGenericStore<TArg1, TReturn>(
        Dictionary<int, object> mockAndArgsStore
    ) =>
        SetupGenericStoreBase<ArgBagWithMemberMock<TArg1, TReturn>>(
            mockAndArgsStore,
            typeof(TArg1)
        );

    protected static List<ArgBagWithMemberMock<TArg1, TArg2, TReturn>> SetupGenericStore<
        TArg1,
        TArg2,
        TReturn
    >(Dictionary<int, object> mockAndArgsStore) =>
        SetupGenericStoreBase<ArgBagWithMemberMock<TArg1, TArg2, TReturn>>(
            mockAndArgsStore,
            typeof(TArg1),
            typeof(TArg2)
        );

    protected static List<ArgBagWithMemberMock<TArg1, TArg2, TArg3, TReturn>> SetupGenericStore<
        TArg1,
        TArg2,
        TArg3,
        TReturn
    >(Dictionary<int, object> mockAndArgsStore) =>
        SetupGenericStoreBase<ArgBagWithMemberMock<TArg1, TArg2, TArg3, TReturn>>(
            mockAndArgsStore,
            typeof(TArg1),
            typeof(TArg2),
            typeof(TArg3)
        );

    private static List<TArgBag> SetupGenericStoreBase<TArgBag>(
        Dictionary<int, object> mockAndArgsStore,
        params Type[] genericParameterTypes
    )
    {
        int hashCode = GetUniqueIntFromTypes(genericParameterTypes);
        if (!mockAndArgsStore.TryGetValue(hashCode, out object? specificStore))
        {
            specificStore = new List<TArgBag>();
            mockAndArgsStore.Add(hashCode, specificStore);
        }
        return (List<TArgBag>)specificStore;
    }

    protected static int GetUniqueIntFromTypes(params Type[] types)
    {
        const int seed = 487;
        const int modifier = 31;
        return types.Aggregate(seed, (current, type) => current * modifier + type.GetHashCode());
    }
}
