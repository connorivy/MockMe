namespace MockMe.Mocks;

internal abstract class CallbackManager
{
    protected int? NumCallbacksRegisteredBeforeReturn { get; private set; }

    internal void ReturnCalled()
    {
        this.NumCallbacksRegisteredBeforeReturn ??= this.GetNumCallbacks();
    }

    protected abstract int GetNumCallbacks();
}

internal class ActionCallbackManager<TCallback> : CallbackManager
{
    internal List<TCallback>? Callbacks { get; private set; }

    public void AddCallback(TCallback action)
    {
        this.Callbacks ??= [];
        this.Callbacks.Add(action);
    }

    internal IEnumerable<TCallback> GetCallbacksRegisteredBeforeReturnCall()
    {
        if (this.Callbacks is null)
        {
            return [];
        }

        if (this.NumCallbacksRegisteredBeforeReturn is null)
        {
            // user never called return
            // so give all callbacks
            return this.Callbacks.AsReadOnly();
        }

        return this.Callbacks.Take(this.NumCallbacksRegisteredBeforeReturn.Value);
    }

    internal IEnumerable<TCallback> GetCallbacksRegisteredAfterReturnCall()
    {
        if (this.Callbacks is null)
        {
            return [];
        }

        if (this.NumCallbacksRegisteredBeforeReturn is null)
        {
            // user never called return
            // so give all callbacks were given in the before method
            return [];
        }

        return this.Callbacks.Skip(this.NumCallbacksRegisteredBeforeReturn.Value);
    }

    protected override int GetNumCallbacks() => this.Callbacks?.Count ?? 0;
}

internal sealed class CallbackManager<TCallback>(Func<Action, TCallback> toCallback)
    : ActionCallbackManager<TCallback>
{
    public void AddCallback(Action action) => this.AddCallback(toCallback(action));
}
