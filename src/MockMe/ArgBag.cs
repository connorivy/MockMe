namespace MockMe;

public class ArgBag<T1>(Arg<T1> arg1) : IArgBag<T1>
{
    public Arg<T1> Arg1 { get; } = arg1;

    public bool AllArgsSatisfy(T1 arg1)
    {
        return this.Arg1.IsSatisfiedBy(arg1);
    }
}

public class ArgBag<T1, T2>(Arg<T1> arg1, Arg<T2> arg2) : IArgBag<ValueTuple<T1, T2>>
{
    public Arg<T1> Arg1 { get; } = arg1;
    public Arg<T2> Arg2 { get; } = arg2;

    public bool AllArgsSatisfy(T1 arg1, T2 arg2)
    {
        return this.Arg1.IsSatisfiedBy(arg1) && this.Arg2.IsSatisfiedBy(arg2);
    }

    public bool AllArgsSatisfy((T1, T2) args) => this.AllArgsSatisfy(args.Item1, args.Item2);
}
