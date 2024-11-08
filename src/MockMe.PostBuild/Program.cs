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

using var definitionAssembly = AssemblyDefinition.ReadAssembly(testAssemblyPath);

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
