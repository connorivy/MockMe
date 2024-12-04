namespace MockMe;

public class OriginalArgBag<T1>(T1 arg1)
{
    internal T1 IntArg1 => this.Arg1;
    protected T1 Arg1 { get; set; } = arg1;
}

public class OriginalArgBag<T1, T2>(T1 arg1, T2 arg2)
{
    protected T1 Arg1 { get; set; } = arg1;
    protected T2 Arg2 { get; set; } = arg2;
    internal T1 IntArg1 => this.Arg1;
    internal T2 IntArg2 => this.Arg2;
}

public class OriginalArgBag<T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3)
{
    protected T1 Arg1 { get; set; } = arg1;
    protected T2 Arg2 { get; set; } = arg2;
    protected T3 Arg3 { get; set; } = arg3;

    internal T1 IntArg1 => this.Arg1;
    internal T2 IntArg2 => this.Arg2;
    internal T3 IntArg3 => this.Arg3;
}

public class OriginalArgBag<T1, T2, T3, T4>(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
{
    protected T1 Arg1 { get; set; } = arg1;
    protected T2 Arg2 { get; set; } = arg2;
    protected T3 Arg3 { get; set; } = arg3;
    protected T4 Arg4 { get; set; } = arg4;

    internal T1 IntArg1 => this.Arg1;
    internal T2 IntArg2 => this.Arg2;
    internal T3 IntArg3 => this.Arg3;
    internal T4 IntArg4 => this.Arg4;
}

public class OriginalArgBag<T1, T2, T3, T4, T5>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
{
    protected T1 Arg1 { get; set; } = arg1;
    protected T2 Arg2 { get; set; } = arg2;
    protected T3 Arg3 { get; set; } = arg3;
    protected T4 Arg4 { get; set; } = arg4;
    protected T5 Arg5 { get; set; } = arg5;

    internal T1 IntArg1 => this.Arg1;
    internal T2 IntArg2 => this.Arg2;
    internal T3 IntArg3 => this.Arg3;
    internal T4 IntArg4 => this.Arg4;
    internal T5 IntArg5 => this.Arg5;
}

public class OriginalArgBag<T1, T2, T3, T4, T5, T6>(
    T1 arg1,
    T2 arg2,
    T3 arg3,
    T4 arg4,
    T5 arg5,
    T6 arg6
)
{
    protected T1 Arg1 { get; set; } = arg1;
    protected T2 Arg2 { get; set; } = arg2;
    protected T3 Arg3 { get; set; } = arg3;
    protected T4 Arg4 { get; set; } = arg4;
    protected T5 Arg5 { get; set; } = arg5;
    protected T6 Arg6 { get; set; } = arg6;

    internal T1 IntArg1 => this.Arg1;
    internal T2 IntArg2 => this.Arg2;
    internal T3 IntArg3 => this.Arg3;
    internal T4 IntArg4 => this.Arg4;
    internal T5 IntArg5 => this.Arg5;
    internal T6 IntArg6 => this.Arg6;
}

public class OriginalArgBag<T1, T2, T3, T4, T5, T6, T7>(
    T1 arg1,
    T2 arg2,
    T3 arg3,
    T4 arg4,
    T5 arg5,
    T6 arg6,
    T7 arg7
)
{
    protected T1 Arg1 { get; set; } = arg1;
    protected T2 Arg2 { get; set; } = arg2;
    protected T3 Arg3 { get; set; } = arg3;
    protected T4 Arg4 { get; set; } = arg4;
    protected T5 Arg5 { get; set; } = arg5;
    protected T6 Arg6 { get; set; } = arg6;
    protected T7 Arg7 { get; set; } = arg7;

    internal T1 IntArg1 => this.Arg1;
    internal T2 IntArg2 => this.Arg2;
    internal T3 IntArg3 => this.Arg3;
    internal T4 IntArg4 => this.Arg4;
    internal T5 IntArg5 => this.Arg5;
    internal T6 IntArg6 => this.Arg6;
    internal T7 IntArg7 => this.Arg7;
}

public class OriginalArgBag<T1, T2, T3, T4, T5, T6, T7, T8>(
    T1 arg1,
    T2 arg2,
    T3 arg3,
    T4 arg4,
    T5 arg5,
    T6 arg6,
    T7 arg7,
    T8 arg8
)
{
    protected T1 Arg1 { get; set; } = arg1;
    protected T2 Arg2 { get; set; } = arg2;
    protected T3 Arg3 { get; set; } = arg3;
    protected T4 Arg4 { get; set; } = arg4;
    protected T5 Arg5 { get; set; } = arg5;
    protected T6 Arg6 { get; set; } = arg6;
    protected T7 Arg7 { get; set; } = arg7;
    protected T8 Arg8 { get; set; } = arg8;

    internal T1 IntArg1 => this.Arg1;
    internal T2 IntArg2 => this.Arg2;
    internal T3 IntArg3 => this.Arg3;
    internal T4 IntArg4 => this.Arg4;
    internal T5 IntArg5 => this.Arg5;
    internal T6 IntArg6 => this.Arg6;
    internal T7 IntArg7 => this.Arg7;
    internal T8 IntArg8 => this.Arg8;
}

public class OriginalArgBag<T1, T2, T3, T4, T5, T6, T7, T8, T9>(
    T1 arg1,
    T2 arg2,
    T3 arg3,
    T4 arg4,
    T5 arg5,
    T6 arg6,
    T7 arg7,
    T8 arg8,
    T9 arg9
)
{
    protected T1 Arg1 { get; set; } = arg1;
    protected T2 Arg2 { get; set; } = arg2;
    protected T3 Arg3 { get; set; } = arg3;
    protected T4 Arg4 { get; set; } = arg4;
    protected T5 Arg5 { get; set; } = arg5;
    protected T6 Arg6 { get; set; } = arg6;
    protected T7 Arg7 { get; set; } = arg7;
    protected T8 Arg8 { get; set; } = arg8;
    protected T9 Arg9 { get; set; } = arg9;

    internal T1 IntArg1 => this.Arg1;
    internal T2 IntArg2 => this.Arg2;
    internal T3 IntArg3 => this.Arg3;
    internal T4 IntArg4 => this.Arg4;
    internal T5 IntArg5 => this.Arg5;
    internal T6 IntArg6 => this.Arg6;
    internal T7 IntArg7 => this.Arg7;
    internal T8 IntArg8 => this.Arg8;
    internal T9 IntArg9 => this.Arg9;
}

public class OriginalArgBag<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(
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
)
{
    protected T1 Arg1 { get; set; } = arg1;
    protected T2 Arg2 { get; set; } = arg2;
    protected T3 Arg3 { get; set; } = arg3;
    protected T4 Arg4 { get; set; } = arg4;
    protected T5 Arg5 { get; set; } = arg5;
    protected T6 Arg6 { get; set; } = arg6;
    protected T7 Arg7 { get; set; } = arg7;
    protected T8 Arg8 { get; set; } = arg8;
    protected T9 Arg9 { get; set; } = arg9;
    protected T10 Arg10 { get; set; } = arg10;

    internal T1 IntArg1 => this.Arg1;
    internal T2 IntArg2 => this.Arg2;
    internal T3 IntArg3 => this.Arg3;
    internal T4 IntArg4 => this.Arg4;
    internal T5 IntArg5 => this.Arg5;
    internal T6 IntArg6 => this.Arg6;
    internal T7 IntArg7 => this.Arg7;
    internal T8 IntArg8 => this.Arg8;
    internal T9 IntArg9 => this.Arg9;
    internal T10 IntArg10 => this.Arg10;
}

public class OriginalArgBag<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(
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
)
{
    protected T1 Arg1 { get; set; } = arg1;
    protected T2 Arg2 { get; set; } = arg2;
    protected T3 Arg3 { get; set; } = arg3;
    protected T4 Arg4 { get; set; } = arg4;
    protected T5 Arg5 { get; set; } = arg5;
    protected T6 Arg6 { get; set; } = arg6;
    protected T7 Arg7 { get; set; } = arg7;
    protected T8 Arg8 { get; set; } = arg8;
    protected T9 Arg9 { get; set; } = arg9;
    protected T10 Arg10 { get; set; } = arg10;
    protected T11 Arg11 { get; set; } = arg11;

    internal T1 IntArg1 => this.Arg1;
    internal T2 IntArg2 => this.Arg2;
    internal T3 IntArg3 => this.Arg3;
    internal T4 IntArg4 => this.Arg4;
    internal T5 IntArg5 => this.Arg5;
    internal T6 IntArg6 => this.Arg6;
    internal T7 IntArg7 => this.Arg7;
    internal T8 IntArg8 => this.Arg8;
    internal T9 IntArg9 => this.Arg9;
    internal T10 IntArg10 => this.Arg10;
    internal T11 IntArg11 => this.Arg11;
}

public class OriginalArgBag<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(
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
)
{
    protected T1 Arg1 { get; set; } = arg1;
    protected T2 Arg2 { get; set; } = arg2;
    protected T3 Arg3 { get; set; } = arg3;
    protected T4 Arg4 { get; set; } = arg4;
    protected T5 Arg5 { get; set; } = arg5;
    protected T6 Arg6 { get; set; } = arg6;
    protected T7 Arg7 { get; set; } = arg7;
    protected T8 Arg8 { get; set; } = arg8;
    protected T9 Arg9 { get; set; } = arg9;
    protected T10 Arg10 { get; set; } = arg10;
    protected T11 Arg11 { get; set; } = arg11;
    protected T12 Arg12 { get; set; } = arg12;

    internal T1 IntArg1 => this.Arg1;
    internal T2 IntArg2 => this.Arg2;
    internal T3 IntArg3 => this.Arg3;
    internal T4 IntArg4 => this.Arg4;
    internal T5 IntArg5 => this.Arg5;
    internal T6 IntArg6 => this.Arg6;
    internal T7 IntArg7 => this.Arg7;
    internal T8 IntArg8 => this.Arg8;
    internal T9 IntArg9 => this.Arg9;
    internal T10 IntArg10 => this.Arg10;
    internal T11 IntArg11 => this.Arg11;
    internal T12 IntArg12 => this.Arg12;
}

public class OriginalArgBag<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(
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
)
{
    protected T1 Arg1 { get; set; } = arg1;
    protected T2 Arg2 { get; set; } = arg2;
    protected T3 Arg3 { get; set; } = arg3;
    protected T4 Arg4 { get; set; } = arg4;
    protected T5 Arg5 { get; set; } = arg5;
    protected T6 Arg6 { get; set; } = arg6;
    protected T7 Arg7 { get; set; } = arg7;
    protected T8 Arg8 { get; set; } = arg8;
    protected T9 Arg9 { get; set; } = arg9;
    protected T10 Arg10 { get; set; } = arg10;
    protected T11 Arg11 { get; set; } = arg11;
    protected T12 Arg12 { get; set; } = arg12;
    protected T13 Arg13 { get; set; } = arg13;

    internal T1 IntArg1 => this.Arg1;
    internal T2 IntArg2 => this.Arg2;
    internal T3 IntArg3 => this.Arg3;
    internal T4 IntArg4 => this.Arg4;
    internal T5 IntArg5 => this.Arg5;
    internal T6 IntArg6 => this.Arg6;
    internal T7 IntArg7 => this.Arg7;
    internal T8 IntArg8 => this.Arg8;
    internal T9 IntArg9 => this.Arg9;
    internal T10 IntArg10 => this.Arg10;
    internal T11 IntArg11 => this.Arg11;
    internal T12 IntArg12 => this.Arg12;
    internal T13 IntArg13 => this.Arg13;
}

public class OriginalArgBag<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(
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
)
{
    protected T1 Arg1 { get; set; } = arg1;
    protected T2 Arg2 { get; set; } = arg2;
    protected T3 Arg3 { get; set; } = arg3;
    protected T4 Arg4 { get; set; } = arg4;
    protected T5 Arg5 { get; set; } = arg5;
    protected T6 Arg6 { get; set; } = arg6;
    protected T7 Arg7 { get; set; } = arg7;
    protected T8 Arg8 { get; set; } = arg8;
    protected T9 Arg9 { get; set; } = arg9;
    protected T10 Arg10 { get; set; } = arg10;
    protected T11 Arg11 { get; set; } = arg11;
    protected T12 Arg12 { get; set; } = arg12;
    protected T13 Arg13 { get; set; } = arg13;
    protected T14 Arg14 { get; set; } = arg14;

    internal T1 IntArg1 => this.Arg1;
    internal T2 IntArg2 => this.Arg2;
    internal T3 IntArg3 => this.Arg3;
    internal T4 IntArg4 => this.Arg4;
    internal T5 IntArg5 => this.Arg5;
    internal T6 IntArg6 => this.Arg6;
    internal T7 IntArg7 => this.Arg7;
    internal T8 IntArg8 => this.Arg8;
    internal T9 IntArg9 => this.Arg9;
    internal T10 IntArg10 => this.Arg10;
    internal T11 IntArg11 => this.Arg11;
    internal T12 IntArg12 => this.Arg12;
    internal T13 IntArg13 => this.Arg13;
    internal T14 IntArg14 => this.Arg14;
}

public class OriginalArgBag<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
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
)
{
    protected T1 Arg1 { get; set; } = arg1;
    protected T2 Arg2 { get; set; } = arg2;
    protected T3 Arg3 { get; set; } = arg3;
    protected T4 Arg4 { get; set; } = arg4;
    protected T5 Arg5 { get; set; } = arg5;
    protected T6 Arg6 { get; set; } = arg6;
    protected T7 Arg7 { get; set; } = arg7;
    protected T8 Arg8 { get; set; } = arg8;
    protected T9 Arg9 { get; set; } = arg9;
    protected T10 Arg10 { get; set; } = arg10;
    protected T11 Arg11 { get; set; } = arg11;
    protected T12 Arg12 { get; set; } = arg12;
    protected T13 Arg13 { get; set; } = arg13;
    protected T14 Arg14 { get; set; } = arg14;
    protected T15 Arg15 { get; set; } = arg15;

    internal T1 IntArg1 => this.Arg1;
    internal T2 IntArg2 => this.Arg2;
    internal T3 IntArg3 => this.Arg3;
    internal T4 IntArg4 => this.Arg4;
    internal T5 IntArg5 => this.Arg5;
    internal T6 IntArg6 => this.Arg6;
    internal T7 IntArg7 => this.Arg7;
    internal T8 IntArg8 => this.Arg8;
    internal T9 IntArg9 => this.Arg9;
    internal T10 IntArg10 => this.Arg10;
    internal T11 IntArg11 => this.Arg11;
    internal T12 IntArg12 => this.Arg12;
    internal T13 IntArg13 => this.Arg13;
    internal T14 IntArg14 => this.Arg14;
    internal T15 IntArg15 => this.Arg15;
}
