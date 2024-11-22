using Microsoft.CodeAnalysis;

namespace MockMe.Generator.MockGenerators.PatchMethodGenerators;

internal static class PatchMethodGeneratorFactory
{
    public static IPatchMethodGenerator? Create(INamedTypeSymbol typeSymbol, IMethodSymbol method)
    {
        if (typeSymbol.TypeKind == TypeKind.Interface)
        {
            return null;
        }

        if (typeSymbol.IsGenericType)
        {
            return new GenericClassMethodPatchMethodGenerator(typeSymbol, method);
        }

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
                    return new IndexerPatchMethodGenerator(typeSymbol, method);
                }
            }

            return new PropertyPatchMethodGenerator(typeSymbol, method);
        }

        return new ClassMethodPatchMethodGenerator(typeSymbol, method);
    }
}
