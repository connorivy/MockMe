using System.Text;
using Microsoft.CodeAnalysis;

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

        StringBuilder callTrackerBuilder = new();
        StringBuilder asserterBuilder = new();
        callTrackerBuilder.AppendLine(
            $@"
        public class {typeSymbol.Name}MockCallTracker : MockCallTracker
        {{
            private readonly {typeSymbol.Name}MockSetup setup;
            public {typeSymbol.Name}MockCallTracker({typeSymbol.Name}MockSetup setup)
            {{
                this.setup = setup;
            }}"
        );

        asserterBuilder.AppendLine(
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

            ConcreteTypeMethodSetupGenerator methodGenerator = new(methodSymbol);

            methodGenerator.AddMethodSetupToStringBuilder(sb);
            methodGenerator.AddMethodCallTrackerToStringBuilder(callTrackerBuilder);
            methodGenerator.AddMethodToAsserterClass(asserterBuilder);
            //new ConcreteTypeMethodSetupGenerator(methodSymbol).AddMethodSetupToStringBuilder(sb);
        }

        asserterBuilder.AppendLine(
            $@"
            }}"
        );

        callTrackerBuilder.Append(asserterBuilder);

        callTrackerBuilder.AppendLine(
            @$"
        }}"
        );

        //CallTrackerGenerator.CreateCallTrackerForConcreteType(typeSymbol, sb);

        sb.Append(callTrackerBuilder);

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
