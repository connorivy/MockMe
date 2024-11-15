namespace MockMe.Mocks;

internal class ReturnManager<TReturnCall>(CallbackManager callbackManager)
{
    internal Queue<TReturnCall>? ReturnCalls { get; set; }

    public void Returns(TReturnCall returnThis, params TReturnCall[]? thenReturnThese)
    {
        callbackManager.ReturnCalled();
        this.ReturnCalls ??= [];
        this.ReturnCalls.Enqueue(returnThis);
        if (thenReturnThese is not null)
        {
            foreach (var returnVal in thenReturnThese)
            {
                this.ReturnCalls.Enqueue(returnVal);
            }
        }
    }

    internal TReturnCall? GetReturnCall()
    {
        if (this.ReturnCalls is not null && this.ReturnCalls.Count > 0)
        {
            if (this.ReturnCalls.Count == 1)
            {
                // don't dequeue the last configured return call
                return this.ReturnCalls.Peek();
            }

            return this.ReturnCalls.Dequeue();
        }

        return default;
    }
}

internal sealed class ReturnManager<TReturn, TReturnCall>(
    CallbackManager callbackManager,
    Func<TReturn, TReturnCall> toReturnCall
) : ReturnManager<TReturnCall>(callbackManager)
{
    public void Returns(TReturn returnThis, params TReturn[]? thenReturnThese) =>
        this.Returns(toReturnCall(returnThis), thenReturnThese?.Select(toReturnCall).ToArray());
}
