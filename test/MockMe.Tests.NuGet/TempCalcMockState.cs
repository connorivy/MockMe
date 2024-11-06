using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockMe.Tests.NuGet
{
    public static class TempCalcMockState<TOriginal, TMock>
        where TOriginal : notnull
    {
        public static ConcurrentDictionary<TOriginal, TMock> MockStore { get; } = new();
    }
}
