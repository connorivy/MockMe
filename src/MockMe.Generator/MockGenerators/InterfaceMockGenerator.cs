using Microsoft.CodeAnalysis;
using MockMe.Generator.Extensions;

namespace MockMe.Generator.MockGenerators;

internal class InterfaceMockGenerator(string typeName) : MockGeneratorBase(typeName)
{
    public override string GetCallTrackerBaseClass(ITypeSymbol typeSymbol) =>
        typeSymbol.ToFullTypeString();

    public override string GetConstructorAndProps(ITypeSymbol typeSymbol) =>
        @$"
        public {this.TypeName}Mock() : base(CreateCallTracker(out var setup, out var asserter))
        {{
            this.Setup = setup;
            this.Assert = asserter;
        }}

        private static {this.TypeName}MockCallTracker CreateCallTracker(
            out {this.TypeName}MockSetup setup,
            out {this.TypeName}MockAsserter asserter 
        )
        {{
            setup = new {this.TypeName}MockSetup();
            var callTracker = new {this.TypeName}MockCallTracker(setup);
            asserter = new {this.TypeName}MockAsserter(callTracker);

            return callTracker;
        }}

        public {this.TypeName}MockSetup Setup {{ get; }}
        public {this.TypeName}MockAsserter Assert {{ get; }}";

    public override string GetMockBaseClass(ITypeSymbol typeSymbol) =>
        $"global::MockMe.Abstractions.InterfaceMock<{typeSymbol.ToFullTypeString()}, {this.TypeName}MockCallTracker>";
}
