using MockMe.Abstractions;
using Mono.Cecil;

namespace MockMe.PostBuild.Extensions;

internal static class AssemblyDefinitionExtensions
{
    public static List<MockReplacementInfo> GetMockReplacementInfo(
        this AssemblyDefinition assemblyDefinition
    )
    {
        List<MockReplacementInfo> genericTypes = [];
        foreach (var assemblyAttr in assemblyDefinition.CustomAttributes)
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
        return genericTypes;
    }
}
