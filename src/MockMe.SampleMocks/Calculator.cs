using System.Collections.Concurrent;
using HarmonyLib;
using MockMe.Mocks;
using static MockMe.SampleMocks.CalculatorMockSetup;
using static MockMe.SampleMocks.CalculatorMockSetup.CalculatorMockCallTracker;

namespace MockMe.SampleMocks;

public class Calculator
{
    public int Add(int x, int y) => x + y;

    public double Multiply(double x, double y) => x * y;

    public CalculatorType CalculatorType { get; set; }

    public void DivideByZero(double numToDivide) =>
        throw new InvalidOperationException("Cannot divide by 0");

    public bool IsOn() => true;

    public void TurnOff() { }
}

public enum CalculatorType
{
    Standard,
    Scientific,
    Graphing
}

public class CalculatorMock : Mock<Calculator>
{
    private static readonly ConcurrentDictionary<Calculator, CalculatorMock> mockStore = new();

    public CalculatorMock()
    {
        this.Setup = new CalculatorMockSetup();
        this.CallTracker = new CalculatorMockCallTracker(this.Setup);
        this.Assert = new CalculatorMockAsserter(this.CallTracker);
        mockStore.TryAdd(this.Value, this);
    }

    public CalculatorMockSetup Setup { get; }
    public CalculatorMockAsserter Assert { get; }
    private CalculatorMockCallTracker CallTracker { get; set; }

    [HarmonyPatch(typeof(Calculator), nameof(Calculator.Add))]
    internal sealed class Patch00
    {
        private static bool Prefix(Calculator __instance, ref int __result, int x, int y)
        {
            if (mockStore.TryGetValue(__instance, out var mock))
            {
                __result = mock.CallTracker.Add(x, y);
                return false;
            }
            return true;
        }
    }
}

public class CalculatorMockSetup : MockSetup
{
    private List<ArgBagWithMemberMock<int, int, int>>? addBagStore;

    public MemberMock<int, int, int> Add(Arg<int> x, Arg<int> y) =>
        SetupMethod(this.addBagStore ??= new(), x, y);

    private List<ArgBagWithVoidMemberMock<double>>? divideByZeroBagStore;

    public VoidMemberMock<double> DivideByZero(Arg<double> numToDivide) =>
        SetupVoidMethod(this.divideByZeroBagStore ??= new(), numToDivide);

    private MemberMock<bool>? isOnMockStore;

    public MemberMock<bool> IsOn() => this.isOnMockStore ??= new();

    private VoidMemberMock? turnOffMockStore;

    public VoidMemberMock TurnOff() => this.turnOffMockStore ??= new();

    public class CalculatorMockCallTracker : MockCallTracker
    {
        private readonly CalculatorMockSetup setup;

        public CalculatorMockCallTracker(CalculatorMockSetup setup)
        {
            this.setup = setup;
        }

        private List<ValueTuple<int, int>>? callStore;

        public int Add(int x, int y) =>
            CallMemberMock(this.setup.addBagStore, this.callStore ??= new(), x, y);

        private List<double>? numToDivideCallStore;

        public void DivideByZero(double numToDivide) =>
            CallVoidMemberMock(
                this.setup.divideByZeroBagStore,
                this.numToDivideCallStore ??= new(),
                numToDivide
            );

        private int isOnCallStore;

        public bool IsOn()
        {
            this.isOnCallStore++;
            return CallMemberMock(this.setup.isOnMockStore);
        }

        private int turnOffCallStore;

        public void TurnOff()
        {
            this.turnOffCallStore++;
            CallVoidMemberMock(this.setup.turnOffMockStore);
        }

        public class CalculatorMockAsserter : MockAsserter
        {
            private readonly CalculatorMockCallTracker tracker;

            public CalculatorMockAsserter(CalculatorMockCallTracker tracker)
            {
                this.tracker = tracker;
            }

            public MemberAsserter Add(Arg<int> x, Arg<int> y) =>
                GetMemberAsserter(this.tracker.callStore, x, y);

            public MemberAsserter DivideByZero(Arg<double> numToDivide) =>
                GetMemberAsserter(this.tracker.numToDivideCallStore, numToDivide);

            public MemberAsserter IsOn() => new(this.tracker.isOnCallStore);

            public MemberAsserter TurnOff() => new(this.tracker.turnOffCallStore);
        }
    }
}
