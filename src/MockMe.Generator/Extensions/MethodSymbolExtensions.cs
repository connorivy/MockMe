using System;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace MockMe.Generator.Extensions;

public static class MethodSymbolExtensions
{
    public static string GetParametersWithOriginalTypesAndModifiers(this IMethodSymbol method) =>
        GetParametersWithTypesAndModifiers(method);

    public static string GetParametersWithArgTypesAndModifiers(this IMethodSymbol method) =>
        GetParametersWithTypesAndModifiers(method, "Arg<", ">");

    private static string GetParametersWithTypesAndModifiers(
        this IMethodSymbol method,
        string? typePrefix = null,
        string? typePostfix = null
    )
    {
        if (method.Parameters.Length == 0)
        {
            return string.Empty;
        }

        return string.Join(
            ", ",
            method.Parameters.Select(p =>
            {
                var modifiers = p.RefKind switch
                {
                    RefKind.Ref => "ref ",
                    RefKind.Out => "out ",
                    RefKind.In => "in ",
                    //RefKind.RefReadOnlyParameter => "ref readonly ",
                    RefKind.None or _ => p.IsParams ? "params " : "",
                };

                var paramString =
                    $"{modifiers}{typePrefix}{p.Type.ToFullTypeString()}{typePostfix} {p.Name}";
                if (p.HasExplicitDefaultValue)
                {
                    var defaultValue =
                        p.ExplicitDefaultValue != null ? p.ExplicitDefaultValue.ToString() : "null";
                    paramString += $" = {defaultValue}";
                }
                return paramString;
            })
        );
    }

    public static string GetParametersWithOriginalTypesAndNoModifiers(this IMethodSymbol method)
    {
        if (method.Parameters.Length == 0)
        {
            return string.Empty;
        }

        return string.Join(
            ", ",
            method.Parameters.Select(p =>
            {
                var paramString = $"{p.Type.ToFullTypeString()} {p.Name}";
                //if (p.HasExplicitDefaultValue)
                //{
                //    var defaultValue =
                //        p.ExplicitDefaultValue != null ? p.ExplicitDefaultValue.ToString() : "null";
                //    paramString += $" = {defaultValue}";
                //}
                return paramString;
            })
        );
    }

    public static string GetParametersWithModifiersAndNoTypes(this IMethodSymbol method)
    {
        if (method.Parameters.Length == 0)
        {
            return string.Empty;
        }

        return string.Join(
            ", ",
            method.Parameters.Select(p =>
            {
                var modifiers = p.RefKind switch
                {
                    RefKind.Ref => "ref ",
                    RefKind.Out => "out ",
                    RefKind.In => "in ",
                    //RefKind.RefReadOnlyParameter => "ref readonly ",
                    RefKind.None or _ => "",
                };

                var paramString = $"{modifiers} {p.Name}";

                return paramString;
            })
        );
    }

    public static string GetParametersWithoutTypesAndModifiers(this IMethodSymbol method) =>
        string.Join(", ", method.Parameters.Select(p => p.Name));

    public static string GetParameterTypesWithoutModifiers(this IMethodSymbol method) =>
        string.Join(", ", method.Parameters.Select(p => p.Type.ToFullTypeString()));

    public static string GetGenericParameterString(this IMethodSymbol method) =>
        string.Join(", ", method.TypeParameters.Select(p => p.Name));

    public static string GetGenericParameterStringInBrackets(this IMethodSymbol method)
    {
        if (method.TypeParameters.Length == 0)
        {
            return string.Empty;
        }

        return $"<{method.GetGenericParameterString()}>";
    }

    public static string GetPropertyName(this IMethodSymbol methodSymbol)
    {
        if (methodSymbol.MethodKind is MethodKind.PropertyGet or MethodKind.PropertySet)
        {
            return methodSymbol.Name[4..];
        }
        return methodSymbol.Name;
    }

    public static string GetUniqueMethodName(this IMethodSymbol methodSymbol)
        => GenerateUniqueName(methodSymbol, false);
    public static string GetUniqueStoreName(this IMethodSymbol methodSymbol)
        => GenerateUniqueName(methodSymbol, true);

    private static string GenerateUniqueName(IMethodSymbol methodSymbol, bool includeContainingType)
    {
        var methodName = methodSymbol.Name;
        var parameterTypes = string.Join("_", methodSymbol.Parameters.Select(p =>
            (p.RefKind == RefKind.None ? "" : p.RefKind.ToString()) + p.Type.Name
        ));
        if (!includeContainingType)
        {
            return $"{methodName}_{parameterTypes}";
        }

        var containingType = methodSymbol.ContainingType?.Name ?? "global";
        var accessModifier = methodSymbol.DeclaredAccessibility;
        var returnType = methodSymbol.ReturnType.Name;
        var typeParameters = string.Join("_", methodSymbol.TypeParameters.Select(tp => tp.Name));

        return $"{containingType}_{accessModifier}_{returnType}_{methodName}_{typeParameters}_{parameterTypes}";
    }
}
