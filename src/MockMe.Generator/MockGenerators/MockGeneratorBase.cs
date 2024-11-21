using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using MockMe.Generator.MockGenerators.MethodGenerators;

namespace MockMe.Generator.MockGenerators;

internal abstract class MockGeneratorBase(string typeName)
{
    private const string MockNamespace = "MockMe.Mocks.Generated";
    private const string voidString = "void";

    protected string TypeName { get; } = typeName;

    public StringBuilder CreateMockType(
        ITypeSymbol typeSymbol,
        StringBuilder assemblyAttributesSource
    )
    {
        var thisNamespace = $"MockMe.Generated.{typeSymbol.ContainingNamespace}";
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
using static {thisNamespace}.{this.TypeName}MockSetup;
using static {thisNamespace}.{this.TypeName}MockSetup.{this.TypeName}MockCallTracker;

namespace {thisNamespace}
{{
    internal class {this.TypeName}Mock
        : {this.GetMockBaseClass(typeSymbol)}
    {{
        {this.GetConstructorAndProps(typeSymbol)}"
        );

        StringBuilder setupBuilder = new();

        setupBuilder.AppendLine(
            $@"
    public class {this.TypeName}MockSetup : global::MockMe.Mocks.ClassMemberMocks.Setup.MemberMockSetup
    {{"
        );

        StringBuilder callTrackerBuilder = new();
        callTrackerBuilder.AppendLine(
            $@"
        public class {this.TypeName}MockCallTracker : {this.GetCallTrackerBaseClass(typeSymbol)}
        {{
            private readonly {this.TypeName}MockSetup setup;
            public {this.TypeName}MockCallTracker({this.TypeName}MockSetup setup)
            {{
                this.setup = setup;
            }}"
        );

        StringBuilder asserterBuilder = new();
        asserterBuilder.AppendLine(
            $@"
            public class {this.TypeName}MockAsserter : MockAsserter
            {{
                private readonly {this.TypeName}MockCallTracker tracker;
                public {this.TypeName}MockAsserter({this.TypeName}MockCallTracker tracker)
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
        foreach (var method in typeSymbol.GetMembers())
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

            if (typeSymbol.TypeKind != TypeKind.Interface)
            {
                methodGenerator.AddPatchMethod(
                    sb,
                    assemblyAttributesSource,
                    staticConstructor,
                    typeSymbol,
                    this.TypeName
                );
            }
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
