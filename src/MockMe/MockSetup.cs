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
        List<ArgBag<TArg1, MemberMock<TArg1, TReturn>>> mockStore,
        Arg<TArg1> arg1
    )
    {
        MemberMock<TArg1, TReturn> memberMock = new();
        mockStore.Add(new() { Arg1 = arg1, Mock = memberMock });

        return memberMock;
    }

    protected static MemberMock<TArg1, TArg2, TReturn> SetupMethod<TArg1, TArg2, TReturn>(
        List<ArgBag<TArg1, TArg2, MemberMock<TArg1, TArg2, TReturn>>> mockStore,
        Arg<TArg1> arg1,
        Arg<TArg2> arg2
    )
    {
        MemberMock<TArg1, TArg2, TReturn> memberMock = new();
        mockStore.Add(
            new()
            {
                Arg1 = arg1,
                Arg2 = arg2,
                Mock = memberMock
            }
        );

        return memberMock;
    }

    protected static MemberMock<TArg1, TArg2, TArg3, TReturn> SetupMethod<
        TArg1,
        TArg2,
        TArg3,
        TReturn
    >(
        List<ArgBag<TArg1, TArg2, TArg3, MemberMock<TArg1, TArg2, TArg3, TReturn>>> mockStore,
        Arg<TArg1> arg1,
        Arg<TArg2> arg2,
        Arg<TArg3> arg3
    )
    {
        MemberMock<TArg1, TArg2, TArg3, TReturn> memberMock = new();
        mockStore.Add(
            new()
            {
                Arg1 = arg1,
                Arg2 = arg2,
                Arg3 = arg3,
                Mock = memberMock
            }
        );

        return memberMock;
    }
}
