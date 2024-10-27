using MockMe.Mocks;

namespace MockMe;

public class MockSetup
{
    protected static MemberMock<TReturn> SetupMethod<TReturn>(List<MemberMock<TReturn>> mockStore)
    {
        MemberMock<TReturn> memberMock = new();
        mockStore.Add(memberMock);
        return memberMock;
    }

    protected static MemberMock<TArg1, TReturn> SetupMethod<TArg1, TReturn>(
        List<ArgBag<TArg1, TReturn>> mockStore,
        Arg<TArg1> arg1
    )
    {
        MemberMock<TArg1, TReturn> memberMock = new();
        mockStore.Add(new(arg1, memberMock));

        return memberMock;
    }

    protected static MemberMock<TArg1, TArg2, TReturn> SetupMethod<TArg1, TArg2, TReturn>(
        List<ArgBag<TArg1, TArg2, TReturn>> mockStore,
        Arg<TArg1> arg1,
        Arg<TArg2> arg2
    )
    {
        MemberMock<TArg1, TArg2, TReturn> memberMock = new();
        mockStore.Add(new(arg1, arg2, memberMock));

        return memberMock;
    }

    protected static MemberMock<TArg1, TArg2, TArg3, TReturn> SetupMethod<
        TArg1,
        TArg2,
        TArg3,
        TReturn
    >(
        List<ArgBag<TArg1, TArg2, TArg3, TReturn>> mockStore,
        Arg<TArg1> arg1,
        Arg<TArg2> arg2,
        Arg<TArg3> arg3
    )
    {
        MemberMock<TArg1, TArg2, TArg3, TReturn> memberMock = new();
        mockStore.Add(new(arg1, arg2, arg3, memberMock));

        return memberMock;
    }

    //protected static MemberMock<TArg1, TReturn> SetupMethod<TArg1, TReturn>(
    //    List<ArgBag<TArg1, TReturn>> mockStore,
    //    Arg<TArg1> arg1
    //) =>
    //    SetupMemberMockBase<
    //        TArg1,
    //        MemberMock<TArg1, TReturn>,
    //        Action<TArg1>,
    //        Func<TArg1, TReturn>,
    //        ArgBag<TArg1, TReturn>
    //    >(arg1, mockStore);

    //private static TMock SetupMemberMockBase<
    //    TArgCollection,
    //    TMock,
    //    TCallback,
    //    TReturnCall,
    //    TArgBag
    //>(TArgCollection argCollection, ICollection<TArgBag> mockStore)
    //    where TMock : IMockCallbackAndReturnCallRetriever<TCallback, TReturnCall>, new()
    //    where TArgBag : IArgBag<TArgCollection, TCallback, TReturnCall, TArgBag>
    //{
    //    TMock memberMock = new();
    //    var x = TArgBag.Construct(argCollection, memberMock);
    //    mockStore.Add(TArgBag.Construct(argCollection, memberMock));
    //    return memberMock;
    //}
}
