using System;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using MockMe.Generator.Extensions;

namespace MockMe.Generator.MockGenerators.PatchMethodGenerators;

internal class GenericClassMethodPatchMethodGenerator(
    INamedTypeSymbol typeSymbol,
    IMethodSymbol methodSymbol
) : ClassMethodPatchMethodGenerator(typeSymbol, methodSymbol)
{
    public override void AddPatchMethod(
        StringBuilder sb,
        StringBuilder assemblyAttributesSource,
        StringBuilder staticConstructor,
        string typeSymbolName
    )
    {
        var methodTypes = this
            .MethodSymbol.Parameters.Select(p => p.Type)
            .Concat([this.MethodSymbol.ReturnType]);

        if (methodTypes.Any(this.TypeSymbol.TypeParameters.Contains))
        {
            this.AddPatchMethodWhenMethodUsesClassParameter(
                sb,
                assemblyAttributesSource,
                typeSymbolName
            );
        }
        else
        {
            base.AddPatchMethod(sb, assemblyAttributesSource, staticConstructor, typeSymbolName);
        }
    }

    private void AddPatchMethodWhenMethodUsesClassParameter(
        StringBuilder sb,
        StringBuilder assemblyAttributesSource,
        string typeSymbolName
    )
    {
        var thisNamespace = $"MockMe.Generated.{this.TypeSymbol.ContainingNamespace}";
        string paramsWithTypesAndMods =
            this.MethodSymbol.GetParametersWithOriginalTypesAndModifiers();
        string paramTypeString = this.MethodSymbol.GetParameterTypesWithoutModifiers();
        string paramString = this.MethodSymbol.GetParametersWithoutTypesAndModifiers();

        var returnType = this.MethodSymbol.ReturnType.ToFullReturnTypeString();

        var isVoidReturnType = this.MethodSymbol.ReturnType.SpecialType == SpecialType.System_Void;

        string callEnd = this.MethodSymbol.MethodKind switch
        {
            MethodKind.PropertyGet => "",
            MethodKind.PropertySet => $" = {paramString}",
            _ => $"({paramString})",
        };

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

                {(isVoidReturnType ? "" : $"return ({returnType})")}
                    callTracker
                        .GetType()
                        .GetMethod(
                            ""{this.MethodSymbol.Name}"",
                            global::System.Reflection.BindingFlags.Public
                                | global::System.Reflection.BindingFlags.Instance
                        )
                        {string.Join(", ", this.MethodSymbol.TypeParameters.Select(p => p.Name.AddOnIfNotEmpty("typeof(", ")"))) .AddOnIfNotEmpty(".MakeGenericMethod(", ")")}
                        .Invoke(callTracker, new object[] {{ {paramString} }});
            }}
            return{(isVoidReturnType ? "" : " default")};
        }}
"
        );

        assemblyAttributesSource.AppendLine(
            $@"
[assembly: global::MockMe.Abstractions.GenericMethodDefinition(
    ""{this.TypeSymbol.ContainingNamespace}"",
    ""{this.TypeSymbol.ContainingNamespace}.{this.TypeSymbol.MetadataName}"",
    ""{this.MethodSymbol.Name}"",
    ""{thisNamespace}"",
    ""{thisNamespace}.{typeSymbolName}Mock{this.TypeSymbol.MetadataName[^2..]}"",
    ""{this.MethodSymbol.Name}""
)]
"
        );
    }
}
