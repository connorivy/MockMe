using Microsoft.CodeAnalysis;
using MockMe.Generator.Extensions;

namespace MockMe.Generator.MockGenerators;

internal class InterfaceMockGenerator : MockGeneratorBase
{
    public override string GetCallTrackerBaseClass(ITypeSymbol typeSymbol) =>
        typeSymbol.ToFullTypeString();

    public override string GetConstructorAndProps(ITypeSymbol typeSymbol) =>
        @$"
        public {typeSymbol.Name}Mock() : base(CreateCallTracker(out var setup, out var asserter))
        {{
            this.Setup = setup;
            this.Assert = asserter;
        }}

        private static {typeSymbol.Name}MockCallTracker CreateCallTracker(
            out {typeSymbol.Name}MockSetup setup,
            out {typeSymbol.Name}MockAsserter asserter 
        )
        {{
            setup = new {typeSymbol.Name}MockSetup();
            var callTracker = new {typeSymbol.Name}MockCallTracker(setup);
            asserter = new {typeSymbol.Name}MockAsserter(callTracker);

            return callTracker;
        }}

        public {typeSymbol.Name}MockSetup Setup {{ get; }}
        public {typeSymbol.Name}MockAsserter Assert {{ get; }}";

    public override string GetMockBaseClass(ITypeSymbol typeSymbol) =>
        $"global::MockMe.Abstractions.InterfaceMock<global::{typeSymbol}, {typeSymbol.Name}MockCallTracker>";
}
