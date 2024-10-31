using System.Reflection;
using HarmonyLib;
using Mono.Cecil;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using MonoMod.RuntimeDetour;
using Xunit;

namespace MockMe.SampleMocks.CalculatorSample;

public class CalculatorTestsForDesign
{
    // Uncomment to disable tests
    //private class FactAttribute : Attribute { }

    //[Fact]
    //public void CalculatorDesignFiddle()
    //{
    //    System.Diagnostics.Debugger.Launch();
    //    Harmony.DEBUG = true;
    //    var originalClass = typeof(Calculator);
    //    //var originalMethod = originalClass.GetMethod(nameof(Calculator.Add));
    //    var originalMethod = originalClass
    //        .GetMethod(nameof(Calculator.AddUpAllOfThese2))
    //        .MakeGenericMethod(typeof(object));

    //    var patchClass = typeof(CalculatorMock.Hook);
    //    //var prefix = patchClass.GetMethod(nameof(CalculatorMock.Hook.Add));
    //    //var prefix = patchClass.GetMethod(nameof(CalculatorMock.Hook.PrefixList3));
    //    var prefix = patchClass.GetMethod(nameof(CalculatorMock.Hook.PrefixList5));

    //    var instance = new Harmony("test");
    //    instance.Patch(originalMethod, new HarmonyMethod(prefix));

    //    //var patcher = instance.CreateProcessor(originalMethod);
    //    //_ = patcher.AddPrefix(prefix);
    //    //_ = patcher.Patch();

    //    Calculator calc = new();
    //    using (var d = new Hook(originalMethod, prefix))
    //    {
    //        //var harmony = new global::HarmonyLib.Harmony("com.mockme.patch");
    //        //harmony.PatchAll();

    //        //CalculatorMock calculatorMock = new();

    //        //calculatorMock.Setup.AddUpAllOfThese<int>(Arg.Any).Returns(1);
    //        //calculatorMock.Setup.AddUpAllOfThese<double>(Arg.Any).Returns(1);
    //        //calculatorMock.Setup.AddUpAllOfThese<List<int>>(Arg.Any).Returns(null as List<int>);
    //        //Calculator calc = (Calculator)calculatorMock;

    //        //var result1 = calc.AddUpAllOfThese([1, 2, 3, 4]);
    //        //var result2 = calc.AddUpAllOfThese<double>([1, 2, 3, 4]);
    //        var result3 = calc.AddUpAllOfThese2<List<int>>(1, [new([1])], 5);
    //    }
    //    //var result3 = calc.AddUpAllOfThese2<List<int>>(1, [new([1])], 5);

    //    var result6 = calc.AddUpAllOfThese2<object>(1, [7], 5);
    //    //var result5 = calc.AddUpAllOfThese<List<int>>([new(), new()]);
    //    //var result4 = calc.Add(0, 0);

    //    //calculatorMock.Assert.AddUpAllOfThese<int>(Arg.Any).WasCalled();
    //}

    //public static int AddReplace(Calculator self, int x, int y) => 99;

    //public static int AddOriginal(Calculator self, int x, int y) => self.Add(x, y);

    //[Fact]
    //public void CalculatorDesignFiddle2()
    //{
    //    Harmony.DEBUG = true;
    //    var originalClass = typeof(Calculator);
    //    //var originalMethod = originalClass.GetMethod(nameof(Calculator.Add));
    //    var originalMethod = originalClass.GetMethod(nameof(Calculator.Add));

    //    var patchClass = typeof(CalculatorTestsForDesign);
    //    //var prefix = patchClass.GetMethod(nameof(CalculatorMock.Hook.Add));
    //    //var prefix = patchClass.GetMethod(nameof(CalculatorMock.Hook.PrefixList3));
    //    var store = patchClass.GetMethod(nameof(AddOriginal));
    //    var newMethod = patchClass.GetMethod(nameof(AddReplace));

    //    Calculator calc = new();
    //    using (var a = new Hook(originalMethod, store))
    //    using (var d = new Hook(originalMethod, newMethod))
    //    {
    //        //var harmony = new global::HarmonyLib.Harmony("com.mockme.patch");
    //        //harmony.PatchAll();

    //        //CalculatorMock calculatorMock = new();

    //        //calculatorMock.Setup.AddUpAllOfThese<int>(Arg.Any).Returns(1);
    //        //calculatorMock.Setup.AddUpAllOfThese<double>(Arg.Any).Returns(1);
    //        //calculatorMock.Setup.AddUpAllOfThese<List<int>>(Arg.Any).Returns(null as List<int>);
    //        //Calculator calc = (Calculator)calculatorMock;

    //        //var result1 = calc.AddUpAllOfThese([1, 2, 3, 4]);
    //        //var result2 = calc.AddUpAllOfThese<double>([1, 2, 3, 4]);
    //        var mockResult = calc.Add(1, 2);
    //        var originalResult = AddOriginal(new(), 1, 2);
    //    }

    //    //var result6 = calc.AddUpAllOfThese2<object>(1, [7], 5);
    //    //var result5 = calc.AddUpAllOfThese<List<int>>([new(), new()]);
    //    //var result4 = calc.Add(0, 0);

    //    //calculatorMock.Assert.AddUpAllOfThese<int>(Arg.Any).WasCalled();
    //}

    //[Fact]
    //public void TryIlHook()
    //{
    //    var originalClass = typeof(Calculator);
    //    //var originalMethod = originalClass.GetMethod(nameof(Calculator.Add));
    //    var methodToReplace = originalClass
    //        .GetMethod(nameof(Calculator.AddUpAllOfThese2))
    //        .MakeGenericMethod(typeof(object));

    //    Calculator calc = new();
    //    object objectToReturn = new List<int>([5]);
    //    using var x = new ILHook(
    //        methodToReplace,
    //        il =>
    //        {
    //            ILCursor c = new ILCursor(il);

    //            // By the way, I'm assuming you're not dealing with void.
    //            // Let's add a safety check though...
    //            Type returnType = (methodToReplace as MethodInfo)?.ReturnType ?? typeof(void);
    //            if (returnType == typeof(void))
    //            {
    //                // Exit early or do whatever else you need to do for void methods.
    //                c.Emit(OpCodes.Ret);
    //                return;
    //            }

    //            c.Emit(OpCodes.Ldarg_1);
    //            c.Emit(OpCodes.Ldarg_2);
    //            c.Emit(OpCodes.Ldarg_3);
    //            c.Emit(
    //                OpCodes.Call,
    //                typeof(CalculatorMock.Hook)
    //                    .GetMethod(nameof(CalculatorMock.Hook.PrefixList7))
    //                    .MakeGenericMethod(typeof(object))
    //            );
    //            c.Emit(OpCodes.Ret);

    //            var x = c.IL;

    //            // Only use ONE of the following 3 variants.
    //            // The blob at the end is important for structs only and applies to all 3 variants.

    //            //// Variation 1: Easiest method but I don't know if you need the dictionary.
    //            //c.EmitDelegate<Func<object>>(() => objectToReturn);

    //            //// Variation 2: If you do need the dict to f.e. update the reference later:
    //            //c.Emit(OpCodes.Ldstr, "a generated ID or another key object");
    //            //c.EmitDelegate<Func<string, object>>(key => objectsToReturn[key]);

    //            // Variation 3: If you want to use MonoMod.RuntimeDetour's reference management:
    //            //int id = c.EmitReference(objectToReturn);
    //            // Skipping the delegate is also the most performant option and what I'd prefer in this case.
    //            // You can then use RuntimeILReferenceBag.Instance with the id as long as the hook isn't disposed.

    //            //// Keep in mind that no matter what path you choose:
    //            //// As long as you use object and not generics, you'll probably need to unbox.
    //            //if (returnType.IsValueType)
    //            //    c.Emit(OpCodes.Unbox_Any, returnType);
    //            //// Return from the injected method, returning your new value.
    //            //c.Emit(OpCodes.Ret);
    //        }
    //    );

    //    var result = calc.AddUpAllOfThese2<object>(1, [new()], 5);
    //    var result2 = calc.AddUpAllOfThese2<List<int>>(1, [new([1])], 5);
    //    ;
    //}

    public int AddNums(Calculator calc, int x, int y)
    {
        return calc.Add(x, y);
    }

    public double AddUpAllDoubles(Calculator calc, double[] dubs)
    {
        return calc.AddUpAllOfThese2<double>(0, dubs, 5.5);
    }
}
