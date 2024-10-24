using System.Collections.Concurrent;
using System.Text;
using HarmonyLib;
using MockMe.Mocks;
using static MockMe.Tests._01_CalculatorMockSetup;
using static MockMe.Tests._01_CalculatorMockSetup._01_CalculatorMockCallTracker;

namespace MockMe.Tests;

public class _01_CalculatorTests
{
    [Fact]
    public void ReplaceMethod_SwapsMethodFunctionality()
    {
        var calculatorMock = MockStore.GetMock();
        calculatorMock.Setup.Add(3, 5).Return((x, y) => x * y);

        int firstArg = 0;
        calculatorMock.Setup.Add(0, 0).Callback((x, _) => firstArg = x);
        Assert.Equal(8, new _01_Calculator().Add(3, 5));
        Assert.Equal(999, calculatorMock.Value.Add(3, 5));
        calculatorMock.Assert.Add(3, 5).WasCalled();
        calculatorMock.Assert.Add(3, new(i => i > 4)).WasCalled();
        calculatorMock.Assert.Add(3, new(i => i > 5)).WasNotCalled();
    }
}

public class _01_Calculator
{
    public int Add(int x, int y) => x + y;
}

public static class MockStore
{
    private static ConcurrentDictionary<_01_Calculator, _01_CalculatorMock> calculatorMocks = [];

    public static _01_CalculatorMock GetMock()
    {
        EnsurePatch();
        _01_CalculatorMock mock = new();
        calculatorMocks.TryAdd(mock.Value, mock);
        return mock;
    }

    public static object GetMock<T>(int useless = 0) => throw new NotImplementedException();

    public static void Me<T>(out object mock)
        where T : Exception => throw new NotImplementedException();

    public static void Me<T>(out _01_CalculatorMock mock)
        where T : _01_Calculator => mock = new _01_CalculatorMock();

    public static void Me<T>(out MockSetup mock)
        where T : MockSetup => mock = new MockSetup();

    public static object Me<T>(object? obj = null)
        where T : Exception => throw new NotImplementedException();

    public static _01_CalculatorMock Me<T>(_01_Calculator? _ = null)
        where T : _01_Calculator => new _01_CalculatorMock();

    public static MockSetup Me<T>(MockSetup? _ = null)
        where T : MockSetup => new MockSetup();

    //public static Mock<T> GetMock<T>()
    //{
    //    EnsurePatch();
    //    _01_CalculatorMock mock = new();
    //    calculatorMocks.TryAdd(mock.Value, mock);
    //    return mock;
    //}

    private static bool isPatched = false;
    private static object lockObj = new object();

    private static void EnsurePatch()
    {
        lock (lockObj)
        {
            if (!isPatched)
            {
                var harmony = new Harmony("com.mockme.patch");
                harmony.PatchAll();
                isPatched = true;
            }
        }
    }

    public static bool TryGetFromInstance(_01_Calculator instance, out _01_CalculatorMock mock)
    {
        return calculatorMocks.TryGetValue(instance, out mock);
    }
}

public class _01_CalculatorMock : Mock<_01_Calculator>
{
    public _01_CalculatorMock()
    {
        Setup = new _01_CalculatorMockSetup();
        CallTracker = new _01_CalculatorMockCallTracker(Setup);
        Assert = new _01_CalculatorMockAsserter(CallTracker);
    }

    public _01_CalculatorMockSetup Setup { get; }
    public _01_CalculatorMockAsserter Assert { get; }
    private _01_CalculatorMockCallTracker CallTracker { get; set; }

    [HarmonyPatch(typeof(_01_Calculator), nameof(_01_Calculator.Add))]
    class Patch00
    {
        static bool Prefix(_01_Calculator __instance, ref int __result, int x, int y)
        {
            if (MockStore.TryGetFromInstance(__instance, out var mock))
            {
                __result = mock.CallTracker.Add(x, y);
                return false;
            }
            return true;
        }
    }
}

public class _01_CalculatorMockSetup : MockSetup
{
    private readonly List<ArgBag<int, int, MemberMock<int, int, int>>> AddBagStore = [];

    public MemberMock<int, int, int> Add(Arg<int> x, Arg<int> y) => SetupMethod(AddBagStore, x, y);

    public class _01_CalculatorMockCallTracker(_01_CalculatorMockSetup setup) : MockCallTracker
    {
        private readonly List<ValueTuple<int, int>> callStore = [];

        public int Add(int x, int y) => CallReplacedCode(setup.AddBagStore, callStore, x, y);

        public class _01_CalculatorMockAsserter(_01_CalculatorMockCallTracker tracker)
            : MockAsserter
        {
            public MemberAsserter Add(Arg<int> x, Arg<int> y) =>
                GetMemberAsserter(tracker.callStore, x, y);
        }
    }
}
