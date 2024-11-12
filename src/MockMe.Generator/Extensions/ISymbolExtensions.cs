using Microsoft.CodeAnalysis;

namespace MockMe.Generator.Extensions;

internal static class ISymbolExtensions
{
    public static string ToFullTypeString(this ISymbol symbol) =>
        symbol.ToDisplayString(SymbolUtils.DisplayFormat);

    public static string ToFullReturnTypeString(this ISymbol symbol)
    {
        if (symbol is ITypeSymbol typeSymbol && typeSymbol.SpecialType == SpecialType.System_Void)
        {
            return symbol.ToDisplayString();
        }
        return symbol.ToFullTypeString();
    }
}
