using MockMe.Abstractions;
using Mono.Cecil;

namespace MockMe.PostBuild.Extensions;

internal static class GenericMethodInfoExtensions
{
    public static (TypeDefinition, MethodDefinition) GetMethodInfo(
        this GenericMethodInfo methodInfo,
        ModuleDefinition module
    )
    {
        TypeDefinition typeToReplace = module.GetType(methodInfo.TypeFullName);
        MethodDefinition methodToReplace = typeToReplace.Methods.First(m =>
            m.Name == methodInfo.MethodName
        );

        return (typeToReplace, methodToReplace);
    }
}
