# Common Errors

> If you run into an error, check here first. If you don't see the error that you are experiencing, or if the recommended resolution doesn't work for you, then please [open an issue](https://github.com/connorivy/MockMe/issues/).

## Cannot convert from 'MyTypeToMock' to 'MockMe.DummyClass'

#### How to resolve
You need to trigger the source generators. This can be done is a couple ways,
1. Save the current file. (This will USUALLY be enough)
2. If that didn't work, the build the test project.

#### Why it occurs
This error occurs when you initially write `Mock.Me(default(MyTypeToMock))` and the source generator has yet to trigger. There is an initial overload of Mock.Me that just serves the purpose of helping to fill in intellisense. The correct method, the one that will create a mock of your object, will not exist until the source generators have created it.

## Mock object is missing methods or properties that exist in the mocked class

#### How to resolve
Rebuild (build alone does not always suffice) the test project.

#### Why it occurs
Even though it looks like the class member doesn't exist, it does. The issue is that intellisense can be a bit slow to pick up all the members on the source generated classes.
