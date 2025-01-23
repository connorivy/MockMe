using System;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace MockMe.Generator.Extensions;

public static class MethodSymbolExtensions
{
    public static string GetParametersWithOriginalTypesAndModifiers(this IMethodSymbol method) =>
        GetParametersWithTypesAndModifiers(method);

    public static string GetParametersWithArgTypesAndModifiers(this IMethodSymbol method) =>
        GetParametersWithTypesAndModifiers(method, "Arg<", ">", true);

    private static string GetParametersWithTypesAndModifiers(
        this IMethodSymbol method,
        string? typePrefix = null,
        string? typePostfix = null,
        bool wrapInArg = false
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

                // Build the main "ref int x" or "Arg<int> x" part
                var paramString =
                    $"{modifiers}{typePrefix}{p.Type.ToFullTypeString()}{typePostfix} {p.Name}";

                // If the original parameter had a default value, we only append it if we're NOT
                // wrapping in Arg<...>. (Skipping avoids e.g. Arg<int> x = 2 which is invalid.)
                if (p.HasExplicitDefaultValue && !wrapInArg)
                {
                    var defaultValue = GetDefaultValueForType(p.Type, p.ExplicitDefaultValue);
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
    {
        var methodName = methodSymbol.Name;
        var parameterTypes = methodSymbol.Parameters.Select(p =>
            (p.RefKind == RefKind.None ? "" : p.RefKind.ToString()) + p.Type.Name
        );
        var uniqueMethodName =
            $"{methodName}_{string.Join("_", methodSymbol.TypeParameters.Select(p => p.Name)).AddSuffixIfNotEmpty("_")}{string.Join("_", parameterTypes)}";

        return uniqueMethodName;
    }

    private static string GetDefaultValueForType(ITypeSymbol type, object? explicitValue)
    {
        // If the compiler recognized a default value, it sets HasExplicitDefaultValue = true,
        // and ExplicitDefaultValue can be null (for '= default') or a constant (for '= 2', '= "x"', etc.)

        switch (explicitValue)
        {
            case null:
                // For a non-nullable value type, 'default'
                // For reference types or nullable, 'null'
                return type.IsValueType ? "default" : "null";
            case bool b:
                return b ? "true" : "false";
            // If we do have a constant, handle string vs. others
            // Wrap string in quotes
            case string s:
                return $"\"{s}\"";
        }

        if (type.TypeKind == TypeKind.Enum && type is INamedTypeSymbol namedEnum)
        {
            return GetEnumDefaultValueString(namedEnum, explicitValue);
        }

        return explicitValue.ToString();
    }

    private static string GetEnumDefaultValueString(INamedTypeSymbol enumType, object? rawValue) =>
        rawValue switch
        {
            int intValue => FindEnumMember(enumType, intValue),
            long longValue => FindEnumMember(enumType, longValue),
            byte byteValue => FindEnumMember(enumType, byteValue),
            sbyte sbyteValue => FindEnumMember(enumType, sbyteValue),
            short shortValue => FindEnumMember(enumType, shortValue),
            ushort ushortValue => FindEnumMember(enumType, ushortValue),
            uint uintValue => FindEnumMember(enumType, uintValue),
            ulong ulongValue => FindEnumMember(enumType, ulongValue),
            null => $"{enumType.ToFullTypeString()} /* unknown null */",
            // Fallback for unexpected cases:
            _ => $"{enumType.ToFullTypeString()} /* unknown = {rawValue} */",
        };

    private static string FindEnumMember<T>(INamedTypeSymbol enumType, T value)
        where T : struct, IComparable
    {
        foreach (var member in enumType.GetMembers().OfType<IFieldSymbol>())
        {
            if (member.HasConstantValue && Equals(member.ConstantValue, value))
            {
                // e.g., "System.DayOfWeek.Monday"
                return $"{enumType.ToFullTypeString()}.{member.Name}";
            }
        }

        // Fallback if no matching member is found
        return $"{enumType.ToFullTypeString()} /* unknown = {value} */";
    }
}
