using System;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using MockMe.Generator.Extensions;

namespace MockMe.Generator.MockGenerators.TypeGenerators;

internal class GenericConcreteTypeMockGenerator(INamedTypeSymbol typeSymbol, string typeName)
    : ConcreteMockGenerator(typeSymbol, typeName) { }
