using System;
using System.Collections.Generic;
using System.Text;

namespace MockMe.Abstractions;

public class MockReplacementInfo(GenericMethodInfo typeToReplace, GenericMethodInfo sourceType)
{
    public GenericMethodInfo TypeToReplace { get; } = typeToReplace;
    public GenericMethodInfo SourceType { get; } = sourceType;
}

public class GenericMethodInfo(string assemblyName, string typeFullName, string methodName)
{
    public string AssemblyName { get; } = assemblyName;
    public string TypeFullName { get; } = typeFullName;
    public string MethodName { get; } = methodName;
}
