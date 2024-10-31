using MockMe.Mocks;

namespace MockMe;

public class ArgBagWithVoidMemberMock<T1>(Arg<T1> arg1, VoidMemberMock<T1> mock)
    : VoidArgBagBase<T1, VoidMemberMock<T1>>(arg1, mock),
        IArgBag<T1, Action<T1>>
{
    IMockCallbackRetriever<Action<T1>> IArgBagWithMock<
        T1,
        IMockCallbackRetriever<Action<T1>>
    >.Mock => this.Mock;
}

public class VoidArgBagBase<T1, TMock>(Arg<T1> arg1, TMock mock)
    where TMock : IMockCallbackRetriever<Action<T1>>
{
    public Arg<T1> Arg1 { get; } = arg1;
    public TMock Mock { get; } = mock;

    public bool AllArgsSatisfy(T1 arg1)
    {
        return this.Arg1.IsSatisfiedBy(arg1);
    }
}

public class ArgBagWithMemberMock<T1, TReturn>(
    Arg<T1> arg1,
    IMockCallbackAndReturnCallRetriever<Action<T1>, Func<T1, TReturn>> mock
) : IArgBag<T1, Action<T1>, Func<T1, TReturn>>
{
    public Arg<T1> Arg1 { get; } = arg1;
    public IMockCallbackAndReturnCallRetriever<Action<T1>, Func<T1, TReturn>> Mock { get; init; } =
        mock;
    IMockCallbackRetriever<Action<T1>> IArgBagWithMock<
        T1,
        IMockCallbackRetriever<Action<T1>>
    >.Mock => this.Mock;

    //public static ArgBag<T1, TReturn> Construct(
    //    T1 collection,
    //    IMockCallbackAndReturnCallRetriever<Action<T1>, Func<T1, TReturn>> mock
    //) => new(collection, mock);

    public bool AllArgsSatisfy(T1 arg1)
    {
        return this.Arg1.IsSatisfiedBy(arg1);
    }
}

public class ArgBagWithMemberMock<T1, T2, TReturn>(
    Arg<T1> arg1,
    Arg<T2> arg2,
    IMockCallbackAndReturnCallRetriever<Action<T1, T2>, Func<T1, T2, TReturn>> mock
) : ArgBag<T1, T2>(arg1, arg2), IArgBag<ValueTuple<T1, T2>, Action<T1, T2>, Func<T1, T2, TReturn>>
{
    public IMockCallbackAndReturnCallRetriever<
        Action<T1, T2>,
        Func<T1, T2, TReturn>
    > Mock { get; } = mock;
    IMockCallbackRetriever<Action<T1, T2>> IArgBagWithMock<
        (T1, T2),
        IMockCallbackRetriever<Action<T1, T2>>
    >.Mock => this.Mock;
}

public class ArgBagWithMemberMock<T1, T2, T3, TReturn>(
    Arg<T1> arg1,
    Arg<T2> arg2,
    Arg<T3> arg3,
    IMockCallbackAndReturnCallRetriever<Action<T1, T2, T3>, Func<T1, T2, T3, TReturn>> mock
) : IArgBag<ValueTuple<T1, T2, T3>, Action<T1, T2, T3>, Func<T1, T2, T3, TReturn>>
{
    public Arg<T1> Arg1 { get; } = arg1;
    public Arg<T2> Arg2 { get; } = arg2;
    public Arg<T3> Arg3 { get; } = arg3;

    public IMockCallbackAndReturnCallRetriever<
        Action<T1, T2, T3>,
        Func<T1, T2, T3, TReturn>
    > Mock { get; } = mock;
    IMockCallbackRetriever<Action<T1, T2, T3>> IArgBagWithMock<
        (T1, T2, T3),
        IMockCallbackRetriever<Action<T1, T2, T3>>
    >.Mock => this.Mock;

    //public static ArgBag<T1, T2, T3, TReturn> Construct(
    //    ValueTuple<T1, T2, T3> collection,
    //    IMockCallbackAndReturnCallRetriever<Action<T1, T2, T3>, Func<T1, T2, T3, TReturn>> mock
    //) => new(collection.Item1, collection.Item2, collection.Item3, mock);

    public bool AllArgsSatisfy(ValueTuple<T1, T2, T3> argCollection) =>
        this.AllArgsSatisfy(argCollection.Item1, argCollection.Item2, argCollection.Item3);

    public bool AllArgsSatisfy(T1 arg1, T2 arg2, T3 arg3) =>
        this.Arg1.IsSatisfiedBy(arg1)
        && this.Arg2.IsSatisfiedBy(arg2)
        && this.Arg3.IsSatisfiedBy(arg3);
}
