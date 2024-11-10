#nullable enable
using System;
using System.Collections.Generic;
using HarmonyLib;
using MockMe.Mocks;
using MockMe.Tests.ExampleClasses;
using static MockMe.Tests.TempCalcMockSetup;
using static MockMe.Tests.TempCalcMockSetup.TempCalcMockCallTracker;

//using static MockMe.Generated.MockMe.Tests.SampleClasses.TempCalcMockSetup;
//using static MockMe.Generated.MockMe.Tests.SampleClasses.TempCalcMockSetup.TempCalcMockCallTracker;

namespace MockMe.Tests
{
    public class TempCalcMock : Mock<ExampleClasses.ComplexCalculator>
    {
        private static readonly IReadOnlyDictionary<
            ExampleClasses.ComplexCalculator,
            TempCalcMock
        > mockStore =
            (IReadOnlyDictionary<ComplexCalculator, TempCalcMock>)
                MockStore<ComplexCalculator>.GetStore();

        public TempCalcMock()
        {
            this.Setup = new TempCalcMockSetup();
            this.CallTracker = new TempCalcMockCallTracker(this.Setup);
            this.Assert = new TempCalcMockAsserter(this.CallTracker);

            MockStore<ComplexCalculator>.Store.TryAdd(this.MockedObject, this);
            //mockStore.TryAdd(this.Value, this);
        }

        //public ExampleClasses.ComplexCalculator Value { get; } = new();
        public TempCalcMockSetup Setup { get; }
        public TempCalcMockAsserter Assert { get; }
        private TempCalcMockCallTracker CallTracker { get; set; }

        public static IReadOnlyDictionary<
            ExampleClasses.ComplexCalculator,
            TempCalcMock
        > GetStore() => mockStore;

        private T AddUpAllOfThese2<T>(int hello, T[] values, double goodbye)
        {
            if (MockStore<ComplexCalculator>.TryGetValue<object>(default, out var mock))
            {
                var callTracker = mock.GetType()
                    .GetProperty(
                        "CallTracker",
                        System.Reflection.BindingFlags.NonPublic
                            | System.Reflection.BindingFlags.Instance
                    )
                    .GetValue(mock);

                return (T)
                    callTracker
                        .GetType()
                        .GetMethod(
                            "AddUpAllOfThese2",
                            System.Reflection.BindingFlags.Public
                                | System.Reflection.BindingFlags.Instance
                        )
                        .MakeGenericMethod(typeof(T))
                        .Invoke(callTracker, new object[] { hello, values, goodbye });
            }
            return default;
        }

        [HarmonyPatch(
            typeof(ExampleClasses.ComplexCalculator),
            nameof(ExampleClasses.ComplexCalculator.Add)
        )]
        internal sealed class Patchab4ae55136cc4fe4a71b172d61b7b1e9
        {
            private static bool Prefix(
                ExampleClasses.ComplexCalculator __instance,
                ref int __result,
                int x,
                int y
            )
            {
                if (mockStore.TryGetValue(__instance, out var mock))
                {
                    __result = mock.CallTracker.Add(x, y);
                    return false;
                }
                return true;
            }
        }

        [HarmonyPatch(
            typeof(ExampleClasses.ComplexCalculator),
            nameof(ExampleClasses.ComplexCalculator.Multiply)
        )]
        internal sealed class Patch75beb36176734f72b6b2c7f0d3ec10a9
        {
            private static bool Prefix(
                ExampleClasses.ComplexCalculator __instance,
                ref double __result,
                double x,
                double y
            )
            {
                if (
                    MockStore<ComplexCalculator>.TryGetValue<TempCalcMock>(__instance, out var mock)
                )
                {
                    __result = mock.CallTracker.Multiply(x, y);
                    return false;
                }
                return true;
            }
        }

        [HarmonyPatch(
            typeof(ExampleClasses.ComplexCalculator),
            nameof(ExampleClasses.ComplexCalculator.DivideByZero)
        )]
        internal sealed class Patch807db347644c441ab52440f7647fea26
        {
            private static bool Prefix(
                ExampleClasses.ComplexCalculator __instance,
                double numToDivide
            )
            {
                if (mockStore.TryGetValue(__instance, out var mock))
                {
                    mock.CallTracker.DivideByZero(numToDivide);
                    return false;
                }
                return true;
            }
        }

        [HarmonyPatch(
            typeof(ExampleClasses.ComplexCalculator),
            nameof(ExampleClasses.ComplexCalculator.IsOn)
        )]
        internal sealed class Patch1214ed53377f46308e50f73ec1716902
        {
            private static bool Prefix(
                ExampleClasses.ComplexCalculator __instance,
                ref bool __result
            )
            {
                if (mockStore.TryGetValue(__instance, out var mock))
                {
                    __result = mock.CallTracker.IsOn();
                    return false;
                }
                return true;
            }
        }

        [HarmonyPatch(
            typeof(ExampleClasses.ComplexCalculator),
            nameof(ExampleClasses.ComplexCalculator.TurnOff)
        )]
        internal sealed class Patch6e549d91311843b3a1092bf4eeac6fcc
        {
            private static bool Prefix(ExampleClasses.ComplexCalculator __instance)
            {
                if (mockStore.TryGetValue(__instance, out var mock))
                {
                    mock.CallTracker.TurnOff();
                    return false;
                }
                return true;
            }
        }
    }

    public class TempCalcMockSetup : MockSetup
    {
        private List<ArgBagWithMemberMock<int, int, int>>? AddBagStore;

        public MemberMock<int, int, int> Add(Arg<int> x, Arg<int> y) =>
            SetupMethod(this.AddBagStore ??= new(), x, y);

        private List<ArgBagWithMemberMock<double, double, double>>? MultiplyBagStore;

        public MemberMock<double, double, double> Multiply(Arg<double> x, Arg<double> y) =>
            SetupMethod(this.MultiplyBagStore ??= new(), x, y);

        private List<ArgBagWithVoidMemberMock<double>>? DivideByZeroBagStore;

        public VoidMemberMock<double> DivideByZero(Arg<double> numToDivide) =>
            SetupVoidMethod(this.DivideByZeroBagStore ??= new(), numToDivide);

        private MemberMock<bool>? IsOnBagStore;

        public MemberMock<bool> IsOn() => this.IsOnBagStore ??= new();

        private VoidMemberMock? TurnOffBagStore;

        public VoidMemberMock TurnOff() => this.TurnOffBagStore ??= new();

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

        private Dictionary<Type, object>? AddUpAllOfThese2BagStore;

        public MemberMock<int, T[], double, T> AddUpAllOfThese2<T>(
            Arg<int> hello,
            Arg<T[]> values,
            Arg<double> goodbye
        )
        {
            this.AddUpAllOfTheseBagStore ??= new();
            if (!this.AddUpAllOfTheseBagStore.TryGetValue(typeof(T), out object? specificStore))
            {
                specificStore = new List<ArgBagWithMemberMock<int, T[], double, T>>();
                this.AddUpAllOfTheseBagStore.Add(typeof(T), specificStore);
            }
            var typedStore = (List<ArgBagWithMemberMock<int, T[], double, T>>)specificStore;
            return SetupMethod(typedStore, hello, values, goodbye);
        }

        public class TempCalcMockCallTracker : MockCallTracker
        {
            private readonly TempCalcMockSetup setup;

            public TempCalcMockCallTracker(TempCalcMockSetup setup)
            {
                this.setup = setup;
            }

            private List<ValueTuple<int, int>>? AddCallStore;

            public int Add(int x, int y) =>
                CallMemberMock(this.setup.AddBagStore, this.AddCallStore ??= new(), x, y);

            private List<ValueTuple<double, double>>? MultiplyCallStore;

            public double Multiply(double x, double y) =>
                CallMemberMock(this.setup.MultiplyBagStore, this.MultiplyCallStore ??= new(), x, y);

            private List<double>? DivideByZeroCallStore;

            public void DivideByZero(double numToDivide) =>
                CallVoidMemberMock(
                    this.setup.DivideByZeroBagStore,
                    this.DivideByZeroCallStore ??= new(),
                    numToDivide
                );

            private int IsOnCallStore;

            public bool IsOn()
            {
                this.IsOnCallStore++;
                return CallMemberMock(this.setup.IsOnBagStore);
            }

            private int TurnOffCallStore;

            public void TurnOff()
            {
                this.TurnOffCallStore++;
                CallVoidMemberMock(this.setup.TurnOffBagStore);
            }

            private Dictionary<Type, object>? AddUpAllOfThese2CallStore;

            public T AddUpAllOfThese2<T>(int hello, T[] values, double goodbye)
            {
                this.AddUpAllOfThese2CallStore ??= new();
                if (
                    !this.AddUpAllOfThese2CallStore.TryGetValue(
                        typeof(T[]),
                        out object? specificStore
                    )
                )
                {
                    specificStore = new List<T[]>();
                    this.AddUpAllOfThese2CallStore.Add(typeof(T[]), specificStore);
                }
                var typedCallStore = (List<T[]>)specificStore;

                this.setup.AddUpAllOfThese2BagStore ??= new();
                if (
                    !this.setup.AddUpAllOfThese2BagStore.TryGetValue(
                        typeof(T[]),
                        out object? setupStore
                    )
                )
                {
                    setupStore = new List<ArgBagWithMemberMock<T[], T>>();
                    this.setup.AddUpAllOfThese2BagStore.Add(typeof(T[]), setupStore);
                }
                var typedStore = (List<ArgBagWithMemberMock<T[], T>>)setupStore;

                return CallMemberMock(typedStore, typedCallStore, values);
            }

            private Dictionary<Type, object>? AddUpAllOfTheseCallStore;

            public T AddUpAllOfThese<T>(T[] values)
            {
                this.AddUpAllOfTheseCallStore ??= new();
                if (
                    !this.AddUpAllOfTheseCallStore.TryGetValue(
                        typeof(T[]),
                        out object? specificStore
                    )
                )
                {
                    specificStore = new List<T[]>();
                    this.AddUpAllOfTheseCallStore.Add(typeof(T[]), specificStore);
                }
                var typedCallStore = (List<T[]>)specificStore;

                this.setup.AddUpAllOfTheseBagStore ??= new();
                if (
                    !this.setup.AddUpAllOfTheseBagStore.TryGetValue(
                        typeof(T[]),
                        out object? setupStore
                    )
                )
                {
                    setupStore = new List<ArgBagWithMemberMock<T[], T>>();
                    this.setup.AddUpAllOfTheseBagStore.Add(typeof(T[]), setupStore);
                }
                var typedStore = (List<ArgBagWithMemberMock<T[], T>>)setupStore;

                return CallMemberMock(typedStore, typedCallStore, values);
            }

            public class TempCalcMockAsserter : MockAsserter
            {
                private readonly TempCalcMockCallTracker tracker;

                public TempCalcMockAsserter(TempCalcMockCallTracker tracker)
                {
                    this.tracker = tracker;
                }

                public MemberAsserter Add(Arg<int> x, Arg<int> y)
                {
                    return GetMemberAsserter(this.tracker.AddCallStore, x, y);
                }

                public MemberAsserter Multiply(Arg<double> x, Arg<double> y)
                {
                    return GetMemberAsserter(this.tracker.MultiplyCallStore, x, y);
                }

                public MemberAsserter DivideByZero(Arg<double> numToDivide)
                {
                    return GetMemberAsserter(this.tracker.DivideByZeroCallStore, numToDivide);
                }

                public MemberAsserter IsOn() => new(this.tracker.IsOnCallStore);

                public MemberAsserter TurnOff() => new(this.tracker.TurnOffCallStore);
            }
        }
    }
}
