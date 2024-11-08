// See https://aka.ms/new-console-template for more information
using MockMe.Abstractions;
using MockMe.PostBuild;
using MockMe.PostBuild.Extensions;
using Mono.Cecil;

# if DEBUG
System.Diagnostics.Debugger.Launch();
# endif

Console.WriteLine("Hello, Task!");

var testAssemblyPath = args[0];
var binLocation = Path.GetDirectoryName(testAssemblyPath);

using var definitionAssembly = AssemblyDefinition.ReadAssembly(
    testAssemblyPath,
    new ReaderParameters
    {
        //ReadWrite = true,
        //ReadingMode = ReadingMode.Immediate,
        //InMemory = true,
        //AssemblyResolver = new CustomResolver(),
    }
);

List<MockReplacementInfo> genericTypes = definitionAssembly.GetMockReplacementInfo();

string libExtensions = ".dll";

//if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
//{
//    libExtensions = ".dll";
//}
//else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
//{
//    libExtensions = ".so";
//}
//else
//{
//    throw new NotImplementedException("mac not implemented");
//}

using var storeAssembly = AssemblyDefinition.ReadAssembly(
    Path.Combine(binLocation, "MockMe.Tests.NuGet.dll"),
    new ReaderParameters
    {
        //ReadWrite = true,
        //ReadingMode = ReadingMode.Immediate,
        //InMemory = true,
        //AssemblyResolver = new CustomResolver(),
    }
);

foreach (var group in genericTypes.GroupBy(info => info.TypeToReplace.AssemblyName))
{
    string currentAssemblyPath = Path.Combine(binLocation, group.Key + ".dll");
    using var assembly = AssemblyDefinition.ReadAssembly(
        currentAssemblyPath,
        new ReaderParameters
        {
            ReadWrite = true,
            ReadingMode = ReadingMode.Immediate,
            InMemory = true,
            AssemblyResolver = new CustomResolver(storeAssembly),
            MetadataResolver = new CustomMetaResolver(new CustomResolver(storeAssembly)),
        }
    );

    foreach (MockReplacementInfo mockReplacementInfo in group)
    {
        TypeDefinition typeToReplace = assembly.MainModule.GetType(
            mockReplacementInfo.TypeToReplace.TypeFullName
        );
        MethodDefinition methodToReplace = typeToReplace.Methods.First(m =>
            m.Name == mockReplacementInfo.TypeToReplace.MethodName
        );

        TypeDefinition replacementType = definitionAssembly.MainModule.GetType(
            mockReplacementInfo.SourceType.TypeFullName
        );
        MethodDefinition replacementMethod = replacementType.Methods.First(m =>
            m.Name == mockReplacementInfo.SourceType.MethodName
        );

        //ILReplacer.Replace(assembly, methodToReplace, replacementMethod);
        ILManipulator.InsertMethodBodyBeforeExisting(assembly, methodToReplace, replacementMethod);
    }

    assembly.Write(currentAssemblyPath);
}

internal class CustomResolver : BaseAssemblyResolver
{
    private readonly AssemblyDefinition definition;
    private DefaultAssemblyResolver _defaultResolver;

    public CustomResolver(AssemblyDefinition definition)
    {
        _defaultResolver = new DefaultAssemblyResolver();
        this.definition = definition;
    }

    public override AssemblyDefinition Resolve(
        AssemblyNameReference name,
        ReaderParameters readerParameters
    )
    {
        if (name.Name == "MockMe.Tests.NuGet")
        {
            return definition;
        }
        AssemblyDefinition assembly;
        try
        {
            assembly = _defaultResolver.Resolve(name);
        }
        catch (AssemblyResolutionException ex)
        {
            ;
            throw;
        }
        return assembly;
    }
}

internal class CustomMetaResolver : Mono.Cecil.MetadataResolver
{
    public CustomMetaResolver(IAssemblyResolver assemblyResolver)
        : base(assemblyResolver) { }

    public override MethodDefinition Resolve(MethodReference method)
    {
        return base.Resolve(method);
    }
}
