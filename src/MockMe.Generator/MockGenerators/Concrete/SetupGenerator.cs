using System.Text;
using Microsoft.CodeAnalysis;
using MockMe.Generator.Extensions;

namespace MockMe.Generator.MockGenerators.Concrete;

internal class SetupGenerator
{
    public static StringBuilder CreateSetupForConcreteType(
        ITypeSymbol typeSymbol,
        StringBuilder? sb = null
    )
    {
        sb ??= new();

        sb.AppendLine(
            $@"
    public class {typeSymbol.Name}MockSetup : MockSetup
    {{"
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

            string returnType = methodSymbol.ReturnType.ToDisplayString();
            string paramsWithTypesAndMods = methodSymbol.GetParametersWithArgTypesAndModifiers();
            string paramTypeString = methodSymbol.GetParameterTypesWithoutModifiers();
            string paramString = methodSymbol.GetParametersWithoutTypesAndModifiers();

            sb.AppendLine(
                $@"
        private List<ArgBag<{paramTypeString}, {returnType}>>? {methodName}BagStore;

        public MemberMock<{paramTypeString}, {returnType}> {methodName}({paramsWithTypesAndMods}) => SetupMethod(this.{methodName}BagStore ??= new(), {paramString});"
            );
        }

        CallTrackerGenerator.CreateCallTrackerForConcreteType(typeSymbol, sb);

        sb.AppendLine(
            @$"
    }}"
        );

        return sb;
    }
}

/*
 *
public class _01_CalculatorMockSetup : MockSetup
{
    private List<ArgBag<int, int, int>>? addBagStore;

    public MemberMock<int, int, int> Add(Arg<int> x, Arg<int> y) =>
        SetupMethod(this.addBagStore ??= new(), x, y);

    public class _01_CalculatorMockCallTracker(_01_CalculatorMockSetup setup) : MockCallTracker
    {
        private readonly List<ValueTuple<int, int>>? callStore;

        public int Add(int x, int y) => CallMemberMock(setup.addBagStore, this.callStore, x, y);

        public class _01_CalculatorMockAsserter(_01_CalculatorMockCallTracker tracker)
            : MockAsserter
        {
            public MemberAsserter Add(Arg<int> x, Arg<int> y) =>
                GetMemberAsserter(tracker.callStore, x, y);
        }
    }
}
 *
 */
