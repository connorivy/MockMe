using System.Text;

namespace MockMe.Generator.MockGenerators.PatchMethodGenerators;

internal interface IPatchMethodGenerator
{
    void AddPatchMethod(
        StringBuilder sb,
        StringBuilder assemblyAttributesSource,
        StringBuilder staticConstructor,
        string typeSymbolName
    );
}
