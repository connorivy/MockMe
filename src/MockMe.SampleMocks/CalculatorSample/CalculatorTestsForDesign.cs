using System.Collections.Generic;
using System.Reflection;
using MockMe.Tests;
using MockMe.Tests.ExampleClasses;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;
using Mono.CompilerServices.SymbolWriter;
using static HarmonyLib.Code;
#if DEBUG
using Xunit;
#endif

namespace MockMe.SampleMocks.CalculatorSample;

public class CalculatorTestsForDesign
{
    // Uncomment to disable tests
#if !DEBUG
    private class FactAttribute : Attribute { }
#endif

    static Assembly LoadFromSameFolder(object sender, ResolveEventArgs args)
    {
        string folderPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        string assemblyPath = Path.Combine(folderPath, new AssemblyName(args.Name).Name + ".dll");
        if (!File.Exists(assemblyPath))
            return null;
        Assembly assembly = Assembly.LoadFrom(assemblyPath);
        return assembly;
    }

    [Fact]
    public void TestMe()
    {
        AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(LoadFromSameFolder);

        var resolver = new DefaultAssemblyResolver();
        resolver.AddSearchDirectory(Path.GetDirectoryName(typeof(object).Assembly.Location));
        resolver.AddSearchDirectory(AppDomain.CurrentDomain.BaseDirectory);

        string assemblyLoc = Assembly.GetExecutingAssembly().Location;
        string binLocation = Path.GetDirectoryName(assemblyLoc);
        string mockedObjPath = Path.Combine(binLocation, "MockMe.Tests.ExampleClasses.dll");
        string mockPath = Path.Combine(binLocation, "MockMe.Tests.dll");

        using var mockedObjAssembly = AssemblyDefinition.ReadAssembly(
            mockedObjPath,
            new ReaderParameters
            {
                ReadWrite = true,
                ReadingMode = ReadingMode.Immediate,
                InMemory = true,
                AssemblyResolver = resolver,
            }
        );

        TypeDefinition typeToReplace = mockedObjAssembly.MainModule.GetType(
            "MockMe.Tests.ExampleClasses.ComplexCalculator"
        );
        MethodDefinition method = typeToReplace.Methods.First(m => m.Name == "AddUpAllOfThese2");

        using var mockAssembly = AssemblyDefinition.ReadAssembly(mockPath);
        TypeDefinition mock = mockAssembly.MainModule.GetType("MockMe.Tests.TempCalcMock");

        MethodDefinition replacementLogic = mock.Methods.First(m => m.Name == "AddUpAllOfThese2");
        //var getStoreRef = ImportReference(mockedObjAssembly.MainModule, getStore);
        var replacementIlProcessor = replacementLogic.Body.Instructions;
        var ilProcessor = method.Body.GetILProcessor();
        var firstInstruction = method.Body.Instructions[0];

        var tempCalcMockStateModule = mockedObjAssembly.MainModule;
        //method.Body.Variables.Insert(
        //    0,
        //    new VariableDefinition(tempCalcMockStateModule.ImportReference(typeof(object)))
        //);

        //method.Body.Variables.Insert(
        //    0,
        //    new VariableDefinition(tempCalcMockStateModule.ImportReference(typeof(object)))
        //);
        method.Body.Variables.Clear();

        foreach (var variable in replacementLogic.Body.Variables)
        {
            method.Body.Variables.Add(
                new(ImportReference(tempCalcMockStateModule, variable.VariableType, method))
            );
        }

        List<Instruction> newInstructions = new();
        bool breakUntilNextBr = false;
        foreach (var instruction in replacementLogic.Body.Instructions)
        {
            if (instruction.OpCode == OpCodes.Ldnull)
            {
                newInstructions.Add(ilProcessor.Create(OpCodes.Ldarg_0));
            }
            else if (instruction.OpCode == OpCodes.Brfalse_S)
            {
                newInstructions.Add(ilProcessor.Create(OpCodes.Brfalse_S, firstInstruction));
            }
            //else if (instruction.OpCode == OpCodes.Br_S)
            //{
            //    breakUntilNextBr = !breakUntilNextBr;
            //}
            //else if (breakUntilNextBr)
            //{
            //    continue;
            //}
            else
            {
                Instruction importedInstruction = instruction;
                if (instruction.Operand is MethodReference methodRef)
                {
                    var importedMethodRef = ImportReference(
                        mockedObjAssembly.MainModule,
                        methodRef
                    //typeToReplace
                    );
                    ////mockedObjAssembly.MainModule.ImportReference(methodRef.ReturnType);
                    //foreach (var parameter in methodRef.Parameters)
                    //{
                    //    mockedObjAssembly.MainModule.ImportReference(parameter.ParameterType);
                    //}
                    //if (methodRef is GenericInstanceMethod genericInstanceMethodRef)
                    //{
                    //    foreach (var genericArg in genericInstanceMethodRef.GenericArguments)
                    //    {
                    //        mockedObjAssembly.MainModule.ImportReference(genericArg);
                    //    }
                    //}
                    //foreach (var parameter in methodRef.GenericParameters)
                    //{
                    //    //assembly.MainModule.ImportReference(parameter.Type);
                    //}
                    //var importedMethodRef = ImportReference(
                    //    mockedObjAssembly.MainModule,
                    //    methodRef
                    //);
                    importedInstruction = ilProcessor.Create(instruction.OpCode, importedMethodRef);
                }
                else if (instruction.Operand is FieldReference fieldRef)
                {
                    var importedFieldRef = ImportReference(mockedObjAssembly.MainModule, fieldRef);
                    importedInstruction = ilProcessor.Create(instruction.OpCode, importedFieldRef);
                }
                else if (instruction.Operand is TypeReference typeRef)
                {
                    TypeReference importedTypeRef = ImportReference(
                        mockedObjAssembly.MainModule,
                        typeRef,
                        method
                    );

                    importedInstruction = ilProcessor.Create(instruction.OpCode, importedTypeRef);
                }
                //ilProcessor.Body.Instructions.Add(importedInstruction);
                newInstructions.Add(importedInstruction);
            }

            if (instruction.OpCode == OpCodes.Ret)
            {
                break;
            }
        }

        foreach (var newInstruction in newInstructions)
        {
            ilProcessor.InsertBefore(firstInstruction, newInstruction);
        }

        var writeParams = new WriterParameters()
        {
            SymbolWriterProvider = new PortablePdbWriterProvider(),
        };

        mockedObjAssembly.Write(mockedObjPath, writeParams);

        Test();
    }

    private void Test()
    {
        var x = new TempCalcMock();

        var calc = (ComplexCalculator)x;

        var y = new ComplexCalculator().AddUpAllOfThese2(0, [5, 4, 3, 2], 2.2);

        var result = calc.AddUpAllOfThese2(0, [5, 4, 3, 2], 2.2);
        ;
    }

    //[Fact]
    //private void Test2()
    //{
    //    var x = new TempCalcMock();

    //    var calc = (ComplexCalculator)x;

    //    var y = new ComplexCalculator().GetDict();

    //    var result = calc.AddUpAllOfThese2_New(0, [5, 4, 3, 2], 2.2);
    //    ;
    //}

    private static TypeReference readOnlyDict;

    private static TypeReference ImportReference(
        ModuleDefinition module,
        TypeReference typeRef,
        MethodDefinition methodToReplace
    )
    {
        if (typeRef.FullName.StartsWith("System."))
        {
            ;
        }
        if (typeRef.FullName == "System.Collections.Generic.IReadOnlyDictionary`2")
        {
            ;
            //return readOnlyDict;
        }
        if (typeRef is ArrayType arrayType)
        {
            return new ArrayType(ImportReference(module, arrayType.ElementType, methodToReplace));
        }
        if (typeRef is ByReferenceType byReferenceType)
        {
            return new ByReferenceType(
                ImportReference(module, byReferenceType.ElementType, methodToReplace)
            );
        }
        if (typeRef is GenericParameter genericParamRef)
        {
            return ImportReference(genericParamRef, methodToReplace);
        }
        if (typeRef is GenericInstanceType genericInstanceType)
        {
            var importedInstanceType = new GenericInstanceType(
                ImportReference(module, genericInstanceType.ElementType, methodToReplace)
            );
            foreach (var genericParam in genericInstanceType.GenericParameters)
            {
                importedInstanceType.GenericArguments.Add(
                    ImportReference(module, genericParam, methodToReplace)
                );
            }
            foreach (var genericArg in genericInstanceType.GenericArguments)
            {
                importedInstanceType.GenericArguments.Add(
                    ImportReference(module, genericArg, methodToReplace)
                );
            }
            return importedInstanceType;
        }
        else
        {
            return module.ImportReference(typeRef);
        }
    }

    private static GenericParameter ImportReference(
        ModuleDefinition _,
        GenericParameter genericParamRef,
        MethodDefinition methodToReplace
    ) => ImportReference(genericParamRef, methodToReplace);

    private static GenericParameter ImportReference(
        GenericParameter genericParamRef,
        MethodDefinition methodToReplace
    )
    {
        if (genericParamRef.Owner is MethodDefinition)
        {
            // Method generic parameter
            return methodToReplace.GenericParameters[genericParamRef.Position];
        }
        else
        {
            // Type generic parameter
            var declaringType = methodToReplace.DeclaringType;
            return declaringType.GenericParameters[genericParamRef.Position];
        }
    }

    private static MethodReference ImportReference(
        ModuleDefinition module,
        MethodReference methodRef,
        TypeReference? declaringType = null
    )
    {
        MethodDefinition methodDefinition = methodRef.Resolve();
        if (declaringType == null)
        {
            declaringType = ImportReference(module, methodRef.DeclaringType, methodDefinition);
        }
        else
        {
            declaringType = ImportReference(module, declaringType, methodDefinition);
        }

        var returnType = ImportReference(module, methodRef.ReturnType, methodDefinition);

        module.ImportReference(methodRef);
        var newMethodRef = new MethodReference(methodRef.Name, returnType, declaringType)
        {
            HasThis = methodRef.HasThis,
            ExplicitThis = methodRef.ExplicitThis,
            CallingConvention = methodRef.CallingConvention,
        };
        foreach (var parameter in methodRef.Parameters)
        {
            //if (parameter.ParameterType.ContainsGenericParameter)
            //{
            //    continue;
            //}
            newMethodRef.Parameters.Add(
                new ParameterDefinition(
                    ImportReference(module, parameter.ParameterType, methodDefinition)
                )
            );
        }
        if (methodRef is GenericInstanceMethod genericInstanceMethod)
        {
            var newGenericInstanceMethod = new GenericInstanceMethod(newMethodRef);
            foreach (var argument in genericInstanceMethod.GenericArguments)
            {
                newGenericInstanceMethod.GenericArguments.Add(module.ImportReference(argument));
            }
            return newGenericInstanceMethod;
        }
        else
        {
            foreach (var genericParam in methodRef.GenericParameters)
            {
                newMethodRef.GenericParameters.Add(
                    ImportReference(module, genericParam, methodDefinition)
                );
            }
        }
        module.ImportReference(newMethodRef);
        return newMethodRef;
    }

    private static FieldReference ImportReference(ModuleDefinition module, FieldReference fieldRef)
    {
        var declaringType = module.ImportReference(fieldRef.DeclaringType);
        return new FieldReference(
            fieldRef.Name,
            module.ImportReference(fieldRef.FieldType),
            declaringType
        );
    }

    //[Fact]
    //public void CalculatorDesignFiddle()
    //{
    //    System.Diagnostics.Debugger.Launch();
    //    Harmony.DEBUG = true;
    //    var originalClass = typeof(Calculator);
    //    //var originalMethod = originalClass.GetMethod(nameof(Calculator.Add));
    //    var originalMethod = originalClass
    //        .GetMethod(nameof(Calculator.AddUpAllOfThese2))
    //        .MakeGenericMethod(typeof(object));

    //    var patchClass = typeof(CalculatorMock.Hook);
    //    //var prefix = patchClass.GetMethod(nameof(CalculatorMock.Hook.Add));
    //    //var prefix = patchClass.GetMethod(nameof(CalculatorMock.Hook.PrefixList3));
    //    var prefix = patchClass.GetMethod(nameof(CalculatorMock.Hook.PrefixList5));

    //    var instance = new Harmony("test");
    //    instance.Patch(originalMethod, new HarmonyMethod(prefix));

    //    //var patcher = instance.CreateProcessor(originalMethod);
    //    //_ = patcher.AddPrefix(prefix);
    //    //_ = patcher.Patch();

    //    Calculator calc = new();
    //    using (var d = new Hook(originalMethod, prefix))
    //    {
    //        //var harmony = new global::HarmonyLib.Harmony("com.mockme.patch");
    //        //harmony.PatchAll();

    //        //CalculatorMock calculatorMock = new();

    //        //calculatorMock.Setup.AddUpAllOfThese<int>(Arg.Any).Returns(1);
    //        //calculatorMock.Setup.AddUpAllOfThese<double>(Arg.Any).Returns(1);
    //        //calculatorMock.Setup.AddUpAllOfThese<List<int>>(Arg.Any).Returns(null as List<int>);
    //        //Calculator calc = (Calculator)calculatorMock;

    //        //var result1 = calc.AddUpAllOfThese([1, 2, 3, 4]);
    //        //var result2 = calc.AddUpAllOfThese<double>([1, 2, 3, 4]);
    //        var result3 = calc.AddUpAllOfThese2<List<int>>(1, [new([1])], 5);
    //    }
    //    //var result3 = calc.AddUpAllOfThese2<List<int>>(1, [new([1])], 5);

    //    var result6 = calc.AddUpAllOfThese2<object>(1, [7], 5);
    //    //var result5 = calc.AddUpAllOfThese<List<int>>([new(), new()]);
    //    //var result4 = calc.Add(0, 0);

    //    //calculatorMock.Assert.AddUpAllOfThese<int>(Arg.Any).WasCalled();
    //}

    //public static int AddReplace(Calculator self, int x, int y) => 99;

    //public static int AddOriginal(Calculator self, int x, int y) => self.Add(x, y);

    //[Fact]
    //public void CalculatorDesignFiddle2()
    //{
    //    Harmony.DEBUG = true;
    //    var originalClass = typeof(Calculator);
    //    //var originalMethod = originalClass.GetMethod(nameof(Calculator.Add));
    //    var originalMethod = originalClass.GetMethod(nameof(Calculator.Add));

    //    var patchClass = typeof(CalculatorTestsForDesign);
    //    //var prefix = patchClass.GetMethod(nameof(CalculatorMock.Hook.Add));
    //    //var prefix = patchClass.GetMethod(nameof(CalculatorMock.Hook.PrefixList3));
    //    var store = patchClass.GetMethod(nameof(AddOriginal));
    //    var newMethod = patchClass.GetMethod(nameof(AddReplace));

    //    Calculator calc = new();
    //    using (var a = new Hook(originalMethod, store))
    //    using (var d = new Hook(originalMethod, newMethod))
    //    {
    //        //var harmony = new global::HarmonyLib.Harmony("com.mockme.patch");
    //        //harmony.PatchAll();

    //        //CalculatorMock calculatorMock = new();

    //        //calculatorMock.Setup.AddUpAllOfThese<int>(Arg.Any).Returns(1);
    //        //calculatorMock.Setup.AddUpAllOfThese<double>(Arg.Any).Returns(1);
    //        //calculatorMock.Setup.AddUpAllOfThese<List<int>>(Arg.Any).Returns(null as List<int>);
    //        //Calculator calc = (Calculator)calculatorMock;

    //        //var result1 = calc.AddUpAllOfThese([1, 2, 3, 4]);
    //        //var result2 = calc.AddUpAllOfThese<double>([1, 2, 3, 4]);
    //        var mockResult = calc.Add(1, 2);
    //        var originalResult = AddOriginal(new(), 1, 2);
    //    }

    //    //var result6 = calc.AddUpAllOfThese2<object>(1, [7], 5);
    //    //var result5 = calc.AddUpAllOfThese<List<int>>([new(), new()]);
    //    //var result4 = calc.Add(0, 0);

    //    //calculatorMock.Assert.AddUpAllOfThese<int>(Arg.Any).WasCalled();
    //}

    //[Fact]
    //public void TryIlHook()
    //{
    //    var originalClass = typeof(Calculator);
    //    //var originalMethod = originalClass.GetMethod(nameof(Calculator.Add));
    //    var methodToReplace = originalClass
    //        .GetMethod(nameof(Calculator.AddUpAllOfThese2))
    //        .MakeGenericMethod(typeof(object));

    //    Calculator calc = new();
    //    object objectToReturn = new List<int>([5]);
    //    using var x = new ILHook(
    //        methodToReplace,
    //        il =>
    //        {
    //            ILCursor c = new ILCursor(il);

    //            // By the way, I'm assuming you're not dealing with void.
    //            // Let's add a safety check though...
    //            Type returnType = (methodToReplace as MethodInfo)?.ReturnType ?? typeof(void);
    //            if (returnType == typeof(void))
    //            {
    //                // Exit early or do whatever else you need to do for void methods.
    //                c.Emit(OpCodes.Ret);
    //                return;
    //            }

    //            c.Emit(OpCodes.Ldarg_1);
    //            c.Emit(OpCodes.Ldarg_2);
    //            c.Emit(OpCodes.Ldarg_3);
    //            c.Emit(
    //                OpCodes.Call,
    //                typeof(CalculatorMock.Hook)
    //                    .GetMethod(nameof(CalculatorMock.Hook.PrefixList7))
    //                    .MakeGenericMethod(typeof(object))
    //            );
    //            c.Emit(OpCodes.Ret);

    //            var x = c.IL;

    //            // Only use ONE of the following 3 variants.
    //            // The blob at the end is important for structs only and applies to all 3 variants.

    //            //// Variation 1: Easiest method but I don't know if you need the dictionary.
    //            //c.EmitDelegate<Func<object>>(() => objectToReturn);

    //            //// Variation 2: If you do need the dict to f.e. update the reference later:
    //            //c.Emit(OpCodes.Ldstr, "a generated ID or another key object");
    //            //c.EmitDelegate<Func<string, object>>(key => objectsToReturn[key]);

    //            // Variation 3: If you want to use MonoMod.RuntimeDetour's reference management:
    //            //int id = c.EmitReference(objectToReturn);
    //            // Skipping the delegate is also the most performant option and what I'd prefer in this case.
    //            // You can then use RuntimeILReferenceBag.Instance with the id as long as the hook isn't disposed.

    //            //// Keep in mind that no matter what path you choose:
    //            //// As long as you use object and not generics, you'll probably need to unbox.
    //            //if (returnType.IsValueType)
    //            //    c.Emit(OpCodes.Unbox_Any, returnType);
    //            //// Return from the injected method, returning your new value.
    //            //c.Emit(OpCodes.Ret);
    //        }
    //    );

    //    var result = calc.AddUpAllOfThese2<object>(1, [new()], 5);
    //    var result2 = calc.AddUpAllOfThese2<List<int>>(1, [new([1])], 5);
    //    ;
    //}

    public int AddNums(Calculator calc, int x, int y)
    {
        return calc.Add(x, y);
    }

    public double AddUpAllDoubles(Calculator calc, double[] dubs)
    {
        return calc.AddUpAllOfThese2<double>(0, dubs, 5.5);
    }
}
