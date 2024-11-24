using System;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using MockMe.Generator.Extensions;

namespace MockMe.Generator.MockGenerators.PatchMethodGenerators;

internal class IndexerPatchMethodGenerator(INamedTypeSymbol typeSymbol, IMethodSymbol methodSymbol)
    : IPatchMethodGenerator
{
    public void AddPatchMethod(
        StringBuilder sb,
        StringBuilder assemblyAttributesSource,
        StringBuilder staticConstructor,
        string typeSymbolName
    )
    {
        var returnType = methodSymbol.ReturnType.ToFullReturnTypeString();

        var isVoidReturnType = methodSymbol.ReturnType.SpecialType == SpecialType.System_Void;

        var thisNamespace = $"MockMe.Generated.{typeSymbol.ContainingNamespace}";
        string paramsWithTypesAndMods = methodSymbol.GetParametersWithOriginalTypesAndModifiers();
        string paramTypeString = methodSymbol.GetParameterTypesWithoutModifiers();

        const string indexPrefixToTrim = "index, ";
        string paramString = methodSymbol.GetParametersWithoutTypesAndModifiers();

        string callEnd = methodSymbol.MethodKind switch
        {
            MethodKind.PropertyGet => "",
            MethodKind.PropertySet => $" = {paramString[indexPrefixToTrim.Length..]}",
            _ => $"({paramString})",
        };

        string patchName = $"Patch{Guid.NewGuid():N}";
        sb.AppendLine(
            $@"
    internal sealed class {patchName}
    {{
        private static bool Prefix({typeSymbol.ToFullTypeString()} __instance{(isVoidReturnType ? string.Empty : $", ref {returnType} __result")}{paramsWithTypesAndMods.AddPrefixIfNotEmpty(", ")})
        {{
            if (global::MockMe.MockStore<{typeSymbol.ToFullTypeString()}>.TryGetValue<{typeSymbolName}Mock>(__instance, out var mock))
            {{
                {(isVoidReturnType ? string.Empty : "__result = ")}mock.CallTracker[index]{callEnd};
                return false;
            }}
            return true;
        }}
    }}"
        );

        staticConstructor.AppendLine(
            $@"
        var original{patchName} = typeof({typeSymbol.ToFullTypeString()}).GetMethod(""{methodSymbol.Name}"", new Type[] {{ {string.Join(", ", methodSymbol.Parameters.Select(p => "typeof(" + p.Type.ToFullTypeString() + ")"))} }} );
        var {patchName} = typeof({patchName}).GetMethod(""Prefix"", global::System.Reflection.BindingFlags.Static | global::System.Reflection.BindingFlags.NonPublic);

        harmony.Patch(original{patchName}, prefix: new HarmonyMethod({patchName}));"
        );
    }
}
