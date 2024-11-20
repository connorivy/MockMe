using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using MockMe.Generator.Extensions;

namespace MockMe.Generator.MockGenerators.MethodGenerators;

internal class IndexerGenerator(IMethodSymbol methodSymbol) : PropertyGenerator(methodSymbol)
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

        string? indexerType = this.methodSymbol.Parameters.First().Type.ToFullTypeString();

        bool isGet = this.methodSymbol.MethodKind == MethodKind.PropertyGet;

        var propertyType = isGet
            ? this.returnType
            : this.methodSymbol.Parameters[1].Type.ToFullReturnTypeString();

        var uniqueMethodName = methodName + propertyType;

        if (!callTrackerMeta.TryGetValue(uniqueMethodName, out var propMeta))
        {
            propMeta = new()
            {
                Name = this.methodSymbol.GetPropertyName(),
                ReturnType = propertyType,
                IndexerType = indexerType,
            };
            callTrackerMeta.Add(uniqueMethodName, propMeta);
        }

        if (this.methodSymbol.MethodKind == MethodKind.PropertyGet)
        {
            propMeta.GetterLogic =
                @$"
            return {this.voidPrefix}MockCallTracker.Call{this.voidPrefix}MemberMock(this.setup.{this.GetBagStoreName()}, this.{this.GetCallStoreName()} ??= new(), index);";
            propMeta.GetterField = $"private List<{indexerType}>? {this.GetCallStoreName()};";
        }
        else if (this.methodSymbol.MethodKind == MethodKind.PropertySet)
        {
            propMeta.SetterLogic =
                @$"
        {this.voidPrefix}MockCallTracker.Call{this.voidPrefix}MemberMock(this.setup.{this.GetBagStoreName()}, this.{this.GetCallStoreName()} ??= new(), index, value);";

            propMeta.SetterField =
                $"private List<{this.methodSymbol.GetMethodArgumentsAsCollection()}>? {this.GetCallStoreName()};";
        }

        return sb;
    }

    //public override StringBuilder AddMethodSetupToStringBuilder(
    //    StringBuilder sb,
    //    Dictionary<string, SetupPropertyMetadata> setupMeta
    //)
    //{
    //    return sb.AppendLine(
    //        $@"
    //    private {this.GetBagStoreType()}? {this.GetBagStoreName()};
    //    public {this.memberMockType} this[{this.methodSymbol.GetParametersWithArgTypesAndModifiers()}] =>
    //        {this.GetSetupMethod()}"
    //    );
    //}

    //public override StringBuilder AddMethodToAsserterClass(
    //    StringBuilder sb,
    //    Dictionary<string, AssertPropertyMetadata> assertMeta
    //)
    //{
    //    var parametersDefinition = this.methodSymbol.GetParametersWithArgTypesAndModifiers();
    //    var parameters = this.methodSymbol.GetParametersWithoutTypesAndModifiers();

    //    if (this.methodSymbol.Parameters.Length == 0)
    //    {
    //        sb.AppendLine(
    //            $@"
    //            public global::MockMe.Asserters.MemberAsserter {this.MethodName()}() =>
    //                new(this.tracker.{this.GetCallStoreName()});"
    //        );
    //    }
    //    else
    //    {
    //        sb.AppendLine(
    //            $@"
    //            public global::MockMe.Asserters.MemberAsserter {this.MethodName()}({parametersDefinition})
    //            {{
    //                return GetMemberAsserter(this.tracker.{this.GetCallStoreName()}, {parameters});
    //            }}"
    //        );
    //    }

    //    return sb;
    //}

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

    protected override string GetPropertyType(bool isGet) =>
        isGet ? this.returnType : this.methodSymbol.Parameters[1].Type.ToFullReturnTypeString();

    protected override SetupPropertyMetadata CreatePropertyMetadata(
        string propertyType,
        string methodName
    )
    {
        return new IndexerSetupPropertyMetadata()
        {
            Name = propertyType,
            PropertyType = propertyType,
            IndexerType = this.GetIndexerType(),
        };
    }

    public override string? GetIndexerType() =>
        this.methodSymbol.Parameters.First().Type.ToFullTypeString();
}
