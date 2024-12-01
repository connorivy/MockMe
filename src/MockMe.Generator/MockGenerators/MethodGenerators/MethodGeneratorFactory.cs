using Microsoft.CodeAnalysis;
using MockMe.Generator.MockGenerators.TypeGenerators;

namespace MockMe.Generator.MockGenerators.MethodGenerators;

internal class MethodGeneratorFactory
{
    public static MethodMockGeneratorBase? Create(
        IMethodSymbol method,
        MockGeneratorBase mockGenerator
    )
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
                    return new InitPropertyGenerator(method);
                }
                if (propertySymbol.IsIndexer)
                {
                    return new IndexerGenerator(method);
                }
            }

            return new PropertyGenerator(method);
        }

        return new ConcreteTypeMethodSetupGenerator(method, mockGenerator);
    }
}
