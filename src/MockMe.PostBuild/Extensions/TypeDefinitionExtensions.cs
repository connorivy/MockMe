using Mono.Cecil;

namespace MockMe.PostBuild.Extensions;

internal static class TypeDefinitionExtensions
{
    public static MethodReference GetMethod(
        this TypeDefinition type,
        string methodName,
        params Type[] parameterTypes
    )
    {
        foreach (var method in type.Methods)
        {
            if (method.FullName != methodName || method.Parameters.Count != parameterTypes.Length)
            {
                continue;
            }

            bool match = true;
            for (int i = 0; i < parameterTypes.Length; i++)
            {
                var paramType = method.Parameters[i].ParameterType;
                if (
                    paramType.IsGenericParameter
                    || paramType.FullName != parameterTypes[i].FullName
                )
                {
                    match = false;
                    break;
                }
            }
            if (match)
            {
                return method;
            }
        }

        throw new InvalidOperationException();
    }
}
