using System;
using System.Linq;
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
    private readonly bool isVoidReturnType;
    private readonly string? taskPrefix;
    private readonly string returnTypeIgnoringTask;

    public ConcreteTypeMethodSetupGenerator(IMethodSymbol methodSymbol)
    {
        this.methodSymbol = methodSymbol;
        this.parametersWithoutTypesAndModifiers =
            methodSymbol.GetParametersWithoutTypesAndModifiers();

        this.returnType = methodSymbol.ReturnType.ToFullReturnTypeString();
        this.returnTypeIgnoringTask = (
            methodSymbol.ReturnType.GetInnerTypeIfTask() ?? methodSymbol.ReturnType
        ).ToFullReturnTypeString();
        this.isVoidReturnType = methodSymbol.ReturnType.SpecialType == SpecialType.System_Void;

        if (methodSymbol.ReturnType.IsTask())
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
            : this.paramTypes.Length == 0 ? this.returnType
            : $", {this.returnTypeIgnoringTask}";
        var paramTypesFollowedByReturnTypeIgnoringTask = GetParamTypesFollowedByReturnType(
            returnTypeIgnoringTaskGenericParamSuffix,
            this.paramTypes
        );

        this.memberMockType =
            $"global::MockMe.Mocks.ClassMemberMocks.{this.voidPrefix}{this.taskPrefix}MemberMock{paramTypesFollowedByReturnTypeIgnoringTask}";
    }

    public StringBuilder AddPatchMethod(
        StringBuilder sb,
        StringBuilder assemblyAttributesSource,
        ITypeSymbol typeSymbol
    )
    {
        var thisNamespace = $"MockMe.Generated.{typeSymbol.ContainingNamespace}";
        string paramsWithTypesAndMods =
            this.methodSymbol.GetParametersWithOriginalTypesAndModifiers();
        string paramTypeString = this.methodSymbol.GetParameterTypesWithoutModifiers();
        string paramString = this.methodSymbol.GetParametersWithoutTypesAndModifiers();

        if (this.methodSymbol.TypeParameters.Length == 0)
        {
            sb.AppendLine(
                $@"
        {this.methodSymbol.GetHarmonyPatchAnnotation(typeSymbol.ToFullTypeString())}
        internal sealed class Patch{Guid.NewGuid():N}
        {{
            private static bool Prefix({typeSymbol.ToFullTypeString()} __instance{(this.isVoidReturnType ? string.Empty : $", ref {this.returnType} __result")}{paramsWithTypesAndMods.AddPrefixIfNotEmpty(", ")})
            {{
                if (global::MockMe.MockStore<{typeSymbol.ToFullTypeString()}>.TryGetValue<{typeSymbol.Name}Mock>(__instance, out var mock))
                {{
                    {(this.isVoidReturnType ? string.Empty : "__result = ")}mock.CallTracker.{this.MethodName()}({paramString});
                    return false;
                }}
                return true;
            }}
        }}"
            );
        }
        else
        {
            sb.AppendLine(
                $@"
        private {this.returnType} {this.MethodName()}{this.methodSymbol.GetGenericParameterStringInBrackets()}({paramsWithTypesAndMods})
        {{
            if (global::MockMe.MockStore<{typeSymbol.ToFullTypeString()}>.GetStore().TryGetValue(default, out var mock))
            {{
                var callTracker = mock.GetType()
                    .GetProperty(
                        ""CallTracker"",
                        System.Reflection.BindingFlags.NonPublic
                            | System.Reflection.BindingFlags.Instance
                    )
                    .GetValue(mock);

                return ({this.returnType})
                    callTracker
                        .GetType()
                        .GetMethod(
                            ""{this.MethodName()}"",
                            System.Reflection.BindingFlags.Public
                                | System.Reflection.BindingFlags.Instance
                        )
                        .MakeGenericMethod({string.Join(", ", this.methodSymbol.TypeParameters.Select(p => p.Name.AddOnIfNotEmpty("typeof(", ")")))})
                        .Invoke(callTracker, new object[] {{ {paramString} }});
            }}
            return default;
        }}
"
            );

            assemblyAttributesSource.AppendLine(
                $@"
[assembly: global::MockMe.Abstractions.GenericMethodDefinition(
    ""{typeSymbol.ContainingNamespace}"",
    ""{typeSymbol}"",
    ""{this.methodSymbol.Name}"",
    ""{thisNamespace}"",
    ""{thisNamespace}.{typeSymbol.Name}Mock"",
    ""{this.MethodName()}""
)]
"
            );
        }
        return sb;
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

    public StringBuilder AddMethodCallTrackerToStringBuilder(StringBuilder sb)
    {
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
                int genericTypeHashCode = GetUniqueIntFromTypes({string.Join(", ", this.methodSymbol.Parameters.Select(p => p.Type.ToFullTypeString().AddOnIfNotEmpty("typeof(", ")")))});
                var mockStore =
                    this.setup.{this.MethodName()}BagStore?.GetValueOrDefault(genericTypeHashCode)
                    as List<ArgBagWith{this.voidPrefix}MemberMock{(this.paramTypes + this.returnType.AddPrefixIfNotEmpty(", ")).AddOnIfNotEmpty("<", ">")}>;

                {(this.isVoidReturnType ? string.Empty : "return ")} MockCallTracker.Call{this.voidPrefix}MemberMock(
                    mockStore,
                    GetGenericCallStore{this.paramTypes.AddOnIfNotEmpty("<", ">")}({this.MethodName()}CallStore ??= new(), genericTypeHashCode){paramString.AddPrefixIfNotEmpty(", ")}
                );
            }}"
            );
        }
        else if (this.methodSymbol.Parameters.Length == 0)
        {
            sb.AppendLine(
                $@"
            private int {this.MethodName()}CallStore;

            public {this.returnType} {this.MethodName()}()
            {{
                this.{this.MethodName()}CallStore++;
                {(this.isVoidReturnType ? string.Empty : "return ")}MockCallTracker.Call{this.voidPrefix}MemberMock(this.setup.{this.MethodName()}BagStore);
            }}"
            );
        }
        else
        {
            sb.AppendLine(
                $@"
            private List<{this.methodSymbol.GetMethodArgumentsAsCollection()}>? {this.MethodName()}CallStore;

            public {this.returnType} {this.MethodName()}({paramsWithTypesAndMods}) => MockCallTracker.Call{this.voidPrefix}MemberMock(this.setup.{this.MethodName()}BagStore, this.{this.MethodName()}CallStore ??= new(), {paramString});"
            );
        }
        return sb;
    }

    public StringBuilder AddMethodToAsserterClass(StringBuilder sb)
    {
        var parametersDefinition = this.methodSymbol.GetParametersWithArgTypesAndModifiers();
        var parameters = this.methodSymbol.GetParametersWithoutTypesAndModifiers();

        if (this.methodSymbol.TypeParameters.Length > 0)
        {
            sb.AppendLine(
                $@"
                public MemberAsserter {this.MethodName()}{this.methodSymbol.GetGenericParameterStringInBrackets()}({parametersDefinition})
                {{
                    int genericTypeHashCode = GetUniqueIntFromTypes({string.Join(", ", this.methodSymbol.TypeParameters.Select(p => p.Name.AddOnIfNotEmpty("typeof(", ")")))});
                    return GetMemberAsserter(this.tracker.{this.MethodName()}CallStore?.GetValueOrDefault(genericTypeHashCode) as List<{this.methodSymbol.GetMethodArgumentsAsCollection()}>, {parameters});
                }}"
            );
        }
        else if (this.methodSymbol.Parameters.Length == 0)
        {
            sb.AppendLine(
                $@"
                public MemberAsserter 
            {this.MethodName()}() =>
                    new(this.tracker.{this.MethodName()}CallStore);"
            );
        }
        else
        {
            sb.AppendLine(
                $@"
                public MemberAsserter {this.MethodName()}({parametersDefinition})
                {{
                    return GetMemberAsserter(this.tracker.{this.MethodName()}CallStore, {parameters});
                }}"
            );
        }

        return sb;
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
            return $"List<ArgBagWith{this.voidPrefix}MemberMock{this.paramTypesFollowedByReturnType}>";
        }
    }

    private string GetSetupMethod()
    {
        if (this.NumGenericParameters() > 0)
        {
            return $"Setup{this.voidPrefix}{this.taskPrefix}Method(SetupGenericStore{this.paramTypesFollowedByReturnType}(this.{this.MethodName()}BagStore ??= new()){this.parametersWithoutTypesAndModifiers.AddPrefixIfNotEmpty(", ")});";
        }
        if (this.NumParameters() == 0)
        {
            return $"this.{this.MethodName()}BagStore ??= new();";
        }
        else
        {
            return $"Setup{this.voidPrefix}{this.taskPrefix}Method(this.{this.MethodName()}BagStore ??= new(), {this.parametersWithoutTypesAndModifiers});";
        }
    }

    private string MethodName()
    {
        //if (this.methodSymbol.MethodKind == MethodKind.PropertyGet)
        //{
        //    return this.methodSymbol.Name.Substring(4) + "_get";
        //}

        //if (this.methodSymbol.MethodKind == MethodKind.PropertySet)
        //{
        //    return this.methodSymbol.Name.Substring(4) + "_set";
        //}

        return this.methodSymbol.Name;
    }

    private int NumGenericParameters() => this.methodSymbol.TypeParameters.Length;

    private int NumParameters() => this.methodSymbol.Parameters.Length;
}
