namespace MockMe.Mocks.ClassMemberMocks.CallTracker;

public class GenericCallStoreRetriever
{
    public static List<TArg1> GetGenericCallStore<TArg1>(
        Dictionary<int, object> argStore,
        int hashCode
    ) => GetGenericCallStoreBase<TArg1>(argStore, hashCode);

    public static List<ValueTuple<TArg1, TArg2>> GetGenericCallStore<TArg1, TArg2>(
        Dictionary<int, object> argStore,
        int hashCode
    ) => GetGenericCallStoreBase<ValueTuple<TArg1, TArg2>>(argStore, hashCode);

    public static List<ValueTuple<TArg1, TArg2, TArg3>> GetGenericCallStore<TArg1, TArg2, TArg3>(
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
