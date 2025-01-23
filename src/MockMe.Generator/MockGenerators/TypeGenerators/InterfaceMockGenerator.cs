using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using MockMe.Generator.Extensions;

namespace MockMe.Generator.MockGenerators.TypeGenerators;

internal class InterfaceMockGenerator(INamedTypeSymbol typeSymbol, string typeName)
    : MockGeneratorBase(typeSymbol, typeName)
{
    // Gather all interfaces, including inherited ones, and ensure distinct results
    public override string GetCallTrackerBaseClass(ITypeSymbol typeSymbol) =>
        typeSymbol.AllInterfaces
            .Concat(new[] { typeSymbol })
            .Distinct(SymbolEqualityComparer.Default)
            .Select(i => i?.ToFullTypeString() ?? "")
            .Aggregate((current, next) => $"{current}, {next}");

    public override string GetConstructorAndProps(ITypeSymbol typeSymbol) =>
        @$"
        public {this.mockTypeName}() : base(CreateCallTracker(out var setup, out var asserter))
        {{
            this.Setup = setup;
            this.Assert = asserter;
        }}

        private static {this.MockSetupTypeName}.{this.mockCallTrackerTypeName} CreateCallTracker(
            out {this.MockSetupTypeName} setup,
            out {this.MockSetupTypeName}.{this.mockCallTrackerTypeName}.{this.mockAsserterTypeName} asserter 
        )
        {{
            setup = new {this.MockSetupTypeName}();
            var callTracker = new {this.MockSetupTypeName}.{this.mockCallTrackerTypeName}(setup);
            asserter = new {this.MockSetupTypeName}.{this.mockCallTrackerTypeName}.{this.mockAsserterTypeName}(callTracker);

            return callTracker;
        }}

        public {this.MockSetupTypeName} Setup {{ get; }}
        public {this.MockSetupTypeName}.{this.mockCallTrackerTypeName}.{this.mockAsserterTypeName} Assert {{ get; }}";

    public override string GetMockBaseClass(ITypeSymbol typeSymbol) =>
        $"global::MockMe.Abstractions.InterfaceMock<{typeSymbol.ToFullTypeString()}, {this.MockSetupTypeName}.{this.mockCallTrackerTypeName}>";
}
