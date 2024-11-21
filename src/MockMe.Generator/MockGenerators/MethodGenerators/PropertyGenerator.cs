using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.CodeAnalysis;
using MockMe.Generator.Extensions;

namespace MockMe.Generator.MockGenerators.MethodGenerators;

internal class PropertyGenerator(IMethodSymbol methodSymbol) : MethodMockGeneratorBase(methodSymbol)
{
    public override StringBuilder AddPatchMethod(
        StringBuilder sb,
        StringBuilder assemblyAttributesSource,
        StringBuilder staticConstructor,
        ITypeSymbol typeSymbol,
        string typeSymbolName
    )
    {
        var thisNamespace = $"MockMe.Generated.{typeSymbol.ContainingNamespace}";
        string paramsWithTypesAndMods =
            this.methodSymbol.GetParametersWithOriginalTypesAndModifiers();
        string paramTypeString = this.methodSymbol.GetParameterTypesWithoutModifiers();
        string paramString = this.methodSymbol.GetParametersWithoutTypesAndModifiers();

        string callEnd = this.methodSymbol.MethodKind switch
        {
            MethodKind.PropertyGet => "",
            MethodKind.PropertySet => $" = {paramString}",
            _ => $"({paramString})",
        };

        string patchName = $"Patch{Guid.NewGuid():N}";
        sb.AppendLine(
            $@"
        internal sealed class {patchName}
        {{
            private static bool Prefix({typeSymbol.ToFullTypeString()} __instance{(this.isVoidReturnType ? string.Empty : $", ref {this.returnType} __result")}{paramsWithTypesAndMods.AddPrefixIfNotEmpty(", ")})
            {{
                if (global::MockMe.MockStore<{typeSymbol.ToFullTypeString()}>.TryGetValue<{typeSymbolName}Mock>(__instance, out var mock))
                {{
                    {(this.isVoidReturnType ? string.Empty : "__result = ")}mock.CallTracker.{this.methodSymbol.GetPropertyName()}{callEnd};
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

    public override StringBuilder AddMethodSetupToStringBuilder(
        StringBuilder sb,
        Dictionary<string, SetupPropertyMetadata> setupMeta
    )
    {
        bool isGet = this.methodSymbol.MethodKind == MethodKind.PropertyGet;
        string propertyType = this.GetPropertyType(isGet);

        var methodName = this.methodSymbol.GetPropertyName();
        var uniqueMethodName = methodName + propertyType;

        if (!setupMeta.TryGetValue(uniqueMethodName, out var propMeta))
        {
            propMeta = this.CreatePropertyMetadata(propertyType, methodName);
            setupMeta.Add(uniqueMethodName, propMeta);
        }

        if (isGet)
        {
            propMeta.GetterFieldName = this.GetBagStoreName();
        }
        else
        {
            propMeta.SetterFieldName = this.GetBagStoreName();
        }

        return sb;
    }

    protected virtual string GetPropertyType(bool isGet) =>
        isGet
            ? this.returnType
            : this.methodSymbol.Parameters.First().Type.ToFullReturnTypeString();

    protected virtual SetupPropertyMetadata CreatePropertyMetadata(
        string propertyType,
        string methodName
    )
    {
        return new SetupPropertyMetadata() { Name = methodName, PropertyType = propertyType };
    }

    public override StringBuilder AddMethodCallTrackerToStringBuilder(
        StringBuilder sb,
        Dictionary<string, PropertyMetadata> callTrackerMeta
    )
    {
        string paramsWithTypesAndMods =
            this.methodSymbol.GetParametersWithOriginalTypesAndModifiers();
        string paramString = this.methodSymbol.GetParametersWithoutTypesAndModifiers();

        if (this.methodSymbol.MethodKind == MethodKind.PropertyGet)
        {
            var methodName = this.methodSymbol.GetPropertyName();

            string? indexerType = null;
            if (
                this.methodSymbol.AssociatedSymbol is IPropertySymbol propertySymbol
                && propertySymbol.IsIndexer
            )
            {
                indexerType = propertySymbol.Type.ToFullTypeString();
            }

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

            if (string.IsNullOrEmpty(indexerType))
            {
                propMeta.GetterLogic =
                    @$"
            this.{this.GetCallStoreName()}++;
            return {this.voidPrefix}MockCallTracker.Call{this.voidPrefix}MemberMock(this.setup.{this.GetBagStoreName()});";
                propMeta.GetterField = $"private int {this.GetCallStoreName()};";
            }
            else
            {
                propMeta.GetterLogic =
                    @$"
            return {this.voidPrefix}MockCallTracker.Call{this.voidPrefix}MemberMock(this.setup.{this.GetBagStoreName()}, this.{this.GetCallStoreName()} ??= new(), index);";
                propMeta.GetterField = $"private List<{indexerType}>? {this.GetCallStoreName()};";
            }
        }
        else if (this.methodSymbol.MethodKind == MethodKind.PropertySet)
        {
            var methodName = this.methodSymbol.GetPropertyName();

            string? indexerType = null;
            if (
                this.methodSymbol.AssociatedSymbol is IPropertySymbol propertySymbol
                && propertySymbol.IsIndexer
            )
            {
                indexerType = propertySymbol.Type.ToFullTypeString();
            }

            if (!callTrackerMeta.TryGetValue(methodName, out var propMeta))
            {
                propMeta = new()
                {
                    Name = methodName,
                    ReturnType = this.methodSymbol.Parameters.First().Type.ToFullReturnTypeString(),
                    IndexerType = indexerType,
                };
                callTrackerMeta.Add(methodName, propMeta);
            }

            propMeta.SetterLogic =
                @$"
        {this.voidPrefix}MockCallTracker.Call{this.voidPrefix}MemberMock(this.setup.{this.GetBagStoreName()}, this.{this.GetCallStoreName()} ??= new(){(string.IsNullOrEmpty(indexerType) ? "" : ", index")}, value);";

            propMeta.SetterField =
                $"private List<{this.methodSymbol.GetMethodArgumentsAsCollection()}>? {this.GetCallStoreName()};";
        }

        return sb;
    }

    public override StringBuilder AddMethodToAsserterClass(
        StringBuilder sb,
        Dictionary<string, AssertPropertyMetadata> assertMeta
    )
    {
        bool isGet = this.methodSymbol.MethodKind == MethodKind.PropertyGet;

        var propertyType = this.GetPropertyType(isGet);

        var methodName = this.methodSymbol.GetPropertyName();
        var uniqueMethodName = methodName + propertyType;

        if (!assertMeta.TryGetValue(uniqueMethodName, out var propMeta))
        {
            propMeta = new()
            {
                Name = methodName,
                PropertyType = propertyType,
                IndexerType = this.GetIndexerType(),
            };
            assertMeta.Add(uniqueMethodName, propMeta);
        }

        if (isGet)
        {
            propMeta.GetterCallStoreName = $"this.tracker.{this.GetCallStoreName()}";
        }
        else
        {
            propMeta.SetterCallStoreName = $"this.tracker.{this.GetCallStoreName()}";
        }

        return sb;
    }

    public virtual string? GetIndexerType() => null;
}
