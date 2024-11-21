using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;

namespace MockMe.Generator.MockGenerators.Concrete;
internal interface IMethodMockGenerator
{
    StringBuilder AddMethodCallTrackerToStringBuilder(StringBuilder sb, Dictionary<string, PropertyMetadata> callTrackerMeta);
    StringBuilder AddMethodSetupToStringBuilder(StringBuilder sb);
    StringBuilder AddMethodToAsserterClass(StringBuilder sb);
    StringBuilder AddPatchMethod(StringBuilder sb, StringBuilder assemblyAttributesSource, StringBuilder staticConstructor, ITypeSymbol typeSymbol);
}