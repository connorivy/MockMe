using System;
using System.Collections.Generic;
using System.Text;

namespace MockMe.Abstractions;

[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
public class GenericMethodDefinitionAttribute(
    string typeToReplaceAssemblyName,
    string typeToReplaceTypeFullName,
    string typeToReplaceMethoName,
    string sourceTypeAssemblyName,
    string sourceTypeFullName,
    string sourceTypeMethodName
) : Attribute
{
    public string TypeToReplaceAssemblyName { get; set; } = typeToReplaceAssemblyName;
    public string TypeToReplaceTypeFullName { get; set; } = typeToReplaceTypeFullName;
    public string TypeToReplaceMethoName { get; set; } = typeToReplaceMethoName;
    public string SourceTypeAssemblyName { get; set; } = sourceTypeAssemblyName;
    public string SourceTypeFullName { get; set; } = sourceTypeFullName;
    public string SourceTypeMethodName { get; set; } = sourceTypeMethodName;
}
