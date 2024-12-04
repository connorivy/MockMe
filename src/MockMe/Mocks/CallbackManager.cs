namespace MockMe.Mocks;

public abstract class CallbackManagerBase
{
    protected int? NumCallbacksRegisteredBeforeReturn { get; private set; }

    internal void ReturnCalled()
    {
        this.NumCallbacksRegisteredBeforeReturn ??= this.GetNumCallbacks();
    }

    protected abstract int GetNumCallbacks();
}

public interface ICallbackManagerBase<TCallback>
{
    public void AddCallback(TCallback action);
}

public interface ICallbackManager : ICallbackManagerBase<Action> { }

public interface ICallbackManager<TArgCollection>
    : ICallbackManager,
        ICallbackManagerBase<Action<TArgCollection>> { }

public abstract class CallbackManagerBase<TCallback> : CallbackManagerBase
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

public sealed class CallbackManager() : CallbackManagerBase<Action>, ICallbackManager { }

public sealed class CallbackManager<TArgCollection>
    : CallbackManagerBase<Action<TArgCollection>>,
        ICallbackManager<TArgCollection>
{
    private static readonly Func<Action, Action<TArgCollection>> toCallback = static action =>
        (_) => action();

    public void AddCallback(Action action) => this.AddCallback(toCallback(action));
}
