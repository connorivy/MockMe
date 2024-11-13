using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using MockMe.Generator.MockGenerators.Concrete;

namespace MockMe.Generator.MockGenerators;

internal abstract class MockGeneratorBase
{
    private const string MockNamespace = "MockMe.Mocks.Generated";
    private const string voidString = "void";

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
using static {thisNamespace}.{typeSymbol.Name}MockSetup;
using static {thisNamespace}.{typeSymbol.Name}MockSetup.{typeSymbol.Name}MockCallTracker;

namespace {thisNamespace}
{{
    internal class {typeSymbol.Name}Mock
        : {this.GetMockBaseClass(typeSymbol)}
    {{
        {this.GetConstructorAndProps(typeSymbol)}"
        );

        StringBuilder setupBuilder = new();

        setupBuilder.AppendLine(
            $@"
    public class {typeSymbol.Name}MockSetup : global::MockMe.Mocks.ClassMemberMocks.Setup.MemberMockSetup
    {{"
        );

        StringBuilder callTrackerBuilder = new();
        callTrackerBuilder.AppendLine(
            $@"
        public class {typeSymbol.Name}MockCallTracker : {this.GetCallTrackerBaseClass(typeSymbol)}
        {{
            private readonly {typeSymbol.Name}MockSetup setup;
            public {typeSymbol.Name}MockCallTracker({typeSymbol.Name}MockSetup setup)
            {{
                this.setup = setup;
            }}"
        );

        StringBuilder asserterBuilder = new();
        asserterBuilder.AppendLine(
            $@"
            public class {typeSymbol.Name}MockAsserter : MockAsserter
            {{
                private readonly {typeSymbol.Name}MockCallTracker tracker;
                public {typeSymbol.Name}MockAsserter({typeSymbol.Name}MockCallTracker tracker)
                {{
                    this.tracker = tracker;
                }}"
        );

        Dictionary<string, PropertyMetadata> callTrackerMeta = [];
        foreach (var method in typeSymbol.GetMembers())
        {
            if (method is not IMethodSymbol methodSymbol)
            {
                continue;
            }

            var methodName = methodSymbol.Name;

            if (methodName == ".ctor")
            {
                continue;
            }

            ConcreteTypeMethodSetupGenerator methodGenerator = new(methodSymbol);

            if (typeSymbol.TypeKind != TypeKind.Interface)
            {
                methodGenerator.AddPatchMethod(sb, assemblyAttributesSource, typeSymbol);
            }
            methodGenerator.AddMethodSetupToStringBuilder(setupBuilder);
            methodGenerator.AddMethodCallTrackerToStringBuilder(
                callTrackerBuilder,
                callTrackerMeta
            );
            methodGenerator.AddMethodToAsserterClass(asserterBuilder);
        }

        // close mock class
        sb.AppendLine(
            @$"
    }}"
        );

        asserterBuilder.AppendLine(
            $@"
            }}"
        );

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
