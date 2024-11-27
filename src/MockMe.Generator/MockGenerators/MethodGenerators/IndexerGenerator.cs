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

        var uniqueMethodName = methodName + indexerType;

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
            if (this.methodSymbol.DeclaredAccessibility != Accessibility.Public)
            {
                propMeta.GetterLogic = $"return default({propertyType});";
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
            if (this.methodSymbol.DeclaredAccessibility != Accessibility.Public)
            {
                propMeta.SetterLogic = $"_ = value;";
            }
            else
            {
                propMeta.SetterLogic =
                    @$"
        {this.voidPrefix}MockCallTracker.Call{this.voidPrefix}MemberMock(this.setup.{this.GetBagStoreName()}, this.{this.GetCallStoreName()} ??= new(), index, value);";

                propMeta.SetterField =
                    $"private List<{this.methodSymbol.GetMethodArgumentsAsCollection()}>? {this.GetCallStoreName()};";
            }
        }

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

    public override string GetIndexerType() =>
        this.methodSymbol.Parameters.First().Type.ToFullTypeString();
}
