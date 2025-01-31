using Microsoft.CodeAnalysis;

namespace MockMe.Generator.Extensions;

public static class INamedTypeSymbolExtensions
{
    public static string GetGenericMethodDefinitionAttribute(
        this INamedTypeSymbol namedTypeSymbol,
        string methodSymbolName,
        string generatedTypeNamespace
    )
    {
        string genericSuffix = namedTypeSymbol.IsGenericType
            ? namedTypeSymbol.MetadataName[^2..] // includes the `3 in GenericType`3
            : string.Empty;

        return $@"
[assembly: global::MockMe.Abstractions.GenericMethodDefinition(
    ""{namedTypeSymbol.ContainingAssembly.Name}"",
    ""{namedTypeSymbol.ContainingNamespace}.{namedTypeSymbol.MetadataName}"",
    ""{methodSymbolName}"",
    ""{generatedTypeNamespace}"",
    ""{generatedTypeNamespace}.{namedTypeSymbol.Name}Mock{genericSuffix}"",
    ""{methodSymbolName}""
)]
";
    }
}
