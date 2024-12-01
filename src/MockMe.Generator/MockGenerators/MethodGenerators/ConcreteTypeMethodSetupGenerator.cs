using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using MockMe.Generator.Extensions;
using MockMe.Generator.MockGenerators.TypeGenerators;

namespace MockMe.Generator.MockGenerators.MethodGenerators;

internal class ConcreteTypeMethodSetupGenerator(
    IMethodSymbol methodSymbol,
    MockGeneratorBase mockGenerator
) : MethodMockGeneratorBase(methodSymbol)
{
    public override StringBuilder AddMethodSetupToStringBuilder(
        StringBuilder sb,
        Dictionary<string, SetupPropertyMetadata> setupMeta
    )
    {
        if (this.methodSymbol.DeclaredAccessibility != Accessibility.Public)
        {
            return sb;
        }

        List<IParameterSymbol> refOrOutParameters = this
            .methodSymbol.Parameters.Where(p => p.RefKind is RefKind.Ref or RefKind.Out)
            .ToList();

        sb.AppendLine(
            $@"
        private {this.GetBagStoreType()}? {this.GetBagStoreName()};
        public {this.memberMockType} {this.MethodName()}{this.methodSymbol.GetGenericParameterStringInBrackets()}({this.methodSymbol.GetParametersWithArgTypesAndModifiers()})
        {{"
        );

        foreach (var p in refOrOutParameters)
        {
            sb.Append(
                $@"
            {p.Name} = default({p.Type.ToFullTypeString()});"
            );
        }

        sb.Append(
            $@"
            return {this.GetSetupMethod()};
        }}"
        );

        return sb;

        //return sb.AppendLine(
        //    $@"
        //private {this.GetBagStoreType()}? {this.GetBagStoreName()};
        //public {this.memberMockType} {this.MethodName()}{this.methodSymbol.GetGenericParameterStringInBrackets()}({this.methodSymbol.GetParametersWithArgTypesAndModifiers()}) =>
        //    {this.GetSetupMethod()}"
        //);
    }

    public override StringBuilder AddMethodCallTrackerToStringBuilder(
        StringBuilder sb,
        Dictionary<string, PropertyMetadata> callTrackerMeta
    )
    {
        string paramsWithTypesAndMods =
            this.methodSymbol.GetParametersWithOriginalTypesAndModifiers();
        string paramString = this.methodSymbol.GetParametersWithoutTypesAndModifiers();

        List<IParameterSymbol> refOrOutParameters = this
            .methodSymbol.Parameters.Where(p => p.RefKind is RefKind.Ref or RefKind.Out)
            .ToList();

        if (this.methodSymbol.DeclaredAccessibility != Accessibility.Public)
        {
            sb.Append(
                $@"
            public {this.returnType} {this.MethodName()}({paramsWithTypesAndMods}) => default({this.returnType});"
            );
            return sb;
        }

        if (this.methodSymbol.TypeParameters.Length > 0)
        {
            sb.AppendLine(
                $@"
            private Dictionary<int, object>? {this.GetCallStoreName()};

            public {this.returnType} {this.MethodName()}{this.methodSymbol.GetGenericParameterStringInBrackets()}({paramsWithTypesAndMods})
            {{"
            );

            foreach (var p in refOrOutParameters)
            {
                sb.Append(
                    $@"
                {p.Name} = default({p.Type.ToFullTypeString()});"
                );
            }

            sb.Append(
                $@"
                int genericTypeHashCode = GetUniqueIntFromTypes({string.Join(", ", this.methodSymbol.Parameters.Select(p => p.Type.ToFullTypeString().AddOnIfNotEmpty("typeof(", ")")))});
                var mockStore =
                    this.setup.{this.GetBagStoreName()}?.GetValueOrDefault(genericTypeHashCode)
                    as List<ArgBagWith{this.voidPrefix}MemberMock{(this.paramTypes + this.returnType.AddPrefixIfNotEmpty(", ")).AddOnIfNotEmpty("<", ">")}>;

                {(this.isVoidReturnType ? string.Empty : "return ")}MockCallTracker.Call{this.voidPrefix}MemberMock<{this.GetArgCollectionName()}{(this.isVoidReturnType ? "" : $", {this.returnType}")}>(
                    mockStore,
                    GenericCallStoreRetriever.GetGenericCallStore{this.paramTypes.AddOnIfNotEmpty("<", ">")}({this.GetCallStoreName()} ??= new(), genericTypeHashCode){paramString.AddOnIfNotEmpty(", new(", ")")}
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
                {(this.isVoidReturnType ? string.Empty : "return ")}{this.voidPrefix}MockCallTracker.Call{this.voidPrefix}MemberMock<{this.returnType}>(this.setup.{this.GetBagStoreName()});
            }}"
            );
        }
        else
        {
            sb.AppendLine(
                $@"
            private List<{mockGenerator.MockSetupTypeName}.{this.GetArgCollectionName()}>? {this.GetCallStoreName()};
            public {this.returnType} {this.MethodName()}({paramsWithTypesAndMods})
            {{"
            );

            foreach (var p in refOrOutParameters)
            {
                sb.Append(
                    $@"
                {p.Name} = default({p.Type.ToFullTypeString()});"
                );
            }

            sb.Append(
                $@"
                {(this.isVoidReturnType ? "" : "return ")}MockCallTracker.Call{this.voidPrefix}MemberMock<{this.GetArgCollectionName()}{(this.isVoidReturnType ? "" : $", {this.returnType}")}>(this.setup.{this.GetBagStoreName()}, this.{this.GetCallStoreName()} ??= new(), new({paramString}));
            }}"
            );
        }
        return sb;
    }

    public override StringBuilder AddMethodToAsserterClass(
        StringBuilder sb,
        Dictionary<string, AssertPropertyMetadata> assertMeta
    )
    {
        if (this.methodSymbol.DeclaredAccessibility != Accessibility.Public)
        {
            return sb;
        }

        var parametersDefinition = this.methodSymbol.GetParametersWithArgTypesAndModifiers();
        var parameters = this.methodSymbol.GetParametersWithoutTypesAndModifiers();
        List<IParameterSymbol> refOrOutParameters = this
            .methodSymbol.Parameters.Where(p => p.RefKind is RefKind.Ref or RefKind.Out)
            .ToList();

        if (this.methodSymbol.TypeParameters.Length > 0)
        {
            sb.AppendLine(
                $@"
                public global::MockMe.Asserters.MemberAsserter {this.MethodName()}{this.methodSymbol.GetGenericParameterStringInBrackets()}({parametersDefinition})
                {{"
            );

            foreach (var p in refOrOutParameters)
            {
                sb.Append(
                    $@"
                    {p.Name} = default({p.Type.ToFullTypeString()});"
                );
            }

            sb.Append(
                $@"
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
                {{"
            );

            foreach (var p in refOrOutParameters)
            {
                sb.Append(
                    $@"
                    {p.Name} = default({p.Type.ToFullTypeString()});"
                );
            }

            sb.Append(
                $@"
                    return GetMemberAsserter(this.tracker.{this.GetCallStoreName()}, {parameters});
                }}"
            );
        }

        return sb;
    }
}
