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

        if (!callTrackerMeta.TryGetValue(methodName, out var propMeta))
        {
            propMeta = new() { Name = methodName, ReturnType = this.returnType };
            callTrackerMeta.Add(methodName, propMeta);
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
