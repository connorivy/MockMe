using System.Collections.Concurrent;
using System.Numerics;
using System.Reflection.Emit;
using HarmonyLib;
using MockMe.Mocks.ClassMemberMocks;
using MockMe.Mocks.ClassMemberMocks.CallTracker;
using MockMe.Mocks.ClassMemberMocks.Setup;
using static MockMe.SampleMocks.CalculatorSample.CalculatorMockSetup;
using static MockMe.SampleMocks.CalculatorSample.CalculatorMockSetup.CalculatorMockCallTracker;

namespace MockMe.SampleMocks.CalculatorSample;

public class CalculatorMock : Mock<Calculator>
{
    private static readonly ConcurrentDictionary<Calculator, CalculatorMock> mockStore = new();

    public CalculatorMock()
    {
        this.Setup = new CalculatorMockSetup();
        this.CallTracker = new CalculatorMockCallTracker(this.Setup);
        this.Assert = new CalculatorMockAsserter(this.CallTracker);
        mockStore.TryAdd(this.MockedObject, this);
    }

    public CalculatorMockSetup Setup { get; }
    public CalculatorMockAsserter Assert { get; }
    public override Calculator MockedObject { get; }
    private CalculatorMockCallTracker CallTracker { get; set; }

    public static IReadOnlyDictionary<Calculator, CalculatorMock> GetMockStore() => mockStore;

    [HarmonyPatch(typeof(Calculator), nameof(Calculator.Add))]
    internal sealed class Patch00
    {
        private static bool Prefix(Calculator __instance, ref int __result, int x, int y)
        {
            var x = typeof(Calculator).GetMethod("asdf", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic)
            if (mockStore.TryGetValue(__instance, out var mock))
            {
                __result = mock.CallTracker.Add(x, y);
                return false;
            }
            return true;
        }
    }

    //[HarmonyPatch(typeof(Calculator), nameof(Calculator.AddUpAllOfThese))]
    internal sealed class Patch01
    {
        public static bool Prefix<T>(Calculator __instance, ref object __result, List<int> values)
        {
            //if (mockStore.TryGetValue(__instance, out var mock))
            //{
            //    __result = mock.AddUpAllOfThese.Add(x, y);
            //    return false;
            //}
            foreach (var x in values)
            {
                Console.WriteLine(x);
            }
            return true;
        }
    }
}

public class CalculatorMockSetup : MemberMockSetup
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

    private Dictionary<int, object>? AddUpAllOfTheseBagStore;

    public MemberMock<T[], T> AddUpAllOfThese<T>(Arg<T[]> values) =>
        SetupMethod(SetupGenericStore<T[], T>(this.AddUpAllOfTheseBagStore ??= new()), values);

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
            VoidMockCallTracker.CallVoidMemberMock(
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
            VoidMockCallTracker.CallVoidMemberMock(this.setup.turnOffMockStore);
        }

        private Dictionary<int, object>? AddUpAllOfTheseCallStore;

        public T AddUpAllOfThese<T>(T[] values)
        {
            int genericTypeHashCode = GetUniqueIntFromTypes(typeof(T));
            //this.AddUpAllOfTheseCallStore ??= new();
            //if (!this.AddUpAllOfTheseCallStore.TryGetValue(0, out object? specificStore))
            //{
            //    specificStore = new List<T[]>();
            //    this.AddUpAllOfTheseCallStore.Add(0, specificStore);
            //}
            //var typedCallStore = (List<T[]>)specificStore;

            //this.setup.AddUpAllOfTheseBagStore ??= new();
            //if (
            //    !this.setup.AddUpAllOfTheseBagStore.TryGetValue(0, out object? setupStore)
            //)
            //{
            //    setupStore = new List<ArgBagWithMemberMock<T[], T>>();
            //    this.setup.AddUpAllOfTheseBagStore.Add(0, setupStore);
            //}
            //var typedStore = (List<ArgBagWithMemberMock<T[], T>>)setupStore;
            var mockStore =
                this.setup.AddUpAllOfTheseBagStore?.GetValueOrDefault(genericTypeHashCode)
                as List<ArgBagWithMemberMock<T[], T>>;

            return CallMemberMock(
                mockStore,
                GenericCallStoreRetriever.GetGenericCallStore<T[]>(
                    AddUpAllOfTheseCallStore ??= new(),
                    genericTypeHashCode
                ),
                values
            );
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

            public MemberAsserter AddUpAllOfThese<T>(Arg<T[]> values)
            {
                this.tracker.AddUpAllOfTheseCallStore ??= new();
                if (
                    !this.tracker.AddUpAllOfTheseCallStore.TryGetValue(0, out object? specificStore)
                )
                {
                    specificStore = new List<T[]>();
                    this.tracker.AddUpAllOfTheseCallStore.Add(0, specificStore);
                }
                var typedCallStore = (List<T[]>)specificStore;

                return GetMemberAsserter(
                    this.tracker.AddUpAllOfTheseCallStore?.GetValueOrDefault(0) as List<T[]>,
                    values
                );
            }
        }
    }
}
