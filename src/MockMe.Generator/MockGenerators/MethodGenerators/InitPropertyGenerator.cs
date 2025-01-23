using System.Collections.Generic;
using System.Linq;
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
        var uniqueMethodName = methodName
            + this.methodSymbol.Parameters.First().Type.ToFullReturnTypeString();

        if (!callTrackerMeta.TryGetValue(uniqueMethodName, out var propMeta))
        {
            propMeta = new() { Name = methodName, ReturnType = this.returnType };
            callTrackerMeta.Add(uniqueMethodName, propMeta);
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
