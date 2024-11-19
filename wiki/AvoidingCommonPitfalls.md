# The type 'MyCoolType' cannot be used as type parameter 'T' in generic method Mock.Me<T\>()

# The call 'Mock.Me<T\>()' is ambiguous between the following methods or properties

tldr - change to

```csharp
Mock.Me<MyCoolType>(default(MyCoolType));
```

These two error stem from the same issue. This issue will occur with sealed types or when mocking multiple classes with an inheritance relationship. Because C# requires your method signatures to be unique,
each generated Mock.Me<>() call takes a single argument of the mocked object type set to null. We can guide the compiler to picking the correct overload using generic constraints
such as below.

```csharp

internal static class Mock
{
    public static ParentMock Me<T>(Parent? unusedInstance = null)
        where T : Parent
    {
        // do stuff
    }

    public static ChildMock Me<T>(Child? unusedInstance = null)
        where T : Child
    {
        // do stuff
    }

    public static SealedMock Me<T>(Sealed? unusedInstance = null)
        // where T : Sealed // Sealed types cannot be used as generic parameter constraints
    {
        // do stuff
    }
}

```

A quick glance at this shows you that collisions are imminent. Mocking the parent class will work using the normal syntax, however the Child class is also a parent, so the compiler
won't know which method to select. Same for the sealed type. Because sealed types cannot be used as generic constraints, the typed parameter must be explicitly specified as shown in
the tldr at the top of this explaination.
