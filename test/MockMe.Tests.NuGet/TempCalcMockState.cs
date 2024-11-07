using System.Collections.Concurrent;
using System.Collections.Generic;

namespace MockMe.Tests.NuGet
{
    public static class TempCalcMockState<TOriginal>
        where TOriginal : notnull
    {
        public static ConcurrentDictionary<TOriginal, object> MockStore { get; } = new();

        public static IReadOnlyDictionary<TOriginal, object> GetStore() => MockStore;
    }
}
