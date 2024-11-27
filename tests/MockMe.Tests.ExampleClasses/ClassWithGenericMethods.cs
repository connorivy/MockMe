using System;

namespace MockMe.Tests.ExampleClasses;

public class ClassWithGenericMethods
{
    public T OneGenericType<T>(T t) => throw new NotImplementedException();

    public T1 TwoGenericTypes<T1, T2>(T1 t, T2 t2) => throw new NotImplementedException();

    public T3 ThreeGenericTypes<T1, T2, T3>(T1 t, T2 t2, T3 t3) =>
        throw new NotImplementedException();
}
