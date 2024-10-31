using System.Text;
using Microsoft.CodeAnalysis;
using MockMe.Generator.Extensions;

namespace MockMe.Generator.MockGenerators.Concrete;

internal class AsserterGenerator
{
    public static StringBuilder CreateMemberAsserterForConcreteType(
        ITypeSymbol typeSymbol,
        StringBuilder? sb = null
    )
    {
        sb ??= new();

        sb.AppendLine(
            $@"
            public class {typeSymbol.Name}MockAsserter : MockAsserter
            {{
                private readonly {typeSymbol.Name}MockCallTracker tracker;
                public {typeSymbol.Name}MockAsserter({typeSymbol.Name}MockCallTracker tracker)
                {{
                    this.tracker = tracker;
                }}"
        );

        foreach (var method in typeSymbol.GetMembers())
        {
            if (method is not IMethodSymbol methodSymbol)
            {
                continue;
            }

            var methodName = methodSymbol.Name;

            if (methodName == ".ctor")
            {
                continue;
            }

            if (methodSymbol.MethodKind == MethodKind.PropertyGet)
            {
                continue;
            }

            if (methodSymbol.MethodKind == MethodKind.PropertySet)
            {
                continue;
            }

            int numParameters = methodSymbol.Parameters.Length;
            if (numParameters == 0)
            {
                sb.AppendLine(
                    $@"
                public MemberAsserter {methodName}() =>
                    new(this.tracker.{methodName}CallStore);"
                );
            }
            else
            {
                var parametersDefinition = methodSymbol.GetParametersWithArgTypesAndModifiers();
                var parameters = methodSymbol.GetParametersWithoutTypesAndModifiers();

                sb.AppendLine(
                    $@"
                public MemberAsserter {methodName}({parametersDefinition})
                {{
                    return GetMemberAsserter(this.tracker.{methodName}CallStore, {parameters});
                }}"
                );
            }
        }

        sb.AppendLine(
            $@"
            }}"
        );

        return sb;
    }
}
