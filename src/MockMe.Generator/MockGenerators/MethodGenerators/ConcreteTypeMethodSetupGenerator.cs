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
        //private {this.GetBagStoreType()}? {this.GetUniqueStoreName()};
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
                {paramString.AddOnIfNotEmpty($"{this.GetArgCollectionName()} mockMe_argCollection = new(", ");")}
                int genericTypeHashCode = typeof({this.GetArgCollectionName()}).GetHashCode();
                var mockStore =
                    this.setup.{this.GetBagStoreName()}?.GetValueOrDefault(genericTypeHashCode)
                    as List<ArgBagWithMock<{this.GetArgCollectionName()}>>;

                {(this.isVoidReturnType ? string.Empty : "var mockMe_methodResult = ")}MockCallTracker.Call{this.voidPrefix}MemberMock<{this.GetArgCollectionName()}{(this.isVoidReturnType ? "" : $", {this.returnType}")}>(
                    mockStore,
                    GenericCallStoreRetriever.GetGenericCallStore<{this.GetArgCollectionName()}>({this.GetCallStoreName()} ??= new(), genericTypeHashCode){(string.IsNullOrEmpty(paramString) ? "" : ", mockMe_argCollection")}
                );"
            );

            foreach (var p in refOrOutParameters)
            {
                sb.Append(
                    $@"
                {p.Name} = mockMe_argCollection.{p.Name};"
                );
            }

            sb.Append(
                $@"
                {(this.isVoidReturnType ? "" : "return mockMe_methodResult;")}
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
                {(this.isVoidReturnType ? string.Empty : "return ")}MockCallTracker.Call{this.voidPrefix}MemberMock{(this.isVoidReturnType ? "" : $"<{this.returnType}>")}(this.setup.{this.GetBagStoreName()});
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
                {this.GetArgCollectionName()} mockMe_argCollection = new({paramString});
                {(this.isVoidReturnType ? "" : "var mockMe_methodResult = ")}MockCallTracker.Call{this.voidPrefix}MemberMock<{this.GetArgCollectionName()}{(this.isVoidReturnType ? "" : $", {this.returnType}")}>(this.setup.{this.GetBagStoreName()}, this.{this.GetCallStoreName()} ??= new(), mockMe_argCollection);"
            );

            foreach (var p in refOrOutParameters)
            {
                sb.Append(
                    $@"
                {p.Name} = mockMe_argCollection.{p.Name};"
                );
            }

            sb.Append(
                $@"
                {(this.isVoidReturnType ? "" : "return mockMe_methodResult;")}
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
                    {p.Name} = Arg.Any();"
                );
            }

            sb.Append(
                $@"
                    int genericTypeHashCode = typeof({this.GetArgCollectionName()}).GetHashCode();
                    return GetMemberAsserter(this.tracker.{this.GetCallStoreName()}?.GetValueOrDefault(genericTypeHashCode) as List<{this.GetArgCollectionName()}>, new ArgBag<{this.paramTypes}>({parameters}));
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
                    {p.Name} = Arg.Any();"
                );
            }

            sb.Append(
                $@"
                    return GetMemberAsserter<{this.GetArgCollectionName()}>(this.tracker.{this.GetCallStoreName()}, new ArgBag<{this.paramTypes}>({parameters}));
                }}"
            );
        }

        return sb;
    }
}
