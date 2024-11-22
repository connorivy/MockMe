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
string testAssemblyName = Path.GetFileNameWithoutExtension(testAssemblyPath);

using var definitionAssembly = AssemblyDefinition.ReadAssembly(
    testAssemblyPath,
    new ReaderParameters
    {
        ReadWrite = true,
        ReadingMode = ReadingMode.Immediate,
        ReadSymbols = true,
        InMemory = true,
        AssemblyResolver = new CustomResolver(binLocation),
        MetadataResolver = new CustomMetaResolver(new CustomResolver(binLocation)),
    }
);

List<MockReplacementInfo> genericTypes = definitionAssembly.GetMockReplacementInfo();

var g1 = genericTypes
    .Where(info => info.TypeToReplace.AssemblyName != testAssemblyName)
    .GroupBy(info => info.TypeToReplace.AssemblyName);

var g2 = genericTypes
    .Where(info => info.TypeToReplace.AssemblyName == testAssemblyName)
    .GroupBy(info => info.TypeToReplace.AssemblyName);

var genericTypesWithTestAssemblyLast = genericTypes
    .Where(info => info.TypeToReplace.AssemblyName != testAssemblyName)
    .GroupBy(info => info.TypeToReplace.AssemblyName)
    .Concat(
        genericTypes
            .Where(info => info.TypeToReplace.AssemblyName == testAssemblyName)
            .GroupBy(info => info.TypeToReplace.AssemblyName)
    );

//WriterParameters writerParameters = new() { WriteSymbols = true };

foreach (var group in genericTypesWithTestAssemblyLast)
{
    string currentAssemblyPath = Path.Combine(binLocation, group.Key + ".dll");
    AssemblyDefinition assembly;

    if (currentAssemblyPath == testAssemblyPath)
    {
        assembly = definitionAssembly;
    }
    else
    {
        try
        {
            assembly = AssemblyDefinition.ReadAssembly(
                currentAssemblyPath,
                new ReaderParameters
                {
                    ReadWrite = true,
                    ReadingMode = ReadingMode.Immediate,
                    ReadSymbols = true,
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

    assembly.Write(currentAssemblyPath, new() { WriteSymbols = true });
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
