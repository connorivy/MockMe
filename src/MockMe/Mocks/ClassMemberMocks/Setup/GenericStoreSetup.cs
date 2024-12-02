namespace MockMe.Mocks.ClassMemberMocks.Setup;

public partial class MemberMockSetup
{
    protected static List<TArgBag> SetupGenericStore<TArgBag>(
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

//    protected static List<ArgBagWithMemberMock<TArg1, TReturn>> SetupGenericStore<TArg1, TReturn>(
//        Dictionary<int, object> mockAndArgsStore
//    ) =>
//        SetupGenericStoreBase<ArgBagWithMemberMock<TArg1, TReturn>>(
//            mockAndArgsStore,
//            typeof(TArg1)
//        );

//    protected static List<ArgBagWithMemberMock<TArg1, TArg2, TReturn>> SetupGenericStore<
//        TArg1,
//        TArg2,
//        TReturn
//    >(Dictionary<int, object> mockAndArgsStore) =>
//        SetupGenericStoreBase<ArgBagWithMemberMock<TArg1, TArg2, TReturn>>(
//            mockAndArgsStore,
//            typeof(TArg1),
//            typeof(TArg2)
//        );

//    protected static List<ArgBagWithMemberMock<TArg1, TArg2, TArg3, TReturn>> SetupGenericStore<
//        TArg1,
//        TArg2,
//        TArg3,
//        TReturn
//    >(Dictionary<int, object> mockAndArgsStore) =>
//        SetupGenericStoreBase<ArgBagWithMemberMock<TArg1, TArg2, TArg3, TReturn>>(
//            mockAndArgsStore,
//            typeof(TArg1),
//            typeof(TArg2),
//            typeof(TArg3)
//        );

//    protected static List<
//        ArgBagWithMemberMock<TArg1, TArg2, TArg3, TArg4, TReturn>
//    > SetupGenericStore<TArg1, TArg2, TArg3, TArg4, TReturn>(
//        Dictionary<int, object> mockAndArgsStore
//    ) =>
//        SetupGenericStoreBase<ArgBagWithMemberMock<TArg1, TArg2, TArg3, TArg4, TReturn>>(
//            mockAndArgsStore,
//            typeof(TArg1),
//            typeof(TArg2),
//            typeof(TArg3),
//            typeof(TArg4)
//        );

//    protected static List<
//        ArgBagWithMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TReturn>
//    > SetupGenericStore<TArg1, TArg2, TArg3, TArg4, TArg5, TReturn>(
//        Dictionary<int, object> mockAndArgsStore
//    ) =>
//        SetupGenericStoreBase<ArgBagWithMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TReturn>>(
//            mockAndArgsStore,
//            typeof(TArg1),
//            typeof(TArg2),
//            typeof(TArg3),
//            typeof(TArg4),
//            typeof(TArg5)
//        );

//    protected static List<
//        ArgBagWithMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TReturn>
//    > SetupGenericStore<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TReturn>(
//        Dictionary<int, object> mockAndArgsStore
//    ) =>
//        SetupGenericStoreBase<
//            ArgBagWithMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TReturn>
//        >(
//            mockAndArgsStore,
//            typeof(TArg1),
//            typeof(TArg2),
//            typeof(TArg3),
//            typeof(TArg4),
//            typeof(TArg5),
//            typeof(TArg6)
//        );

//    protected static List<
//        ArgBagWithMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TReturn>
//    > SetupGenericStore<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TReturn>(
//        Dictionary<int, object> mockAndArgsStore
//    ) =>
//        SetupGenericStoreBase<
//            ArgBagWithMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TReturn>
//        >(
//            mockAndArgsStore,
//            typeof(TArg1),
//            typeof(TArg2),
//            typeof(TArg3),
//            typeof(TArg4),
//            typeof(TArg5),
//            typeof(TArg6),
//            typeof(TArg7)
//        );

//    protected static List<
//        ArgBagWithMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TReturn>
//    > SetupGenericStore<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TReturn>(
//        Dictionary<int, object> mockAndArgsStore
//    ) =>
//        SetupGenericStoreBase<
//            ArgBagWithMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TReturn>
//        >(
//            mockAndArgsStore,
//            typeof(TArg1),
//            typeof(TArg2),
//            typeof(TArg3),
//            typeof(TArg4),
//            typeof(TArg5),
//            typeof(TArg6),
//            typeof(TArg7),
//            typeof(TArg8)
//        );

//    protected static List<
//        ArgBagWithMemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TReturn>
//    > SetupGenericStore<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TReturn>(
//        Dictionary<int, object> mockAndArgsStore
//    ) =>
//        SetupGenericStoreBase<
//            ArgBagWithMemberMock<
//                TArg1,
//                TArg2,
//                TArg3,
//                TArg4,
//                TArg5,
//                TArg6,
//                TArg7,
//                TArg8,
//                TArg9,
//                TReturn
//            >
//        >(
//            mockAndArgsStore,
//            typeof(TArg1),
//            typeof(TArg2),
//            typeof(TArg3),
//            typeof(TArg4),
//            typeof(TArg5),
//            typeof(TArg6),
//            typeof(TArg7),
//            typeof(TArg8),
//            typeof(TArg9)
//        );

//    protected static List<
//        ArgBagWithMemberMock<
//            TArg1,
//            TArg2,
//            TArg3,
//            TArg4,
//            TArg5,
//            TArg6,
//            TArg7,
//            TArg8,
//            TArg9,
//            TArg10,
//            TReturn
//        >
//    > SetupGenericStore<
//        TArg1,
//        TArg2,
//        TArg3,
//        TArg4,
//        TArg5,
//        TArg6,
//        TArg7,
//        TArg8,
//        TArg9,
//        TArg10,
//        TReturn
//    >(Dictionary<int, object> mockAndArgsStore) =>
//        SetupGenericStoreBase<
//            ArgBagWithMemberMock<
//                TArg1,
//                TArg2,
//                TArg3,
//                TArg4,
//                TArg5,
//                TArg6,
//                TArg7,
//                TArg8,
//                TArg9,
//                TArg10,
//                TReturn
//            >
//        >(
//            mockAndArgsStore,
//            typeof(TArg1),
//            typeof(TArg2),
//            typeof(TArg3),
//            typeof(TArg4),
//            typeof(TArg5),
//            typeof(TArg6),
//            typeof(TArg7),
//            typeof(TArg8),
//            typeof(TArg9),
//            typeof(TArg10)
//        );

//    protected static List<
//        ArgBagWithMemberMock<
//            TArg1,
//            TArg2,
//            TArg3,
//            TArg4,
//            TArg5,
//            TArg6,
//            TArg7,
//            TArg8,
//            TArg9,
//            TArg10,
//            TArg11,
//            TReturn
//        >
//    > SetupGenericStore<
//        TArg1,
//        TArg2,
//        TArg3,
//        TArg4,
//        TArg5,
//        TArg6,
//        TArg7,
//        TArg8,
//        TArg9,
//        TArg10,
//        TArg11,
//        TReturn
//    >(Dictionary<int, object> mockAndArgsStore) =>
//        SetupGenericStoreBase<
//            ArgBagWithMemberMock<
//                TArg1,
//                TArg2,
//                TArg3,
//                TArg4,
//                TArg5,
//                TArg6,
//                TArg7,
//                TArg8,
//                TArg9,
//                TArg10,
//                TArg11,
//                TReturn
//            >
//        >(
//            mockAndArgsStore,
//            typeof(TArg1),
//            typeof(TArg2),
//            typeof(TArg3),
//            typeof(TArg4),
//            typeof(TArg5),
//            typeof(TArg6),
//            typeof(TArg7),
//            typeof(TArg8),
//            typeof(TArg9),
//            typeof(TArg10),
//            typeof(TArg11)
//        );

//    protected static List<
//        ArgBagWithMemberMock<
//            TArg1,
//            TArg2,
//            TArg3,
//            TArg4,
//            TArg5,
//            TArg6,
//            TArg7,
//            TArg8,
//            TArg9,
//            TArg10,
//            TArg11,
//            TArg12,
//            TReturn
//        >
//    > SetupGenericStore<
//        TArg1,
//        TArg2,
//        TArg3,
//        TArg4,
//        TArg5,
//        TArg6,
//        TArg7,
//        TArg8,
//        TArg9,
//        TArg10,
//        TArg11,
//        TArg12,
//        TReturn
//    >(Dictionary<int, object> mockAndArgsStore) =>
//        SetupGenericStoreBase<
//            ArgBagWithMemberMock<
//                TArg1,
//                TArg2,
//                TArg3,
//                TArg4,
//                TArg5,
//                TArg6,
//                TArg7,
//                TArg8,
//                TArg9,
//                TArg10,
//                TArg11,
//                TArg12,
//                TReturn
//            >
//        >(
//            mockAndArgsStore,
//            typeof(TArg1),
//            typeof(TArg2),
//            typeof(TArg3),
//            typeof(TArg4),
//            typeof(TArg5),
//            typeof(TArg6),
//            typeof(TArg7),
//            typeof(TArg8),
//            typeof(TArg9),
//            typeof(TArg10),
//            typeof(TArg11),
//            typeof(TArg12)
//        );

//    protected static List<
//        ArgBagWithMemberMock<
//            TArg1,
//            TArg2,
//            TArg3,
//            TArg4,
//            TArg5,
//            TArg6,
//            TArg7,
//            TArg8,
//            TArg9,
//            TArg10,
//            TArg11,
//            TArg12,
//            TArg13,
//            TReturn
//        >
//    > SetupGenericStore<
//        TArg1,
//        TArg2,
//        TArg3,
//        TArg4,
//        TArg5,
//        TArg6,
//        TArg7,
//        TArg8,
//        TArg9,
//        TArg10,
//        TArg11,
//        TArg12,
//        TArg13,
//        TReturn
//    >(Dictionary<int, object> mockAndArgsStore) =>
//        SetupGenericStoreBase<
//            ArgBagWithMemberMock<
//                TArg1,
//                TArg2,
//                TArg3,
//                TArg4,
//                TArg5,
//                TArg6,
//                TArg7,
//                TArg8,
//                TArg9,
//                TArg10,
//                TArg11,
//                TArg12,
//                TArg13,
//                TReturn
//            >
//        >(
//            mockAndArgsStore,
//            typeof(TArg1),
//            typeof(TArg2),
//            typeof(TArg3),
//            typeof(TArg4),
//            typeof(TArg5),
//            typeof(TArg6),
//            typeof(TArg7),
//            typeof(TArg8),
//            typeof(TArg9),
//            typeof(TArg10),
//            typeof(TArg11),
//            typeof(TArg12),
//            typeof(TArg13)
//        );

//    protected static List<
//        ArgBagWithMemberMock<
//            TArg1,
//            TArg2,
//            TArg3,
//            TArg4,
//            TArg5,
//            TArg6,
//            TArg7,
//            TArg8,
//            TArg9,
//            TArg10,
//            TArg11,
//            TArg12,
//            TArg13,
//            TArg14,
//            TReturn
//        >
//    > SetupGenericStore<
//        TArg1,
//        TArg2,
//        TArg3,
//        TArg4,
//        TArg5,
//        TArg6,
//        TArg7,
//        TArg8,
//        TArg9,
//        TArg10,
//        TArg11,
//        TArg12,
//        TArg13,
//        TArg14,
//        TReturn
//    >(Dictionary<int, object> mockAndArgsStore) =>
//        SetupGenericStoreBase<
//            ArgBagWithMemberMock<
//                TArg1,
//                TArg2,
//                TArg3,
//                TArg4,
//                TArg5,
//                TArg6,
//                TArg7,
//                TArg8,
//                TArg9,
//                TArg10,
//                TArg11,
//                TArg12,
//                TArg13,
//                TArg14,
//                TReturn
//            >
//        >(
//            mockAndArgsStore,
//            typeof(TArg1),
//            typeof(TArg2),
//            typeof(TArg3),
//            typeof(TArg4),
//            typeof(TArg5),
//            typeof(TArg6),
//            typeof(TArg7),
//            typeof(TArg8),
//            typeof(TArg9),
//            typeof(TArg10),
//            typeof(TArg11),
//            typeof(TArg12),
//            typeof(TArg13),
//            typeof(TArg14)
//        );

//    protected static List<
//        ArgBagWithMemberMock<
//            TArg1,
//            TArg2,
//            TArg3,
//            TArg4,
//            TArg5,
//            TArg6,
//            TArg7,
//            TArg8,
//            TArg9,
//            TArg10,
//            TArg11,
//            TArg12,
//            TArg13,
//            TArg14,
//            TArg15,
//            TReturn
//        >
//    > SetupGenericStore<
//        TArg1,
//        TArg2,
//        TArg3,
//        TArg4,
//        TArg5,
//        TArg6,
//        TArg7,
//        TArg8,
//        TArg9,
//        TArg10,
//        TArg11,
//        TArg12,
//        TArg13,
//        TArg14,
//        TArg15,
//        TReturn
//    >(Dictionary<int, object> mockAndArgsStore) =>
//        SetupGenericStoreBase<
//            ArgBagWithMemberMock<
//                TArg1,
//                TArg2,
//                TArg3,
//                TArg4,
//                TArg5,
//                TArg6,
//                TArg7,
//                TArg8,
//                TArg9,
//                TArg10,
//                TArg11,
//                TArg12,
//                TArg13,
//                TArg14,
//                TArg15,
//                TReturn
//            >
//        >(
//            mockAndArgsStore,
//            typeof(TArg1),
//            typeof(TArg2),
//            typeof(TArg3),
//            typeof(TArg4),
//            typeof(TArg5),
//            typeof(TArg6),
//            typeof(TArg7),
//            typeof(TArg8),
//            typeof(TArg9),
//            typeof(TArg10),
//            typeof(TArg11),
//            typeof(TArg12),
//            typeof(TArg13),
//            typeof(TArg14),
//            typeof(TArg15)
//        );
//}
