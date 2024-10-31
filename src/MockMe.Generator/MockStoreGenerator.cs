using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using MockMe.Generator.MockGenerators.Concrete;

namespace MockMe.Generator;

[Generator]
public class MockStoreGenerator : IIncrementalGenerator
{
    private const string StoreClassName = "Mock";
    private const string StoreMethodName = "Me";
    private const string NamespaceName = "MockMe";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(GenerateStaticClass);
        var methodDeclarations = context
            .SyntaxProvider.CreateSyntaxProvider(
                predicate: static (s, _) => s is InvocationExpressionSyntax,
                transform: static (ctx, _) => (InvocationExpressionSyntax)ctx.Node
            )
            .Where(node => node != null);

        var compilationAndMethods = context.CompilationProvider.Combine(
            methodDeclarations.Collect()
        );

        //System.Diagnostics.Debugger.Launch();
        context.RegisterSourceOutput(
            compilationAndMethods,
            (ctx, source) =>
            {
                var sourceBuilder = new StringBuilder();
                sourceBuilder.AppendLine(
                    @"
// Auto-generated by MethodCallFinderIncrementalGenerator
#nullable enable"
                );
                sourceBuilder.AppendLine(
                    @$"
namespace {NamespaceName}
{{
    public static partial class {StoreClassName}
    {{
"
                );

                Dictionary<string, int> typeNameUsageCounts = [];
                foreach (var typeToMock in GetTypesToBeMocked(source.Left, source.Right))
                {
                    sourceBuilder.AppendLine(
                        @$"
        public static global::MockMe.Generated.{typeToMock.ContainingNamespace}.{typeToMock.Name}Mock {StoreMethodName}<T>(global::{typeToMock}? unusedInstance = null) 
            where T : global::{typeToMock} 
        {{
            EnsurePatch();
            return new();
        }}"
                    );

                    string classNameSuffix = string.Empty;
                    if (typeNameUsageCounts.TryGetValue(typeToMock.Name, out int numUsages))
                    {
                        classNameSuffix = $"-{numUsages}";
                        typeNameUsageCounts[typeToMock.Name] = numUsages + 1;
                    }
                    else
                    {
                        typeNameUsageCounts.Add(typeToMock.Name, 1);
                    }

                    ctx.AddSource(
                        $"{typeToMock.Name}Mock{classNameSuffix}.g.cs",
                        SourceText.From(
                            MockGenerator.CreateMockForConcreteType(typeToMock).ToString(),
                            Encoding.UTF8
                        )
                    );
                }

                sourceBuilder.AppendLine(
                    @$"
    }}
}}
"
                );

                ctx.AddSource(
                    $"{StoreClassName}.g.cs",
                    SourceText.From(sourceBuilder.ToString(), Encoding.UTF8)
                );
            }
        );
    }

    private static IEnumerable<ITypeSymbol> GetTypesToBeMocked(
        Compilation compilation,
        ImmutableArray<InvocationExpressionSyntax> methods
    )
    {
        if (methods.IsDefaultOrEmpty)
        {
            yield break;
        }

        HashSet<ITypeSymbol> usedSymbols = [];
        foreach (var method in methods.Distinct())
        {
            if (
                method.Expression is MemberAccessExpressionSyntax memberAccess
                && memberAccess.Expression is IdentifierNameSyntax identifierName
                && identifierName.Identifier.Text == StoreClassName
                && memberAccess.Name is GenericNameSyntax genericName
                && genericName.TypeArgumentList.Arguments.Count == 1
                && genericName.Identifier.Text == StoreMethodName
            )
            {
                var model = compilation.GetSemanticModel(method.SyntaxTree);
                var genericArgSymbol = model
                    .GetTypeInfo(genericName.TypeArgumentList.Arguments[0])
                    .Type;

                if (genericArgSymbol is not null && usedSymbols.Add(genericArgSymbol))
                {
                    yield return genericArgSymbol;
                }
            }
        }
    }

    //private static void GenerateMock(ITypeSymbol typeSymbol) { }

    private static void GenerateStaticClass(IncrementalGeneratorPostInitializationContext context)
    {
        const string mockStoreSource =
            $@"
namespace {NamespaceName}
{{
    public static partial class {StoreClassName}
    {{
        public static Mock<T> {StoreMethodName}<T>(global::{NamespaceName}.DummyClass unusedInstance = null)
            where T : global::{NamespaceName}.DummyClass
        {{
            throw new NotImplementedException();
        }}

        private static bool isPatched;
        private static readonly object LockObj = new();

        private static void EnsurePatch()
        {{
            lock (LockObj)
            {{
                if (!isPatched)
                {{
                    var harmony = new global::HarmonyLib.Harmony(""com.mockme.patch"");
                    harmony.PatchAll();
                    isPatched = true;
                }}
            }}
        }}
    }}
}}
";
        context.AddSource(
            "Mock.DummyDeclaration.g.cs",
            SourceText.From(mockStoreSource, Encoding.UTF8)
        );
    }
}
