# Controlling Return Value

You can control the return value of public methods and properties by using the 'Setup' property combined with the 'Returns' method.


```csharp

var mock = Mock.Me<ComplexCalculator>();

// specify that 'Add(1, 1)' should return 99
mock.Setup.Add(1, 1).Returns(99);

// the returns method also takes a function with the required arguments
// specify that 'Add(x, y)' should return x * y
mock.Setup.Add(5, 5).Returns((x, y) => x * y);

// properties are broken down into 'get_Property' and 'set_Property' methods.
// if there isn't a public setter, then only the 'get_Property' method will exist
mock.Setup.get_CalculatorType().Returns(CalculatorType.Scientific);

ComplexCalculator calc = (ComplexCalculator)mock;
// alternatively ComplexCalculator calc = mock.MockedObject;

calc.Add(1, 1); // result is 99
calc.Add(5, 5); // result is 25 (5 * 5)

```

# Matching Arguments

```csharp

var mock = Mock.Me<ComplexCalculator>();

// return no matter the arguments passed into the 'Add' method
mock.Setup.Add(Arg.Any, Arg.Any).Returns(99);

// match for specific type of argument 
// in this case, both numbers must be less than 0
mock.Setup.Add(Arg.Where<int>(i => i < 0), Arg.Where<int>(i => i < 0)).Returns(-1);

// match for exact values
mock.Setup.Add(5, 5).Returns(555);

var calc = (ComplexCalculator)mock;

mock.Add(1, 1); // result is 99
mock.Add(-1, -1); // result is -1
mock.Add(5, 5); // result is 555

```

# Callbacks

```csharp

var mock = Mock.Me<ComplexCalculator>();

int numCalls = 0;
mock.Setup.Add(1, 1)
    .Callback(() => numCalls++)

// the returns has an overload with you can use to access the method's parameters
// specify that 'Add(x, y)' should return x * y
mock.Setup.Add(Arg.Any, Arg.Any).Returns((x, y) => x * y);

// callbacks can be specified before and after invocation
mock.Setup.Add(1, 1)
    .Callback(() => Console.WriteLine("This is called before return"))
    .Returns(55)
    .Callback(() => Console.WriteLine("This is called after returns"));

// capture invocation arguments
int? firstParameter = null;
mock.Setup.Add(2, 2)
    .Callback((x, _) => firstParameter = x); // the variable 'firstParameter' will be assigned

```

# Assertions

```csharp

var mock = Mock.Me<ComplexCalculator>();

// called at least once
mock.Assert.Add(1, 1).WasCalled();

mock.Assert.Add(1, 1).WasNotCalled();

mock.Assert.Add(1, 1).WasCalled(NumTimes.AtLeast, 3);

mock.Assert.Add(1, 1).WasCalled(NumTimes.AtMost, 3);

// assert property was set to specific value
mock.Assert.set_CalculatorType(CalculatorType.Graphing).WasCalled();

```
