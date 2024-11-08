using System.Text;
using Microsoft.CodeAnalysis;
using MockMe.Generator.Extensions;

namespace MockMe.Generator.MockGenerators.Concrete;

internal class ConcreteTypeMethodSetupGenerator
{
    private const string Void = "Void";
    private readonly IMethodSymbol methodSymbol;
    private readonly string parametersWithoutTypesAndModifiers;
    private readonly string returnType;
    private readonly string voidPrefix;
    private readonly string paramTypes;
    private readonly string paramTypesFollowedByReturnType;
    private readonly string memberMockType;
    private readonly string returnTypeGenericParamSuffix;

    public ConcreteTypeMethodSetupGenerator(IMethodSymbol methodSymbol)
    {
        this.methodSymbol = methodSymbol;
        this.parametersWithoutTypesAndModifiers =
            methodSymbol.GetParametersWithoutTypesAndModifiers();

        this.returnType = methodSymbol.ReturnType.ToDisplayString();
        this.paramTypes = methodSymbol.GetParameterTypesWithoutModifiers();
        this.returnTypeGenericParamSuffix =
            this.returnType == "void" ? string.Empty
            : this.paramTypes.Length == 0 ? this.returnType
            : $", {this.returnType}";
        this.voidPrefix = this.returnType == "void" ? Void : string.Empty;
        this.paramTypesFollowedByReturnType = GetParamTypesFollowedByReturnType(
            this.returnTypeGenericParamSuffix,
            this.paramTypes
        );

        this.memberMockType = $"{this.voidPrefix}MemberMock{this.paramTypesFollowedByReturnType}";
    }

    public StringBuilder AddMethodSetupToStringBuilder(StringBuilder sb)
    {
        return sb.AppendLine(
            $@"
        private {this.GetBagStoreType()}? {this.MethodName()}BagStore;
        public {this.memberMockType} {this.MethodName()}{this.methodSymbol.GetGenericParameterStringInBrackets()}({this.methodSymbol.GetParametersWithArgTypesAndModifiers()}) =>
            {this.GetSetupMethod()}"
        );
    }

    private static string GetParamTypesFollowedByReturnType(string returnSuffix, string paramTypes)
    {
        if (paramTypes.Length + returnSuffix.Length == 0)
        {
            return string.Empty;
        }

        return $"<{paramTypes}{returnSuffix}>";
    }

    private string GetBagStoreType()
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
            return $"List<ArgBagWith{this.memberMockType}>";
        }
    }

    private string GetSetupMethod()
    {
        if (this.NumGenericParameters() > 0)
        {
            return $"Setup{this.voidPrefix}Method(SetupGenericStore{this.paramTypesFollowedByReturnType}(this.{this.MethodName()}BagStore ??= new()){this.parametersWithoutTypesAndModifiers.AddPrefixIfNotEmpty(", ")});";
        }
        if (this.NumParameters() == 0)
        {
            return $"this.{this.MethodName()}BagStore ??= new();";
        }
        else
        {
            return $"Setup{this.voidPrefix}Method(this.{this.MethodName()}BagStore ??= new(), {parametersWithoutTypesAndModifiers});";
        }
    }

    private string MethodName() => this.methodSymbol.Name;

    private int NumGenericParameters() => this.methodSymbol.TypeParameters.Length;

    private int NumParameters() => this.methodSymbol.Parameters.Length;
}
