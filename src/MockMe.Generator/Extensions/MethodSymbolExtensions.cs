using System;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace MockMe.Generator.Extensions;

public static class MethodSymbolExtensions
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="method"></param>
    /// <returns>
    /// A single object type as a string that can hold all items that the method takes as parameters.
    /// If a method only takes a string, then this method will return "string",
    /// otherwise it will return "ValueTuple of T1, T2, ..., TN"
    /// </returns>
    public static string GetMethodArgumentsAsCollection(this IMethodSymbol method)
    {
        var types = method.Parameters.Select(p => p.Type.ToDisplayString()).ToArray();
        if (types.Length == 0)
        {
            throw new InvalidOperationException(
                "Cannot turn list of zero length in argument collection"
            );
        }
        if (types.Length == 1)
        {
            return types[0];
        }

        return $"ValueTuple<{string.Join(", ", types)}>";
    }

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
        return string.Join(
            ", ",
            method.Parameters.Select(p =>
            {
                var modifiers = p.RefKind switch
                {
                    RefKind.Ref => "ref ",
                    RefKind.Out => "out ",
                    RefKind.In => "in ",
                    RefKind.RefReadOnlyParameter => "ref readonly ",
                    RefKind.None or _ => p.IsParams ? "params " : ""
                };
                var paramString =
                    $"{modifiers}{typePrefix}{p.Type.ToDisplayString()}{typePostfix} {p.Name}";
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

    public static string GetParametersWithoutTypesAndModifiers(this IMethodSymbol method) =>
        string.Join(", ", method.Parameters.Select(p => p.Name));

    public static string GetParameterTypesWithoutModifiers(this IMethodSymbol method) =>
        string.Join(", ", method.Parameters.Select(p => p.Type.ToDisplayString()));
}
