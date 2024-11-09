using Microsoft.Build.Framework;
using MockMe.Abstractions;
using Mono.Cecil;
using Task = Microsoft.Build.Utilities.Task;

namespace MockMe.Build;

public class ReplaceGenericMethods : Task
{
    [Required]
    public string TestAssemblyPath { get; set; }

    public override bool Execute()
    {
        System.Diagnostics.Debugger.Launch();
        Console.WriteLine("Hello, Task!");

        //var testAssemblyPath =
        //    "C:\\Users\\conno\\Documents\\GitHub\\MockMe\\test\\MockMeTests\\bin\\Debug\\net6.0\\MockMe.Tests.dll";
        var binLocation = Path.GetDirectoryName(this.TestAssemblyPath);

        //var dllAssembly = Assembly.LoadFile(TestAssemblyPath);

        //var types = dllAssembly.GetTypes();

        //var mockType = dllAssembly
        //    .GetTypes()
        //    .Where(t => t.Namespace == "MockMe" && t.Name == "Mock")
        //    .First();

        //var methods = mockType.GetMethods();

        //List<MockReplacementInfo> genericTypes =
        //    (List<MockReplacementInfo>)
        //        mockType
        //            .GetMethod("GenericTypes", BindingFlags.Public | BindingFlags.Static)
        //            .Invoke(null, null);

        //if (genericTypes.Count == 0)
        //{
        //    return true;
        //}

        using var definitionAssembly = AssemblyDefinition.ReadAssembly(this.TestAssemblyPath);

        List<MockReplacementInfo> genericTypes = [];
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

        return true;
    }
}
