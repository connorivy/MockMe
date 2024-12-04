namespace MockMe.Mocks.ClassMemberMocks.Setup;

public partial class MemberMockSetup
{
    public static TMock SetupMethod<TMock>(List<TMock> mockAndArgsStore)
        where TMock : new()
    {
        TMock mock = new();
        mockAndArgsStore.Add(mock);
        return mock;
    }

    public static TMock SetupMethod<TOriginalArgCollection, TMock>(
        List<ArgBagWithMock<TOriginalArgCollection>> mockAndArgsStore,
        IArgBag<TOriginalArgCollection> argBag
    )
        where TMock : IMockCallbackRetriever<Action<TOriginalArgCollection>>, new()
    {
        TMock mock = new();
        mockAndArgsStore.Add(new ArgBagWithMock<TOriginalArgCollection>(argBag, mock));
        return mock;
    }
}
