using System.Text;
using Microsoft.CodeAnalysis;

namespace MockMe.Generator.MockGenerators.Concrete;

internal class MockGenerator
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
    public class {typeSymbol.Name}Mock : Mock<global::{typeSymbol}>
    {{
        public {typeSymbol.Name}Mock()
        {{
            this.Setup = new {typeSymbol.Name}MockSetup();
            this.CallTracker = new {typeSymbol.Name}MockCallTracker(this.Setup);
            this.Assert = new {typeSymbol.Name}MockAsserter(this.CallTracker);
            global::MockMe.MockStore<global::{typeSymbol}>.Store.TryAdd(this.MockedObject, this);
        }}

        public {typeSymbol.Name}MockSetup Setup {{ get; }}
        public {typeSymbol.Name}MockAsserter Assert {{ get; }}
        private {typeSymbol.Name}MockCallTracker CallTracker {{ get; set; }}
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
        public class {typeSymbol.Name}MockCallTracker : MockCallTracker
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

            methodGenerator.AddPatchMethod(sb, assemblyAttributesSource, typeSymbol);
            methodGenerator.AddMethodSetupToStringBuilder(setupBuilder);
            methodGenerator.AddMethodCallTrackerToStringBuilder(callTrackerBuilder);
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
