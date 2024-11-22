using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using MockMe.Generator.Extensions;

namespace MockMe.Generator.MockGenerators.MethodGenerators;

internal class ConcreteTypeMethodSetupGenerator(IMethodSymbol methodSymbol)
    : MethodMockGeneratorBase(methodSymbol)
{
    public override StringBuilder AddMethodSetupToStringBuilder(
        StringBuilder sb,
        Dictionary<string, SetupPropertyMetadata> setupMeta
    )
    {
        return sb.AppendLine(
            $@"
        private {this.GetBagStoreType()}? {this.GetBagStoreName()};
        public {this.memberMockType} {this.MethodName()}{this.methodSymbol.GetGenericParameterStringInBrackets()}({this.methodSymbol.GetParametersWithArgTypesAndModifiers()}) =>
            {this.GetSetupMethod()}"
        );
    }

    public override StringBuilder AddMethodCallTrackerToStringBuilder(
        StringBuilder sb,
        Dictionary<string, PropertyMetadata> callTrackerMeta
    )
    {
        string paramsWithTypesAndMods =
            this.methodSymbol.GetParametersWithOriginalTypesAndModifiers();
        string paramString = this.methodSymbol.GetParametersWithoutTypesAndModifiers();

        if (this.methodSymbol.MethodKind == MethodKind.PropertyGet)
        {
            var methodName = this.methodSymbol.GetPropertyName();

            string? indexerType = null;
            if (
                this.methodSymbol.AssociatedSymbol is IPropertySymbol propertySymbol
                && propertySymbol.IsIndexer
            )
            {
                indexerType = propertySymbol.Type.ToFullTypeString();
            }

            if (!callTrackerMeta.TryGetValue(methodName, out var propMeta))
            {
                propMeta = new()
                {
                    Name = methodName,
                    ReturnType = this.returnType,
                    IndexerType = indexerType,
                };
                callTrackerMeta.Add(methodName, propMeta);
            }

            if (string.IsNullOrEmpty(indexerType))
            {
                propMeta.GetterLogic =
                    @$"
            this.{this.GetCallStoreName()}++;
            return {this.voidPrefix}MockCallTracker.Call{this.voidPrefix}MemberMock(this.setup.{this.GetBagStoreName()});";
                propMeta.GetterField = $"private int {this.GetCallStoreName()};";
            }
            else
            {
                propMeta.GetterLogic =
                    @$"
            return {this.voidPrefix}MockCallTracker.Call{this.voidPrefix}MemberMock(this.setup.{this.GetBagStoreName()}, this.{this.GetCallStoreName()} ??= new(), index);";
                propMeta.GetterField = $"private List<{indexerType}>? {this.GetCallStoreName()};";
            }
        }
        else if (this.methodSymbol.MethodKind == MethodKind.PropertySet)
        {
            var methodName = this.methodSymbol.GetPropertyName();

            string? indexerType = null;
            if (
                this.methodSymbol.AssociatedSymbol is IPropertySymbol propertySymbol
                && propertySymbol.IsIndexer
            )
            {
                indexerType = propertySymbol.Type.ToFullTypeString();
            }

            if (!callTrackerMeta.TryGetValue(methodName, out var propMeta))
            {
                propMeta = new()
                {
                    Name = methodName,
                    ReturnType = this.methodSymbol.Parameters.First().Type.ToFullReturnTypeString(),
                    IndexerType = indexerType,
                };
                callTrackerMeta.Add(methodName, propMeta);
            }

            propMeta.SetterLogic =
                @$"
        {this.voidPrefix}MockCallTracker.Call{this.voidPrefix}MemberMock(this.setup.{this.GetBagStoreName()}, this.{this.GetCallStoreName()} ??= new(){(string.IsNullOrEmpty(indexerType) ? "" : ", index")}, value);";

            propMeta.SetterField =
                $"private List<{this.methodSymbol.GetMethodArgumentsAsCollection()}>? {this.GetCallStoreName()};";
        }
        else if (this.methodSymbol.TypeParameters.Length > 0)
        {
            sb.AppendLine(
                $@"
            private Dictionary<int, object>? {this.GetCallStoreName()};

            public {this.returnType} {this.MethodName()}{this.methodSymbol.GetGenericParameterStringInBrackets()}({paramsWithTypesAndMods})
            {{
                int genericTypeHashCode = GetUniqueIntFromTypes({string.Join(", ", this.methodSymbol.Parameters.Select(p => p.Type.ToFullTypeString().AddOnIfNotEmpty("typeof(", ")")))});
                var mockStore =
                    this.setup.{this.GetBagStoreName()}?.GetValueOrDefault(genericTypeHashCode)
                    as List<ArgBagWith{this.voidPrefix}MemberMock{(this.paramTypes + this.returnType.AddPrefixIfNotEmpty(", ")).AddOnIfNotEmpty("<", ">")}>;

                {(this.isVoidReturnType ? string.Empty : "return ")}{this.voidPrefix}MockCallTracker.Call{this.voidPrefix}MemberMock(
                    mockStore,
                    GenericCallStoreRetriever.GetGenericCallStore{this.paramTypes.AddOnIfNotEmpty("<", ">")}({this.GetCallStoreName()} ??= new(), genericTypeHashCode){paramString.AddPrefixIfNotEmpty(", ")}
                );
            }}"
            );
        }
        else if (this.methodSymbol.Parameters.Length == 0)
        {
            sb.AppendLine(
                $@"
            private int {this.GetCallStoreName()};

            public {this.returnType} {this.MethodName()}()
            {{
                this.{this.GetCallStoreName()}++;
                {(this.isVoidReturnType ? string.Empty : "return ")}{this.voidPrefix}MockCallTracker.Call{this.voidPrefix}MemberMock(this.setup.{this.GetBagStoreName()});
            }}"
            );
        }
        else
        {
            sb.AppendLine(
                $@"
            private List<{this.methodSymbol.GetMethodArgumentsAsCollection()}>? {this.GetCallStoreName()};

            public {this.returnType} {this.MethodName()}({paramsWithTypesAndMods}) => {this.voidPrefix}MockCallTracker.Call{this.voidPrefix}MemberMock(this.setup.{this.GetBagStoreName()}, this.{this.GetCallStoreName()} ??= new(), {paramString});"
            );
        }
        return sb;
    }

    public override StringBuilder AddMethodToAsserterClass(
        StringBuilder sb,
        Dictionary<string, AssertPropertyMetadata> assertMeta
    )
    {
        var parametersDefinition = this.methodSymbol.GetParametersWithArgTypesAndModifiers();
        var parameters = this.methodSymbol.GetParametersWithoutTypesAndModifiers();

        if (this.methodSymbol.TypeParameters.Length > 0)
        {
            sb.AppendLine(
                $@"
                public global::MockMe.Asserters.MemberAsserter {this.MethodName()}{this.methodSymbol.GetGenericParameterStringInBrackets()}({parametersDefinition})
                {{
                    int genericTypeHashCode = GetUniqueIntFromTypes({string.Join(", ", this.methodSymbol.TypeParameters.Select(p => p.Name.AddOnIfNotEmpty("typeof(", ")")))});
                    return GetMemberAsserter(this.tracker.{this.GetCallStoreName()}?.GetValueOrDefault(genericTypeHashCode) as List<{this.methodSymbol.GetMethodArgumentsAsCollection()}>, {parameters});
                }}"
            );
        }
        else if (this.methodSymbol.Parameters.Length == 0)
        {
            sb.AppendLine(
                $@"
                public global::MockMe.Asserters.MemberAsserter {this.MethodName()}() =>
                    new(this.tracker.{this.GetCallStoreName()});"
            );
        }
        else
        {
            sb.AppendLine(
                $@"
                public global::MockMe.Asserters.MemberAsserter {this.MethodName()}({parametersDefinition})
                {{
                    return GetMemberAsserter(this.tracker.{this.GetCallStoreName()}, {parameters});
                }}"
            );
        }

        return sb;
    }
}
