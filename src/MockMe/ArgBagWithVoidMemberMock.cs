using MockMe.Mocks.ClassMemberMocks;

namespace MockMe;

public class ArgBagWithVoidMemberMock<T1>(Arg<T1> arg1, VoidMemberMock<T1> mock)
    : ArgBag<T1>(arg1),
        IArgBag<T1, Action<T1>>
{
    IMockCallbackRetriever<Action<T1>> IArgBagWithMock<
        T1,
        IMockCallbackRetriever<Action<T1>>
    >.Mock => mock;
}

public class ArgBagWithVoidMemberMock<T1, T2>(
    Arg<T1> arg1,
    Arg<T2> arg2,
    IMockCallbackRetriever<Action<T1, T2>> mock
) : ArgBag<T1, T2>(arg1, arg2), IArgBag<(T1, T2), Action<T1, T2>>
{
    IMockCallbackRetriever<Action<T1, T2>> IArgBagWithMock<
        (T1, T2),
        IMockCallbackRetriever<Action<T1, T2>>
    >.Mock => mock;
}

public class ArgBagWithVoidMemberMock<T1, T2, T3>(
    Arg<T1> arg1,
    Arg<T2> arg2,
    Arg<T3> arg3,
    IMockCallbackRetriever<Action<T1, T2, T3>> mock
) : ArgBag<T1, T2, T3>(arg1, arg2, arg3), IArgBag<(T1, T2, T3), Action<T1, T2, T3>>
{
    IMockCallbackRetriever<Action<T1, T2, T3>> IArgBagWithMock<
        (T1, T2, T3),
        IMockCallbackRetriever<Action<T1, T2, T3>>
    >.Mock => mock;
}

public class ArgBagWithVoidMemberMock<T1, T2, T3, T4>(
    Arg<T1> arg1,
    Arg<T2> arg2,
    Arg<T3> arg3,
    Arg<T4> arg4,
    IMockCallbackRetriever<Action<T1, T2, T3, T4>> mock
)
    : ArgBag<T1, T2, T3, T4>(arg1, arg2, arg3, arg4),
        IArgBag<(T1, T2, T3, T4), Action<T1, T2, T3, T4>>
{
    IMockCallbackRetriever<Action<T1, T2, T3, T4>> IArgBagWithMock<
        (T1, T2, T3, T4),
        IMockCallbackRetriever<Action<T1, T2, T3, T4>>
    >.Mock => mock;
}

public class ArgBagWithVoidMemberMock<T1, T2, T3, T4, T5>(
    Arg<T1> arg1,
    Arg<T2> arg2,
    Arg<T3> arg3,
    Arg<T4> arg4,
    Arg<T5> arg5,
    IMockCallbackRetriever<Action<T1, T2, T3, T4, T5>> mock
)
    : ArgBag<T1, T2, T3, T4, T5>(arg1, arg2, arg3, arg4, arg5),
        IArgBag<(T1, T2, T3, T4, T5), Action<T1, T2, T3, T4, T5>>
{
    IMockCallbackRetriever<Action<T1, T2, T3, T4, T5>> IArgBagWithMock<
        (T1, T2, T3, T4, T5),
        IMockCallbackRetriever<Action<T1, T2, T3, T4, T5>>
    >.Mock => mock;
}

public class ArgBagWithVoidMemberMock<T1, T2, T3, T4, T5, T6>(
    Arg<T1> arg1,
    Arg<T2> arg2,
    Arg<T3> arg3,
    Arg<T4> arg4,
    Arg<T5> arg5,
    Arg<T6> arg6,
    IMockCallbackRetriever<Action<T1, T2, T3, T4, T5, T6>> mock
)
    : ArgBag<T1, T2, T3, T4, T5, T6>(arg1, arg2, arg3, arg4, arg5, arg6),
        IArgBag<(T1, T2, T3, T4, T5, T6), Action<T1, T2, T3, T4, T5, T6>>
{
    IMockCallbackRetriever<Action<T1, T2, T3, T4, T5, T6>> IArgBagWithMock<
        (T1, T2, T3, T4, T5, T6),
        IMockCallbackRetriever<Action<T1, T2, T3, T4, T5, T6>>
    >.Mock => mock;
}

public class ArgBagWithVoidMemberMock<T1, T2, T3, T4, T5, T6, T7>(
    Arg<T1> arg1,
    Arg<T2> arg2,
    Arg<T3> arg3,
    Arg<T4> arg4,
    Arg<T5> arg5,
    Arg<T6> arg6,
    Arg<T7> arg7,
    IMockCallbackRetriever<Action<T1, T2, T3, T4, T5, T6, T7>> mock
)
    : ArgBag<T1, T2, T3, T4, T5, T6, T7>(arg1, arg2, arg3, arg4, arg5, arg6, arg7),
        IArgBag<(T1, T2, T3, T4, T5, T6, T7), Action<T1, T2, T3, T4, T5, T6, T7>>
{
    IMockCallbackRetriever<Action<T1, T2, T3, T4, T5, T6, T7>> IArgBagWithMock<
        (T1, T2, T3, T4, T5, T6, T7),
        IMockCallbackRetriever<Action<T1, T2, T3, T4, T5, T6, T7>>
    >.Mock => mock;
}

public class ArgBagWithVoidMemberMock<T1, T2, T3, T4, T5, T6, T7, T8>(
    Arg<T1> arg1,
    Arg<T2> arg2,
    Arg<T3> arg3,
    Arg<T4> arg4,
    Arg<T5> arg5,
    Arg<T6> arg6,
    Arg<T7> arg7,
    Arg<T8> arg8,
    IMockCallbackRetriever<Action<T1, T2, T3, T4, T5, T6, T7, T8>> mock
)
    : ArgBag<T1, T2, T3, T4, T5, T6, T7, T8>(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8),
        IArgBag<(T1, T2, T3, T4, T5, T6, T7, T8), Action<T1, T2, T3, T4, T5, T6, T7, T8>>
{
    IMockCallbackRetriever<Action<T1, T2, T3, T4, T5, T6, T7, T8>> IArgBagWithMock<
        (T1, T2, T3, T4, T5, T6, T7, T8),
        IMockCallbackRetriever<Action<T1, T2, T3, T4, T5, T6, T7, T8>>
    >.Mock => mock;
}

public class ArgBagWithVoidMemberMock<T1, T2, T3, T4, T5, T6, T7, T8, T9>(
    Arg<T1> arg1,
    Arg<T2> arg2,
    Arg<T3> arg3,
    Arg<T4> arg4,
    Arg<T5> arg5,
    Arg<T6> arg6,
    Arg<T7> arg7,
    Arg<T8> arg8,
    Arg<T9> arg9,
    IMockCallbackRetriever<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9>> mock
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
        IArgBag<(T1, T2, T3, T4, T5, T6, T7, T8, T9), Action<T1, T2, T3, T4, T5, T6, T7, T8, T9>>
{
    IMockCallbackRetriever<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9>> IArgBagWithMock<
        (T1, T2, T3, T4, T5, T6, T7, T8, T9),
        IMockCallbackRetriever<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9>>
    >.Mock => mock;
}

public class ArgBagWithVoidMemberMock<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(
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
    IMockCallbackRetriever<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>> mock
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
            Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>
        >
{
    IMockCallbackRetriever<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>> IArgBagWithMock<
        (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10),
        IMockCallbackRetriever<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>>
    >.Mock => mock;
}

public class ArgBagWithVoidMemberMock<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(
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
    IMockCallbackRetriever<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>> mock
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
            Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>
        >
{
    IMockCallbackRetriever<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>> IArgBagWithMock<
        (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11),
        IMockCallbackRetriever<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>>
    >.Mock => mock;
}

public class ArgBagWithVoidMemberMock<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(
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
    IMockCallbackRetriever<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>> mock
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
            Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>
        >
{
    IMockCallbackRetriever<
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>
    > IArgBagWithMock<
        (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12),
        IMockCallbackRetriever<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>>
    >.Mock => mock;
}

public class ArgBagWithVoidMemberMock<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(
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
    IMockCallbackRetriever<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>> mock
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
            Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>
        >
{
    IMockCallbackRetriever<
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>
    > IArgBagWithMock<
        (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13),
        IMockCallbackRetriever<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>>
    >.Mock => mock;
}

public class ArgBagWithVoidMemberMock<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(
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
    IMockCallbackRetriever<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>> mock
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
            Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>
        >
{
    IMockCallbackRetriever<
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>
    > IArgBagWithMock<
        (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14),
        IMockCallbackRetriever<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>>
    >.Mock => mock;
}

public class ArgBagWithVoidMemberMock<
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
    T15
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
    IMockCallbackRetriever<
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>
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
            Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>
        >
{
    IMockCallbackRetriever<
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>
    > IArgBagWithMock<
        (T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15),
        IMockCallbackRetriever<
            Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>
        >
    >.Mock => mock;
}
