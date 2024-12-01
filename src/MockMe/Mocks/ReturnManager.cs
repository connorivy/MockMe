namespace MockMe.Mocks;

internal abstract class ReturnManagerBase<TReturn, TReturnCall>(
    CallbackManagerBase callbackManager,
    Func<TReturn, TReturnCall> toReturnCall,
    Func<Exception, TReturnCall> exceptionFuncFactory
)
{
    internal Queue<TReturnCall>? ReturnCalls { get; set; }

    public void Returns(TReturn returnThis, params TReturn[]? thenReturnThese) =>
        this.Returns(toReturnCall(returnThis), thenReturnThese?.Select(toReturnCall).ToArray());

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

    public void Throws(Exception ex)
    {
        this.ReturnCalls ??= [];
        this.ReturnCalls.Enqueue(exceptionFuncFactory(ex));
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

internal sealed class ReturnManager<TReturn>(CallbackManagerBase callbackManager)
    : ReturnManagerBase<TReturn, Func<TReturn>>(callbackManager, toReturnCall, toThrownEx)
{
    private static readonly Func<TReturn, Func<TReturn>> toReturnCall = static ret => () => ret;
    private static readonly Func<Exception, Func<TReturn>> toThrownEx = static ex => () => throw ex;
}

internal sealed class ReturnManager<TArgCollection, TReturn>(CallbackManagerBase callbackManager)
    : ReturnManagerBase<TReturn, Func<TArgCollection, TReturn>>(
        callbackManager,
        toReturnCall,
        toThrownEx
    )
{
    private static readonly Func<TReturn, Func<TArgCollection, TReturn>> toReturnCall =
        static ret => (_) => ret;
    private static readonly Func<Exception, Func<TArgCollection, TReturn>> toThrownEx = static ex =>
        (_) => throw ex;

    public void Returns(Func<TReturn> returnThis)
    {
        TReturn func(TArgCollection _) => returnThis();
        this.Returns(func);
    }
}
