using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using MockMe.Generator.Extensions;
using MockMe.Generator.MockGenerators.TypeGenerators;

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

        System.Diagnostics.Debugger.Launch();
        context.RegisterSourceOutput(
            compilationAndMethods,
            (ctx, source) =>
            {
                var sourceBuilder = new StringBuilder();
                StringBuilder assemblyAttributesSource = new();
                sourceBuilder.AppendLine(
                    @"
// Auto-generated by MethodCallFinderIncrementalGenerator
#nullable enable"
                );
                sourceBuilder.AppendLine(
                    @$"
namespace {NamespaceName}
{{
    internal static partial class {StoreClassName}
    {{
"
                );

                Dictionary<string, int> typeNameUsageCounts = [];
                Dictionary<ITypeSymbol, string> createMockSymbolToNameDict = [];
                foreach (var typeToMock in GetTypesToBeMocked(source.Left, source.Right))
                {
                    string patchCall = "";
                    //if (typeToMock.TypeKind != TypeKind.Interface)
                    //{
                    //    patchCall = "EnsurePatch();";
                    //}

                    string genericConstraint;
                    if (typeToMock.IsSealed)
                    {
                        genericConstraint = "";
                    }
                    else
                    {
                        genericConstraint = $"where T : {typeToMock.ToFullTypeString()}";
                    }

                    bool mockExists = false;
                    var typeSymbolToMock = MockGeneratorFactory.GetTypeSymbolToMock(typeToMock);
                    if (
                        !createMockSymbolToNameDict.TryGetValue(
                            typeSymbolToMock,
                            out var typeToMockName
                        )
                    )
                    {
                        if (typeNameUsageCounts.TryGetValue(typeToMock.Name, out int numUsages))
                        {
                            typeToMockName = typeToMock.Name + $"_{numUsages}";
                            typeNameUsageCounts[typeToMock.Name] = numUsages + 1;
                        }
                        else
                        {
                            typeToMockName = typeToMock.Name;
                            typeNameUsageCounts.Add(typeToMock.Name, 1);
                        }
                        createMockSymbolToNameDict.Add(typeSymbolToMock, typeToMockName);
                    }
                    else
                    {
                        mockExists = true;
                    }

                    var genericArgs = string.Join(
                            ", ",
                            typeToMock.TypeArguments.Select(p => p.ToFullTypeString())
                        )
                        .AddOnIfNotEmpty("<", ">");

                    sourceBuilder.AppendLine(
                        @$"
        public static global::MockMe.Generated.{typeToMock.ContainingNamespace}.{typeToMockName}Mock{genericArgs} {StoreMethodName}<T>(global::{typeToMock}? unusedInstance{(typeToMock.IsSealed ? "" : " = null")})
            {genericConstraint}
        {{
            {patchCall}
            return new();
        }}"
                    );

                    if (!mockExists)
                    {
                        string newMockCode = MockGeneratorFactory
                            .Create(typeToMock, typeToMockName)
                            .CreateMockType(assemblyAttributesSource)
                            .ToString();

                        ctx.AddSource(
                            $"{typeToMockName}Mock.g.cs",
                            SourceText.From(newMockCode, Encoding.UTF8)
                        );
                    }
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

                ctx.AddSource(
                    $"AssemblyAttributes.g.cs",
                    SourceText.From(assemblyAttributesSource.ToString(), Encoding.UTF8)
                );
            }
        );
    }

    private static IEnumerable<INamedTypeSymbol> GetTypesToBeMocked(
        Compilation compilation,
        ImmutableArray<InvocationExpressionSyntax> methods
    )
    {
        if (methods.IsDefaultOrEmpty)
        {
            yield break;
        }

        HashSet<ITypeSymbol> usedSymbols = new();
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

                if (
                    genericArgSymbol is not null
                    && genericArgSymbol.TypeKind != TypeKind.Error
                    && genericArgSymbol is INamedTypeSymbol namedTypeSymbol
                    && usedSymbols.Add(namedTypeSymbol)
                )
                {
                    yield return namedTypeSymbol;
                }
            }
        }
    }

    //private static void GenerateMock(ITypeSymbol typeSymbol) { }

    private static void GenerateStaticClass(IncrementalGeneratorPostInitializationContext context)
    {
        const string mockStoreSource =
            $@"
using System;

namespace {NamespaceName}
{{
    internal static partial class {StoreClassName}
    {{
        public static Mock<T> {StoreMethodName}<T>(global::{NamespaceName}.DummyClass unusedInstance)
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

        //AppDomain.CurrentDomain.TypeResolve += new ResolveEventHandler(HandleTypeResolve);
        //// Load the assembly
        //var assemblyPath = Path.Combine(
        //    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
        //    "MockMe.SampleMocks.dll"
        //);
        //var assembly = AssemblyDefinition.ReadAssembly(assemblyPath);
        //// Find the type to delete
        ////
        //var typeToRemove = assembly.MainModule.Types.FirstOrDefault(t => t.Name == "Calculator");
        //if (typeToRemove != null)
        //{
        //    assembly.MainModule.Types.Remove(typeToRemove);
        //}
        //var outputPath = "MockMe.SampleMocks.dll";
    }

    //static Assembly HandleTypeResolve(object sender, ResolveEventArgs args)
    //{
    //    Console.WriteLine("TypeResolve event handler.");

    //    // Save the dynamic assembly, and then load it using its
    //    // display name. Return the loaded assembly.
    //    //
    //    //ab.Save(moduleName);
    //    return Assembly.Load(Assembly.GetExecutingAssembly().Location);
    //}
}
