using MockMe.Asserters;

namespace MockMe;

public class MockAsserter
{
    private static readonly MemberAsserter defaultAsserter = new(0);

    protected static MemberAsserter GetMemberAsserter<TArgCollection>(
        IReadOnlyList<TArgCollection>? callStore,
        IArgBag<TArgCollection> argBag
    )
    {
        if (callStore is null)
        {
            return defaultAsserter;
        }

        int numTimesCalled = 0;
        for (int i = callStore.Count - 1; i >= 0; i--)
        {
            var argCollection = callStore[i];
            if (argBag.AllArgsSatisfy(argCollection))
            {
                numTimesCalled++;
            }
        }

        return new(numTimesCalled);
    }
}
