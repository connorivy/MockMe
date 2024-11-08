using Mono.Cecil;

namespace MockMe.PostBuild.Extensions;

internal static class ModuleDefinitionExtensions
{
    public static TypeReference ImportMockMeReference(
        this ModuleDefinition module,
        TypeReference typeRef,
        MethodDefinition methodDefinition
    )
    {
        if (typeRef is ArrayType arrayType)
        {
            return new ArrayType(
                module.ImportMockMeReference(arrayType.ElementType, methodDefinition)
            );
        }
        if (typeRef is ByReferenceType byReferenceType)
        {
            return new ByReferenceType(
                module.ImportMockMeReference(byReferenceType.ElementType, methodDefinition)
            );
        }
        if (typeRef is GenericParameter genericParamRef)
        {
            return genericParamRef.ToImportedParameter(methodDefinition);
        }
        if (typeRef is GenericInstanceType genericInstanceType)
        {
            var importedInstanceType = new GenericInstanceType(
                module.ImportMockMeReference(genericInstanceType.ElementType, methodDefinition)
            );
            foreach (var genericParam in genericInstanceType.GenericParameters)
            {
                importedInstanceType.GenericArguments.Add(
                    module.ImportMockMeReference(genericParam, methodDefinition)
                );
            }
            foreach (var genericArg in genericInstanceType.GenericArguments)
            {
                importedInstanceType.GenericArguments.Add(
                    module.ImportMockMeReference(genericArg, methodDefinition)
                );
            }
            return importedInstanceType;
        }
        else
        {
            return module.ImportReference(typeRef);
        }
    }

    public static MethodReference ImportMockMeReference(
        this ModuleDefinition module,
        MethodReference methodRef,
        TypeReference? declaringType = null
    )
    {
        MethodDefinition methodDefinition = methodRef.Resolve();
        if (declaringType == null)
        {
            declaringType = module.ImportMockMeReference(methodRef.DeclaringType, methodDefinition);
        }
        else
        {
            declaringType = module.ImportMockMeReference(declaringType, methodDefinition);
        }

        var returnType = module.ImportMockMeReference(methodRef.ReturnType, methodDefinition);

        module.ImportReference(methodRef);
        var newMethodRef = new MethodReference(methodRef.Name, returnType, declaringType)
        {
            HasThis = methodRef.HasThis,
            ExplicitThis = methodRef.ExplicitThis,
            CallingConvention = methodRef.CallingConvention,
        };
        foreach (var parameter in methodRef.Parameters)
        {
            newMethodRef.Parameters.Add(
                new ParameterDefinition(
                    module.ImportMockMeReference(parameter.ParameterType, methodDefinition)
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
                    genericParam.ToImportedParameter(methodDefinition)
                );
            }
        }
        module.ImportReference(newMethodRef);
        return newMethodRef;
    }

    public static FieldReference ImportMockMeReference(
        this ModuleDefinition module,
        FieldReference fieldRef
    )
    {
        var declaringType = module.ImportReference(fieldRef.DeclaringType);
        return new FieldReference(
            fieldRef.Name,
            module.ImportReference(fieldRef.FieldType),
            declaringType
        );
    }

    public static MethodReference GetType(this ModuleDefinition module, Type[] types)
    {
        ;
    }
}
