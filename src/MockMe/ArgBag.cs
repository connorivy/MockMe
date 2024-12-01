namespace MockMe;

public class ArgBag<T1>(Arg<T1> arg1) : IArgBag<T1>, IArgBag<OriginalArgBag<T1>>
{
    public Arg<T1> Arg1 { get; } = arg1;

    public bool AllArgsSatisfy(T1 arg1)
    {
        return this.Arg1.IsSatisfiedBy(arg1);
    }

    public bool AllArgsSatisfy(OriginalArgBag<T1> originalArgBag)
    {
        return this.Arg1.IsSatisfiedBy(originalArgBag.IntArg1);
    }
}

public class ArgBag<T1, T2>(Arg<T1> arg1, Arg<T2> arg2)
    : IArgBag<ValueTuple<T1, T2>>,
        IArgBag<OriginalArgBag<T1, T2>>
{
    public Arg<T1> Arg1 { get; } = arg1;
    public Arg<T2> Arg2 { get; } = arg2;

    public bool AllArgsSatisfy(T1 arg1, T2 arg2)
    {
        return this.Arg1.IsSatisfiedBy(arg1) && this.Arg2.IsSatisfiedBy(arg2);
    }

    public bool AllArgsSatisfy((T1, T2) args) => this.AllArgsSatisfy(args.Item1, args.Item2);

    public bool AllArgsSatisfy(OriginalArgBag<T1, T2> args) =>
        this.AllArgsSatisfy(args.IntArg1, args.IntArg2);
}

public class ArgBag<T1, T2, T3>(Arg<T1> arg1, Arg<T2> arg2, Arg<T3> arg3)
    : IArgBag<ValueTuple<T1, T2, T3>>,
        IArgBag<OriginalArgBag<T1, T2, T3>>
{
    public Arg<T1> Arg1 { get; } = arg1;
    public Arg<T2> Arg2 { get; } = arg2;
    public Arg<T3> Arg3 { get; } = arg3;

    public bool AllArgsSatisfy(T1 arg1, T2 arg2, T3 arg3)
    {
        return this.Arg1.IsSatisfiedBy(arg1)
            && this.Arg2.IsSatisfiedBy(arg2)
            && this.Arg3.IsSatisfiedBy(arg3);
    }

    public bool AllArgsSatisfy((T1, T2, T3) args) =>
        this.AllArgsSatisfy(args.Item1, args.Item2, args.Item3);

    public bool AllArgsSatisfy(OriginalArgBag<T1, T2, T3> args) =>
        this.AllArgsSatisfy(args.IntArg1, args.IntArg2, args.IntArg3);
}

public class ArgBag<T1, T2, T3, T4>(Arg<T1> arg1, Arg<T2> arg2, Arg<T3> arg3, Arg<T4> arg4)
    : IArgBag<ValueTuple<T1, T2, T3, T4>>,
        IArgBag<OriginalArgBag<T1, T2, T3, T4>>
{
    public Arg<T1> Arg1 { get; } = arg1;
    public Arg<T2> Arg2 { get; } = arg2;
    public Arg<T3> Arg3 { get; } = arg3;
    public Arg<T4> Arg4 { get; } = arg4;

    public bool AllArgsSatisfy(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
    {
        return this.Arg1.IsSatisfiedBy(arg1)
            && this.Arg2.IsSatisfiedBy(arg2)
            && this.Arg3.IsSatisfiedBy(arg3)
            && this.Arg4.IsSatisfiedBy(arg4);
    }

    public bool AllArgsSatisfy((T1, T2, T3, T4) args) =>
        this.AllArgsSatisfy(args.Item1, args.Item2, args.Item3, args.Item4);

    public bool AllArgsSatisfy(OriginalArgBag<T1, T2, T3, T4> args) =>
        this.AllArgsSatisfy(args.IntArg1, args.IntArg2, args.IntArg3, args.IntArg4);
}

public class ArgBag<T1, T2, T3, T4, T5>(
    Arg<T1> arg1,
    Arg<T2> arg2,
    Arg<T3> arg3,
    Arg<T4> arg4,
    Arg<T5> arg5
) : IArgBag<ValueTuple<T1, T2, T3, T4, T5>>
{
    public Arg<T1> Arg1 { get; } = arg1;
    public Arg<T2> Arg2 { get; } = arg2;
    public Arg<T3> Arg3 { get; } = arg3;
    public Arg<T4> Arg4 { get; } = arg4;
    public Arg<T5> Arg5 { get; } = arg5;

    public bool AllArgsSatisfy(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
    {
        return this.Arg1.IsSatisfiedBy(arg1)
            && this.Arg2.IsSatisfiedBy(arg2)
            && this.Arg3.IsSatisfiedBy(arg3)
            && this.Arg4.IsSatisfiedBy(arg4)
            && this.Arg5.IsSatisfiedBy(arg5);
    }

    public bool AllArgsSatisfy((T1, T2, T3, T4, T5) args) =>
        this.AllArgsSatisfy(args.Item1, args.Item2, args.Item3, args.Item4, args.Item5);
}

public class ArgBag<T1, T2, T3, T4, T5, T6>(
    Arg<T1> arg1,
    Arg<T2> arg2,
    Arg<T3> arg3,
    Arg<T4> arg4,
    Arg<T5> arg5,
    Arg<T6> arg6
) : IArgBag<(T1, T2, T3, T4, T5, T6)>
{
    public Arg<T1> Arg1 { get; } = arg1;
    public Arg<T2> Arg2 { get; } = arg2;
    public Arg<T3> Arg3 { get; } = arg3;
    public Arg<T4> Arg4 { get; } = arg4;
    public Arg<T5> Arg5 { get; } = arg5;
    public Arg<T6> Arg6 { get; } = arg6;

    public bool AllArgsSatisfy(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6) =>
        this.Arg1.IsSatisfiedBy(arg1)
        && this.Arg2.IsSatisfiedBy(arg2)
        && this.Arg3.IsSatisfiedBy(arg3)
        && this.Arg4.IsSatisfiedBy(arg4)
        && this.Arg5.IsSatisfiedBy(arg5)
        && this.Arg6.IsSatisfiedBy(arg6);

    public bool AllArgsSatisfy((T1, T2, T3, T4, T5, T6) args) =>
        this.AllArgsSatisfy(args.Item1, args.Item2, args.Item3, args.Item4, args.Item5, args.Item6);
}

public class ArgBag<T1, T2, T3, T4, T5, T6, T7>(
    Arg<T1> arg1,
    Arg<T2> arg2,
    Arg<T3> arg3,
    Arg<T4> arg4,
    Arg<T5> arg5,
    Arg<T6> arg6,
    Arg<T7> arg7
) : IArgBag<(T1, T2, T3, T4, T5, T6, T7)>
{
    public Arg<T1> Arg1 { get; } = arg1;
    public Arg<T2> Arg2 { get; } = arg2;
    public Arg<T3> Arg3 { get; } = arg3;
    public Arg<T4> Arg4 { get; } = arg4;
    public Arg<T5> Arg5 { get; } = arg5;
    public Arg<T6> Arg6 { get; } = arg6;
    public Arg<T7> Arg7 { get; } = arg7;

    public bool AllArgsSatisfy(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7) =>
        this.Arg1.IsSatisfiedBy(arg1)
        && this.Arg2.IsSatisfiedBy(arg2)
        && this.Arg3.IsSatisfiedBy(arg3)
        && this.Arg4.IsSatisfiedBy(arg4)
        && this.Arg5.IsSatisfiedBy(arg5)
        && this.Arg6.IsSatisfiedBy(arg6)
        && this.Arg7.IsSatisfiedBy(arg7);

    public bool AllArgsSatisfy((T1, T2, T3, T4, T5, T6, T7) args) =>
        this.AllArgsSatisfy(
            args.Item1,
            args.Item2,
            args.Item3,
            args.Item4,
            args.Item5,
            args.Item6,
            args.Item7
        );
}

public class ArgBag<T1, T2, T3, T4, T5, T6, T7, T8>(
    Arg<T1> arg1,
    Arg<T2> arg2,
    Arg<T3> arg3,
    Arg<T4> arg4,
    Arg<T5> arg5,
    Arg<T6> arg6,
    Arg<T7> arg7,
    Arg<T8> arg8
) : IArgBag<(T1, T2, T3, T4, T5, T6, T7, T8)>
{
    public Arg<T1> Arg1 { get; } = arg1;
    public Arg<T2> Arg2 { get; } = arg2;
    public Arg<T3> Arg3 { get; } = arg3;
    public Arg<T4> Arg4 { get; } = arg4;
    public Arg<T5> Arg5 { get; } = arg5;
    public Arg<T6> Arg6 { get; } = arg6;
    public Arg<T7> Arg7 { get; } = arg7;
    public Arg<T8> Arg8 { get; } = arg8;

    public bool AllArgsSatisfy(
        T1 arg1,
        T2 arg2,
        T3 arg3,
        T4 arg4,
        T5 arg5,
        T6 arg6,
        T7 arg7,
        T8 arg8
    ) =>
        this.Arg1.IsSatisfiedBy(arg1)
        && this.Arg2.IsSatisfiedBy(arg2)
        && this.Arg3.IsSatisfiedBy(arg3)
        && this.Arg4.IsSatisfiedBy(arg4)
        && this.Arg5.IsSatisfiedBy(arg5)
        && this.Arg6.IsSatisfiedBy(arg6)
        && this.Arg7.IsSatisfiedBy(arg7)
        && this.Arg8.IsSatisfiedBy(arg8);

    public bool AllArgsSatisfy((T1, T2, T3, T4, T5, T6, T7, T8) args) =>
        this.AllArgsSatisfy(
            args.Item1,
            args.Item2,
            args.Item3,
            args.Item4,
            args.Item5,
            args.Item6,
            args.Item7,
            args.Item8
        );
}

public class ArgBag<T1, T2, T3, T4, T5, T6, T7, T8, T9>(
    Arg<T1> arg1,
    Arg<T2> arg2,
    Arg<T3> arg3,
    Arg<T4> arg4,
    Arg<T5> arg5,
    Arg<T6> arg6,
    Arg<T7> arg7,
    Arg<T8> arg8,
    Arg<T9> arg9
) : IArgBag<(T1, T2, T3, T4, T5, T6, T7, T8, T9)>
{
    public Arg<T1> Arg1 { get; } = arg1;
    public Arg<T2> Arg2 { get; } = arg2;
    public Arg<T3> Arg3 { get; } = arg3;
    public Arg<T4> Arg4 { get; } = arg4;
    public Arg<T5> Arg5 { get; } = arg5;
    public Arg<T6> Arg6 { get; } = arg6;
    public Arg<T7> Arg7 { get; } = arg7;
    public Arg<T8> Arg8 { get; } = arg8;
    public Arg<T9> Arg9 { get; } = arg9;

    public bool AllArgsSatisfy(
        T1 arg1,
        T2 arg2,
        T3 arg3,
        T4 arg4,
        T5 arg5,
        T6 arg6,
        T7 arg7,
        T8 arg8,
        T9 arg9
    ) =>
        this.Arg1.IsSatisfiedBy(arg1)
        && this.Arg2.IsSatisfiedBy(arg2)
        && this.Arg3.IsSatisfiedBy(arg3)
        && this.Arg4.IsSatisfiedBy(arg4)
        && this.Arg5.IsSatisfiedBy(arg5)
        && this.Arg6.IsSatisfiedBy(arg6)
        && this.Arg7.IsSatisfiedBy(arg7)
        && this.Arg8.IsSatisfiedBy(arg8)
        && this.Arg9.IsSatisfiedBy(arg9);

    public bool AllArgsSatisfy((T1, T2, T3, T4, T5, T6, T7, T8, T9) args) =>
        this.AllArgsSatisfy(
            args.Item1,
            args.Item2,
            args.Item3,
            args.Item4,
            args.Item5,
            args.Item6,
            args.Item7,
            args.Item8,
            args.Item9
        );
}

public class ArgBag<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(
    Arg<T1> arg1,
    Arg<T2> arg2,
    Arg<T3> arg3,
    Arg<T4> arg4,
    Arg<T5> arg5,
    Arg<T6> arg6,
    Arg<T7> arg7,
    Arg<T8> arg8,
    Arg<T9> arg9,
    Arg<T10> arg10
) : IArgBag<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)>
{
    public Arg<T1> Arg1 { get; } = arg1;
    public Arg<T2> Arg2 { get; } = arg2;
    public Arg<T3> Arg3 { get; } = arg3;
    public Arg<T4> Arg4 { get; } = arg4;
    public Arg<T5> Arg5 { get; } = arg5;
    public Arg<T6> Arg6 { get; } = arg6;
    public Arg<T7> Arg7 { get; } = arg7;
    public Arg<T8> Arg8 { get; } = arg8;
    public Arg<T9> Arg9 { get; } = arg9;
    public Arg<T10> Arg10 { get; } = arg10;

    public bool AllArgsSatisfy(
        T1 arg1,
        T2 arg2,
        T3 arg3,
        T4 arg4,
        T5 arg5,
        T6 arg6,
        T7 arg7,
        T8 arg8,
        T9 arg9,
        T10 arg10
    ) =>
        this.Arg1.IsSatisfiedBy(arg1)
        && this.Arg2.IsSatisfiedBy(arg2)
        && this.Arg3.IsSatisfiedBy(arg3)
        && this.Arg4.IsSatisfiedBy(arg4)
        && this.Arg5.IsSatisfiedBy(arg5)
        && this.Arg6.IsSatisfiedBy(arg6)
        && this.Arg7.IsSatisfiedBy(arg7)
        && this.Arg8.IsSatisfiedBy(arg8)
        && this.Arg9.IsSatisfiedBy(arg9)
        && this.Arg10.IsSatisfiedBy(arg10);

    public bool AllArgsSatisfy((T1, T2, T3, T4, T5, T6, T7, T8, T9, T10) args) =>
        this.AllArgsSatisfy(
            args.Item1,
            args.Item2,
            args.Item3,
            args.Item4,
            args.Item5,
            args.Item6,
            args.Item7,
            args.Item8,
            args.Item9,
            args.Item10
        );
}

public class ArgBag<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(
    Arg<T1> arg1,
    Arg<T2> arg2,
    Arg<T3> arg3,
    Arg<T4> arg4,
    Arg<T5> arg5,
    Arg<T6> arg6,
    Arg<T7> arg7,
    Arg<T8> arg8,
    Arg<T9> arg9,
    Arg<T10> arg10,
    Arg<T11> arg11
) : IArgBag<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11)>
{
    public Arg<T1> Arg1 { get; } = arg1;
    public Arg<T2> Arg2 { get; } = arg2;
    public Arg<T3> Arg3 { get; } = arg3;
    public Arg<T4> Arg4 { get; } = arg4;
    public Arg<T5> Arg5 { get; } = arg5;
    public Arg<T6> Arg6 { get; } = arg6;
    public Arg<T7> Arg7 { get; } = arg7;
    public Arg<T8> Arg8 { get; } = arg8;
    public Arg<T9> Arg9 { get; } = arg9;
    public Arg<T10> Arg10 { get; } = arg10;
    public Arg<T11> Arg11 { get; } = arg11;

    public bool AllArgsSatisfy(
        T1 arg1,
        T2 arg2,
        T3 arg3,
        T4 arg4,
        T5 arg5,
        T6 arg6,
        T7 arg7,
        T8 arg8,
        T9 arg9,
        T10 arg10,
        T11 arg11
    ) =>
        this.Arg1.IsSatisfiedBy(arg1)
        && this.Arg2.IsSatisfiedBy(arg2)
        && this.Arg3.IsSatisfiedBy(arg3)
        && this.Arg4.IsSatisfiedBy(arg4)
        && this.Arg5.IsSatisfiedBy(arg5)
        && this.Arg6.IsSatisfiedBy(arg6)
        && this.Arg7.IsSatisfiedBy(arg7)
        && this.Arg8.IsSatisfiedBy(arg8)
        && this.Arg9.IsSatisfiedBy(arg9)
        && this.Arg10.IsSatisfiedBy(arg10)
        && this.Arg11.IsSatisfiedBy(arg11);

    public bool AllArgsSatisfy((T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11) args) =>
        this.AllArgsSatisfy(
            args.Item1,
            args.Item2,
            args.Item3,
            args.Item4,
            args.Item5,
            args.Item6,
            args.Item7,
            args.Item8,
            args.Item9,
            args.Item10,
            args.Item11
        );
}

public class ArgBag<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(
    Arg<T1> arg1,
    Arg<T2> arg2,
    Arg<T3> arg3,
    Arg<T4> arg4,
    Arg<T5> arg5,
    Arg<T6> arg6,
    Arg<T7> arg7,
    Arg<T8> arg8,
    Arg<T9> arg9,
    Arg<T10> arg10,
    Arg<T11> arg11,
    Arg<T12> arg12
) : IArgBag<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12)>
{
    public Arg<T1> Arg1 { get; } = arg1;
    public Arg<T2> Arg2 { get; } = arg2;
    public Arg<T3> Arg3 { get; } = arg3;
    public Arg<T4> Arg4 { get; } = arg4;
    public Arg<T5> Arg5 { get; } = arg5;
    public Arg<T6> Arg6 { get; } = arg6;
    public Arg<T7> Arg7 { get; } = arg7;
    public Arg<T8> Arg8 { get; } = arg8;
    public Arg<T9> Arg9 { get; } = arg9;
    public Arg<T10> Arg10 { get; } = arg10;
    public Arg<T11> Arg11 { get; } = arg11;
    public Arg<T12> Arg12 { get; } = arg12;

    public bool AllArgsSatisfy(
        T1 arg1,
        T2 arg2,
        T3 arg3,
        T4 arg4,
        T5 arg5,
        T6 arg6,
        T7 arg7,
        T8 arg8,
        T9 arg9,
        T10 arg10,
        T11 arg11,
        T12 arg12
    ) =>
        this.Arg1.IsSatisfiedBy(arg1)
        && this.Arg2.IsSatisfiedBy(arg2)
        && this.Arg3.IsSatisfiedBy(arg3)
        && this.Arg4.IsSatisfiedBy(arg4)
        && this.Arg5.IsSatisfiedBy(arg5)
        && this.Arg6.IsSatisfiedBy(arg6)
        && this.Arg7.IsSatisfiedBy(arg7)
        && this.Arg8.IsSatisfiedBy(arg8)
        && this.Arg9.IsSatisfiedBy(arg9)
        && this.Arg10.IsSatisfiedBy(arg10)
        && this.Arg11.IsSatisfiedBy(arg11)
        && this.Arg12.IsSatisfiedBy(arg12);

    public bool AllArgsSatisfy((T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12) args) =>
        this.AllArgsSatisfy(
            args.Item1,
            args.Item2,
            args.Item3,
            args.Item4,
            args.Item5,
            args.Item6,
            args.Item7,
            args.Item8,
            args.Item9,
            args.Item10,
            args.Item11,
            args.Item12
        );
}

public class ArgBag<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(
    Arg<T1> arg1,
    Arg<T2> arg2,
    Arg<T3> arg3,
    Arg<T4> arg4,
    Arg<T5> arg5,
    Arg<T6> arg6,
    Arg<T7> arg7,
    Arg<T8> arg8,
    Arg<T9> arg9,
    Arg<T10> arg10,
    Arg<T11> arg11,
    Arg<T12> arg12,
    Arg<T13> arg13
) : IArgBag<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13)>
{
    public Arg<T1> Arg1 { get; } = arg1;
    public Arg<T2> Arg2 { get; } = arg2;
    public Arg<T3> Arg3 { get; } = arg3;
    public Arg<T4> Arg4 { get; } = arg4;
    public Arg<T5> Arg5 { get; } = arg5;
    public Arg<T6> Arg6 { get; } = arg6;
    public Arg<T7> Arg7 { get; } = arg7;
    public Arg<T8> Arg8 { get; } = arg8;
    public Arg<T9> Arg9 { get; } = arg9;
    public Arg<T10> Arg10 { get; } = arg10;
    public Arg<T11> Arg11 { get; } = arg11;
    public Arg<T12> Arg12 { get; } = arg12;
    public Arg<T13> Arg13 { get; } = arg13;

    public bool AllArgsSatisfy(
        T1 arg1,
        T2 arg2,
        T3 arg3,
        T4 arg4,
        T5 arg5,
        T6 arg6,
        T7 arg7,
        T8 arg8,
        T9 arg9,
        T10 arg10,
        T11 arg11,
        T12 arg12,
        T13 arg13
    ) =>
        this.Arg1.IsSatisfiedBy(arg1)
        && this.Arg2.IsSatisfiedBy(arg2)
        && this.Arg3.IsSatisfiedBy(arg3)
        && this.Arg4.IsSatisfiedBy(arg4)
        && this.Arg5.IsSatisfiedBy(arg5)
        && this.Arg6.IsSatisfiedBy(arg6)
        && this.Arg7.IsSatisfiedBy(arg7)
        && this.Arg8.IsSatisfiedBy(arg8)
        && this.Arg9.IsSatisfiedBy(arg9)
        && this.Arg10.IsSatisfiedBy(arg10)
        && this.Arg11.IsSatisfiedBy(arg11)
        && this.Arg12.IsSatisfiedBy(arg12)
        && this.Arg13.IsSatisfiedBy(arg13);

    public bool AllArgsSatisfy((T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13) args) =>
        this.AllArgsSatisfy(
            args.Item1,
            args.Item2,
            args.Item3,
            args.Item4,
            args.Item5,
            args.Item6,
            args.Item7,
            args.Item8,
            args.Item9,
            args.Item10,
            args.Item11,
            args.Item12,
            args.Item13
        );
}

public class ArgBag<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(
    Arg<T1> arg1,
    Arg<T2> arg2,
    Arg<T3> arg3,
    Arg<T4> arg4,
    Arg<T5> arg5,
    Arg<T6> arg6,
    Arg<T7> arg7,
    Arg<T8> arg8,
    Arg<T9> arg9,
    Arg<T10> arg10,
    Arg<T11> arg11,
    Arg<T12> arg12,
    Arg<T13> arg13,
    Arg<T14> arg14
) : IArgBag<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)>
{
    public Arg<T1> Arg1 { get; } = arg1;
    public Arg<T2> Arg2 { get; } = arg2;
    public Arg<T3> Arg3 { get; } = arg3;
    public Arg<T4> Arg4 { get; } = arg4;
    public Arg<T5> Arg5 { get; } = arg5;
    public Arg<T6> Arg6 { get; } = arg6;
    public Arg<T7> Arg7 { get; } = arg7;
    public Arg<T8> Arg8 { get; } = arg8;
    public Arg<T9> Arg9 { get; } = arg9;
    public Arg<T10> Arg10 { get; } = arg10;
    public Arg<T11> Arg11 { get; } = arg11;
    public Arg<T12> Arg12 { get; } = arg12;
    public Arg<T13> Arg13 { get; } = arg13;
    public Arg<T14> Arg14 { get; } = arg14;

    public bool AllArgsSatisfy(
        T1 arg1,
        T2 arg2,
        T3 arg3,
        T4 arg4,
        T5 arg5,
        T6 arg6,
        T7 arg7,
        T8 arg8,
        T9 arg9,
        T10 arg10,
        T11 arg11,
        T12 arg12,
        T13 arg13,
        T14 arg14
    ) =>
        this.Arg1.IsSatisfiedBy(arg1)
        && this.Arg2.IsSatisfiedBy(arg2)
        && this.Arg3.IsSatisfiedBy(arg3)
        && this.Arg4.IsSatisfiedBy(arg4)
        && this.Arg5.IsSatisfiedBy(arg5)
        && this.Arg6.IsSatisfiedBy(arg6)
        && this.Arg7.IsSatisfiedBy(arg7)
        && this.Arg8.IsSatisfiedBy(arg8)
        && this.Arg9.IsSatisfiedBy(arg9)
        && this.Arg10.IsSatisfiedBy(arg10)
        && this.Arg11.IsSatisfiedBy(arg11)
        && this.Arg12.IsSatisfiedBy(arg12)
        && this.Arg13.IsSatisfiedBy(arg13)
        && this.Arg14.IsSatisfiedBy(arg14);

    public bool AllArgsSatisfy(
        (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14) args
    ) =>
        this.AllArgsSatisfy(
            args.Item1,
            args.Item2,
            args.Item3,
            args.Item4,
            args.Item5,
            args.Item6,
            args.Item7,
            args.Item8,
            args.Item9,
            args.Item10,
            args.Item11,
            args.Item12,
            args.Item13,
            args.Item14
        );
}

public class ArgBag<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
    Arg<T1> arg1,
    Arg<T2> arg2,
    Arg<T3> arg3,
    Arg<T4> arg4,
    Arg<T5> arg5,
    Arg<T6> arg6,
    Arg<T7> arg7,
    Arg<T8> arg8,
    Arg<T9> arg9,
    Arg<T10> arg10,
    Arg<T11> arg11,
    Arg<T12> arg12,
    Arg<T13> arg13,
    Arg<T14> arg14,
    Arg<T15> arg15
) : IArgBag<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15)>
{
    public Arg<T1> Arg1 { get; } = arg1;
    public Arg<T2> Arg2 { get; } = arg2;
    public Arg<T3> Arg3 { get; } = arg3;
    public Arg<T4> Arg4 { get; } = arg4;
    public Arg<T5> Arg5 { get; } = arg5;
    public Arg<T6> Arg6 { get; } = arg6;
    public Arg<T7> Arg7 { get; } = arg7;
    public Arg<T8> Arg8 { get; } = arg8;
    public Arg<T9> Arg9 { get; } = arg9;
    public Arg<T10> Arg10 { get; } = arg10;
    public Arg<T11> Arg11 { get; } = arg11;
    public Arg<T12> Arg12 { get; } = arg12;
    public Arg<T13> Arg13 { get; } = arg13;
    public Arg<T14> Arg14 { get; } = arg14;
    public Arg<T15> Arg15 { get; } = arg15;

    public bool AllArgsSatisfy(
        T1 arg1,
        T2 arg2,
        T3 arg3,
        T4 arg4,
        T5 arg5,
        T6 arg6,
        T7 arg7,
        T8 arg8,
        T9 arg9,
        T10 arg10,
        T11 arg11,
        T12 arg12,
        T13 arg13,
        T14 arg14,
        T15 arg15
    ) =>
        this.Arg1.IsSatisfiedBy(arg1)
        && this.Arg2.IsSatisfiedBy(arg2)
        && this.Arg3.IsSatisfiedBy(arg3)
        && this.Arg4.IsSatisfiedBy(arg4)
        && this.Arg5.IsSatisfiedBy(arg5)
        && this.Arg6.IsSatisfiedBy(arg6)
        && this.Arg7.IsSatisfiedBy(arg7)
        && this.Arg8.IsSatisfiedBy(arg8)
        && this.Arg9.IsSatisfiedBy(arg9)
        && this.Arg10.IsSatisfiedBy(arg10)
        && this.Arg11.IsSatisfiedBy(arg11)
        && this.Arg12.IsSatisfiedBy(arg12)
        && this.Arg13.IsSatisfiedBy(arg13)
        && this.Arg14.IsSatisfiedBy(arg14)
        && this.Arg15.IsSatisfiedBy(arg15);

    public bool AllArgsSatisfy(
        (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15) args
    ) =>
        this.AllArgsSatisfy(
            args.Item1,
            args.Item2,
            args.Item3,
            args.Item4,
            args.Item5,
            args.Item6,
            args.Item7,
            args.Item8,
            args.Item9,
            args.Item10,
            args.Item11,
            args.Item12,
            args.Item13,
            args.Item14,
            args.Item15
        );
}
