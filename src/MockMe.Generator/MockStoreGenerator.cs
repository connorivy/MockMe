using Microsoft.CodeAnalysis;

namespace MockMe.Generator;

[Generator]
public class MockStoreGenerator : ISourceGenerator
{
    public void Execute(GeneratorExecutionContext context)
    {
        string namespaceName = "MockMe";

        // Code generation goes here
        string mockStoreSource =
            $@"
namespace {namespaceName}
{{
    public static class Mock
    {{
        public static Mock<T> Me<T>() => throw new NotImplementedException();
    }}
}}
";

        context.AddSource($"Mock.g.cs", mockStoreSource);
    }

    public void Initialize(GeneratorInitializationContext context)
    {
        // No initialization required for this one
    }
}
