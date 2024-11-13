using System;
using System.Collections.Generic;
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
        this.returnTypeIgnoringTask =
            methodSymbol.ReturnType.GetInnerTypeIfTask()?.ToFullReturnTypeString()
            ?? methodSymbol.ReturnType.GetVoidIfTask()
            ?? methodSymbol.ReturnType.ToFullReturnTypeString();

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
            (this.isVoidReturnType || this.methodSymbol.ReturnType.IsNonGenericTask())
                ? string.Empty
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

        string callEnd = this.methodSymbol.MethodKind switch
        {
            MethodKind.PropertyGet => "",
            MethodKind.PropertySet => $" = {paramString}",
            _ => $"({paramString})",
        };

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
                    {(this.isVoidReturnType ? string.Empty : "__result = ")}mock.CallTracker.{this.methodSymbol.GetPropertyName()}{callEnd};
                    return false;
                }}
                return true;
            }}
        }}"
            );
            sb.AppendLine(
                $@"
                    
"
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
        private {this.GetBagStoreType()}? {this.GetBagStoreName()};
        public {this.memberMockType} {this.MethodName()}{this.methodSymbol.GetGenericParameterStringInBrackets()}({this.methodSymbol.GetParametersWithArgTypesAndModifiers()}) =>
            {this.GetSetupMethod()}"
        );
    }

    public StringBuilder AddMethodCallTrackerToStringBuilder(
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

            if (!callTrackerMeta.TryGetValue(methodName, out var propMeta))
            {
                propMeta = new() { Name = methodName, ReturnType = this.returnType };
                callTrackerMeta.Add(methodName, propMeta);
            }

            propMeta.GetterLogic =
                @$"
        this.{this.GetCallStoreName()}++;
        return MockCallTracker.Call{this.voidPrefix}MemberMock(this.setup.{this.GetBagStoreName()});";

            propMeta.GetterField = $"private int {this.GetCallStoreName()};";
        }
        else if (this.methodSymbol.MethodKind == MethodKind.PropertySet)
        {
            var methodName = this.methodSymbol.GetPropertyName();

            if (!callTrackerMeta.TryGetValue(methodName, out var propMeta))
            {
                propMeta = new() { Name = methodName, ReturnType = this.returnType };
                callTrackerMeta.Add(methodName, propMeta);
            }

            propMeta.SetterLogic =
                @$"
        MockCallTracker.Call{this.voidPrefix}MemberMock(this.setup.{this.GetBagStoreName()}, this.{this.GetCallStoreName()} ??= new(), value);";

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

                {(this.isVoidReturnType ? string.Empty : "return ")} MockCallTracker.Call{this.voidPrefix}MemberMock(
                    mockStore,
                    GetGenericCallStore{this.paramTypes.AddOnIfNotEmpty("<", ">")}({this.GetCallStoreName()} ??= new(), genericTypeHashCode){paramString.AddPrefixIfNotEmpty(", ")}
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
                {(this.isVoidReturnType ? string.Empty : "return ")}MockCallTracker.Call{this.voidPrefix}MemberMock(this.setup.{this.GetBagStoreName()});
            }}"
            );
        }
        else
        {
            sb.AppendLine(
                $@"
            private List<{this.methodSymbol.GetMethodArgumentsAsCollection()}>? {this.GetCallStoreName()};

            public {this.returnType} {this.MethodName()}({paramsWithTypesAndMods}) => MockCallTracker.Call{this.voidPrefix}MemberMock(this.setup.{this.GetBagStoreName()}, this.{this.GetCallStoreName()} ??= new(), {paramString});"
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
                    return GetMemberAsserter(this.tracker.{this.GetCallStoreName()}?.GetValueOrDefault(genericTypeHashCode) as List<{this.methodSymbol.GetMethodArgumentsAsCollection()}>, {parameters});
                }}"
            );
        }
        else if (this.methodSymbol.Parameters.Length == 0)
        {
            sb.AppendLine(
                $@"
                public MemberAsserter 
            {this.MethodName()}() =>
                    new(this.tracker.{this.GetCallStoreName()});"
            );
        }
        else
        {
            sb.AppendLine(
                $@"
                public MemberAsserter {this.MethodName()}({parametersDefinition})
                {{
                    return GetMemberAsserter(this.tracker.{this.GetCallStoreName()}, {parameters});
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

    private string GetBagStoreName() => this.methodSymbol.GetUniqueMethodName() + "BagStore";

    private string GetCallStoreName() => this.methodSymbol.GetUniqueMethodName() + "CallStore";

    private string GetSetupMethod()
    {
        if (this.NumGenericParameters() > 0)
        {
            return $"Setup{this.voidPrefix}{this.taskPrefix}Method(SetupGenericStore{this.paramTypesFollowedByReturnType}(this.{this.GetBagStoreName()} ??= new()){this.parametersWithoutTypesAndModifiers.AddPrefixIfNotEmpty(", ")});";
        }
        if (this.NumParameters() == 0)
        {
            return $"this.{this.GetBagStoreName()} ??= new();";
        }
        else
        {
            return $"Setup{this.voidPrefix}{this.taskPrefix}Method(this.{this.GetBagStoreName()} ??= new(), {this.parametersWithoutTypesAndModifiers});";
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
