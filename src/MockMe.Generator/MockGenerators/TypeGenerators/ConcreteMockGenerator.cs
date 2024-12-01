using System;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using MockMe.Generator.Extensions;

namespace MockMe.Generator.MockGenerators.TypeGenerators;

internal class ConcreteMockGenerator(INamedTypeSymbol typeSymbol, string typeName)
    : MockGeneratorBase(typeSymbol, typeName)
{
    public override string GetCallTrackerBaseClass(ITypeSymbol typeSymbol) => "MockCallTracker";

    public override string GetConstructorAndProps(ITypeSymbol typeSymbol) =>
        $@"
        public {this.TypeName}Mock()
        {{
            this.Setup = new {this.MockSetupTypeName}();
            this.CallTracker = new {this.MockSetupTypeName}.{this.mockCallTrackerTypeName}(this.Setup);
            this.Assert = new {this.MockSetupTypeName}.{this.mockCallTrackerTypeName}.{this.mockAsserterTypeName}(this.CallTracker);
            global::MockMe.MockStore<{typeSymbol.ToFullTypeString()}>.Store.TryAdd(this.MockedObject, this);
        }}

        public {this.MockSetupTypeName} Setup {{ get; }}
        public {this.MockSetupTypeName}.{this.mockCallTrackerTypeName}.{this.mockAsserterTypeName} Assert {{ get; }}
        private {this.MockSetupTypeName}.{this.mockCallTrackerTypeName} CallTracker {{ get; }}";

    public override string GetMockBaseClass(ITypeSymbol typeSymbol) =>
        $"global::MockMe.Abstractions.SealedTypeMock<{typeSymbol.ToFullTypeString()}>";
}
