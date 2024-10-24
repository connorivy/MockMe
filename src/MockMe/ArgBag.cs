namespace MockMe;

public class ArgBag<T1, TMock>
{
    public Arg<T1> Arg1 { get; init; }
    public TMock Mock { get; init; }

    public bool AllArgsSatisfy(T1 arg1)
    {
        return Arg1.IsSatisfiedBy(arg1);
    }
}

public class ArgBag<T1, T2, TMock>
{
    public Arg<T1> Arg1 { get; init; }
    public Arg<T2> Arg2 { get; init; }
    public TMock Mock { get; init; }

    public bool AllArgsSatisfy(T1 arg1, T2 arg2) =>
        Arg1.IsSatisfiedBy(arg1) && Arg2.IsSatisfiedBy(arg2);
}

public class ArgBag<T1, T2, T3, TMock>
{
    public Arg<T1> Arg1 { get; init; }
    public Arg<T2> Arg2 { get; init; }
    public Arg<T3> Arg3 { get; init; }
    public TMock Mock { get; init; }

    public bool AllArgsSatisfy(T1 arg1, T2 arg2, T3 arg3) =>
        Arg1.IsSatisfiedBy(arg1) && Arg2.IsSatisfiedBy(arg2) && Arg3.IsSatisfiedBy(arg3);
}
