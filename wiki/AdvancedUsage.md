# Ref and Out parameters

```csharp
class MyCoolClass
{
    public bool TryGetVal(int num, out string val)
    {
        // code
    }
}

var mock = Mock.Me<MyCoolClass>();

// the out parameter can be discarded, as it doesn't do anything here.
// the only reason it is here is because dropping it could lead to conflicts with other method overloads
mock.Setup.TryGetVal(Arg.Any(), out _).Returns(args => 
{
    args.val = "my out value";
    return true;
});

MyCoolClass myClass = mock;

myClass.TryGetValue(99, out var outValue); // outValue is "my out value"

```

**_NOTE:_** The `arg.val` will only be settable if the method parameter is passed by reference, such as with ref and out parameters because assigning to this parameter is meaningless for a normal method parameter.
