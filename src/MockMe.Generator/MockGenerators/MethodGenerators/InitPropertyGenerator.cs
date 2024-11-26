using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using MockMe.Generator.Extensions;

namespace MockMe.Generator.MockGenerators.MethodGenerators;

internal class InitPropertyGenerator(IMethodSymbol methodSymbol)
    : MethodMockGeneratorBase(methodSymbol)
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
        propMeta.HasInit = true;

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
