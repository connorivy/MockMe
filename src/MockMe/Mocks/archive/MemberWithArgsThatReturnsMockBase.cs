namespace MockMe.Mocks.archive;

public abstract class MemberWithArgsThatReturnsMockBase<TReturn, TSelf, TCallback, TReturnFunc>
    : IMemberMockWithCallback<TSelf, TCallback>,
        IMemberThatReturnsMock<TReturn, TSelf, TReturnFunc>
{
    List<TCallback>? IMemberMockWithCallback<TSelf, TCallback>.Callbacks { get; set; }
    Queue<TReturnFunc>? IMemberThatReturnsMock<
        TReturn,
        TSelf,
        TReturnFunc
    >.ReturnCalls { get; set; }
}

public class MemberMock<TArg1, TReturn>
    : MemberWithArgsThatReturnsMockBase<
        TReturn,
        MemberMock<TArg1, TReturn>,
        Action<TArg1>,
        Func<TArg1, TReturn>
    >,
        IMemberMock<TArg1, TReturn, MemberMock<TArg1, TReturn>> { }

public class MemberMock<TArg1, TArg2, TReturn>
    : MemberWithArgsThatReturnsMockBase<
        TReturn,
        MemberMock<TArg1, TArg2, TReturn>,
        Action<TArg1, TArg2>,
        Func<TArg1, TArg2, TReturn>
    >,
        IMemberWithArgsThatReturnsMock<TArg1, TArg2, TReturn, MemberMock<TArg1, TArg2, TReturn>> { }

public class MemberMock<TArg1, TArg2, TArg3, TReturn>
    : MemberWithArgsThatReturnsMockBase<
        TReturn,
        MemberMock<TArg1, TArg2, TArg3, TReturn>,
        Action<TArg1, TArg2, TArg3>,
        Func<TArg1, TArg2, TArg3, TReturn>
    >,
        IMemberWithArgsThatReturnsMock<
            TArg1,
            TArg2,
            TArg3,
            TReturn,
            MemberMock<TArg1, TArg2, TArg3, TReturn>
        > { }

public class MemberMock<TArg1, TArg2, TArg3, TArg4, TReturn>
    : MemberWithArgsThatReturnsMockBase<
        TReturn,
        MemberMock<TArg1, TArg2, TArg3, TArg4, TReturn>,
        Action<TArg1, TArg2, TArg3, TArg4>,
        Func<TArg1, TArg2, TArg3, TArg4, TReturn>
    >,
        IMemberWithArgsThatReturnsMock<
            TArg1,
            TArg2,
            TArg3,
            TArg4,
            TReturn,
            MemberMock<TArg1, TArg2, TArg3, TArg4, TReturn>
        > { }

public class MemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TReturn>
    : MemberWithArgsThatReturnsMockBase<
        TReturn,
        MemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TReturn>,
        Action<TArg1, TArg2, TArg3, TArg4, TArg5>,
        Func<TArg1, TArg2, TArg3, TArg4, TArg5, TReturn>
    >,
        IMemberWithArgsThatReturnsMock<
            TArg1,
            TArg2,
            TArg3,
            TArg4,
            TArg5,
            TReturn,
            MemberMock<TArg1, TArg2, TArg3, TArg4, TArg5, TReturn>
        > { }
