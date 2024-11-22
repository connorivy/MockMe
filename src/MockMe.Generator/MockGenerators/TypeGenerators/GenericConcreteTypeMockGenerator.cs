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
                methodSymbol
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
        IMethodSymbol methodSymbol
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
    }
}
