using Microsoft.CodeAnalysis;
using MockMe.Generator.MockGenerators.Concrete;

namespace MockMe.Generator.MockGenerators.MethodGenerators;

internal class MethodGeneratorFactory
{
    public static MethodMockGeneratorBase? Create(IMethodSymbol method)
    {
        if (method.MethodKind is MethodKind.PropertyGet or MethodKind.PropertySet)
        {
            if (method.AssociatedSymbol is IPropertySymbol propertySymbol)
            {
                if (
                    method.MethodKind is MethodKind.PropertySet
                    && (propertySymbol.SetMethod?.IsInitOnly ?? false)
                )
                {
                    // we skip 'init' properties because they can't be set at runtime
                    return null;
                }
                if (propertySymbol.IsIndexer)
                {
                    return new IndexerGenerator(method);
                }
            }

            return new PropertyGenerator(method);
        }

        return new ConcreteTypeMethodSetupGenerator(method);
    }
}
