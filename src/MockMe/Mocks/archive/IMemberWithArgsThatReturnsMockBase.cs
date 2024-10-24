namespace MockMe.Mocks.archive;

public interface IMemberWithArgsThatReturnsMockBase<TReturn, TSelf, TReturnFunc>
    : IMemberThatReturnsMock<TReturn, TSelf, TReturnFunc>
{
    protected TReturnFunc ToFunc(TReturn returnVal);
    public TSelf Returns(TReturn returnThis, params TReturn[]? thenReturnThese) =>
        Returns(ToFunc(returnThis), thenReturnThese?.Select(ToFunc).ToArray());
}

public interface IMemberMock<TArg1, TReturn, TSelf>
    : IMemberWithArgsThatReturnsMockBase<TReturn, TSelf, Func<TArg1, TReturn>>,
        IMemberWithArgsMock<TArg1, TSelf>
{
    Func<TArg1, TReturn> IMemberWithArgsThatReturnsMockBase<
        TReturn,
        TSelf,
        Func<TArg1, TReturn>
    >.ToFunc(TReturn returnVal) => (_) => returnVal;
}

public interface IMemberWithArgsThatReturnsMock<TArg1, TArg2, TReturn, TSelf>
    : IMemberWithArgsThatReturnsMockBase<TReturn, TSelf, Func<TArg1, TArg2, TReturn>>,
        IMemberWithArgsMock<TArg1, TArg2, TSelf>
{
    Func<TArg1, TArg2, TReturn> IMemberWithArgsThatReturnsMockBase<
        TReturn,
        TSelf,
        Func<TArg1, TArg2, TReturn>
    >.ToFunc(TReturn returnVal) => (_, _) => returnVal;
}

public interface IMemberWithArgsThatReturnsMock<TArg1, TArg2, TArg3, TReturn, TSelf>
    : IMemberWithArgsThatReturnsMockBase<TReturn, TSelf, Func<TArg1, TArg2, TArg3, TReturn>>,
        IMemberWithArgsMock<TArg1, TArg2, TArg3, TSelf>
{
    Func<TArg1, TArg2, TArg3, TReturn> IMemberWithArgsThatReturnsMockBase<
        TReturn,
        TSelf,
        Func<TArg1, TArg2, TArg3, TReturn>
    >.ToFunc(TReturn returnVal) => (_, _, _) => returnVal;
}

public interface IMemberWithArgsThatReturnsMock<TArg1, TArg2, TArg3, TArg4, TReturn, TSelf>
    : IMemberWithArgsThatReturnsMockBase<TReturn, TSelf, Func<TArg1, TArg2, TArg3, TArg4, TReturn>>,
        IMemberWithArgsMock<TArg1, TArg2, TArg3, TArg4, TSelf>
{
    Func<TArg1, TArg2, TArg3, TArg4, TReturn> IMemberWithArgsThatReturnsMockBase<
        TReturn,
        TSelf,
        Func<TArg1, TArg2, TArg3, TArg4, TReturn>
    >.ToFunc(TReturn returnVal) => (_, _, _, _) => returnVal;
}

public interface IMemberWithArgsThatReturnsMock<TArg1, TArg2, TArg3, TArg4, TArg5, TReturn, TSelf>
    : IMemberWithArgsThatReturnsMockBase<
        TReturn,
        TSelf,
        Func<TArg1, TArg2, TArg3, TArg4, TArg5, TReturn>
    >,
        IMemberWithArgsMock<TArg1, TArg2, TArg3, TArg4, TArg5, TSelf>
{
    Func<TArg1, TArg2, TArg3, TArg4, TArg5, TReturn> IMemberWithArgsThatReturnsMockBase<
        TReturn,
        TSelf,
        Func<TArg1, TArg2, TArg3, TArg4, TArg5, TReturn>
    >.ToFunc(TReturn returnVal) => (_, _, _, _, _) => returnVal;
}
