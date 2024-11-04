using System.Collections.Concurrent;
using System.Numerics;
using System.Reflection.Emit;
using HarmonyLib;
using MockMe.Mocks;
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

    [HarmonyPatch(typeof(Calculator), nameof(Calculator.AddUpAllOfThese))]
    internal sealed class Patch01
    {
        public static bool Prefix(Calculator __instance, ref object __result, List<int> values)
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

    public sealed class Hook
    {
        public static int Add(Calculator self, int x, int y)
        {
            //if (mockStore.TryGetValue(__instance, out var mock))
            //{
            //    __result = mock.AddUpAllOfThese.Add(x, y);
            //    return false;
            //}
            return 7;
        }

        public static T Prefix<T>(Calculator self, T[] values)
        {
            //if (mockStore.TryGetValue(__instance, out var mock))
            //{
            //    __result = mock.AddUpAllOfThese.Add(x, y);
            //    return false;
            //}
            var y = typeof(T);
            foreach (var x in values)
            {
                Console.WriteLine(x);
            }
            return default;
        }

        public static List<int> PrefixList(Calculator self, List<int>[] values)
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
            return default;
        }

        public static int PrefixList2(Calculator self, int hello, int[] values, double goodbye)
        {
            //if (mockStore.TryGetValue(__instance, out var mock))
            //{
            //    __result = mock.AddUpAllOfThese.Add(x, y);
            //    return false;
            //}

            var a = (int)(object)values;
            var b = (List<int>[])(object)goodbye;

            foreach (var x in values)
            {
                Console.WriteLine(x);
            }
            return default;
        }

        public static object PrefixList3(Calculator self, int hello, object values, double goodbye)
        {
            //if (mockStore.TryGetValue(__instance, out var mock))
            //{
            //    __result = mock.AddUpAllOfThese.Add(x, y);
            //    return false;
            //}

            var a = (int)(object)values;
            var b = (List<int>[])(object)goodbye;

            //foreach (var x in values)
            //{
            //    Console.WriteLine(x);
            //}
            return default;
        }

        public static T PrefixList4<T>(Calculator self, int hello, T[] values, double goodbye)
        {
            //if (mockStore.TryGetValue(__instance, out var mock))
            //{
            //    __result = mock.AddUpAllOfThese.Add(x, y);
            //    return false;
            //}

            var a = (int)(object)values;
            var b = (List<int>[])(object)goodbye;

            //foreach (var x in values)
            //{
            //    Console.WriteLine(x);
            //}
            return default;
        }

        public static bool PrefixList5(
            Calculator __instance,
            object __result,
            int hello,
            object values,
            double goodbye
        )
        {
            //if (mockStore.TryGetValue(__instance, out var mock))
            //{
            //    __result = mock.AddUpAllOfThese.Add(x, y);
            //    return false;
            //}

            var a = (int)(object)values;
            var b = (List<int>[])(object)goodbye;

            //foreach (var x in values)
            //{
            //    Console.WriteLine(x);
            //}
            return default;
        }

        //public static IEnumerable<CodeInstruction> PrefixList6(
        //    IEnumerable<CodeInstruction> instructions
        //)
        //{
        //    //List<CodeInstruction> additional = [];
        //    //additional.Add(new(OpCodes.Ldarg, 1));
        //    //additional.Add(new(OpCodes.Ldarg, 2));
        //    //additional.Add(new(OpCodes.Ldarg, 3));
        //    //additional.Add(new(OpCodes.Call, typeof(Hook).GetMethod(nameof(PrefixList7))));
        //    //additional.Add(new(OpCodes.Ret));

        //    return
        //    [
        //        new(OpCodes.Ldarg_1),
        //        new(OpCodes.Ldarg_2),
        //        new(OpCodes.Ldarg_3),
        //        new(
        //            OpCodes.Call,
        //            typeof(Hook).GetMethod(nameof(PrefixList7)).MakeGenericMethod(typeof(object))
        //        ),
        //        new(OpCodes.Ret)
        //    ];

        //    //return additional;
        //}

        //public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        //{
        //    // Without ILGenerator, the CodeMatcher will not be able to create labels
        //    var codeMatcher = new CodeMatcher();

        //    codeMatcher.


        //    codeMatcher.MatchStartForward(
        //            CodeMatch.Calls(() => default(DamageHandler).Kill(default(Player)))
        //        )
        //        .ThrowIfInvalid("Could not find call to DamageHandler.Kill")
        //        .RemoveInstruction()
        //        .InsertAndAdvance(
        //            CodeInstruction.Call(() => MyDeathHandler(default, default))
        //        )
        //        .insertand

        //    return codeMatcher.Instructions();
        //}

        public static T PrefixList7<T>(int hello, T[] values, double goodbye)
        {
            //if (mockStore.TryGetValue(__instance, out var mock))
            //{
            //    __result = mock.AddUpAllOfThese.Add(x, y);
            //    return false;
            //}

            var a = (int)(object)values;
            var b = (List<int>[])(object)goodbye;

            foreach (var x in values)
            {
                Console.WriteLine(x);
            }
            return default;
        }

        public static object PrefixList8(int hello, object[] values, double goodbye)
        {
            //if (mockStore.TryGetValue(__instance, out var mock))
            //{
            //    __result = mock.AddUpAllOfThese.Add(x, y);
            //    return false;
            //}

            var a = (int)(object)values;
            var b = (List<int>[])(object)goodbye;

            foreach (var x in values)
            {
                Console.WriteLine(x);
            }
            return default;
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

    private Dictionary<Type, object>? AddUpAllOfTheseBagStore;

    public MemberMock<T[], T> AddUpAllOfThese<T>(Arg<T[]> values)
    {
        this.AddUpAllOfTheseBagStore ??= new();
        if (!this.AddUpAllOfTheseBagStore.TryGetValue(typeof(T[]), out object? specificStore))
        {
            specificStore = new List<ArgBagWithMemberMock<T[], T>>();
            this.AddUpAllOfTheseBagStore.Add(typeof(T[]), specificStore);
        }
        var typedStore = (List<ArgBagWithMemberMock<T[], T>>)specificStore;
        return SetupMethod(typedStore, values);
    }

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

        private Dictionary<Type, object>? AddUpAllOfTheseCallStore;

        public T AddUpAllOfThese<T>(T[] values)
        {
            this.AddUpAllOfTheseCallStore ??= new();
            if (!this.AddUpAllOfTheseCallStore.TryGetValue(typeof(T[]), out object? specificStore))
            {
                specificStore = new List<T[]>();
                this.AddUpAllOfTheseCallStore.Add(typeof(T[]), specificStore);
            }
            var typedCallStore = (List<T[]>)specificStore;

            this.setup.AddUpAllOfTheseBagStore ??= new();
            if (
                !this.setup.AddUpAllOfTheseBagStore.TryGetValue(typeof(T[]), out object? setupStore)
            )
            {
                setupStore = new List<ArgBagWithMemberMock<T[], T>>();
                this.setup.AddUpAllOfTheseBagStore.Add(typeof(T[]), setupStore);
            }
            var typedStore = (List<ArgBagWithMemberMock<T[], T>>)setupStore;

            return CallMemberMock(typedStore, typedCallStore, values);
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
                    !this.tracker.AddUpAllOfTheseCallStore.TryGetValue(
                        typeof(T[]),
                        out object? specificStore
                    )
                )
                {
                    specificStore = new List<T[]>();
                    this.tracker.AddUpAllOfTheseCallStore.Add(typeof(T[]), specificStore);
                }
                var typedCallStore = (List<T[]>)specificStore;

                return GetMemberAsserter(typedCallStore, values);
            }
        }
    }
}
