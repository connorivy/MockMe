using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using MockMe.Generator.Extensions;
using MockMe.Generator.MockGenerators.MethodGenerators;
using MockMe.Generator.MockGenerators.PatchMethodGenerators;

namespace MockMe.Generator.MockGenerators.TypeGenerators;

internal abstract class MockGeneratorBase
{
    private const string MockNamespace = "MockMe.Mocks.Generated";
    private const string voidString = "void";
    private readonly string genericParamsInBrackets;
    public string GenericParamTypesInBrackets { get; }
    protected string mockTypeName { get; }
    protected string mockSetupTypeName { get; }
    protected string mockCallTrackerTypeName { get; }
    protected string mockAsserterTypeName { get; }
    protected string thisNamespace { get; }

    protected string TypeName { get; }
    public INamedTypeSymbol TypeSymbolToMock { get; }

    protected MockGeneratorBase(INamedTypeSymbol typeSymbol, string typeName)
    {
        this.TypeName = typeName;
        this.TypeSymbolToMock = typeSymbol;

        this.thisNamespace = $"MockMe.Generated.{this.TypeSymbolToMock.ContainingNamespace}";

        this.genericParamsInBrackets = string.Join(
                ", ",
                typeSymbol.TypeParameters.Select(p => p.Name)
            )
            .AddOnIfNotEmpty("<", ">");
        this.GenericParamTypesInBrackets = string.Join(
                ", ",
                typeSymbol.TypeArguments.Select(p => p.ToFullTypeString())
            )
            .AddOnIfNotEmpty("<", ">");

        this.mockTypeName = $"{this.TypeName}Mock{this.genericParamsInBrackets}";
        this.mockSetupTypeName = $"{this.TypeName}MockSetup{this.genericParamsInBrackets}";
        this.mockCallTrackerTypeName = $"{this.TypeName}MockCallTracker";
        this.mockAsserterTypeName = $"{this.TypeName}MockAsserter";
    }

    public StringBuilder CreateMockType(StringBuilder assemblyAttributesSource)
    {
        var thisNamespace = $"MockMe.Generated.{this.TypeSymbolToMock.ContainingNamespace}";
        StringBuilder sb = new();

        sb.AppendLine(
            $@"
#nullable enable
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using MockMe;
using MockMe.Mocks;
using MockMe.Mocks.ClassMemberMocks.CallTracker;

namespace {thisNamespace}
{{
    internal class {this.mockTypeName}
        : {this.GetMockBaseClass(this.TypeSymbolToMock)}
    {{
        {this.GetConstructorAndProps(this.TypeSymbolToMock)}"
        );

        StringBuilder setupBuilder = new();

        setupBuilder.AppendLine(
            $@"
    public class {this.mockSetupTypeName} : global::MockMe.Mocks.ClassMemberMocks.Setup.MemberMockSetup
    {{"
        );

        StringBuilder callTrackerBuilder = new();
        callTrackerBuilder.AppendLine(
            $@"
        public class {this.mockCallTrackerTypeName} : {this.GetCallTrackerBaseClass(this.TypeSymbolToMock)}
        {{
            private readonly {this.mockSetupTypeName} setup;
            public {this.mockCallTrackerTypeName}({this.mockSetupTypeName} setup)
            {{
                this.setup = setup;
            }}"
        );

        StringBuilder asserterBuilder = new();
        asserterBuilder.AppendLine(
            $@"
            public class {this.mockAsserterTypeName} : MockAsserter
            {{
                private readonly {this.mockSetupTypeName}.{this.mockCallTrackerTypeName} tracker;
                public {this.mockAsserterTypeName}({this.mockSetupTypeName}.{this.mockCallTrackerTypeName} tracker)
                {{
                    this.tracker = tracker;
                }}"
        );

        StringBuilder staticConstructor = new();
        staticConstructor.AppendLine(
            $@"
        static {this.TypeName}Mock()
        {{
            var harmony = new global::HarmonyLib.Harmony(""com.mockme.patch"");"
        );

        Dictionary<string, PropertyMetadata> callTrackerMeta = [];
        Dictionary<string, SetupPropertyMetadata> setupMeta = [];
        Dictionary<string, AssertPropertyMetadata> assertMeta = [];
        foreach (var method in this.TypeSymbolToMock.GetMembers())
        {
            if (method is not IMethodSymbol methodSymbol)
            {
                continue;
            }

            if (methodSymbol.MethodKind == MethodKind.Constructor)
            {
                continue;
            }

            if (methodSymbol.DeclaredAccessibility != Accessibility.Public)
            {
                continue;
            }

            MethodMockGeneratorBase? methodGenerator = MethodGeneratorFactory.Create(methodSymbol);

            if (methodGenerator is null)
            {
                continue;
            }

            PatchMethodGeneratorFactory
                .Create(this.TypeSymbolToMock, methodSymbol)
                ?.AddPatchMethod(sb, assemblyAttributesSource, staticConstructor, this.TypeName);

            //this.AddPatchMethod(
            //    sb,
            //    assemblyAttributesSource,
            //    staticConstructor,
            //    methodSymbol,
            //    this.TypeName
            //);

            //if (typeSymbol.TypeKind != TypeKind.Interface)
            //{
            //    methodGenerator.AddPatchMethod(
            //        sb,
            //        assemblyAttributesSource,
            //        staticConstructor,
            //        typeSymbol,
            //        this.TypeName
            //    );
            //}
            methodGenerator.AddMethodSetupToStringBuilder(setupBuilder, setupMeta);
            methodGenerator.AddMethodCallTrackerToStringBuilder(
                callTrackerBuilder,
                callTrackerMeta
            );
            methodGenerator.AddMethodToAsserterClass(asserterBuilder, assertMeta);
        }

        staticConstructor.AppendLine(
            $@"
        }}"
        );

        sb.Append(staticConstructor);

        // close mock class
        sb.AppendLine(
            @$"
    }}"
        );

        foreach (var meta in assertMeta.Values)
        {
            meta.AddPropToSb(asserterBuilder);
        }

        asserterBuilder.AppendLine(
            $@"
            }}"
        );

        foreach (var meta in setupMeta.Values)
        {
            meta.AddPropToSb(setupBuilder);
        }

        foreach (var meta in callTrackerMeta.Values)
        {
            meta.AddPropToSb(callTrackerBuilder);
        }
        callTrackerBuilder.Append(asserterBuilder);

        callTrackerBuilder.AppendLine(
            @$"
        }}"
        );

        setupBuilder.Append(callTrackerBuilder);

        setupBuilder.AppendLine(
            @$"
    }}"
        );

        sb.Append(setupBuilder);

        // close namespace
        sb.AppendLine(
            @$"
}}"
        );

        return sb;
    }

    public abstract string GetMockBaseClass(ITypeSymbol typeSymbol);
    public abstract string GetCallTrackerBaseClass(ITypeSymbol typeSymbol);
    public abstract string GetConstructorAndProps(ITypeSymbol typeSymbol);
}
