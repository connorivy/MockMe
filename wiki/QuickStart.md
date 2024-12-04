# Controlling Return Value

You can control the return value of public methods and properties by using the 'Setup' property combined with the 'Returns' method.


```csharp

var mock = Mock.Me<Calculator>();

// specify that 'Add(1, 1)' should return 99
mock.Setup.Add(1, 1).Returns(99);

// the returns method also takes a function with an argument that is a collection of the arguments that the method takes.
// specify that 'Add(x, y)' should return x * y
mock.Setup.Add(5, 5).Returns(args => args.x * args.y);

// properties are broken down futher into 'Get()' and 'Set(T propValue)' methods.
// if there isn't a public setter, then only the 'Get()' method will exist
mock.Setup.CalculatorType.Get().Returns(CalculatorType.Scientific);

// configure methods to throw
mock.Setup.Add(-99, -99).Throws(new InvalidOperationException());

// async methods can be configured with typical "Returns" or "ReturnsAsync" methods
mock.Setup.AddAsync(10, 10).ReturnsAsync(-10);
mock.Setup.AddAsync(20, 20).Returns(Task.FromResult(-20));

mock.Setup["string indexer"].Get().Returns(9.999);

Calculator calc = mock; // convert to mocked object with implicit cast
// alternatively Calculator calc = mock.MockedObject;

calc.Add(1, 1); // result is 99
calc.Add(5, 5); // result is 25 (5 * 5)
calc.Add(-99, -99); // throws invalidOperationException
calc.AddAsync(10, 10); // result is -10
calc.AddAsync(20, 20); // result is -20
calc["string indexer"]; // result is 9.999

```

# Matching Arguments

```csharp

var mock = Mock.Me<Calculator>();

// return no matter the arguments passed into the 'Add' method
mock.Setup.Add(Arg.Any(), Arg.Any()).Returns(99);

// match any argument while specifying the argument type
mock.Setup.Add(Arg.Any<double>(), Arg.Any<double>()).Returns(99);

// match for specific type of argument 
// in this case, both numbers must be less than 0
mock.Setup.Add(Arg.Where<int>(i => i < 0), Arg.Where<int>(i => i < 0)).Returns(-1);

// match for exact values
mock.Setup.Add(5, 5).Returns(555);

var calc = (Calculator)mock;

mock.Add(1, 1); // result is 99
mock.Add(-1, -1); // result is -1
mock.Add(5, 5); // result is 555

```

# Callbacks

```csharp

var mock = Mock.Me<Calculator>();

int numCalls = 0;
mock.Setup.Add(1, 1)
    .Callback(() => numCalls++)

// the returns has an overload with you can use to access the method's parameters
// specify that 'Add(x, y)' should return x * y
mock.Setup.Add(Arg.Any(), Arg.Any()).Returns((x, y) => x * y);

// callbacks can be specified before and after invocation
mock.Setup.Add(1, 1)
    .Callback(() => Console.WriteLine("This is called before return"))
    .Returns(55)
    .Callback(() => Console.WriteLine("This is called after returns"));

// capture invocation arguments
int? firstParameter = null;
mock.Setup.Add(2, 2)
    .Callback(args => xParameter = args.x); // the variable 'firstParameter' will be assigned

// access instances of objects that are assigned to properties
CalculatorType? typeUsedBySetter
mock.Setup.CalculatorType.Set(Arg.Any()).Callback(args => typeUsedBySetter = args.Value);
```

# Assertions

```csharp

var mock = Mock.Me<Calculator>();

// called at least once
mock.Assert.Add(1, 1).WasCalled();

mock.Assert.Add(1, 1).WasNotCalled();

mock.Assert.Add(1, 1).WasCalled(NumTimes.AtLeast(3));

mock.Assert.Add(1, 1).WasCalled(NumTimes.AtMost(3));

// assert property was set to specific value
mock.Assert.CalculatorType.Set(CalculatorType.Graphing).WasCalled();

```
