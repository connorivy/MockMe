using System;

namespace MockMe.Tests.ExampleClasses;

public class ClassWithGenericMethods
{
    public T OneGenericType<T>(T t) => throw new NotImplementedException();

    public T TwoGenericTypes<T, U>(T t, U u) => throw new NotImplementedException();

    public T ThreeGenericTypes<T, U, V>(T t, U u, V v) => throw new NotImplementedException();
}
