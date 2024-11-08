using System.Collections.Concurrent;

namespace MockMe.Mocks.Generic;

public static class MockStore<TOriginal>
    where TOriginal : notnull
{
    public static ConcurrentDictionary<TOriginal, object> Store { get; } = new();

    public static IReadOnlyDictionary<TOriginal, object> GetStore() => Store;
}
