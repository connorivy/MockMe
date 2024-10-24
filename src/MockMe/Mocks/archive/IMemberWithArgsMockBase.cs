namespace MockMe.Mocks.archive;

public interface IMemberWithArgsMockBase<TSelf, TCallback>
    : IMemberMockWithCallback<TSelf, TCallback>
{
    public TSelf Callback(Action action) => Callback(ToTypedAction(action));
    protected TCallback ToTypedAction(Action callback);
}

public interface IMemberWithArgsMock<TArg1, TSelf> : IMemberWithArgsMockBase<TSelf, Action<TArg1>>
{
    Action<TArg1> IMemberWithArgsMockBase<TSelf, Action<TArg1>>.ToTypedAction(Action action) =>
        (_) => action();
}

public interface IMemberWithArgsMock<TArg1, TArg2, TSelf>
    : IMemberWithArgsMockBase<TSelf, Action<TArg1, TArg2>>
{
    Action<TArg1, TArg2> IMemberWithArgsMockBase<TSelf, Action<TArg1, TArg2>>.ToTypedAction(
        Action action
    ) => (_, _) => action();
}

public interface IMemberWithArgsMock<TArg1, TArg2, TArg3, TSelf>
    : IMemberWithArgsMockBase<TSelf, Action<TArg1, TArg2, TArg3>>
{
    Action<TArg1, TArg2, TArg3> IMemberWithArgsMockBase<
        TSelf,
        Action<TArg1, TArg2, TArg3>
    >.ToTypedAction(Action action) => (_, _, _) => action();
}

public interface IMemberWithArgsMock<TArg1, TArg2, TArg3, TArg4, TSelf>
    : IMemberWithArgsMockBase<TSelf, Action<TArg1, TArg2, TArg3, TArg4>>
{
    Action<TArg1, TArg2, TArg3, TArg4> IMemberWithArgsMockBase<
        TSelf,
        Action<TArg1, TArg2, TArg3, TArg4>
    >.ToTypedAction(Action action) => (_, _, _, _) => action();
}

public interface IMemberWithArgsMock<TArg1, TArg2, TArg3, TArg4, TArg5, TSelf>
    : IMemberWithArgsMockBase<TSelf, Action<TArg1, TArg2, TArg3, TArg4, TArg5>>
{
    Action<TArg1, TArg2, TArg3, TArg4, TArg5> IMemberWithArgsMockBase<
        TSelf,
        Action<TArg1, TArg2, TArg3, TArg4, TArg5>
    >.ToTypedAction(Action action) => (_, _, _, _, _) => action();
}
