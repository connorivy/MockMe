using Microsoft.CodeAnalysis;

namespace MockMe.Generator.MockGenerators.TypeGenerators;

internal class MockGeneratorFactory
{
    public static MockGeneratorBase Create(INamedTypeSymbol typeSymbol, string typeName)
    {
        if (typeSymbol.TypeKind == TypeKind.Interface)
        {
            return new InterfaceMockGenerator(typeSymbol, typeName);
        }

        if (typeSymbol.IsGenericType)
        {
            return new GenericConcreteTypeMockGenerator(typeSymbol.OriginalDefinition, typeName);
        }

        return new ConcreteMockGenerator(typeSymbol, typeName);
    }

    public static INamedTypeSymbol GetTypeSymbolToMock(INamedTypeSymbol typeSymbol)
    {
        if (typeSymbol.IsGenericType)
        {
            return typeSymbol.OriginalDefinition;
        }
        return typeSymbol;
    }
}
