namespace MockMe;

public interface IArgBagWithMock2<TOriginalArgCollection, TCallback>
{
    public IArgBag<TOriginalArgCollection> ArgBag { get; }
    public IMockCallbackRetriever<TCallback> Mock { get; }
}

public class ArgBagWithMock<TOriginalArgCollection>(
    IArgBag<TOriginalArgCollection> argBag,
    IMockCallbackRetriever<Action<TOriginalArgCollection>> mock
) : IArgBagWithMock2<TOriginalArgCollection, Action<TOriginalArgCollection>>
{
    public IArgBag<TOriginalArgCollection> ArgBag { get; } = argBag;
    public IMockCallbackRetriever<Action<TOriginalArgCollection>> Mock { get; } = mock;
}
