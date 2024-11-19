using Microsoft.CodeAnalysis;

namespace MockMe.Generator.MockGenerators;

internal class ConcreteMockGenerator(string typeName) : MockGeneratorBase(typeName)
{
    public override string GetCallTrackerBaseClass(ITypeSymbol typeSymbol) => "MockCallTracker";

    public override string GetConstructorAndProps(ITypeSymbol typeSymbol) =>
        $@"
        public {this.TypeName}Mock()
        {{
            this.Setup = new {this.TypeName}MockSetup();
            this.CallTracker = new {this.TypeName}MockCallTracker(this.Setup);
            this.Assert = new {this.TypeName}MockAsserter(this.CallTracker);
            global::MockMe.MockStore<global::{typeSymbol}>.Store.TryAdd(this.MockedObject, this);
        }}

        public {this.TypeName}MockSetup Setup {{ get; }}
        public {this.TypeName}MockAsserter Assert {{ get; }}
        private {this.TypeName}MockCallTracker CallTracker {{ get; }}";

    public override string GetMockBaseClass(ITypeSymbol typeSymbol) =>
        $"global::MockMe.Abstractions.SealedTypeMock<global::{typeSymbol}>";
}
