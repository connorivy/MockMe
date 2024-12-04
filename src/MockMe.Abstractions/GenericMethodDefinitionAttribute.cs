using System;
using System.Diagnostics.CodeAnalysis;

namespace MockMe.Abstractions;

[ExcludeFromCodeCoverage]
[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
public class GenericMethodDefinitionAttribute(
    string typeToReplaceAssemblyName,
    string typeToReplaceTypeFullName,
    string typeToReplaceMethodName,
    string sourceTypeAssemblyName,
    string sourceTypeFullName,
    string sourceTypeMethodName
) : Attribute
{
    public string TypeToReplaceAssemblyName { get; } = typeToReplaceAssemblyName;
    public string TypeToReplaceTypeFullName { get; } = typeToReplaceTypeFullName;
    public string TypeToReplaceMethoName { get; } = typeToReplaceMethodName;
    public string SourceTypeAssemblyName { get; } = sourceTypeAssemblyName;
    public string SourceTypeFullName { get; } = sourceTypeFullName;
    public string SourceTypeMethodName { get; } = sourceTypeMethodName;
}
