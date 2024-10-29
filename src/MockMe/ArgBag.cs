namespace MockMe;

public class ArgBag<T1, TReturn>(
    Arg<T1> arg1,
    IMockCallbackAndReturnCallRetriever<Action<T1>, Func<T1, TReturn>> mock
) : IArgBag<T1, Action<T1>, Func<T1, TReturn>>
{
    public Arg<T1> Arg1 { get; } = arg1;
    public IMockCallbackAndReturnCallRetriever<Action<T1>, Func<T1, TReturn>> Mock { get; init; } =
        mock;

    //public static ArgBag<T1, TReturn> Construct(
    //    T1 collection,
    //    IMockCallbackAndReturnCallRetriever<Action<T1>, Func<T1, TReturn>> mock
    //) => new(collection, mock);

    public bool AllArgsSatisfy(T1 arg1)
    {
        return this.Arg1.IsSatisfiedBy(arg1);
    }
}

public class ArgBag<T1, T2, TReturn>(
    Arg<T1> arg1,
    Arg<T2> arg2,
    IMockCallbackAndReturnCallRetriever<Action<T1, T2>, Func<T1, T2, TReturn>> mock
) : IArgBag<ValueTuple<T1, T2>, Action<T1, T2>, Func<T1, T2, TReturn>>
{
    public Arg<T1> Arg1 { get; } = arg1;
    public Arg<T2> Arg2 { get; } = arg2;

    public IMockCallbackAndReturnCallRetriever<
        Action<T1, T2>,
        Func<T1, T2, TReturn>
    > Mock { get; } = mock;

    //public static ArgBag<T1, T2, TReturn> Construct(
    //    ValueTuple<T1, T2> collection,
    //    IMockCallbackAndReturnCallRetriever<Action<T1, T2>, Func<T1, T2, TReturn>> mock
    //) => new(collection.Item1, collection.Item2, mock);

    public bool AllArgsSatisfy(ValueTuple<T1, T2> argCollection) =>
        this.AllArgsSatisfy(argCollection.Item1, argCollection.Item2);

    public bool AllArgsSatisfy(T1 arg1, T2 arg2) =>
        this.Arg1.IsSatisfiedBy(arg1) && this.Arg2.IsSatisfiedBy(arg2);
}

public class ArgBag<T1, T2, T3, TReturn>(
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
