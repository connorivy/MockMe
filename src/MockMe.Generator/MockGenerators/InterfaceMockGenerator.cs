using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using MockMe.Generator.Extensions;
using MockMe.Generator.MockGenerators.Concrete;

namespace MockMe.Generator.MockGenerators;

internal class InterfaceMockGenerator
{
    private const string MockNamespace = "MockMe.Mocks.Generated";
    private const string voidString = "void";

    public static StringBuilder CreateMockForConcreteType(
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
    public class {typeSymbol.Name}Mock
        : global::MockMe.Abstractions.InterfaceMock<global::{typeSymbol}, {typeSymbol.Name}MockCallTracker>
    {{
        public {typeSymbol.Name}Mock() : base(CreateCallTracker(out var setup, out var asserter))
        {{
            this.Setup = setup;
            this.Assert = asserter;
        }}

        private static {typeSymbol.Name}MockCallTracker CreateCallTracker(
            out {typeSymbol.Name}MockSetup setup,
            out {typeSymbol.Name}MockAsserter asserter 
        )
        {{
            setup = new {typeSymbol.Name}MockSetup();
            var callTracker = new {typeSymbol.Name}MockCallTracker(setup);
            asserter = new {typeSymbol.Name}MockAsserter(callTracker);

            return callTracker;
        }}

        public {typeSymbol.Name}MockSetup Setup {{ get; }}
        public {typeSymbol.Name}MockAsserter Assert {{ get; }}
"
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
        public class {typeSymbol.Name}MockCallTracker : {typeSymbol.ToFullTypeString()}
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

            //methodGenerator.AddPatchMethod(sb, assemblyAttributesSource, typeSymbol);
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
}
