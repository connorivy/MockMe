namespace MockMe;

public class MockAsserter
{
    //private readonly MockCallTracker mockCallTracker;

    //internal MockAsserter(MockCallTracker mockCallTracker)
    //{
    //    this.mockCallTracker = mockCallTracker;
    //}

    //public MemberAsserter SayMyNameNTimes(Arg<int> arg)
    //{
    //    int numTimesCalled = 0;
    //    for (int i = mockCallTracker.SayMyNameNTimesCalls.Count - 1; i >= 0; i--)
    //    {
    //        var argBag = mockCallTracker.SayMyNameNTimesCalls[i];
    //        if (arg.IsSatisfiedBy(argBag.Item1))
    //        {
    //            numTimesCalled++;
    //        }
    //    }

    //    return new(numTimesCalled);
    //}

    private static readonly MemberAsserter defaultAsserter = new(0);

    protected static MemberAsserter GetMemberAsserter<TArg1>(
        List<TArg1>? callStore,
        Arg<TArg1> arg1
    ) => GetMemberAsserterBase(callStore, new ArgBag<TArg1>(arg1));

    protected static MemberAsserter GetMemberAsserter<TArg1, TArg2>(
        List<ValueTuple<TArg1, TArg2>>? callStore,
        Arg<TArg1> arg1,
        Arg<TArg2> arg2
    ) => GetMemberAsserterBase(callStore, new ArgBag<TArg1, TArg2>(arg1, arg2));

    private static MemberAsserter GetMemberAsserterBase<TArgCollection>(
        IList<TArgCollection>? callStore,
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
