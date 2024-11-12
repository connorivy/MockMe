using Microsoft.CodeAnalysis;

namespace MockMe.Generator.MockGenerators;

internal class ConcreteMockGenerator : MockGeneratorBase
{
    public override string GetCallTrackerBaseClass(ITypeSymbol typeSymbol) => "MockCallTracker";

    public override string GetConstructorAndProps(ITypeSymbol typeSymbol) =>
        $@"
        public {typeSymbol.Name}Mock()
        {{
            this.Setup = new {typeSymbol.Name}MockSetup();
            this.CallTracker = new {typeSymbol.Name}MockCallTracker(this.Setup);
            this.Assert = new {typeSymbol.Name}MockAsserter(this.CallTracker);
            global::MockMe.MockStore<global::{typeSymbol}>.Store.TryAdd(this.MockedObject, this);
        }}

        public {typeSymbol.Name}MockSetup Setup {{ get; }}
        public {typeSymbol.Name}MockAsserter Assert {{ get; }}
        private {typeSymbol.Name}MockCallTracker CallTracker {{ get; }}";

    public override string GetMockBaseClass(ITypeSymbol typeSymbol) =>
        $"global::MockMe.Abstractions.SealedTypeMock<global::{typeSymbol}>";
}
