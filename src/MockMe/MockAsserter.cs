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

    protected static MemberAsserter GetMemberAsserter<TArg1>()
    {
        return new(0);
    }

    protected static MemberAsserter GetMemberAsserter<TArg1, TArg2>(
        List<ValueTuple<TArg1, TArg2>> callStore,
        Arg<TArg1> arg1,
        Arg<TArg2> arg2
    )
    {
        int numTimesCalled = 0;
        for (int i = callStore.Count - 1; i >= 0; i--)
        {
            var argBag = callStore[i];
            if (arg1.IsSatisfiedBy(argBag.Item1) && arg2.IsSatisfiedBy(argBag.Item2))
            {
                numTimesCalled++;
            }
        }

        return new(numTimesCalled);
    }
}
