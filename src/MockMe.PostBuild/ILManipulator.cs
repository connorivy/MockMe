using MockMe.PostBuild.Extensions;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Collections.Generic;

namespace MockMe.PostBuild;

internal class ILManipulator
{
    public static void InsertMethodBodyBeforeExisting(
        AssemblyDefinition methodAssembly,
        MethodDefinition originalMethod,
        MethodDefinition methodToInsert
    )
    {
        var originalIlProcessor = originalMethod.Body.GetILProcessor();
        var originalFirstInstruction = originalMethod.Body.Instructions[0];

        originalMethod.Body.Variables.Clear();

        foreach (var variable in methodToInsert.Body.Variables)
        {
            originalMethod.Body.Variables.Add(
                new(
                    methodAssembly.MainModule.ImportMockMeReference(
                        variable.VariableType,
                        originalMethod
                    )
                )
            );
        }

        var newInstructions = GetImportedInstructionsToInsert(
            methodAssembly.MainModule,
            originalMethod,
            originalIlProcessor,
            methodToInsert.Body.Instructions,
            originalFirstInstruction
        );

        foreach (var newInstruction in newInstructions)
        {
            originalIlProcessor.InsertBefore(originalFirstInstruction, newInstruction);
        }
    }

    private static List<Instruction> GetImportedInstructionsToInsert(
        ModuleDefinition module,
        MethodDefinition originalMethod,
        ILProcessor originalIlProcessor,
        Collection<Instruction> insertMethodInstructions,
        Instruction originalFirstInstruction
    )
    {
        List<Instruction> newInstructions = [];
        foreach (var instruction in insertMethodInstructions)
        {
            if (instruction.OpCode == OpCodes.Ldnull)
            {
                newInstructions.Add(originalIlProcessor.Create(OpCodes.Ldarg_0));
            }
            else if (instruction.OpCode == OpCodes.Brfalse_S)
            {
                newInstructions.Add(
                    originalIlProcessor.Create(OpCodes.Brfalse_S, originalFirstInstruction)
                );
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
                    var importedMethodRef = module.ImportMockMeReference(methodRef);

                    importedInstruction = originalIlProcessor.Create(
                        instruction.OpCode,
                        importedMethodRef
                    );
                }
                else if (instruction.Operand is FieldReference fieldRef)
                {
                    var importedFieldRef = module.ImportMockMeReference(fieldRef);

                    importedInstruction = originalIlProcessor.Create(
                        instruction.OpCode,
                        importedFieldRef
                    );
                }
                else if (instruction.Operand is TypeReference typeRef)
                {
                    TypeReference importedTypeRef = module.ImportMockMeReference(
                        typeRef,
                        originalMethod
                    );

                    importedInstruction = originalIlProcessor.Create(
                        instruction.OpCode,
                        importedTypeRef
                    );
                }

                newInstructions.Add(importedInstruction);
            }

            if (instruction.OpCode == OpCodes.Ret)
            {
                break;
            }
        }

        return newInstructions;
    }
}
