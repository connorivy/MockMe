using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using MockMe.Generator.Extensions;

namespace MockMe.Generator.MockGenerators.MethodGenerators;

internal abstract class MethodMockGeneratorBase
{
    protected const string Void = "Void";
    protected IMethodSymbol methodSymbol { get; }
    protected string parametersWithoutTypesAndModifiers { get; }
    protected string returnType { get; }
    protected string voidPrefix { get; }
    protected string paramTypes { get; }
    protected string paramTypesFollowedByReturnType { get; }
    protected string memberMockType { get; }
    protected string returnTypeGenericParamSuffix { get; }
    protected bool isVoidReturnType { get; }
    protected string? taskPrefix { get; }
    protected string returnTypeIgnoringTask { get; }

    public MethodMockGeneratorBase(IMethodSymbol methodSymbol)
    {
        this.methodSymbol = methodSymbol;
        this.parametersWithoutTypesAndModifiers =
            methodSymbol.GetParametersWithoutTypesAndModifiers();

        this.returnType = methodSymbol.ReturnType.ToFullReturnTypeString();
        this.returnTypeIgnoringTask =
            methodSymbol.ReturnType.GetInnerTypeIfTask()?.ToFullReturnTypeString()
            ?? methodSymbol.ReturnType.GetVoidIfTask()
            ?? methodSymbol.ReturnType.ToFullReturnTypeString();

        string? asyncReturnType = methodSymbol
            .ReturnType.GetInnerTypeIfTask()
            ?.ToFullReturnTypeString();

        this.isVoidReturnType = methodSymbol.ReturnType.SpecialType == SpecialType.System_Void;

        if (methodSymbol.ReturnType.IsGenericTask())
        {
            this.taskPrefix = "Task";
        }
        else if (methodSymbol.ReturnType.IsValueTask())
        {
            this.taskPrefix = "ValueTask";
        }

        this.voidPrefix = this.isVoidReturnType ? Void : string.Empty;
        this.paramTypes = methodSymbol.GetParameterTypesWithoutModifiers();

        this.returnTypeGenericParamSuffix =
            this.isVoidReturnType ? string.Empty
            : this.paramTypes.Length == 0 ? this.returnType
            : $", {this.returnType}";
        this.paramTypesFollowedByReturnType = GetParamTypesFollowedByReturnType(
            this.returnTypeGenericParamSuffix,
            this.paramTypes
        );

        var returnTypeIgnoringTaskGenericParamSuffix =
            this.isVoidReturnType ? string.Empty
            : this.paramTypes.Length == 0 ? (asyncReturnType ?? this.returnType)
            : $", {asyncReturnType ?? this.returnType}";
        var paramTypesFollowedByReturnTypeIgnoringTask = GetParamTypesFollowedByReturnType(
            returnTypeIgnoringTaskGenericParamSuffix,
            this.methodSymbol.Parameters.Length == 0 ? "" : this.GetArgCollectionName()
        );

        this.memberMockType =
            $"global::MockMe.Mocks.ClassMemberMocks.{this.voidPrefix}{this.taskPrefix}MemberMock{paramTypesFollowedByReturnTypeIgnoringTask}";
    }

    public virtual StringBuilder AddOriginalCollectionType(StringBuilder sb)
    {
        if (this.methodSymbol.Parameters.Length == 0)
        {
            return sb;
        }

        sb.Append(
            $@"
        internal class {this.GetArgCollectionName()} : OriginalArgBag<{this.paramTypes}>
        {{
            public {this.GetArgCollectionName(includeGenericType: false)}({this.methodSymbol.GetParametersWithOriginalTypesAndModifiers()}) : base({this.parametersWithoutTypesAndModifiers})
            {{ }}"
        );

        for (int i = 0; i < this.methodSymbol.Parameters.Length; i++)
        {
            sb.Append(
                $@"
            public {this .methodSymbol.Parameters[i] .Type.ToFullReturnTypeString()} {this.methodSymbol.Parameters[i].Name} {{ get => this.Arg{i + 1}; set => this.Arg{i + 1} = value; }}"
            );
        }

        return sb.Append(
            $@"
        }}"
        );
    }

    public virtual StringBuilder AddMethodSetupToStringBuilder(
        StringBuilder sb,
        Dictionary<string, SetupPropertyMetadata> setupMeta
    )
    {
        return sb.AppendLine(
            $@"
        protected {this.GetBagStoreType()}? {this.GetBagStoreName()};
        public {this.memberMockType} {this.MethodName()}{this.methodSymbol.GetGenericParameterStringInBrackets()}({this.methodSymbol.GetParametersWithArgTypesAndModifiers()}) =>
            {this.GetSetupMethod()}"
        );
    }

    public virtual StringBuilder AddMethodCallTrackerToStringBuilder(
        StringBuilder sb,
        Dictionary<string, PropertyMetadata> callTrackerMeta
    )
    {
        return sb;
    }

    public virtual StringBuilder AddMethodToAsserterClass(
        StringBuilder sb,
        Dictionary<string, AssertPropertyMetadata> assertMeta
    )
    {
        return sb;
    }

    protected static string GetParamTypesFollowedByReturnType(
        string returnSuffix,
        string paramTypes
    )
    {
        if (paramTypes.Length + returnSuffix.Length == 0)
        {
            return string.Empty;
        }

        return $"<{paramTypes}{returnSuffix}>";
    }

    protected string GetBagStoreType()
    {
        if (this.methodSymbol.TypeParameters.Length > 0)
        {
            return "Dictionary<int, object>";
        }
        else if (this.methodSymbol.Parameters.Length == 0)
        {
            return this.memberMockType;
        }
        else
        {
            //return $"List<ArgBagWith{this.voidPrefix}MemberMock{this.paramTypesFollowedByReturnType}>";
            return $"List<ArgBagWithMock<{this.GetArgCollectionName()}>>";
        }
    }

    protected string GetArgCollectionName(bool includeGenericType = true)
    {
        if (includeGenericType)
        {
            return this.methodSymbol.GetUniqueMethodName()
                + "Collection"
                + this.methodSymbol.GetGenericParameterStringInBrackets();
        }
        return this.methodSymbol.GetUniqueMethodName() + "Collection";
    }

    protected string GetBagStoreName() => this.methodSymbol.GetUniqueMethodName() + "BagStore";

    protected string GetCallStoreName() => this.methodSymbol.GetUniqueMethodName() + "CallStore";

    protected string GetSetupMethod()
    {
        if (this.NumGenericParameters() > 0)
        {
            return $"SetupMethod<{this.GetArgCollectionName()}, {this.memberMockType}>(SetupGenericStore<ArgBagWithMock<{this.GetArgCollectionName()}>>(this.{this.GetBagStoreName()} ??= new()){this.parametersWithoutTypesAndModifiers.AddOnIfNotEmpty($", new ArgBag<{this.paramTypes}>(", ")")});";
        }
        if (this.NumParameters() == 0)
        {
            return $"this.{this.GetBagStoreName()} ??= new();";
        }
        else
        {
            return $"SetupMethod<{this.GetArgCollectionName()}, {this.memberMockType}>(this.{this.GetBagStoreName()} ??= new(), new ArgBag<{this.paramTypes}>({this.parametersWithoutTypesAndModifiers}));";
        }
    }

    protected string MethodName()
    {
        return this.methodSymbol.Name;
    }

    protected int NumGenericParameters() => this.methodSymbol.TypeParameters.Length;

    protected int NumParameters() => this.methodSymbol.Parameters.Length;
}
