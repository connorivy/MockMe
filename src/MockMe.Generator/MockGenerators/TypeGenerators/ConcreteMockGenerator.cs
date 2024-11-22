using System;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using MockMe.Generator.Extensions;

namespace MockMe.Generator.MockGenerators.TypeGenerators;

internal class ConcreteMockGenerator(INamedTypeSymbol typeSymbol, string typeName)
    : MockGeneratorBase(typeSymbol, typeName)
{
    public override string GetCallTrackerBaseClass(ITypeSymbol typeSymbol) => "MockCallTracker";

    public override string GetConstructorAndProps(ITypeSymbol typeSymbol) =>
        $@"
        public {this.TypeName}Mock()
        {{
            this.Setup = new {this.mockSetupTypeName}();
            this.CallTracker = new {this.mockSetupTypeName}.{this.mockCallTrackerTypeName}(this.Setup);
            this.Assert = new {this.mockSetupTypeName}.{this.mockCallTrackerTypeName}.{this.mockAsserterTypeName}(this.CallTracker);
            global::MockMe.MockStore<{typeSymbol.ToFullTypeString()}>.Store.TryAdd(this.MockedObject, this);
        }}

        public {this.mockSetupTypeName} Setup {{ get; }}
        public {this.mockSetupTypeName}.{this.mockCallTrackerTypeName}.{this.mockAsserterTypeName} Assert {{ get; }}
        private {this.mockSetupTypeName}.{this.mockCallTrackerTypeName} CallTracker {{ get; }}";

    public override string GetMockBaseClass(ITypeSymbol typeSymbol) =>
        $"global::MockMe.Abstractions.SealedTypeMock<{typeSymbol.ToFullTypeString()}>";

    public override void AddPatchMethod(
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

        if (methodSymbol.TypeParameters.Length == 0)
        {
            string patchName = $"Patch{Guid.NewGuid():N}";
            sb.AppendLine(
                $@"
        internal sealed class {patchName}
        {{
            private static bool Prefix({this.TypeSymbolToMock.ToFullTypeString()} __instance{(isVoidReturnType ? string.Empty : $", ref {returnType} __result")}{paramsWithTypesAndMods.AddPrefixIfNotEmpty(", ")})
            {{
                if (global::MockMe.MockStore<{this.TypeSymbolToMock.ToFullTypeString()}>.TryGetValue<{typeSymbolName}Mock>(__instance, out var mock))
                {{
                    {(isVoidReturnType ? string.Empty : "__result = ")}mock.CallTracker.{methodSymbol.GetPropertyName()}{callEnd};
                    return false;
                }}
                return true;
            }}
        }}"
            );

            staticConstructor.AppendLine(
                $@"
            var original{patchName} = typeof({this.TypeSymbolToMock.ToFullTypeString()}).GetMethod(""{methodSymbol.Name}"", new Type[] {{ {string.Join(", ", methodSymbol.Parameters.Select(p => "typeof(" + p.Type.ToFullTypeString() + ")"))} }} );
            var {patchName} = typeof({patchName}).GetMethod(""Prefix"", global::System.Reflection.BindingFlags.Static | global::System.Reflection.BindingFlags.NonPublic);

            harmony.Patch(original{patchName}, prefix: new HarmonyMethod({patchName}));"
            );
        }
        else
        {
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
                        .MakeGenericMethod({string.Join(", ", methodSymbol.TypeParameters.Select(p => p.Name.AddOnIfNotEmpty("typeof(", ")")))})
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
    ""{this.TypeSymbolToMock}"",
    ""{methodSymbol.Name}"",
    ""{thisNamespace}"",
    ""{thisNamespace}.{this.TypeSymbolToMock.Name}Mock"",
    ""{methodSymbol.Name}""
)]
"
            );
        }
    }
}
