// See https://aka.ms/new-console-template for more information
using System.Runtime.InteropServices;
using MockMe.Abstractions;
using MockMe.PostBuild;
using Mono.Cecil;

# if DEBUG
//System.Diagnostics.Debugger.Launch();
# endif

Console.WriteLine("Hello, Task!");

var testAssemblyPath = args[0];
var binLocation = Path.GetDirectoryName(testAssemblyPath);

using var definitionAssembly = AssemblyDefinition.ReadAssembly(testAssemblyPath);

List<MockReplacementInfo> genericTypes = new();
foreach (var assemblyAttr in definitionAssembly.CustomAttributes)
{
    if (assemblyAttr.AttributeType.Name != nameof(GenericMethodDefinitionAttribute))
    {
        continue;
    }

    genericTypes.Add(
        new(
            new(
                (string)assemblyAttr.ConstructorArguments[0].Value,
                (string)assemblyAttr.ConstructorArguments[1].Value,
                (string)assemblyAttr.ConstructorArguments[2].Value
            ),
            new(
                (string)assemblyAttr.ConstructorArguments[3].Value,
                (string)assemblyAttr.ConstructorArguments[4].Value,
                (string)assemblyAttr.ConstructorArguments[5].Value
            )
        )
    );
}

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

foreach (var group in genericTypes.GroupBy(info => info.TypeToReplace.AssemblyName))
{
    string currentAssemblyPath = Path.Combine(binLocation, group.Key + libExtensions);
    using var assembly = AssemblyDefinition.ReadAssembly(
        currentAssemblyPath,
        new ReaderParameters
        {
            ReadWrite = true,
            ReadingMode = ReadingMode.Immediate,
            InMemory = true
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

        ILReplacer.Replace(assembly, methodToReplace, replacementMethod);
    }
    assembly.Write(currentAssemblyPath);
}
