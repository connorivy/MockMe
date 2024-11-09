namespace MockMe.Abstractions;

public class MockReplacementInfo
{
    public MockReplacementInfo(GenericMethodInfo typeToReplace, GenericMethodInfo sourceType)
    {
        this.TypeToReplace = typeToReplace;
        this.SourceType = sourceType;
    }

    public GenericMethodInfo TypeToReplace { get; }
    public GenericMethodInfo SourceType { get; }
}

public class GenericMethodInfo
{
    public GenericMethodInfo(string assemblyName, string typeFullName, string methodName)
    {
        this.AssemblyName = assemblyName;
        this.TypeFullName = typeFullName;
        this.MethodName = methodName;
    }

    public string AssemblyName { get; }
    public string TypeFullName { get; }
    public string MethodName { get; }
}
