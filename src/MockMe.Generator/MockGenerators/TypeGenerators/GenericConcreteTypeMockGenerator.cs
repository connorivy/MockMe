using System;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using MockMe.Generator.Extensions;

namespace MockMe.Generator.MockGenerators.TypeGenerators;

internal class GenericConcreteTypeMockGenerator(INamedTypeSymbol typeSymbol, string typeName)
    : ConcreteMockGenerator(typeSymbol, typeName)
{
    public override void AddPatchMethod(
        StringBuilder sb,
        StringBuilder assemblyAttributesSource,
        StringBuilder staticConstructor,
        IMethodSymbol methodSymbol,
        string typeSymbolName
    )
    {
        var methodTypes = methodSymbol
            .Parameters.Select(p => p.Type)
            .Concat([methodSymbol.ReturnType]);

        if (methodTypes.Any(this.TypeSymbolToMock.TypeParameters.Contains))
        {
            this.AddPatchMethodWhenMethodUsesClassParameter(
                sb,
                assemblyAttributesSource,
                staticConstructor,
                methodSymbol,
                typeSymbolName
            );
        }
        else
        {
            base.AddPatchMethod(
                sb,
                assemblyAttributesSource,
                staticConstructor,
                methodSymbol,
                typeSymbolName
            );
        }
    }

    private void AddPatchMethodWhenMethodUsesClassParameter(
        StringBuilder sb,
        StringBuilder assemblyAttributesSource,
        StringBuilder staticConstructor,
        IMethodSymbol methodSymbol,
        string typeSymbolName
    )
    {
        var thisNamespace = $"MockMe.Generated.{this.TypeSymbolToMock.ContainingNamespace}";
        string paramsWithTypesAndMods = methodSymbol.GetParametersWithOriginalTypesAndModifiers();
        string paramTypeString = methodSymbol.GetParameterTypesWithoutModifiers();
        string paramString = methodSymbol.GetParametersWithoutTypesAndModifiers();

        var returnType = methodSymbol.ReturnType.ToFullReturnTypeString();

        var isVoidReturnType = methodSymbol.ReturnType.SpecialType == SpecialType.System_Void;

        string callEnd = methodSymbol.MethodKind switch
        {
            MethodKind.PropertyGet => "",
            MethodKind.PropertySet => $" = {paramString}",
            _ => $"({paramString})",
        };

        //if (methodSymbol.TypeParameters.Length == 0)
        //{
        //    string patchName = $"Patch{Guid.NewGuid():N}";
        //    sb.AppendLine(
        //        $@"
        //internal sealed class {patchName}
        //{{
        //    private static bool Prefix({this.TypeSymbol.ToFullTypeString()} __instance{(isVoidReturnType ? string.Empty : $", ref {returnType} __result")}{paramsWithTypesAndMods.AddPrefixIfNotEmpty(", ")})
        //    {{
        //        if (global::MockMe.MockStore<{this.TypeSymbol.ToFullTypeString()}>.TryGetValue<{typeSymbolName}Mock>(__instance, out var mock))
        //        {{
        //            {(isVoidReturnType ? string.Empty : "__result = ")}mock.CallTracker.{methodSymbol.GetPropertyName()}{callEnd};
        //            return false;
        //        }}
        //        return true;
        //    }}
        //}}"
        //    );

        //    staticConstructor.AppendLine(
        //        $@"
        //    var original{patchName} = typeof({this.TypeSymbol.ToFullTypeString()}).GetMethod(""{methodSymbol.Name}"", new Type[] {{ {string.Join(", ", methodSymbol.Parameters.Select(p => "typeof(" + p.Type.ToFullTypeString() + ")"))} }} );
        //    var {patchName} = typeof({patchName}).GetMethod(""Prefix"", global::System.Reflection.BindingFlags.Static | global::System.Reflection.BindingFlags.NonPublic);

        //    harmony.Patch(original{patchName}, prefix: new HarmonyMethod({patchName}));"
        //    );
        //}
        //else
        //{
        sb.AppendLine(
            $@"
        private {returnType} {methodSymbol.Name}{methodSymbol.GetGenericParameterStringInBrackets()}({paramsWithTypesAndMods})
        {{
            if (global::MockMe.MockStore<{this.TypeSymbolToMock.ToFullTypeString()}>.GetStore().TryGetValue(default, out var mock))
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
                            ""{methodSymbol.Name}"",
                            global::System.Reflection.BindingFlags.Public
                                | global::System.Reflection.BindingFlags.Instance
                        )
                        {string.Join(", ", methodSymbol.TypeParameters.Select(p => p.Name.AddOnIfNotEmpty("typeof(", ")"))) .AddOnIfNotEmpty(".MakeGenericMethod(", ")")}
                        .Invoke(callTracker, new object[] {{ {paramString} }});
            }}
            return default;
        }}
"
        );

        assemblyAttributesSource.AppendLine(
            $@"
[assembly: global::MockMe.Abstractions.GenericMethodDefinition(
    ""{this.TypeSymbolToMock.ContainingNamespace}"",
    ""{this.TypeSymbolToMock.ContainingNamespace}.{this.TypeSymbolToMock.MetadataName}"",
    ""{methodSymbol.Name}"",
    ""{thisNamespace}"",
    ""{thisNamespace}.{this.TypeName}Mock{this.TypeSymbolToMock.MetadataName[^2..]}"",
    ""{methodSymbol.Name}""
)]
"
        );
        //}
    }
}
