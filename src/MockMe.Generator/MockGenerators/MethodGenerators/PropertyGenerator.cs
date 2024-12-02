using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using MockMe.Generator.Extensions;

namespace MockMe.Generator.MockGenerators.MethodGenerators;

internal class PropertyGenerator(IMethodSymbol methodSymbol) : MethodMockGeneratorBase(methodSymbol)
{
    public override StringBuilder AddMethodSetupToStringBuilder(
        StringBuilder sb,
        Dictionary<string, SetupPropertyMetadata> setupMeta
    )
    {
        if (this.methodSymbol.DeclaredAccessibility != Accessibility.Public)
        {
            return sb;
        }

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

        var methodName = this.methodSymbol.GetPropertyName();

        bool isGet = this.methodSymbol.MethodKind == MethodKind.PropertyGet;

        var propertyType = isGet
            ? this.returnType
            : this.methodSymbol.Parameters[0].Type.ToFullReturnTypeString();

        if (!callTrackerMeta.TryGetValue(methodName, out var propMeta))
        {
            propMeta = new()
            {
                Name = methodName,
                ReturnType = propertyType,
                //IndexerType = indexerType,
            };
            callTrackerMeta.Add(methodName, propMeta);
        }

        if (isGet)
        {
            if (this.methodSymbol.DeclaredAccessibility != Accessibility.Public)
            {
                propMeta.GetterLogic = $"return default({propertyType});";
            }
            else
            {
                propMeta.GetterLogic =
                    @$"
                this.{this.GetCallStoreName()}++;
                return MockCallTracker.CallMemberMock<{propertyType}>(this.setup.{this.GetBagStoreName()});";
                propMeta.GetterField = $"private int {this.GetCallStoreName()};";
            }
        }
        else if (this.methodSymbol.MethodKind == MethodKind.PropertySet)
        {
            if (this.methodSymbol.DeclaredAccessibility != Accessibility.Public)
            {
                propMeta.SetterLogic = "_ = value;";
            }
            else
            {
                propMeta.SetterLogic =
                    @$"
        MockCallTracker.CallVoidMemberMock<PropertySetterArgs<{propertyType}>>(this.setup.{this.GetBagStoreName()}, this.{this.GetCallStoreName()} ??= new(), new PropertySetterArgs<{propertyType}>(value));";

                propMeta.SetterField =
                    $"private List<PropertySetterArgs<{propertyType}>>? {this.GetCallStoreName()};";
            }
        }

        return sb;
    }

    public override StringBuilder AddMethodToAsserterClass(
        StringBuilder sb,
        Dictionary<string, AssertPropertyMetadata> assertMeta
    )
    {
        if (this.methodSymbol.DeclaredAccessibility != Accessibility.Public)
        {
            return sb;
        }

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
