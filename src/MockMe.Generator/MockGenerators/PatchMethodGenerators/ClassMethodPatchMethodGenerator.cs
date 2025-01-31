using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using MockMe.Generator.Extensions;

namespace MockMe.Generator.MockGenerators.PatchMethodGenerators;

internal class ClassMethodPatchMethodGenerator(
    INamedTypeSymbol typeSymbol,
    IMethodSymbol methodSymbol
) : IPatchMethodGenerator
{
    protected INamedTypeSymbol TypeSymbol => typeSymbol;
    protected IMethodSymbol MethodSymbol => methodSymbol;

    public virtual void AddPatchMethod(
        StringBuilder sb,
        StringBuilder assemblyAttributesSource,
        StringBuilder staticConstructor,
        string typeSymbolName
    )
    {
        var thisNamespace = $"MockMe.Generated.{this.TypeSymbol.ContainingNamespace}";
        string paramsWithTypesAndMods =
            this.MethodSymbol.GetParametersWithOriginalTypesAndModifiers();
        string paramTypeString = this.MethodSymbol.GetParameterTypesWithoutModifiers();
        string paramString = this.MethodSymbol.GetParametersWithModifiersAndNoTypes();

        var returnType = this.MethodSymbol.ReturnType.ToFullReturnTypeString();

        var isVoidReturnType = this.MethodSymbol.ReturnType.SpecialType == SpecialType.System_Void;

        List<IParameterSymbol> outParameters = this
            .MethodSymbol.Parameters.Where(p => p.RefKind is RefKind.Out)
            .ToList();

        string callEnd = this.MethodSymbol.MethodKind switch
        {
            MethodKind.PropertyGet => "",
            MethodKind.PropertySet => $" = {paramString}",
            _ => $"({paramString})",
        };

        if (this.MethodSymbol.TypeParameters.Length == 0)
        {
            string patchName = $"Patch{Guid.NewGuid():N}";
            sb.AppendLine(
                $@"
        internal sealed class {patchName}
        {{
            private static bool Prefix({this.TypeSymbol.ToFullTypeString()} __instance{(isVoidReturnType ? string.Empty : $", ref {returnType} __result")}{paramsWithTypesAndMods.AddPrefixIfNotEmpty(", ")})
            {{
                if (global::MockMe.MockStore<{this.TypeSymbol.ToFullTypeString()}>.TryGetValue<{typeSymbolName}Mock>(__instance, out var mock))
                {{
                    {(isVoidReturnType ? string.Empty : "__result = ")}mock.CallTracker.{this.MethodSymbol.GetPropertyName()}{callEnd};
                    return false;
                }}"
            );

            foreach (var p in outParameters)
            {
                sb.Append(
                    $@"
                {p.Name} = default({p.Type.ToFullTypeString()});"
                );
            }

            sb.Append(
                $@"
                return true;
            }}
        }}"
            );

            staticConstructor.AppendLine(
                $@"
            var original{patchName} = typeof({this.TypeSymbol.ToFullTypeString()}).GetMethod(""{this.MethodSymbol.Name}"", new Type[] {{ {string.Join(", ", this.MethodSymbol.Parameters.Select(p => "typeof(" + p.Type.ToFullTypeString() + ")" + (p.RefKind is not RefKind.None ? ".MakeByRefType()" : "")))} }} );
            var {patchName} = typeof({patchName}).GetMethod(""Prefix"", global::System.Reflection.BindingFlags.Static | global::System.Reflection.BindingFlags.NonPublic);

            harmony.Patch(original{patchName}, prefix: new HarmonyMethod({patchName}));"
            );
        }
        else
        {
            sb.AppendLine(
                $@"
        private {returnType} {this.MethodSymbol.Name}{this.MethodSymbol.GetGenericParameterStringInBrackets()}({paramsWithTypesAndMods})
        {{
            if (global::MockMe.MockStore<{this.TypeSymbol.ToFullTypeString()}>.GetStore().TryGetValue(default, out var mock))
            {{
                var callTracker = mock.GetType()
                    .GetProperty(
                        ""CallTracker"",
                        global::System.Reflection.BindingFlags.NonPublic
                            | global::System.Reflection.BindingFlags.Instance
                    )
                    .GetValue(mock);

                return ({returnType})
                    callTracker
                        .GetType()
                        .GetMethod(
                            ""{this.MethodSymbol.Name}"",
                            global::System.Reflection.BindingFlags.Public
                                | global::System.Reflection.BindingFlags.Instance
                        )
                        .MakeGenericMethod({string.Join(", ", this.MethodSymbol.TypeParameters.Select(p => p.Name.AddOnIfNotEmpty("typeof(", ")")))})
                        .Invoke(callTracker, new object[] {{ {paramString} }});
            }}
            return default;
        }}
"
            );

            assemblyAttributesSource.AppendLine(
                this.TypeSymbol.GetGenericMethodDefinitionAttribute(
                    this.MethodSymbol.Name,
                    thisNamespace
                )
            );
        }
    }
}
