namespace MockMe;

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
) : ArgBag<T1>(arg1), IArgBag<T1, Action<T1>, Func<T1, TReturn>>
{
    public IMockCallbackAndReturnCallRetriever<Action<T1>, Func<T1, TReturn>> Mock { get; } = mock;

    IMockCallbackRetriever<Action<T1>> IArgBagWithMock<
        T1,
        IMockCallbackRetriever<Action<T1>>
    >.Mock => this.Mock;
}

public interface IArgBagWithMock2<TOriginalArgCollection, TCallback>
{
    public IArgBag<TOriginalArgCollection> ArgBag { get; }
    public IMockCallbackRetriever<TCallback> Mock { get; }
}

//public record ArgBagWithMock<TReturn>(
//    IArgBag<OriginalArgBag<TReturn>> ArgBag,
//    IMockCallbackAndReturnCallRetriever<Action, Func<TReturn>> Mock
//) : IHasArgBag<OriginalArgBag<TReturn>>;

//public class ArgBagWithMock<TOriginalArgCollection, TReturnCall>(
//    IArgBag<TOriginalArgCollection> argBag,
//    IMockCallbackAndReturnCallRetriever<Action<TOriginalArgCollection>, TReturnCall> mock
//) : IArgBagWithMock2<TOriginalArgCollection, Action<TOriginalArgCollection>>
//{
//    public IArgBag<TOriginalArgCollection> ArgBag { get; } = argBag;
//    public IMockCallbackRetriever<Action<TOriginalArgCollection>> Mock { get; } = mock;
//}

public class ArgBagWithMock<TOriginalArgCollection>(
    IArgBag<TOriginalArgCollection> argBag,
    IMockCallbackRetriever<Action<TOriginalArgCollection>> mock
) : IArgBagWithMock2<TOriginalArgCollection, Action<TOriginalArgCollection>>
{
    public IArgBag<TOriginalArgCollection> ArgBag { get; } = argBag;
    public IMockCallbackRetriever<Action<TOriginalArgCollection>> Mock { get; } = mock;
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
)
    : ArgBag<T1, T2, T3>(arg1, arg2, arg3),
        IArgBag<(T1, T2, T3), Action<T1, T2, T3>, Func<T1, T2, T3, TReturn>>
{
    public IMockCallbackAndReturnCallRetriever<
        Action<T1, T2, T3>,
        Func<T1, T2, T3, TReturn>
    > Mock { get; } = mock;

    IMockCallbackRetriever<Action<T1, T2, T3>> IArgBagWithMock<
        (T1, T2, T3),
        IMockCallbackRetriever<Action<T1, T2, T3>>
    >.Mock => this.Mock;
}

public class ArgBagWithMemberMock<T1, T2, T3, T4, TReturn>(
    Arg<T1> arg1,
    Arg<T2> arg2,
    Arg<T3> arg3,
    Arg<T4> arg4,
    IMockCallbackAndReturnCallRetriever<Action<T1, T2, T3, T4>, Func<T1, T2, T3, T4, TReturn>> mock
)
    : ArgBag<T1, T2, T3, T4>(arg1, arg2, arg3, arg4),
        IArgBag<(T1, T2, T3, T4), Action<T1, T2, T3, T4>, Func<T1, T2, T3, T4, TReturn>>
{
    public IMockCallbackAndReturnCallRetriever<
        Action<T1, T2, T3, T4>,
        Func<T1, T2, T3, T4, TReturn>
    > Mock { get; } = mock;

    IMockCallbackRetriever<Action<T1, T2, T3, T4>> IArgBagWithMock<
        (T1, T2, T3, T4),
        IMockCallbackRetriever<Action<T1, T2, T3, T4>>
    >.Mock => this.Mock;
}

public class ArgBagWithMemberMock<T1, T2, T3, T4, T5, TReturn>(
    Arg<T1> arg1,
    Arg<T2> arg2,
    Arg<T3> arg3,
    Arg<T4> arg4,
    Arg<T5> arg5,
    IMockCallbackAndReturnCallRetriever<
        Action<T1, T2, T3, T4, T5>,
        Func<T1, T2, T3, T4, T5, TReturn>
    > mock
)
    : ArgBag<T1, T2, T3, T4, T5>(arg1, arg2, arg3, arg4, arg5),
        IArgBag<(T1, T2, T3, T4, T5), Action<T1, T2, T3, T4, T5>, Func<T1, T2, T3, T4, T5, TReturn>>
{
    public IMockCallbackAndReturnCallRetriever<
        Action<T1, T2, T3, T4, T5>,
        Func<T1, T2, T3, T4, T5, TReturn>
    > Mock { get; } = mock;

    IMockCallbackRetriever<Action<T1, T2, T3, T4, T5>> IArgBagWithMock<
        (T1, T2, T3, T4, T5),
        IMockCallbackRetriever<Action<T1, T2, T3, T4, T5>>
    >.Mock => this.Mock;
}

public class ArgBagWithMemberMock<T1, T2, T3, T4, T5, T6, TReturn>(
    Arg<T1> arg1,
    Arg<T2> arg2,
    Arg<T3> arg3,
    Arg<T4> arg4,
    Arg<T5> arg5,
    Arg<T6> arg6,
    IMockCallbackAndReturnCallRetriever<
        Action<T1, T2, T3, T4, T5, T6>,
        Func<T1, T2, T3, T4, T5, T6, TReturn>
    > mock
)
    : ArgBag<T1, T2, T3, T4, T5, T6>(arg1, arg2, arg3, arg4, arg5, arg6),
        IArgBag<
            (T1, T2, T3, T4, T5, T6),
            Action<T1, T2, T3, T4, T5, T6>,
            Func<T1, T2, T3, T4, T5, T6, TReturn>
        >
{
    public IMockCallbackAndReturnCallRetriever<
        Action<T1, T2, T3, T4, T5, T6>,
        Func<T1, T2, T3, T4, T5, T6, TReturn>
    > Mock { get; } = mock;

    IMockCallbackRetriever<Action<T1, T2, T3, T4, T5, T6>> IArgBagWithMock<
        (T1, T2, T3, T4, T5, T6),
        IMockCallbackRetriever<Action<T1, T2, T3, T4, T5, T6>>
    >.Mock => this.Mock;
}

public class ArgBagWithMemberMock<T1, T2, T3, T4, T5, T6, T7, TReturn>(
    Arg<T1> arg1,
    Arg<T2> arg2,
    Arg<T3> arg3,
    Arg<T4> arg4,
    Arg<T5> arg5,
    Arg<T6> arg6,
    Arg<T7> arg7,
    IMockCallbackAndReturnCallRetriever<
        Action<T1, T2, T3, T4, T5, T6, T7>,
        Func<T1, T2, T3, T4, T5, T6, T7, TReturn>
    > mock
)
    : ArgBag<T1, T2, T3, T4, T5, T6, T7>(arg1, arg2, arg3, arg4, arg5, arg6, arg7),
        IArgBag<
            (T1, T2, T3, T4, T5, T6, T7),
            Action<T1, T2, T3, T4, T5, T6, T7>,
            Func<T1, T2, T3, T4, T5, T6, T7, TReturn>
        >
{
    public IMockCallbackAndReturnCallRetriever<
        Action<T1, T2, T3, T4, T5, T6, T7>,
        Func<T1, T2, T3, T4, T5, T6, T7, TReturn>
    > Mock { get; } = mock;

    IMockCallbackRetriever<Action<T1, T2, T3, T4, T5, T6, T7>> IArgBagWithMock<
        (T1, T2, T3, T4, T5, T6, T7),
        IMockCallbackRetriever<Action<T1, T2, T3, T4, T5, T6, T7>>
    >.Mock => this.Mock;
}

public class ArgBagWithMemberMock<T1, T2, T3, T4, T5, T6, T7, T8, TReturn>(
    Arg<T1> arg1,
    Arg<T2> arg2,
    Arg<T3> arg3,
    Arg<T4> arg4,
    Arg<T5> arg5,
    Arg<T6> arg6,
    Arg<T7> arg7,
    Arg<T8> arg8,
    IMockCallbackAndReturnCallRetriever<
        Action<T1, T2, T3, T4, T5, T6, T7, T8>,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, TReturn>
    > mock
)
    : ArgBag<T1, T2, T3, T4, T5, T6, T7, T8>(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8),
        IArgBag<
            (T1, T2, T3, T4, T5, T6, T7, T8),
            Action<T1, T2, T3, T4, T5, T6, T7, T8>,
            Func<T1, T2, T3, T4, T5, T6, T7, T8, TReturn>
        >
{
    public IMockCallbackAndReturnCallRetriever<
        Action<T1, T2, T3, T4, T5, T6, T7, T8>,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, TReturn>
    > Mock { get; } = mock;

    IMockCallbackRetriever<Action<T1, T2, T3, T4, T5, T6, T7, T8>> IArgBagWithMock<
        (T1, T2, T3, T4, T5, T6, T7, T8),
        IMockCallbackRetriever<Action<T1, T2, T3, T4, T5, T6, T7, T8>>
    >.Mock => this.Mock;
}

public class ArgBagWithMemberMock<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn>(
    Arg<T1> arg1,
    Arg<T2> arg2,
    Arg<T3> arg3,
    Arg<T4> arg4,
    Arg<T5> arg5,
    Arg<T6> arg6,
    Arg<T7> arg7,
    Arg<T8> arg8,
    Arg<T9> arg9,
    IMockCallbackAndReturnCallRetriever<
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9>,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn>
    > mock
)
    : ArgBag<T1, T2, T3, T4, T5, T6, T7, T8, T9>(
        arg1,
        arg2,
        arg3,
        arg4,
        arg5,
        arg6,
        arg7,
        arg8,
        arg9
    ),
        IArgBag<
            (T1, T2, T3, T4, T5, T6, T7, T8, T9),
            Action<T1, T2, T3, T4, T5, T6, T7, T8, T9>,
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn>
        >
{
    public IMockCallbackAndReturnCallRetriever<
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9>,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TReturn>
    > Mock { get; } = mock;

    IMockCallbackRetriever<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9>> IArgBagWithMock<
        (T1, T2, T3, T4, T5, T6, T7, T8, T9),
        IMockCallbackRetriever<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9>>
    >.Mock => this.Mock;
}

public class ArgBagWithMemberMock<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn>(
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
    IMockCallbackAndReturnCallRetriever<
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn>
    > mock
)
    : ArgBag<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(
        arg1,
        arg2,
        arg3,
        arg4,
        arg5,
        arg6,
        arg7,
        arg8,
        arg9,
        arg10
    ),
        IArgBag<
            (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10),
            Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>,
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn>
        >
{
    public IMockCallbackAndReturnCallRetriever<
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TReturn>
    > Mock { get; } = mock;

    IMockCallbackRetriever<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>> IArgBagWithMock<
        (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10),
        IMockCallbackRetriever<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>>
    >.Mock => this.Mock;
}

public class ArgBagWithMemberMock<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn>(
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
    IMockCallbackAndReturnCallRetriever<
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn>
    > mock
)
    : ArgBag<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(
        arg1,
        arg2,
        arg3,
        arg4,
        arg5,
        arg6,
        arg7,
        arg8,
        arg9,
        arg10,
        arg11
    ),
        IArgBag<
            (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11),
            Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>,
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn>
        >
{
    public IMockCallbackAndReturnCallRetriever<
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TReturn>
    > Mock { get; } = mock;

    IMockCallbackRetriever<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>> IArgBagWithMock<
        (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11),
        IMockCallbackRetriever<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>>
    >.Mock => this.Mock;
}

public class ArgBagWithMemberMock<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn>(
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
    IMockCallbackAndReturnCallRetriever<
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn>
    > mock
)
    : ArgBag<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(
        arg1,
        arg2,
        arg3,
        arg4,
        arg5,
        arg6,
        arg7,
        arg8,
        arg9,
        arg10,
        arg11,
        arg12
    ),
        IArgBag<
            (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12),
            Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>,
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn>
        >
{
    public IMockCallbackAndReturnCallRetriever<
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TReturn>
    > Mock { get; } = mock;

    IMockCallbackRetriever<
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>
    > IArgBagWithMock<
        (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12),
        IMockCallbackRetriever<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>>
    >.Mock => this.Mock;
}

public class ArgBagWithMemberMock<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn>(
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
    IMockCallbackAndReturnCallRetriever<
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn>
    > mock
)
    : ArgBag<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(
        arg1,
        arg2,
        arg3,
        arg4,
        arg5,
        arg6,
        arg7,
        arg8,
        arg9,
        arg10,
        arg11,
        arg12,
        arg13
    ),
        IArgBag<
            (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13),
            Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>,
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn>
        >
{
    public IMockCallbackAndReturnCallRetriever<
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TReturn>
    > Mock { get; } = mock;

    IMockCallbackRetriever<
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>
    > IArgBagWithMock<
        (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13),
        IMockCallbackRetriever<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>>
    >.Mock => this.Mock;
}

public class ArgBagWithMemberMock<
    T1,
    T2,
    T3,
    T4,
    T5,
    T6,
    T7,
    T8,
    T9,
    T10,
    T11,
    T12,
    T13,
    T14,
    TReturn
>(
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
    IMockCallbackAndReturnCallRetriever<
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn>
    > mock
)
    : ArgBag<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(
        arg1,
        arg2,
        arg3,
        arg4,
        arg5,
        arg6,
        arg7,
        arg8,
        arg9,
        arg10,
        arg11,
        arg12,
        arg13,
        arg14
    ),
        IArgBag<
            (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14),
            Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>,
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn>
        >
{
    public IMockCallbackAndReturnCallRetriever<
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TReturn>
    > Mock { get; } = mock;

    IMockCallbackRetriever<
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>
    > IArgBagWithMock<
        (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14),
        IMockCallbackRetriever<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>>
    >.Mock => this.Mock;
}

public class ArgBagWithMemberMock<
    T1,
    T2,
    T3,
    T4,
    T5,
    T6,
    T7,
    T8,
    T9,
    T10,
    T11,
    T12,
    T13,
    T14,
    T15,
    TReturn
>(
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
    Arg<T15> arg15,
    IMockCallbackAndReturnCallRetriever<
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn>
    > mock
)
    : ArgBag<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
        arg1,
        arg2,
        arg3,
        arg4,
        arg5,
        arg6,
        arg7,
        arg8,
        arg9,
        arg10,
        arg11,
        arg12,
        arg13,
        arg14,
        arg15
    ),
        IArgBag<
            (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15),
            Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>,
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn>
        >
{
    public IMockCallbackAndReturnCallRetriever<
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TReturn>
    > Mock { get; } = mock;

    IMockCallbackRetriever<
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>
    > IArgBagWithMock<
        (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15),
        IMockCallbackRetriever<
            Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>
        >
    >.Mock => this.Mock;
}
