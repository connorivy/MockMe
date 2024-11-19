using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using MockMe.Generator.Extensions;

namespace MockMe.Generator.MockGenerators.MethodGenerators;

internal class IndexerGenerator(IMethodSymbol methodSymbol) : MethodMockGeneratorBase(methodSymbol)
{
    public override StringBuilder AddMethodCallTrackerToStringBuilder(
        StringBuilder sb,
        Dictionary<string, PropertyMetadata> callTrackerMeta
    )
    {
        string paramsWithTypesAndMods =
            this.methodSymbol.GetParametersWithOriginalTypesAndModifiers();
        string paramString = this.methodSymbol.GetParametersWithoutTypesAndModifiers();

        var methodName = this.methodSymbol.GetPropertyName();

        string? indexerType = null;
        if (
            this.methodSymbol.AssociatedSymbol is IPropertySymbol propertySymbol
            && propertySymbol.IsIndexer
        )
        {
            indexerType = propertySymbol.Type.ToFullTypeString();
        }

        if (this.methodSymbol.MethodKind == MethodKind.PropertyGet)
        {
            if (!callTrackerMeta.TryGetValue(methodName, out var propMeta))
            {
                propMeta = new()
                {
                    Name = methodName,
                    ReturnType = this.returnType,
                    IndexerType = indexerType,
                };
                callTrackerMeta.Add(methodName, propMeta);
            }

            propMeta.GetterLogic =
                @$"
            return {this.voidPrefix}MockCallTracker.Call{this.voidPrefix}MemberMock(this.setup.{this.GetBagStoreName()}, this.{this.GetCallStoreName()} ??= new(), index);";
            propMeta.GetterField = $"private List<{indexerType}>? {this.GetCallStoreName()};";
        }
        else if (this.methodSymbol.MethodKind == MethodKind.PropertySet)
        {
            if (!callTrackerMeta.TryGetValue(methodName, out var propMeta))
            {
                propMeta = new()
                {
                    Name = methodName,
                    ReturnType = this.returnType,
                    IndexerType = indexerType,
                };
                callTrackerMeta.Add(methodName, propMeta);
            }

            propMeta.SetterLogic =
                @$"
        {this.voidPrefix}MockCallTracker.Call{this.voidPrefix}MemberMock(this.setup.{this.GetBagStoreName()}, this.{this.GetCallStoreName()} ??= new(), index, value);";

            propMeta.SetterField =
                $"private List<{this.methodSymbol.GetMethodArgumentsAsCollection()}>? {this.GetCallStoreName()};";
        }

        return sb;
    }

    public override StringBuilder AddMethodSetupToStringBuilder(StringBuilder sb)
    {
        return sb.AppendLine(
            $@"
        private {this.GetBagStoreType()}? {this.GetBagStoreName()};
        public {this.memberMockType} {this.MethodName()}{this.methodSymbol.GetGenericParameterStringInBrackets()}({this.methodSymbol.GetParametersWithArgTypesAndModifiers()}) =>
            {this.GetSetupMethod()}"
        );
    }

    public override StringBuilder AddMethodToAsserterClass(StringBuilder sb)
    {
        var parametersDefinition = this.methodSymbol.GetParametersWithArgTypesAndModifiers();
        var parameters = this.methodSymbol.GetParametersWithoutTypesAndModifiers();

        if (this.methodSymbol.Parameters.Length == 0)
        {
            sb.AppendLine(
                $@"
                public MemberAsserter 
            {this.MethodName()}() =>
                    new(this.tracker.{this.GetCallStoreName()});"
            );
        }
        else
        {
            sb.AppendLine(
                $@"
                public MemberAsserter {this.MethodName()}({parametersDefinition})
                {{
                    return GetMemberAsserter(this.tracker.{this.GetCallStoreName()}, {parameters});
                }}"
            );
        }

        return sb;
    }

    public override StringBuilder AddPatchMethod(
        StringBuilder sb,
        StringBuilder assemblyAttributesSource,
        StringBuilder staticConstructor,
        ITypeSymbol typeSymbol,
        string typeName
    )
    {
        var thisNamespace = $"MockMe.Generated.{typeSymbol.ContainingNamespace}";
        string paramsWithTypesAndMods =
            this.methodSymbol.GetParametersWithOriginalTypesAndModifiers();
        string paramTypeString = this.methodSymbol.GetParameterTypesWithoutModifiers();

        const string indexPrefixToTrim = "index, ";
        string paramString = this.methodSymbol.GetParametersWithoutTypesAndModifiers();

        string callEnd = this.methodSymbol.MethodKind switch
        {
            MethodKind.PropertyGet => "",
            MethodKind.PropertySet => $" = {paramString[indexPrefixToTrim.Length..]}",
            _ => $"({paramString})",
        };

        string patchName = $"Patch{Guid.NewGuid():N}";
        sb.AppendLine(
            $@"
    internal sealed class {patchName}
    {{
        private static bool Prefix({typeSymbol.ToFullTypeString()} __instance{(this.isVoidReturnType ? string.Empty : $", ref {this.returnType} __result")}{paramsWithTypesAndMods.AddPrefixIfNotEmpty(", ")})
        {{
            if (global::MockMe.MockStore<{typeSymbol.ToFullTypeString()}>.TryGetValue<{typeName}Mock>(__instance, out var mock))
            {{
                {(this.isVoidReturnType ? string.Empty : "__result = ")}mock.CallTracker[index]{callEnd};
                return false;
            }}
            return true;
        }}
    }}"
        );

        staticConstructor.AppendLine(
            $@"
        var original{patchName} = typeof({typeSymbol.ToFullTypeString()}).GetMethod(""{this.methodSymbol.Name}"", new Type[] {{ {string.Join(", ", this.methodSymbol.Parameters.Select(p => "typeof(" + p.Type.ToFullTypeString() + ")"))} }} );
        var {patchName} = typeof({patchName}).GetMethod(""Prefix"", global::System.Reflection.BindingFlags.Static | global::System.Reflection.BindingFlags.NonPublic);

        harmony.Patch(original{patchName}, prefix: new HarmonyMethod({patchName}));"
        );

        return sb;
    }
}
