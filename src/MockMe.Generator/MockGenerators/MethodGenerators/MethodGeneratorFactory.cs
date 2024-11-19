using Microsoft.CodeAnalysis;
using MockMe.Generator.MockGenerators.Concrete;

namespace MockMe.Generator.MockGenerators.MethodGenerators;

internal class MethodGeneratorFactory
{
    public static MethodMockGeneratorBase Create(IMethodSymbol method)
    {
        if (
            method.MethodKind is MethodKind.PropertyGet or MethodKind.PropertySet
            && method.AssociatedSymbol is IPropertySymbol propertySymbol
            && propertySymbol.IsIndexer
        )
        {
            return new IndexerGenerator(method);
        }

        return new ConcreteTypeMethodSetupGenerator(method);
    }
}
