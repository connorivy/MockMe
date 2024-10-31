using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MockMe.SampleMocks.CalculatorSample;
using MockMe.Tests.SampleClasses;
//using MockMe.SampleMocks.CalculatorSample;
using Mono.Cecil;
using Mono.Cecil.Cil;
using MonoMod.Utils;

namespace MockMe.Tests
{
    public class RandomTests
    {
        //static RandomTests()
        //{
        //    AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(OnAssemblyResolve);
        //    var t = Type.GetType(
        //        "MockMe.SampleMocks.CalculatorSample.Calculator, MockMe.SampleMocks"
        //    );
        //}

        [Fact]
        public void CecliModifyAssembly()
        {
            var assemblyPath = Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                "MockMe.SampleMocks.dll"
            );
            var newAssemblyPath = Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                "MockMe.SampleMocks-1.dll"
            );
            var assembly = AssemblyDefinition.ReadAssembly(
                assemblyPath,
                new ReaderParameters
                {
                    ReadWrite = true,
                    ReadingMode = ReadingMode.Immediate,
                    InMemory = true
                }
            );
            // Find the type to delete
            //
            var typeToRemove = assembly.MainModule.Types.FirstOrDefault(t =>
                t.Name == "Calculator"
            );

            var newTypeAssembly = AssemblyDefinition.ReadAssembly(
                Assembly.GetExecutingAssembly().Location
            );
            var newType = newTypeAssembly.MainModule.Types.First(t => t.Name == "Calculator2");
            var importedNewType = assembly.MainModule.ImportReference(newType);

            foreach (var type in assembly.MainModule.Types)
            {
                for (int methodIndex = type.Methods.Count - 1; methodIndex >= 0; methodIndex--)
                {
                    var method = type.Methods[methodIndex];
                    if (!method.HasBody)
                        continue;

                    //// add original method that casts to new type
                    //if (
                    //    method.ReturnType.FullName == typeToRemove.FullName
                    //    || method.Parameters.Any(p =>
                    //        p.ParameterType.FullName == typeToRemove.FullName
                    //    )
                    //)
                    //{
                    //    var methodBuilder = new MethodDefinition(
                    //        method.Name + "_Original",
                    //        method.Attributes,
                    //        method.ReturnType
                    //    );
                    //    foreach (var parameter in method.Parameters)
                    //    {
                    //        methodBuilder.Parameters.Add(
                    //            new ParameterDefinition(
                    //                parameter.Name,
                    //                parameter.Attributes,
                    //                parameter.ParameterType
                    //            )
                    //        );
                    //    }
                    //    var ilProcessor = methodBuilder.Body.GetILProcessor();
                    //    ilProcessor.Emit(OpCodes.Ldarg_0); // Load 'this'
                    //    for (int i = 0; i < method.Parameters.Count; i++)
                    //    {
                    //        ilProcessor.Emit(OpCodes.Ldarg, i + 1); // Load arguments
                    //        if (
                    //            method.Parameters[i].ParameterType.FullName == typeToRemove.FullName
                    //        )
                    //        {
                    //            ilProcessor.Emit(OpCodes.Castclass, importedNewType);
                    //        }
                    //    }
                    //    ilProcessor.Emit(OpCodes.Call, method);
                    //    if (method.ReturnType.FullName == typeToRemove.FullName)
                    //    {
                    //        ilProcessor.Emit(OpCodes.Castclass, importedNewType);
                    //    }
                    //    ilProcessor.Emit(OpCodes.Ret);
                    //    type.Methods.Add(methodBuilder);
                    //    Console.WriteLine(
                    //        $"Added intermediary method '{methodBuilder.Name}' to '{type.Name}'"
                    //    );
                    //}

                    // Update method signatures
                    //if (method.ReturnType.FullName == typeToRemove.FullName)
                    //{
                    //    method.ReturnType = importedNewType;
                    //}
                    //for (int j = 0; j < method.Parameters.Count; j++)
                    //{
                    //    if (method.Parameters[j].ParameterType.FullName == typeToRemove.FullName)
                    //    {
                    //        method.Parameters[j].ParameterType = importedNewType;
                    //    }
                    //}

                    foreach (var instruction in method.Body.Instructions)
                    {
                        if (
                            instruction.Operand is TypeReference typeRef
                            && typeRef.Name == "Calculator"
                        )
                        {
                            instruction.Operand = importedNewType;
                        }
                        else if (
                            instruction.Operand is MethodReference methodRef
                            && methodRef.DeclaringType.FullName == typeToRemove.FullName
                        )
                        {
                            //var newMethod = newType.Methods.First(m => m.Name == methodRef.Name);
                            //var importedMethod = assembly.MainModule.ImportReference(newMethod);

                            var newMethodRef = new MethodReference(
                                methodRef.Name,
                                methodRef.ReturnType,
                                importedNewType
                            )
                            {
                                HasThis = methodRef.HasThis,
                                ExplicitThis = methodRef.ExplicitThis,
                                CallingConvention = methodRef.CallingConvention
                            };
                            foreach (var parameter in methodRef.Parameters)
                            {
                                //newMethodRef.Parameters.Add(
                                //    new ParameterDefinition(parameter.ParameterType)
                                //);
                                newMethodRef.Parameters.Add(
                                    new ParameterDefinition(
                                        parameter.Name,
                                        Mono.Cecil.ParameterAttributes.None,
                                        parameter.ParameterType
                                    )
                                );
                            }
                            if (methodRef is GenericInstanceMethod genericInstanceMethodRef)
                            {
                                GenericInstanceMethod newGenericInstanceMethod = new(newMethodRef);
                                foreach (
                                    var genericArg in genericInstanceMethodRef.GenericArguments
                                )
                                {
                                    //var genericParam = new GenericParameter(genericArg)
                                    var importedGenericArg = assembly.MainModule.ImportReference(
                                        genericArg
                                    );
                                    newGenericInstanceMethod.GenericArguments.Add(
                                        importedGenericArg
                                    );
                                }
                                //methodRef.Relink(new(), newGenericInstanceMethod);
                                instruction.Operand = newGenericInstanceMethod;
                            }
                            else
                            {
                                foreach (var genericParam in methodRef.GenericParameters)
                                {
                                    newMethodRef.GenericParameters.Add(
                                        new GenericParameter(genericParam.Name, newMethodRef)
                                    );
                                }
                                instruction.Operand = newMethodRef;
                            }
                        }
                        else if (
                            instruction.Operand is FieldReference fieldRef
                            && fieldRef.DeclaringType.FullName == typeToRemove.FullName
                        )
                        {
                            var newFieldRef = new FieldReference(
                                fieldRef.Name,
                                fieldRef.FieldType,
                                importedNewType
                            );
                            instruction.Operand = newFieldRef;
                        }
                    }
                }
            }

            assembly.Write(assemblyPath);
            assembly.Write(newAssemblyPath);
            Console.WriteLine(
                $"Type '{typeToRemove.Name}' removed and saved to '{newAssemblyPath}'"
            );

            DoCalculatorStuff();
        }

        [Fact]
        public void DoCalculatorStuff()
        {
            //AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(OnAssemblyResolve);
            //var t = Type.GetType(
            //    "MockMe.SampleMocks.CalculatorSample.Calculator, MockMe.SampleMocks"
            //);
            CalculatorTestsForDesign tests = new();
            //var restul = tests.AddNums(new(), 1, 2);
            var dubsResult = tests.AddUpAllDoubles(new(), new double[] { 7.9, 1, 1, 1 });
        }

        [Fact]
        public void CecliModifyAssembly2()
        {
            var assemblyPath = Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                "MockMe.SampleMocks.dll"
            );
            var newAssemblyPath = Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                "MockMe.SampleMocks-1.dll"
            );
            var assembly = AssemblyDefinition.ReadAssembly(
                assemblyPath,
                new ReaderParameters
                {
                    ReadWrite = true,
                    ReadingMode = ReadingMode.Immediate,
                    InMemory = true
                }
            );
            // Find the type to delete
            //
            var typeToRemove = assembly.MainModule.Types.FirstOrDefault(t =>
                t.Name == "Calculator"
            );
            var firstMethod = typeToRemove.Methods.First(m =>
                m.Name == nameof(Calculator2.AddUpAllOfThese2)
            );

            var newTypeAssembly = AssemblyDefinition.ReadAssembly(
                Assembly.GetExecutingAssembly().Location
            );
            var newType = newTypeAssembly.MainModule.Types.First(t => t.Name == "Calculator2");
            var secondMethod = newType.Methods.First(m =>
                m.Name == nameof(Calculator2.AddUpAllOfThese2)
            );
            //var newTypeRef = assembly.MainModule.ImportReference(secondMethod.ReturnType);
            //var importedNewType = assembly.MainModule.ImportReference(secondMethod);

            //var module = firstMethod.Module;

            // Clear the body of the first method
            firstMethod.Body.Instructions.Clear(); // Copy the IL from the second method to the first method

            var ilProcessor = firstMethod.Body.GetILProcessor();
            foreach (var instruction in secondMethod.Body.Instructions)
            {
                Instruction importedInstruction = instruction;
                if (instruction.Operand is MethodReference methodRef)
                {
                    var importedMethodRef = ImportReference(assembly.MainModule, methodRef);
                    importedInstruction = ilProcessor.Create(instruction.OpCode, importedMethodRef);
                }
                else if (instruction.Operand is FieldReference fieldRef)
                {
                    var importedFieldRef = ImportReference(assembly.MainModule, fieldRef);
                    importedInstruction = ilProcessor.Create(instruction.OpCode, importedFieldRef);
                }
                else if (instruction.Operand is TypeReference typeRef)
                {
                    var importedTypeRef = ImportReference(assembly.MainModule, typeRef);
                    importedInstruction = ilProcessor.Create(instruction.OpCode, importedTypeRef);
                }
                ilProcessor.Append(importedInstruction);
            }

            assembly.Write(assemblyPath);
            assembly.Write(newAssemblyPath);
            Console.WriteLine(
                $"Type '{typeToRemove.Name}' removed and saved to '{newAssemblyPath}'"
            );

            DoCalculatorStuff();
        }

        //public class CalcMock : asdfasdf

        //[Fact]
        //public void DynamicAssemblyTest()
        //{
        //    var assembly = Assembly.LoadFile("path_to_your_assembly.dll");
        //    var assemblyName = new AssemblyName("DynamicCopiedAssembly");
        //    var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(
        //        assemblyName,
        //        AssemblyBuilderAccess.Run
        //    );
        //    var moduleBuilder = assemblyBuilder.DefineDynamicModule("MainModule");
        //}

        private static MemberReference ImportReference(
            ModuleDefinition module,
            MemberReference member
        )
        {
            if (member is TypeReference typeRef)
            {
                return module.ImportReference(typeRef);
            }
            else if (member is MethodReference methodRef)
            {
                var declaringType = module.ImportReference(methodRef.DeclaringType);
                var newMethodRef = new MethodReference(
                    methodRef.Name,
                    methodRef.ReturnType,
                    declaringType
                )
                {
                    HasThis = methodRef.HasThis,
                    ExplicitThis = methodRef.ExplicitThis,
                    CallingConvention = methodRef.CallingConvention
                };
                foreach (var parameter in methodRef.Parameters)
                {
                    newMethodRef.Parameters.Add(
                        new ParameterDefinition(module.ImportReference(parameter.ParameterType))
                    );
                }
                if (methodRef is GenericInstanceMethod genericInstanceMethod)
                {
                    var newGenericInstanceMethod = new GenericInstanceMethod(newMethodRef);
                    foreach (var argument in genericInstanceMethod.GenericArguments)
                    {
                        newGenericInstanceMethod.GenericArguments.Add(
                            module.ImportReference(argument)
                        );
                    }
                    return newGenericInstanceMethod;
                }
                return newMethodRef;
            }
            else if (member is FieldReference fieldRef)
            {
                var declaringType = module.ImportReference(fieldRef.DeclaringType);
                return new FieldReference(
                    fieldRef.Name,
                    module.ImportReference(fieldRef.FieldType),
                    declaringType
                );
            }
            return member;
        }

        static Assembly HandleTypeResolve(object sender, ResolveEventArgs args)
        {
            Console.WriteLine("TypeResolve event handler.");

            // Save the dynamic assembly, and then load it using its
            // display name. Return the loaded assembly.
            //
            //ab.Save(moduleName);
            return Assembly.Load(Assembly.GetExecutingAssembly().Location);
        }

        static Assembly OnAssemblyResolve(object sender, ResolveEventArgs args)
        {
            // Check the name of the requested assembly
            if (args.Name.Contains("MockMe.SampleMocks"))
            {
                Assembly.LoadFrom("MockMe.SampleMocks-1.dll");
            }
            return null;
        }
    }

    public enum CalculatorType
    {
        Standard,
        Scientific,
        Graphing
    }
}
