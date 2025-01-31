# Ref and Out parameters

```csharp
class MyCoolClass
{
    public bool TryGetVal(int num, out string val)
    {
        // code
    }

    public void MethodWithRef(ref int val)
    {
        // code
    }
}

var mock = Mock.Me(default(MyCoolClass));
MyCoolClass myClass = mock;

// the out parameter can be discarded, as it doesn't do anything when used by the Setup or Assert property methods.
// the only reason it is here is because dropping it could lead to conflicts with other method overloads
mock.Setup.TryGetVal(Arg.Any(), out _).Returns(args => 
{
    args.val = "my out value";
    return true;
});
myClass.TryGetValue(99, out var outValue); // outValue is "my out value"


// the 'Arg' parameter must be passed by reference 
// (again because dropping the ref keyword could lead to conflicts with other method overloads))
// but it has the same behavior as it does when not passed by reference
Arg<int> negative = new(i => i < 0);
mock.Setup.MethodWithRef(ref negative).Returns(args => 
{
    args.val += 100;
});
int negative = -1;
myClass.MethodWithRef(ref negative); // negative is now 99


```

**_NOTE:_** The `arg.val` will only be settable if the method parameter is passed by reference, such as with ref and out parameters because assigning to this parameter is meaningless for a normal method parameter.
