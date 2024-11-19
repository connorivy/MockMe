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

foreach (var group in genericTypes.GroupBy(info => info.TypeToReplace.AssemblyName))
{
    string currentAssemblyPath = Path.Combine(binLocation, group.Key + ".dll");
    AssemblyDefinition assembly;
    try
    {
        assembly = AssemblyDefinition.ReadAssembly(
            currentAssemblyPath,
            new ReaderParameters
            {
                ReadWrite = true,
                ReadingMode = ReadingMode.Immediate,
                InMemory = true,
                AssemblyResolver = new CustomResolver(binLocation),
                MetadataResolver = new CustomMetaResolver(new CustomResolver(binLocation)),
            }
        );
    }
    catch (System.IO.FileNotFoundException)
    {
        continue;
    }

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
    assembly.Dispose();
}

internal class CustomResolver(string binLocation) : BaseAssemblyResolver
{
    private readonly DefaultAssemblyResolver defaultResolver = new();

    public override AssemblyDefinition Resolve(
        AssemblyNameReference name,
        ReaderParameters readerParameters
    )
    {
        AssemblyDefinition assembly;
        try
        {
            assembly = this.defaultResolver.Resolve(name);
        }
        catch (AssemblyResolutionException)
        {
            assembly = AssemblyDefinition.ReadAssembly(
                Path.Combine(binLocation, name.Name + ".dll"),
                readerParameters
            );
        }
        return assembly;
    }
}

internal class CustomMetaResolver(IAssemblyResolver assemblyResolver)
    : MetadataResolver(assemblyResolver)
{
    public override MethodDefinition Resolve(MethodReference method)
    {
        return base.Resolve(method);
    }
}
