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

            if (methodSymbol.MethodKind == MethodKind.Constructor)
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
            string returnType = methodSymbol.ReturnType.ToDisplayString();
            string paramTypeString = methodSymbol.GetParameterTypesWithoutModifiers();

            string voidPrefix = returnType == "void" ? "Void" : string.Empty;
            string memberMockType = GetMemberMockWithProperGeneric(returnType, paramTypeString);
            if (numParameters == 0)
            {
                sb.AppendLine(
                    $@"
        private {voidPrefix}{memberMockType}? {methodName}BagStore;
        public {voidPrefix}{memberMockType} {methodName}() => this.{methodName}BagStore ??= new();"
                );
            }
            else
            {
                string paramsWithTypesAndMods =
                    methodSymbol.GetParametersWithArgTypesAndModifiers();
                string paramString = methodSymbol.GetParametersWithoutTypesAndModifiers();
                sb.AppendLine(
                    $@"
        private List<ArgBagWith{voidPrefix}{memberMockType}>? {methodName}BagStore;
        public {voidPrefix}{memberMockType} {methodName}({paramsWithTypesAndMods}) => Setup{voidPrefix}Method(this.{methodName}BagStore ??= new(), {paramString});"
                );
            }
        }

        CallTrackerGenerator.CreateCallTrackerForConcreteType(typeSymbol, sb);

        sb.AppendLine(
            @$"
    }}"
        );

        return sb;
    }

    private static string GetMemberMockWithProperGeneric(string returnType, string paramTypeString)
    {
        string returnSuffix =
            returnType == "void"
                ? string.Empty
                : paramTypeString.Length == 0
                    ? returnType
                    : $", {returnType}";

        string memberMock = "MemberMock";
        if (paramTypeString.Length + returnSuffix.Length == 0)
        {
            return memberMock;
        }

        return memberMock + $"<{paramTypeString}{returnSuffix}>";
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
