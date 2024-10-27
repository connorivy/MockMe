namespace MockMe.Mocks;

internal class ReturnManager<TReturnCall>(CallbackManager callbackManager)
{
    internal Queue<TReturnCall>? ReturnCalls { get; set; }

    public void Returns(TReturnCall returnThis, params TReturnCall[]? thenReturnThese)
    {
        callbackManager.ReturnCalled();
        ReturnCalls ??= [];
        ReturnCalls.Enqueue(returnThis);
        if (thenReturnThese is not null)
        {
            foreach (var returnVal in thenReturnThese)
            {
                ReturnCalls.Enqueue(returnVal);
            }
        }
    }

    internal TReturnCall? GetReturnCall()
    {
        if (ReturnCalls is not null && ReturnCalls.TryDequeue(out var returnCall))
        {
            return returnCall;
        }

        return default;
    }
}

internal class ReturnManager<TReturn, TReturnCall>(
    CallbackManager callbackManager,
    Func<TReturn, TReturnCall> toReturnCall
) : ReturnManager<TReturnCall>(callbackManager)
{
    public void Returns(TReturn returnThis, params TReturn[]? thenReturnThese) =>
        Returns(toReturnCall(returnThis), thenReturnThese?.Select(toReturnCall).ToArray());
}
