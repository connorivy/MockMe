using Mono.Cecil;

namespace MockMe.PostBuild.Extensions;

internal static class GenericParameterExtensions
{
    public static GenericParameter ToImportedParameter(
        this GenericParameter genericParamRef,
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
            return genericParamRef.Owner.GenericParameters[genericParamRef.Position];
            //var declaringType = methodToReplace.DeclaringType;
            //return declaringType.GenericParameters[genericParamRef.Position];
        }
    }
}
