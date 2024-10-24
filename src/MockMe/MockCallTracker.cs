using MockMe.Mocks;

namespace MockMe;

public class MockCallTracker
{
    protected static TReturn CallReplacedCode<TArg1, TReturn>(
        List<ArgBag<TArg1, MemberMock<TArg1, TReturn>>> mockStore,
        List<TArg1> argStore,
        TArg1 arg1
    )
    {
        //for (int i = mockStore.Count - 1; i >= 0; i--)
        //{
        //    var argBag = mockStore[i];
        //    if (!argBag.AllArgsSatisfy(arg1))
        //    {
        //        continue;
        //    }

        //    argStore.Add(arg1);
        //    var returnVal = argBag.Mock.ReturnVal;

        //    if (argBag.Mock.GenericActions is not null)
        //    {
        //        foreach (Action action in argBag.Mock.GenericActions)
        //        {
        //            action.Invoke();
        //        }
        //    }
        //    if (argBag.Mock.Actions is not null)
        //    {
        //        foreach (var action in argBag.Mock.Actions)
        //        {
        //            action.Invoke(arg1);
        //        }
        //    }
        //    return returnVal;
        //}

        return default;
    }

    protected static TReturn CallReplacedCode<TArg1, TArg2, TReturn>(
        List<ArgBag<TArg1, TArg2, MemberMock<TArg1, TArg2, TReturn>>> mockStore,
        List<ValueTuple<TArg1, TArg2>> argStore,
        TArg1 arg1,
        TArg2 arg2
    )
    {
        //for (int i = mockStore.Count - 1; i >= 0; i--)
        //{
        //    var argBag = mockStore[i];
        //    if (!argBag.AllArgsSatisfy(arg1, arg2))
        //    {
        //        continue;
        //    }

        //    argStore.Add((arg1, arg2));
        //    var returnVal = argBag.Mock.ReturnVal;

        //    if (argBag.Mock.GenericActions is not null)
        //    {
        //        foreach (Action action in argBag.Mock.GenericActions)
        //        {
        //            action.Invoke();
        //        }
        //    }
        //    if (argBag.Mock.Actions is not null)
        //    {
        //        foreach (var action in argBag.Mock.Actions)
        //        {
        //            action.Invoke(arg1, arg2);
        //        }
        //    }
        //    return returnVal;
        //}

        return default;
    }
}
