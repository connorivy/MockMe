using Microsoft.CodeAnalysis;

namespace MockMe.Generator;

internal static class SymbolUtils
{
    public static SymbolDisplayFormat DisplayFormat { get; } =
        new SymbolDisplayFormat(
            typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces,
            globalNamespaceStyle: SymbolDisplayGlobalNamespaceStyle.Included
        );
}
