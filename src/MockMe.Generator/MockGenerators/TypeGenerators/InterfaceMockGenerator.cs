using System.Text;
using Microsoft.CodeAnalysis;
using MockMe.Generator.Extensions;

namespace MockMe.Generator.MockGenerators.TypeGenerators;

internal class InterfaceMockGenerator(INamedTypeSymbol typeSymbol, string typeName)
    : MockGeneratorBase(typeSymbol, typeName)
{
    public override string GetCallTrackerBaseClass(ITypeSymbol typeSymbol) =>
        typeSymbol.ToFullTypeString();

    public override string GetConstructorAndProps(ITypeSymbol typeSymbol) =>
        @$"
        public {this.mockTypeName}() : base(CreateCallTracker(out var setup, out var asserter))
        {{
            this.Setup = setup;
            this.Assert = asserter;
        }}

        private static {this.mockSetupTypeName}.{this.mockCallTrackerTypeName} CreateCallTracker(
            out {this.mockSetupTypeName} setup,
            out {this.mockSetupTypeName}.{this.mockCallTrackerTypeName}.{this.mockAsserterTypeName} asserter 
        )
        {{
            setup = new {this.mockSetupTypeName}();
            var callTracker = new {this.mockSetupTypeName}.{this.mockCallTrackerTypeName}(setup);
            asserter = new {this.mockSetupTypeName}.{this.mockCallTrackerTypeName}.{this.mockAsserterTypeName}(callTracker);

            return callTracker;
        }}

        public {this.mockSetupTypeName} Setup {{ get; }}
        public {this.mockSetupTypeName}.{this.mockCallTrackerTypeName}.{this.mockAsserterTypeName} Assert {{ get; }}";

    public override string GetMockBaseClass(ITypeSymbol typeSymbol) =>
        $"global::MockMe.Abstractions.InterfaceMock<{typeSymbol.ToFullTypeString()}, {this.mockSetupTypeName}.{this.mockCallTrackerTypeName}>";
}
