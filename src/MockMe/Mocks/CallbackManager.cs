namespace MockMe.Mocks;

internal class ActionCallbackManager<TCallback>
{
    internal List<TCallback>? Callbacks { get; private set; }

    public void AddCallback(TCallback action)
    {
        Callbacks ??= [];
        Callbacks.Add(action);
    }
}

internal class CallbackManager<TCallback> : ActionCallbackManager<TCallback>
{
    private readonly Func<Action, TCallback> toCallback;

    public CallbackManager(Func<Action, TCallback> toCallback)
    {
        this.toCallback = toCallback;
    }

    public void AddCallback(Action action) => AddCallback(toCallback(action));
}
