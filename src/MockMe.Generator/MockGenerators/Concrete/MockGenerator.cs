using System;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using MockMe.Generator.Extensions;

namespace MockMe.Generator.MockGenerators.Concrete;

internal class MockGenerator
{
    private const string MockNamespace = "MockMe.Mocks.Generated";
    private const string voidString = "void";

    public static StringBuilder CreateMockForConcreteType(
        ITypeSymbol typeSymbol,
        StringBuilder assemblyAttributesSource
    )
    {
        var thisNamespace = $"MockMe.Generated.{typeSymbol.ContainingNamespace}";
        StringBuilder sb = new();

        sb.AppendLine(
            $@"
#nullable enable
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using MockMe;
using MockMe.Mocks;
using static {thisNamespace}.{typeSymbol.Name}MockSetup;
using static {thisNamespace}.{typeSymbol.Name}MockSetup.{typeSymbol.Name}MockCallTracker;

namespace {thisNamespace}
{{
    public class {typeSymbol.Name}Mock : Mock<global::{typeSymbol}>
    {{
        public {typeSymbol.Name}Mock()
        {{
            this.Setup = new {typeSymbol.Name}MockSetup();
            this.CallTracker = new {typeSymbol.Name}MockCallTracker(this.Setup);
            this.Assert = new {typeSymbol.Name}MockAsserter(this.CallTracker);
            global::MockMe.MockStore<global::{typeSymbol}>.Store.TryAdd(this.MockedObject, this);
        }}

        public {typeSymbol.Name}MockSetup Setup {{ get; }}
        public {typeSymbol.Name}MockAsserter Assert {{ get; }}
        private {typeSymbol.Name}MockCallTracker CallTracker {{ get; set; }}
"
        );

        StringBuilder setupBuilder = new();

        setupBuilder.AppendLine(
            $@"
    public class {typeSymbol.Name}MockSetup : MockSetup
    {{"
        );

        StringBuilder callTrackerBuilder = new();
        StringBuilder asserterBuilder = new();
        callTrackerBuilder.AppendLine(
            $@"
        public class {typeSymbol.Name}MockCallTracker : MockCallTracker
        {{
            private readonly {typeSymbol.Name}MockSetup setup;
            public {typeSymbol.Name}MockCallTracker({typeSymbol.Name}MockSetup setup)
            {{
                this.setup = setup;
            }}"
        );

        asserterBuilder.AppendLine(
            $@"
            public class {typeSymbol.Name}MockAsserter : MockAsserter
            {{
                private readonly {typeSymbol.Name}MockCallTracker tracker;
                public {typeSymbol.Name}MockAsserter({typeSymbol.Name}MockCallTracker tracker)
                {{
                    this.tracker = tracker;
                }}"
        );

        foreach (var method in typeSymbol.GetMembers())
        {
            if (method is not IMethodSymbol methodSymbol)
            {
                continue;
            }

            var methodName = methodSymbol.Name;

            if (methodName == ".ctor")
            {
                continue;
            }

            //if (methodSymbol.MethodKind == MethodKind.PropertyGet)
            //{
            //    continue;
            //    methodName = methodName.Substring(4) + "_get";
            //}

            //if (methodSymbol.MethodKind == MethodKind.PropertySet)
            //{
            //    continue;
            //    methodName = methodName.Substring(4) + "_set";
            //}

            ConcreteTypeMethodSetupGenerator methodGenerator = new(methodSymbol);

            methodGenerator.AddPatchMethod(sb, assemblyAttributesSource, typeSymbol);
            methodGenerator.AddMethodSetupToStringBuilder(setupBuilder);
            methodGenerator.AddMethodCallTrackerToStringBuilder(callTrackerBuilder);
            methodGenerator.AddMethodToAsserterClass(asserterBuilder);

            //string returnType = methodSymbol.ReturnType.ToDisplayString();
            //string paramsWithTypesAndMods =
            //    methodSymbol.GetParametersWithOriginalTypesAndModifiers();
            //string paramTypeString = methodSymbol.GetParameterTypesWithoutModifiers();
            //string paramString = methodSymbol.GetParametersWithoutTypesAndModifiers();

            //            if (methodSymbol.TypeParameters.Length == 0)
            //            {
            //                sb.AppendLine(
            //                    $@"
            //        [HarmonyPatch(typeof(global::{typeSymbol}), nameof(global::{typeSymbol}.{methodName}))]
            //        internal sealed class Patch{Guid.NewGuid():N}
            //        {{
            //            private static bool Prefix(global::{typeSymbol} __instance{(returnType == "void" ? string.Empty : $", ref {returnType} __result")}{paramsWithTypesAndMods.AddPrefixIfNotEmpty(", ")})
            //            {{
            //                if (global::MockMe.MockStore<global::{typeSymbol}>.TryGetValue<{typeSymbol.Name}Mock>(__instance, out var mock))
            //                {{
            //                    {(returnType == "void" ? string.Empty : "__result = ")}mock.CallTracker.{methodName}({paramString});
            //                    return false;
            //                }}
            //                return true;
            //            }}
            //        }}"
            //                );
            //            }
            //            else
            //            {
            //                sb.AppendLine(
            //                    $@"
            //        private {returnType} {methodName}{methodSymbol.GetGenericParameterStringInBrackets()}({paramsWithTypesAndMods})
            //        {{
            //            if (global::MockMe.MockStore<global::{typeSymbol}>.GetStore().TryGetValue(default, out var mock))
            //            {{
            //                var callTracker = mock.GetType()
            //                    .GetProperty(
            //                        ""CallTracker"",
            //                        System.Reflection.BindingFlags.NonPublic
            //                            | System.Reflection.BindingFlags.Instance
            //                    )
            //                    .GetValue(mock);

            //                return ({returnType})
            //                    callTracker
            //                        .GetType()
            //                        .GetMethod(
            //                            ""{methodName}"",
            //                            System.Reflection.BindingFlags.Public
            //                                | System.Reflection.BindingFlags.Instance
            //                        )
            //                        .MakeGenericMethod({string.Join(", ", methodSymbol.TypeParameters.Select(p => p.Name.AddOnIfNotEmpty("typeof(", ")")))})
            //                        .Invoke(callTracker, new object[] {{ {paramString} }});
            //            }}
            //            return default;
            //        }}
            //"
            //                );

            //                assemblyAttributesSource.AppendLine(
            //                    $@"
            //[assembly: global::MockMe.Abstractions.GenericMethodDefinition(
            //    ""{typeSymbol.ContainingNamespace}"",
            //    ""{typeSymbol}"",
            //    ""{methodSymbol.Name}"",
            //    ""{thisNamespace}"",
            //    ""{thisNamespace}.{typeSymbol.Name}Mock"",
            //    ""{methodName}""
            //)]
            //"
            //                );

            //public GenericMethodDefinitionAttribute(
            //    string typeToReplaceAssemblyName,
            //    string typeToReplaceTypeFullName,
            //    string typeToReplaceMethodName,
            //    string sourceTypeAssemblyName,
            //    string sourceTypeFullName,
            //    string sourceTypeMethodName
            //)
            //}
        }

        // close mock class
        sb.AppendLine(
            @$"
    }}"
        );

        asserterBuilder.AppendLine(
            $@"
            }}"
        );

        callTrackerBuilder.Append(asserterBuilder);

        callTrackerBuilder.AppendLine(
            @$"
        }}"
        );

        //CallTrackerGenerator.CreateCallTrackerForConcreteType(typeSymbol, sb);

        setupBuilder.Append(callTrackerBuilder);

        setupBuilder.AppendLine(
            @$"
    }}"
        );

        sb.Append(setupBuilder);

        //SetupGenerator.CreateSetupForConcreteType(typeSymbol, sb);

        // close namespace
        sb.AppendLine(
            @$"
}}"
        );

        return sb;
    }
}

/*
 public class _01_CalculatorMock : Mock<_01_Calculator>
{
    public _01_CalculatorMock()
    {
        this.Setup = new _01_CalculatorMockSetup();
        this.CallTracker = new _01_CalculatorMockCallTracker(this.Setup);
        this.Assert = new _01_CalculatorMockAsserter(this.CallTracker);
    }

    public _01_CalculatorMockSetup Setup { get; }
    public _01_CalculatorMockAsserter Assert { get; }
    private _01_CalculatorMockCallTracker CallTracker { get; set; }

    [HarmonyPatch(typeof(_01_Calculator), nameof(_01_Calculator.Add))]
    internal sealed class Patch00
    {
        private static bool Prefix(_01_Calculator __instance, ref int __result, int x, int y)
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
 */
