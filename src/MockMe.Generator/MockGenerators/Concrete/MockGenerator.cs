using System;
using System.Text;
using Microsoft.CodeAnalysis;
using MockMe.Generator.Extensions;

namespace MockMe.Generator.MockGenerators.Concrete;

internal class MockGenerator
{
    private const string MockNamespace = "MockMe.Mocks.Generated";

    public static StringBuilder CreateMockForConcreteType(ITypeSymbol typeSymbol)
    {
        var thisNamespace = $"MockMe.Generated.{typeSymbol.ContainingNamespace}";
        StringBuilder sb = new();

        sb.AppendLine(
            $@"
#nullable enable
using System.Collections.Concurrent;
using HarmonyLib;
using MockMe.Mocks;
using static {thisNamespace}.{typeSymbol.Name}MockSetup;
using static {thisNamespace}.{typeSymbol.Name}MockSetup.{typeSymbol.Name}MockCallTracker;

namespace MockMe.Generated.{typeSymbol.ContainingNamespace};

    public class {typeSymbol.Name}Mock : Mock<global::{typeSymbol}>
    {{
        private static readonly ConcurrentDictionary<global::{typeSymbol}, {typeSymbol.Name}Mock> mockStore = new();
        public {typeSymbol.Name}Mock()
        {{
            this.Setup = new {typeSymbol.Name}MockSetup();
            this.CallTracker = new {typeSymbol.Name}MockCallTracker(this.Setup);
            this.Assert = new {typeSymbol.Name}MockAsserter(this.CallTracker);
            mockStore.TryAdd(this.Value, this);
        }}

        public {typeSymbol.Name}MockSetup Setup {{ get; }}
        public {typeSymbol.Name}MockAsserter Assert {{ get; }}
        private {typeSymbol.Name}MockCallTracker CallTracker {{ get; set; }}
"
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

            string returnType = methodSymbol.ReturnType.ToDisplayString();
            string paramsWithTypesAndMods =
                methodSymbol.GetParametersWithOriginalTypesAndModifiers();
            string paramTypeString = methodSymbol.GetParameterTypesWithoutModifiers();
            string paramString = methodSymbol.GetParametersWithoutTypesAndModifiers();

            sb.AppendLine(
                $@"
        [HarmonyPatch(typeof(global::{typeSymbol}), nameof(global::{typeSymbol}.{method.Name}))]
        internal sealed class Patch{Guid.NewGuid():N}
        {{
            private static bool Prefix(global::{typeSymbol} __instance, ref {returnType} __result, {paramsWithTypesAndMods})
            {{
                if (mockStore.TryGetValue(__instance, out var mock))
                {{
                    __result = mock.CallTracker.{method.Name}({paramString});
                    return false;
                }}
                return true;
            }}
        }}"
            );
        }

        sb.AppendLine(
            @$"
    }}"
        );

        SetupGenerator.CreateSetupForConcreteType(typeSymbol, sb);

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
