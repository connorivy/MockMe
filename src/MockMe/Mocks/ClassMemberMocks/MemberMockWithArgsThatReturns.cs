//namespace MockMe.Mocks.ClassMemberMocks;

//public abstract class MemberMockWithArgsThatReturns<TReturn, TSelf, TArgCollection>
//    : VoidMemberMockBase<TSelf, TArgCollection>,
//        IMemberMockWithArgs<TReturn, TSelf, Action<TArgCollection>, Func<TArgCollection, TReturn>>,
//        IMockCallbackAndReturnCallRetriever<Action<TArgCollection>, Func<TArgCollection, TReturn>>
//    where TSelf : MemberMockWithArgsThatReturns<TReturn, TSelf, TArgCollection>
//{
//    private readonly ReturnManager<TArgCollection, TReturn> returnManager;

//    internal MemberMockWithArgsThatReturns(
//        CallbackManager<TArgCollection> callbackManager,
//        Func<TReturn, Func<TArgCollection, TReturn>> toReturnCall,
//        Func<Exception, Func<TArgCollection, TReturn>> toThrownException
//    )
//        : base(callbackManager)
//    {
//        this.returnManager = new(callbackManager, toReturnCall, toThrownException);
//    }

//    public TSelf Returns(
//        Func<TArgCollection, TReturn> returnThis,
//        params Func<TArgCollection, TReturn>[] thenReturnThese
//    )
//    {
//        this.returnManager.Returns(returnThis, thenReturnThese);
//        return (TSelf)this;
//    }

//    public TSelf Returns(TReturn returnThis, params TReturn[] thenReturnThese)
//    {
//        this.returnManager.Returns(returnThis, thenReturnThese);
//        return (TSelf)this;
//    }

//    public void Throws(Exception ex)
//    {
//        this.returnManager.Throws(ex);
//    }

//    Func<TArgCollection, TReturn>? IMockReturnCallRetriever<
//        Func<TArgCollection, TReturn>
//    >.GetReturnValue() => this.returnManager.GetReturnCall();
//}

//public class MemberMock<TArgCollection, TReturn>
//    : MemberMockWithArgsThatReturns<TReturn, MemberMock<TArgCollection, TReturn>, TArgCollection>
//{
//    public MemberMock()
//        : base(
//            new(static action => (_) => action()),
//            static ret => (_) => ret,
//            static ex => (_) => throw ex
//        ) { }
//}
