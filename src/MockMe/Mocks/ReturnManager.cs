using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockMe.Mocks;

internal class ReturnManager<TReturnCall>
{
    internal Queue<TReturnCall>? ReturnCalls { get; set; }

    public void Returns(TReturnCall returnThis, params TReturnCall[]? thenReturnThese)
    {
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
}

internal class ReturnManager<TReturn, TReturnCall> : ReturnManager<TReturnCall>
{
    private readonly Func<TReturn, TReturnCall> ToReturnCall;

    public ReturnManager(Func<TReturn, TReturnCall> toReturnCall)
    {
        ToReturnCall = toReturnCall;
    }

    public void Returns(TReturn returnThis, params TReturn[]? thenReturnThese) =>
        Returns(ToReturnCall(returnThis), thenReturnThese?.Select(ToReturnCall).ToArray());
}
