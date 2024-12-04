namespace MockMe.Mocks.ClassMemberMocks.Setup;

public partial class MemberMockSetup
{
    protected static List<
        ArgBagWithMock<TOriginalArgCollection>
    > SetupGenericStore<TOriginalArgCollection>(Dictionary<int, object> mockAndArgsStore)
    {
        int hashCode = typeof(TOriginalArgCollection).GetHashCode();
        if (!mockAndArgsStore.TryGetValue(hashCode, out object? specificStore))
        {
            specificStore = new List<ArgBagWithMock<TOriginalArgCollection>>();
            mockAndArgsStore.Add(hashCode, specificStore);
        }
        return (List<ArgBagWithMock<TOriginalArgCollection>>)specificStore;
    }

    protected static int GetUniqueIntFromTypes(params Type[] types)
    {
        const int seed = 487;
        const int modifier = 31;
        return types.Aggregate(seed, (current, type) => current * modifier + type.GetHashCode());
    }
}
