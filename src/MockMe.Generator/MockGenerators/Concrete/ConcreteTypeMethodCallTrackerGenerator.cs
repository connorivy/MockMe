using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using MockMe.Generator.Extensions;

namespace MockMe.Generator.MockGenerators.Concrete;

internal class ConcreteTypeMethodCallTrackerGenerator
{
    private const string Void = "Void";
    private readonly IMethodSymbol methodSymbol;
    private readonly string returnType;
    private readonly string voidPrefix;
    private readonly string paramTypes;

    public ConcreteTypeMethodCallTrackerGenerator(IMethodSymbol methodSymbol)
    {
        this.methodSymbol = methodSymbol;
        this.returnType = methodSymbol.ReturnType.ToDisplayString();
        this.paramTypes = methodSymbol.GetParameterTypesWithoutModifiers();
        this.voidPrefix = this.returnType == "void" ? Void : string.Empty;
    }

    public StringBuilder AddMethodCallTrackerToStringBuilder(StringBuilder sb)
    {
        int numParameters = this.methodSymbol.Parameters.Length;

        string paramsWithTypesAndMods =
            this.methodSymbol.GetParametersWithOriginalTypesAndModifiers();
        string paramString = this.methodSymbol.GetParametersWithoutTypesAndModifiers();
        if (this.methodSymbol.TypeParameters.Length > 0)
        {
            sb.AppendLine(
                $@"
            private Dictionary<int, object>? {this.MethodName()}CallStore;

            public {this.returnType} {this.MethodName()}{this.methodSymbol.GetGenericParameterStringInBrackets()}({paramsWithTypesAndMods})
            {{
                int genericTypeHashCode = GetUniqueIntFromTypes({string.Join(", ", this.methodSymbol.Parameters.Select(p => p.Type.ToDisplayString().AddOnIfNotEmpty("typeof(", ")")))});
                var mockStore =
                    this.setup.{this.MethodName()}BagStore?.GetValueOrDefault(genericTypeHashCode)
                    as List<ArgBagWith{this.voidPrefix}MemberMock{(this.paramTypes + this.returnType.AddPrefixIfNotEmpty(", ")).AddOnIfNotEmpty("<", ">")}>;

                {(this.returnType == "void" ? string.Empty : "return ")} Call{this.voidPrefix}MemberMock(
                    mockStore,
                    GetGenericCallStore{this.paramTypes.AddOnIfNotEmpty("<", ">")}({this.MethodName()}CallStore ??= new(), genericTypeHashCode){paramString.AddPrefixIfNotEmpty(", ")}
                );
            }}"
            );
        }
        else if (numParameters == 0)
        {
            sb.AppendLine(
                $@"
            private int {this.MethodName()}CallStore;

            public {this.returnType} {this.MethodName()}()
            {{
                this.{this.MethodName()}CallStore++;
                {(this.returnType == "void" ? string.Empty : "return ")}Call{this.voidPrefix}MemberMock(this.setup.{this.MethodName()}BagStore);
            }}"
            );
        }
        else
        {
            sb.AppendLine(
                $@"
            private List<{this.methodSymbol.GetMethodArgumentsAsCollection()}>? {this.MethodName()}CallStore;

            public {this.returnType} {this.MethodName()}({paramsWithTypesAndMods}) => Call{this.voidPrefix}MemberMock(this.setup.{this.MethodName()}BagStore, this.{this.MethodName()}CallStore ??= new(), {paramString});"
            );
        }
        return sb;
    }

    private string GetCallStoreType()
    {
        if (this.NumGenericParameters() > 0)
        {
            return "Dictionary<int, object>";
        }
        else if (this.NumParameters() == 0)
        {
            return "int";
        }
        else
        {
            return $"List<{this.methodSymbol.GetMethodArgumentsAsCollection()}>";
        }
    }

    private string MethodName() => this.methodSymbol.Name;

    private int NumGenericParameters() => this.methodSymbol.TypeParameters.Length;

    private int NumParameters() => this.methodSymbol.Parameters.Length;
}
