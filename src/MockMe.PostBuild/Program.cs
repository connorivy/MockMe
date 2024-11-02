// See https://aka.ms/new-console-template for more information
using System.Reflection;
using MockMe.Abstractions;
using MockMe.PostBuild;
using Mono.Cecil;

//System.Diagnostics.Debugger.Launch();
Console.WriteLine("Hello, World!");

//var testAssemblyPath =
//    "C:\\Users\\conno\\Documents\\GitHub\\MockMe\\test\\MockMeTests\\bin\\Debug\\net6.0\\MockMe.Tests.dll";
var testAssemblyPath = args[0];
var binLocation = Path.GetDirectoryName(testAssemblyPath);

var dllAssembly = Assembly.LoadFrom(testAssemblyPath);
var mockType = dllAssembly
    .GetTypes()
    .Where(t => t.Namespace == "MockMe" && t.Name == "Mock")
    .First();

var methods = mockType.GetMethods();

List<MockReplacementInfo> genericTypes =
    (List<MockReplacementInfo>)
        mockType
            .GetMethod("GenericTypes", BindingFlags.Public | BindingFlags.Static)
            .Invoke(null, null);

if (genericTypes.Count == 0)
{
    System.Environment.Exit(0);
}

using var definitionAssembly = AssemblyDefinition.ReadAssembly(testAssemblyPath);

foreach (var group in genericTypes.GroupBy(info => info.TypeToReplace.AssemblyName))
{
    string currentAssemblyPath = Path.Combine(binLocation, group.Key + ".dll");
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
