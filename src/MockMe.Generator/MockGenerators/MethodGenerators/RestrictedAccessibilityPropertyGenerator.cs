using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using MockMe.Generator.Extensions;

namespace MockMe.Generator.MockGenerators.MethodGenerators;

internal class RestrictedAccessibilityPropertyGenerator(IMethodSymbol methodSymbol)
    : PropertyGenerator(methodSymbol)
{
    public override StringBuilder AddMethodCallTrackerToStringBuilder(
        StringBuilder sb,
        Dictionary<string, PropertyMetadata> callTrackerMeta
    )
    {
        var methodName = this.methodSymbol.GetPropertyName();
        bool isGet = this.methodSymbol.MethodKind == MethodKind.PropertyGet;
        var propertyType =  this.GetPropertyType(isGet);
        var uniqueMethodName = methodName + propertyType;

        if (!callTrackerMeta.TryGetValue(uniqueMethodName, out var propMeta))
        {
            propMeta = new() { Name = methodName, ReturnType = this.returnType };
            callTrackerMeta.Add(uniqueMethodName, propMeta);
        }

        if (methodSymbol.MethodKind == MethodKind.PropertyGet)
        {
            propMeta.GetterLogic = PropertyMetadata.ThrowExceptionLogic;
        }
        else
        {
            propMeta.SetterLogic = PropertyMetadata.ThrowExceptionLogic;
        }

        return sb;
    }

    public override StringBuilder AddMethodSetupToStringBuilder(
        StringBuilder sb,
        Dictionary<string, SetupPropertyMetadata> setupMeta
    ) => sb;

    public override StringBuilder AddMethodToAsserterClass(
        StringBuilder sb,
        Dictionary<string, AssertPropertyMetadata> assertMeta
    ) => sb;
}
