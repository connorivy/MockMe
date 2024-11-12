using Microsoft.CodeAnalysis;

namespace MockMe.Generator.MockGenerators;

internal class MockGeneratorFactory
{
    public static MockGeneratorBase Create(ITypeSymbol typeSymbol)
    {
        if (typeSymbol.TypeKind == TypeKind.Interface)
        {
            return new InterfaceMockGenerator();
        }

        return new ConcreteMockGenerator();
    }
}
