using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;

namespace MockMe.Mocks.Generic;

internal class ILReplacer
{
    public static void Replace(
        AssemblyDefinition assembly,
        MethodDefinition methodToReplace,
        MethodDefinition sourceMethod
    )
    {
        // Clear the body of the first method
        methodToReplace.Body.Instructions.Clear();

        var ilProcessor = methodToReplace.Body.GetILProcessor();
        foreach (var instruction in sourceMethod.Body.Instructions)
        {
            Instruction importedInstruction = instruction;
            if (instruction.Operand is MethodReference methodRef)
            {
                assembly.MainModule.ImportReference(methodRef.ReturnType);
                foreach (var parameter in methodRef.Parameters)
                {
                    assembly.MainModule.ImportReference(parameter.ParameterType);
                }
                if (methodRef is GenericInstanceMethod genericInstanceMethodRef)
                {
                    foreach (var genericArg in genericInstanceMethodRef.GenericArguments)
                    {
                        assembly.MainModule.ImportReference(genericArg);
                    }
                }
                foreach (var parameter in methodRef.GenericParameters)
                {
                    //assembly.MainModule.ImportReference(parameter.Type);
                }
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
                TypeReference importedTypeRef;
                if (typeRef.IsGenericParameter)
                {
                    var genericParam = typeRef as GenericParameter;
                    if (genericParam.Owner is MethodDefinition)
                    {
                        // Method generic parameter
                        importedTypeRef = methodToReplace.GenericParameters[genericParam.Position];
                    }
                    else
                    {
                        // Type generic parameter
                        var declaringType = methodToReplace.DeclaringType;
                        importedTypeRef = declaringType.GenericParameters[genericParam.Position];
                    }
                }
                else
                {
                    importedTypeRef = assembly.MainModule.ImportReference(typeRef);
                }
                //importedTypeRef = assembly.MainModule.ImportReference(typeRef);

                importedInstruction = ilProcessor.Create(instruction.OpCode, importedTypeRef);
            }
            ilProcessor.Body.Instructions.Add(importedInstruction);
        }

        //foreach (var variable in sourceMethod.Body.Variables)
        //{
        //    var importedVariableType = assembly.MainModule.ImportReference(
        //        variable.VariableType
        //    );
        //    targetMethod.Body.Variables.Add(new VariableDefinition(importedVariableType));
        //}

        //secondMethod.ReturnType = firstMethod.ReturnType;
        //secondMethod.GenericParameters.Clear();
        //foreach (var parameter in firstMethod.GenericParameters)
        //{
        //    secondMethod.GenericParameters.Add(parameter);
        //}
        //secondMethod.Parameters.Clear();
        //foreach (var parameter in firstMethod.Parameters)
        //{
        //    secondMethod.Parameters.Add(parameter);
        //}

        methodToReplace.Body.OptimizeMacros();

        assembly.Write();
    }

    private static TypeReference ImportReference(
        ModuleDefinition module,
        MethodDefinition methodToReplace,
        TypeReference typeRef
    )
    {
        if (typeRef is GenericParameter genericParam)
        {
            TypeReference importedTypeRef;
            if (genericParam.Owner is MethodDefinition)
            {
                // Method generic parameter
                importedTypeRef = methodToReplace.GenericParameters[genericParam.Position];
            }
            else
            {
                // Type generic parameter
                var declaringType = methodToReplace.DeclaringType;
                importedTypeRef = declaringType.GenericParameters[genericParam.Position];
            }
            return importedTypeRef;
        }
        else
        {
            return module.ImportReference(typeRef);
        }
    }

    private static MethodReference ImportReference(
        ModuleDefinition module,
        MethodReference methodRef
    )
    {
        var declaringType = module.ImportReference(methodRef.DeclaringType);
        var newMethodRef = new MethodReference(methodRef.Name, methodRef.ReturnType, declaringType)
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
                newGenericInstanceMethod.GenericArguments.Add(module.ImportReference(argument));
            }
            return newGenericInstanceMethod;
        }
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
}
