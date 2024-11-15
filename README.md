## What is it?

MockMe is a library for mocking dependencies in your production code. Unlike other libraries that can only mock interfaces and virtual methods, MockMe can mock sealed classes and non-virtual methods.

## Why use MockMe?

MockMe is for people like myself who don't like making interfaces with a single implementation, just for the purpose of testing your code.

## Getting Started

Download NuGet package, then the source generators and the "MockMe.Mock" type will be available in your project.

```csharp

var mock = Mock.Me<MyRepo>();

mock.Setup.ExpensiveDatabaseCall().Returns(99);

MyRepo myRepo = mock.MockedObject;

Assert.Equal(99, myRepo.ExpensiveDatabaseCall());
mock.Assert.ExpensiveDatabaseCall().WasCalled();

```

Check out the [Wiki](https://github.com/connorivy/MockMe/wiki) for more examples.
