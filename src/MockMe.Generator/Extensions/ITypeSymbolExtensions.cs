using Microsoft.CodeAnalysis;

namespace MockMe.Generator.Extensions;

internal static class ITypeSymbolExtensions
{
    public static bool IsTask(this ITypeSymbol typeSymbol)
    {
        if (typeSymbol.ContainingNamespace.ToDisplayString() != "System.Threading.Tasks")
        {
            return false;
        }

        return typeSymbol.Name is "Task" or "Task`1";
    }

    public static bool IsNonGenericTask(this ITypeSymbol typeSymbol)
    {
        if (typeSymbol.ToDisplayString() != "System.Threading.Tasks.Task")
        {
            return false;
        }

        return true;
    }

    public static bool IsGenericTask(this ITypeSymbol typeSymbol)
    {
        if (
            typeSymbol.Name != "Task"
            || typeSymbol.ContainingNamespace.ToDisplayString() != "System.Threading.Tasks"
        )
        {
            return false;
        }

        return typeSymbol is INamedTypeSymbol namedTypeSymbol
            && namedTypeSymbol.TypeArguments.Length == 1;
    }

    public static bool IsValueTask(this ITypeSymbol typeSymbol)
    {
        // containing namespace may be null for system types
        if (typeSymbol.ContainingNamespace?.ToDisplayString() != "System.Threading.Tasks")
        {
            return false;
        }

        return typeSymbol.Name is "ValueTask" or "ValueTask`1";
    }

    public static ITypeSymbol? GetInnerTypeIfTask(this ITypeSymbol typeSymbol)
    {
        if (
            typeSymbol is INamedTypeSymbol namedTypeSymbol
            && typeSymbol.IsTask()
            && namedTypeSymbol.TypeArguments.Length > 0
        )
        {
            return namedTypeSymbol.TypeArguments[0];
        }
        return null;
    }

    public static string? GetVoidIfTask(this ITypeSymbol typeSymbol)
    {
        if (
            typeSymbol is INamedTypeSymbol namedTypeSymbol
            && typeSymbol.IsTask()
            && namedTypeSymbol.TypeArguments.Length == 0
        )
        {
            return "void";
        }
        return null;
    }

    //public static string GetMockBaseTypeName(this ITypeSymbol typeSymbol)
    //{
    //    if (typeSymbol.TypeKind == TypeKind.Interface)
    //    {
    //        return nameof(Abstractions.InterfaceMock<bool, bool, bool>);
    //    }
    //    else
    //    {
    //        return nameof(Abstractions.SealedTypeMock<bool, bool, bool>);
    //    }
    //}
}
