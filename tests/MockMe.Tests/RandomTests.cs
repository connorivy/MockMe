using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
//using MockMe.SampleMocks.CalculatorSample;
//using MockMe.SampleMocks.CalculatorSample;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;
using MonoMod.Utils;
using Newtonsoft.Json.Linq;

namespace MockMe.Tests
{
    public class RandomTests
    {
        // Uncomment to disable tests
        private class FactAttribute : Attribute { }

        [Fact]
        public void Test() { }
    }

    internal class ConnorsCoolGenericType_1Mock<T>
        : global::MockMe.Abstractions.SealedTypeMock<global::MockMe.Tests.ConnorsCoolGenericType<T>>
    {
        public ConnorsCoolGenericType_1Mock()
        {
            this.Setup = new ConnorsCoolGenericType_1MockSetup<T>();
            this.CallTracker =
                new MockMe.Tests.ConnorsCoolGenericType_1MockSetup<T>.ConnorsCoolGenericType_1MockCallTracker(
                    this.Setup
                );
            this.Assert =
                new MockMe.Tests.ConnorsCoolGenericType_1MockSetup<T>.ConnorsCoolGenericType_1MockCallTracker.ConnorsCoolGenericType_1MockAsserter(
                    this.CallTracker
                );
            global::MockMe.MockStore<global::MockMe.Tests.ConnorsCoolGenericType<T>>.Store.TryAdd(
                this.MockedObject,
                this
            );
        }

        public ConnorsCoolGenericType_1MockSetup<T> Setup { get; }
        public MockMe.Tests.ConnorsCoolGenericType_1MockSetup<T>.ConnorsCoolGenericType_1MockCallTracker.ConnorsCoolGenericType_1MockAsserter Assert { get; }
        private MockMe.Tests.ConnorsCoolGenericType_1MockSetup<T>.ConnorsCoolGenericType_1MockCallTracker CallTracker { get; }

        private T GetRandomVal()
        {
            if (
                global::MockMe.MockStore<global::MockMe.Tests.ConnorsCoolGenericType<T>>.TryGetValue<
                    ConnorsCoolGenericType_1Mock<T>
                >(default, out var mock)
            )
            {
                var callTracker = mock.GetType()
                    .GetProperty(
                        "CallTracker",
                        global::System.Reflection.BindingFlags.NonPublic
                            | global::System.Reflection.BindingFlags.Instance
                    )
                    .GetValue(mock);

                return (T)
                    callTracker
                        .GetType()
                        .GetMethod(
                            "GetRandomVal",
                            global::System.Reflection.BindingFlags.Public
                                | global::System.Reflection.BindingFlags.Instance
                        )
                        //.MakeGenericMethod(typeof(T))
                        .Invoke(callTracker, new object[] { });
            }
            return default;
        }

        //private T GetRandomVal()
        //{
        //    if (global::MockMe.MockStore<global::MockMe.Tests.ConnorsCoolGenericType<T>>.TryGetValue<
        //            ConnorsCoolGenericType_1Mock<T>
        //        >(default, out var mock))
        //    {
        //        var callTracker = mock.GetType()
        //            .GetProperty(
        //                "CallTracker",
        //                global::System.Reflection.BindingFlags.NonPublic
        //                    | global::System.Reflection.BindingFlags.Instance
        //            )
        //            .GetValue(mock);

        //        return (T)
        //            callTracker
        //                .GetType()
        //                .GetMethod(
        //                    "GetRandomVal",
        //                    global::System.Reflection.BindingFlags.Public
        //                        | global::System.Reflection.BindingFlags.Instance
        //                )
        //                //.MakeGenericMethod(typeof(T))
        //                .Invoke(callTracker, new object[] { });
        //    }
        //    return default;
        //}

        internal sealed class Patch8ec79a7993df4f629bec06faf0471048
        {
            private static bool Prefix(
                global::MockMe.Tests.ConnorsCoolGenericType<T> __instance,
                ref T __result
            )
            {
                if (
                    global::MockMe.MockStore<global::MockMe.Tests.ConnorsCoolGenericType<T>>.TryGetValue<
                        ConnorsCoolGenericType_1Mock<T>
                    >(__instance, out var mock)
                )
                {
                    __result = mock.CallTracker.MyCoolProp;
                    return false;
                }
                return true;
            }
        }

        internal sealed class Patch1ec57d74711f4edca0a02202a86ef4ac
        {
            private static bool Prefix(
                global::MockMe.Tests.ConnorsCoolGenericType<T> __instance,
                ref global::System.String __result,
                T input
            )
            {
                if (
                    global::MockMe.MockStore<global::MockMe.Tests.ConnorsCoolGenericType<T>>.TryGetValue<
                        ConnorsCoolGenericType_1Mock<T>
                    >(__instance, out var mock)
                )
                {
                    __result = mock.CallTracker.TakeAT(input);
                    return false;
                }
                return true;
            }
        }

        static ConnorsCoolGenericType_1Mock()
        {
            var harmony = new global::HarmonyLib.Harmony("com.mockme.patch");

            //var originalPatchda3d87b696c842a3b100cfdd86e2a81e =
            //    typeof(global::MockMe.Tests.ConnorsCoolGenericType<T>).GetMethod(
            //        "GetRandomVal",
            //        new Type[] { }
            //    );
            //var Patchda3d87b696c842a3b100cfdd86e2a81e =
            //    typeof(Patchda3d87b696c842a3b100cfdd86e2a81e).GetMethod(
            //        "Prefix",
            //        global::System.Reflection.BindingFlags.Static
            //            | global::System.Reflection.BindingFlags.NonPublic
            //    );

            //harmony.Patch(
            //    originalPatchda3d87b696c842a3b100cfdd86e2a81e,
            //    prefix: new HarmonyMethod(Patchda3d87b696c842a3b100cfdd86e2a81e)
            //);

            var originalPatch8ec79a7993df4f629bec06faf0471048 =
                typeof(global::MockMe.Tests.ConnorsCoolGenericType<T>).GetMethod(
                    "get_MyCoolProp",
                    new Type[] { }
                );
            var Patch8ec79a7993df4f629bec06faf0471048 =
                typeof(Patch8ec79a7993df4f629bec06faf0471048).GetMethod(
                    "Prefix",
                    global::System.Reflection.BindingFlags.Static
                        | global::System.Reflection.BindingFlags.NonPublic
                );

            harmony.Patch(
                originalPatch8ec79a7993df4f629bec06faf0471048,
                prefix: new HarmonyMethod(Patch8ec79a7993df4f629bec06faf0471048)
            );

            var originalPatch1ec57d74711f4edca0a02202a86ef4ac =
                typeof(global::MockMe.Tests.ConnorsCoolGenericType<T>).GetMethod(
                    "TakeAT",
                    new Type[] { typeof(T) }
                );
            var Patch1ec57d74711f4edca0a02202a86ef4ac =
                typeof(Patch1ec57d74711f4edca0a02202a86ef4ac).GetMethod(
                    "Prefix",
                    global::System.Reflection.BindingFlags.Static
                        | global::System.Reflection.BindingFlags.NonPublic
                );

            harmony.Patch(
                originalPatch1ec57d74711f4edca0a02202a86ef4ac,
                prefix: new HarmonyMethod(Patch1ec57d74711f4edca0a02202a86ef4ac)
            );
        }
    }

    public class ConnorsCoolGenericType_1MockSetup<T>
        : global::MockMe.Mocks.ClassMemberMocks.Setup.MemberMockSetup
    {
        private global::MockMe.Mocks.ClassMemberMocks.MemberMock<T>? GetRandomVal_BagStore;

        public global::MockMe.Mocks.ClassMemberMocks.MemberMock<T> GetRandomVal() =>
            this.GetRandomVal_BagStore ??= new();

        private List<ArgBagWithMemberMock<T, global::System.String>>? TakeAT_DoubleBagStore;

        public global::MockMe.Mocks.ClassMemberMocks.MemberMock<T, global::System.String> TakeAT(
            Arg<T> input
        ) => SetupMethod(this.TakeAT_DoubleBagStore ??= new(), input);

        private global::MockMe.Mocks.ClassMemberMocks.MemberMock<T>? get_MyCoolProp_BagStore;
        public global::MockMe.Mocks.ClassMemberMocks.GetPropertyMock<T> MyCoolProp =>
            new(get_MyCoolProp_BagStore ??= new());

        public class ConnorsCoolGenericType_1MockCallTracker : MockCallTracker
        {
            private readonly ConnorsCoolGenericType_1MockSetup<T> setup;

            public ConnorsCoolGenericType_1MockCallTracker(
                ConnorsCoolGenericType_1MockSetup<T> setup
            )
            {
                this.setup = setup;
            }

            private int GetRandomVal_CallStore;

            public T GetRandomVal()
            {
                this.GetRandomVal_CallStore++;
                return MockCallTracker.CallMemberMock(this.setup.GetRandomVal_BagStore);
            }

            private List<T>? TakeAT_DoubleCallStore;

            public global::System.String TakeAT(T input) =>
                MockCallTracker.CallMemberMock(
                    this.setup.TakeAT_DoubleBagStore,
                    this.TakeAT_DoubleCallStore ??= new(),
                    input
                );

            private int get_MyCoolProp_CallStore;

            public T MyCoolProp
            {
                get
                {
                    this.get_MyCoolProp_CallStore++;
                    return MockCallTracker.CallMemberMock(this.setup.get_MyCoolProp_BagStore);
                }
            }

            public class ConnorsCoolGenericType_1MockAsserter : MockAsserter
            {
                private readonly ConnorsCoolGenericType_1MockCallTracker tracker;

                public ConnorsCoolGenericType_1MockAsserter(
                    ConnorsCoolGenericType_1MockCallTracker tracker
                )
                {
                    this.tracker = tracker;
                }

                public global::MockMe.Asserters.MemberAsserter GetRandomVal() =>
                    new(this.tracker.GetRandomVal_CallStore);

                public global::MockMe.Asserters.MemberAsserter TakeAT(Arg<T> input)
                {
                    return GetMemberAsserter(this.tracker.TakeAT_DoubleCallStore, input);
                }

                public global::MockMe.Asserters.GetPropertyAsserter<T> MyCoolProp =>
                    new(this.tracker.get_MyCoolProp_CallStore);
            }
        }
    }
}
