using System.Text;
using Microsoft.CodeAnalysis;
using MockMe.Generator.Extensions;

namespace MockMe.Generator.MockGenerators.Concrete;

internal class CallTrackerGenerator
{
    public static StringBuilder CreateCallTrackerForConcreteType(
        ITypeSymbol typeSymbol,
        StringBuilder? sb = null
    )
    {
        sb ??= new();

        sb.AppendLine(
            $@"
        public class {typeSymbol.Name}MockCallTracker : MockCallTracker
        {{
            private readonly {typeSymbol.Name}MockSetup setup;
            public {typeSymbol.Name}MockCallTracker({typeSymbol.Name}MockSetup setup)
            {{
                this.setup = setup;
            }}"
        );

        foreach (var method in typeSymbol.GetMembers())
        {
            if (method is not IMethodSymbol methodSymbol)
            {
                continue;
            }

            var returnType = methodSymbol.ReturnType.ToDisplayString();
            var methodName = methodSymbol.Name;

            if (methodName == ".ctor")
            {
                continue;
            }

            string argCollection = methodSymbol.GetMethodArgumentsAsCollection();
            string paramsWithTypesAndMods =
                methodSymbol.GetParametersWithOriginalTypesAndModifiers();
            string paramString = methodSymbol.GetParametersWithoutTypesAndModifiers();

            sb.AppendLine(
                $@"
            private List<{argCollection}>? {methodName}CallStore;

            public {returnType} {methodName}({paramsWithTypesAndMods}) => CallMemberMock(this.setup.{methodName}BagStore, this.{methodName}CallStore ??= new(), {paramString});"
            );
        }

        AsserterGenerator.CreateMemberAsserterForConcreteType(typeSymbol, sb);

        sb.AppendLine(
            @$"
        }}"
        );

        return sb;
    }
}
