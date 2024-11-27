# How does it work?

## Interfaces

No magic is happening when mocking interfaces and virtual methods. Source generators will generate an implementation of your contract and will fill it in with the logic for a MockMe mock.

## Concrete / Sealed Classes

For most scenarios involving concrete classes, the work that MockMe is doing is actually pretty simple. MockMe uses a different library called [Harmony](https://github.com/pardeike/Harmony) which is made for patching methods without modifying dlls. 

The patching process involves creating a new method using intermediate language instructions, and then modifying the memory address of the original method's pointer to point to the newly created method.

## Edge cases

### Generics

Generic methods of concrete classes are tricky / sometimes impossible to patch. In this scenario, MockMe is actually modifying you dlls using an amazing library called [Mono.Cecil](https://github.com/jbevain/cecil). This is the same library that Harmony uses to generate IL code. 

This dll modification does mean that MockMe is modifying your code. However, it will only modify the dlls that are in the bin folder of the test project. This means that the dll living in the publish folder or in the bin folder of your 'src' project will be unchanged. This is a similar process to other popular dll instrumentation tools such as Coverlet.
