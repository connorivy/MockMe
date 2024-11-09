using System;

namespace MockMe.Abstractions;

[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
public class GenericMethodDefinitionAttribute : Attribute
{
    public GenericMethodDefinitionAttribute(
        string typeToReplaceAssemblyName,
        string typeToReplaceTypeFullName,
        string typeToReplaceMethodName,
        string sourceTypeAssemblyName,
        string sourceTypeFullName,
        string sourceTypeMethodName
    )
    {
        this.TypeToReplaceAssemblyName = typeToReplaceAssemblyName;
        this.TypeToReplaceTypeFullName = typeToReplaceTypeFullName;
        this.TypeToReplaceMethoName = typeToReplaceMethodName;
        this.SourceTypeAssemblyName = sourceTypeAssemblyName;
        this.SourceTypeFullName = sourceTypeFullName;
        this.SourceTypeMethodName = sourceTypeMethodName;
    }

    public string TypeToReplaceAssemblyName { get; }
    public string TypeToReplaceTypeFullName { get; }
    public string TypeToReplaceMethoName { get; }
    public string SourceTypeAssemblyName { get; }
    public string SourceTypeFullName { get; }
    public string SourceTypeMethodName { get; }
}

[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
public class GenericMethodDefinition2Attribute : Attribute
{
    public GenericMethodDefinition2Attribute(
        string typeToReplaceAssemblyName,
        string typeToReplaceTypeFullName,
        string typeToReplaceMethodName,
        string mockClassNamespace,
        string mockClassName
    ) { }
}
