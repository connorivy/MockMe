namespace MockMe.Mocks;

internal abstract class CallbackManager
{
    protected int? NumCallbacksRegisteredBeforeReturn { get; private set; } = null;

    internal void ReturnCalled()
    {
        this.NumCallbacksRegisteredBeforeReturn ??= GetNumCallbacks();
    }

    protected abstract int GetNumCallbacks();
}

internal class ActionCallbackManager<TCallback> : CallbackManager
{
    internal List<TCallback>? Callbacks { get; private set; }

    public void AddCallback(TCallback action)
    {
        Callbacks ??= [];
        Callbacks.Add(action);
    }

    internal IEnumerable<TCallback> GetCallbacksRegisteredBeforeReturnCall()
    {
        if (Callbacks is null)
        {
            return [];
        }

        if (NumCallbacksRegisteredBeforeReturn is null)
        {
            // user never called return
            // so give all callbacks
            return Callbacks.AsReadOnly();
        }

        return Callbacks.Take(NumCallbacksRegisteredBeforeReturn.Value);
    }

    internal IEnumerable<TCallback> GetCallbacksRegisteredAfterReturnCall()
    {
        if (Callbacks is null)
        {
            return [];
        }

        if (NumCallbacksRegisteredBeforeReturn is null)
        {
            // user never called return
            // so give all callbacks were given in the before method
            return [];
        }

        return Callbacks.Skip(NumCallbacksRegisteredBeforeReturn.Value);
    }

    protected override int GetNumCallbacks() => Callbacks?.Count ?? 0;
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
