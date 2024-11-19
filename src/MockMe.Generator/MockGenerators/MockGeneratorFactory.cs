using Microsoft.CodeAnalysis;

namespace MockMe.Generator.MockGenerators;

internal class MockGeneratorFactory
{
    public static MockGeneratorBase Create(ITypeSymbol typeSymbol, string typeName)
    {
        if (typeSymbol.TypeKind == TypeKind.Interface)
        {
            return new InterfaceMockGenerator(typeName);
        }

        return new ConcreteMockGenerator(typeName);
    }
}
