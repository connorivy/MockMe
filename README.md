![MockMeFull](https://github.com/user-attachments/assets/43d8b58f-98b0-4469-95c3-7e5ca0683ffc)

___

[![Coverage Status](https://coveralls.io/repos/github/connorivy/MockMe/badge.svg?branch=main)](https://coveralls.io/github/connorivy/MockMe?branch=main)

## What is it?

MockMe is a library for mocking dependencies in your production code. Unlike other libraries that can only mock interfaces and virtual methods, MockMe can mock sealed classes and non-virtual methods.

## Getting Started

Download NuGet package, then the source generators and the "MockMe.Mock" type will be available in your project.

```csharp

var mock = Mock.Me(default(MyRepo));

mock.Setup.ExpensiveDatabaseCall().Returns(99);

MyRepo myRepo = mock.MockedObject;

Assert.Equal(99, myRepo.ExpensiveDatabaseCall());
mock.Assert.ExpensiveDatabaseCall().WasCalled();

```

Check out the [Wiki](https://github.com/connorivy/MockMe/wiki/QuickStart) for more examples.
