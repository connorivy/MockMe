using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MockMe.Tests.NuGet
{
    public static class MockStore<TOriginal>
        where TOriginal : notnull
    {
        public static ConcurrentDictionary<TOriginal, object> Store { get; } = new();

        public static IReadOnlyDictionary<TOriginal, object> GetStore() => Store;

        public static bool TryGetValue<T>(TOriginal key, [MaybeNullWhen(false)] out T value)
        {
            if (!Store.TryGetValue(key, out var mockAsObject))
            {
                value = default;
                return false;
            }

            value = (T)mockAsObject;
            return true;
        }
    }
}
