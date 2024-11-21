using MockMe.Asserters;

namespace MockMe;

public class MockAsserter
{
    private static readonly MemberAsserter defaultAsserter = new(0);

    private static MemberAsserter GetMemberAsserterBase<TArgCollection>(
        IList<TArgCollection>? callStore,
        IArgBag<TArgCollection> argBag
    )
    {
        if (callStore is null)
        {
            return defaultAsserter;
        }

        int numTimesCalled = 0;
        for (int i = callStore.Count - 1; i >= 0; i--)
        {
            var argCollection = callStore[i];
            if (argBag.AllArgsSatisfy(argCollection))
            {
                numTimesCalled++;
            }
        }

        return new(numTimesCalled);
    }

    protected static MemberAsserter GetMemberAsserter<T1>(List<T1>? callStore, Arg<T1> arg1) =>
        GetMemberAsserterBase(callStore, new ArgBag<T1>(arg1));

    protected static MemberAsserter GetMemberAsserter<T1, T2>(
        List<ValueTuple<T1, T2>>? callStore,
        Arg<T1> arg1,
        Arg<T2> arg2
    ) => GetMemberAsserterBase(callStore, new ArgBag<T1, T2>(arg1, arg2));

    protected static MemberAsserter GetMemberAsserter<T1, T2, T3>(
        List<(T1, T2, T3)>? callStore,
        Arg<T1> arg1,
        Arg<T2> arg2,
        Arg<T3> arg3
    ) => GetMemberAsserterBase(callStore, new ArgBag<T1, T2, T3>(arg1, arg2, arg3));

    protected static MemberAsserter GetMemberAsserter<T1, T2, T3, T4>(
        List<(T1, T2, T3, T4)>? callStore,
        Arg<T1> arg1,
        Arg<T2> arg2,
        Arg<T3> arg3,
        Arg<T4> arg4
    ) => GetMemberAsserterBase(callStore, new ArgBag<T1, T2, T3, T4>(arg1, arg2, arg3, arg4));

    protected static MemberAsserter GetMemberAsserter<T1, T2, T3, T4, T5>(
        List<(T1, T2, T3, T4, T5)>? callStore,
        Arg<T1> arg1,
        Arg<T2> arg2,
        Arg<T3> arg3,
        Arg<T4> arg4,
        Arg<T5> arg5
    ) =>
        GetMemberAsserterBase(
            callStore,
            new ArgBag<T1, T2, T3, T4, T5>(arg1, arg2, arg3, arg4, arg5)
        );

    protected static MemberAsserter GetMemberAsserter<T1, T2, T3, T4, T5, T6>(
        List<(T1, T2, T3, T4, T5, T6)>? callStore,
        Arg<T1> arg1,
        Arg<T2> arg2,
        Arg<T3> arg3,
        Arg<T4> arg4,
        Arg<T5> arg5,
        Arg<T6> arg6
    ) =>
        GetMemberAsserterBase(
            callStore,
            new ArgBag<T1, T2, T3, T4, T5, T6>(arg1, arg2, arg3, arg4, arg5, arg6)
        );

    protected static MemberAsserter GetMemberAsserter<T1, T2, T3, T4, T5, T6, T7>(
        List<(T1, T2, T3, T4, T5, T6, T7)>? callStore,
        Arg<T1> arg1,
        Arg<T2> arg2,
        Arg<T3> arg3,
        Arg<T4> arg4,
        Arg<T5> arg5,
        Arg<T6> arg6,
        Arg<T7> arg7
    ) =>
        GetMemberAsserterBase(
            callStore,
            new ArgBag<T1, T2, T3, T4, T5, T6, T7>(arg1, arg2, arg3, arg4, arg5, arg6, arg7)
        );

    protected static MemberAsserter GetMemberAsserter<T1, T2, T3, T4, T5, T6, T7, T8>(
        List<(T1, T2, T3, T4, T5, T6, T7, T8)>? callStore,
        Arg<T1> arg1,
        Arg<T2> arg2,
        Arg<T3> arg3,
        Arg<T4> arg4,
        Arg<T5> arg5,
        Arg<T6> arg6,
        Arg<T7> arg7,
        Arg<T8> arg8
    ) =>
        GetMemberAsserterBase(
            callStore,
            new ArgBag<T1, T2, T3, T4, T5, T6, T7, T8>(
                arg1,
                arg2,
                arg3,
                arg4,
                arg5,
                arg6,
                arg7,
                arg8
            )
        );

    protected static MemberAsserter GetMemberAsserter<T1, T2, T3, T4, T5, T6, T7, T8, T9>(
        List<(T1, T2, T3, T4, T5, T6, T7, T8, T9)>? callStore,
        Arg<T1> arg1,
        Arg<T2> arg2,
        Arg<T3> arg3,
        Arg<T4> arg4,
        Arg<T5> arg5,
        Arg<T6> arg6,
        Arg<T7> arg7,
        Arg<T8> arg8,
        Arg<T9> arg9
    ) =>
        GetMemberAsserterBase(
            callStore,
            new ArgBag<T1, T2, T3, T4, T5, T6, T7, T8, T9>(
                arg1,
                arg2,
                arg3,
                arg4,
                arg5,
                arg6,
                arg7,
                arg8,
                arg9
            )
        );

    protected static MemberAsserter GetMemberAsserter<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(
        List<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)>? callStore,
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
    ) =>
        GetMemberAsserterBase(
            callStore,
            new ArgBag<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(
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
            )
        );

    protected static MemberAsserter GetMemberAsserter<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(
        List<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11)>? callStore,
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
    ) =>
        GetMemberAsserterBase(
            callStore,
            new ArgBag<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(
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
            )
        );

    protected static MemberAsserter GetMemberAsserter<
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
        T12
    >(
        List<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12)>? callStore,
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
    ) =>
        GetMemberAsserterBase(
            callStore,
            new ArgBag<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(
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
            )
        );

    protected static MemberAsserter GetMemberAsserter<
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
        T13
    >(
        List<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13)>? callStore,
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
    ) =>
        GetMemberAsserterBase(
            callStore,
            new ArgBag<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(
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
            )
        );

    protected static MemberAsserter GetMemberAsserter<
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
        T14
    >(
        List<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)>? callStore,
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
    ) =>
        GetMemberAsserterBase(
            callStore,
            new ArgBag<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(
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
            )
        );

    protected static MemberAsserter GetMemberAsserter<
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
        List<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15)>? callStore,
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
    ) =>
        GetMemberAsserterBase(
            callStore,
            new ArgBag<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
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
            )
        );
}
