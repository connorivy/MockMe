using System;
using MockMe.Asserters;
using Xunit;

namespace MockMe.Tests
{
    public enum EnumLong : long
    {
        Unknown = 0,
        First = 1
    }

    public class ClassWithDefaultArgs
    {
        private const string Dummy = "Hello World";

        public void MethodWithPrimitiveDefault(int i = 5)
        {
        }

        public void MethodWithNullableDefault(int? arg = 15)
        {
        }

        public void MethodWithBoolDefault(bool value = true)
        {
        }

        public void MethodWithConstStringDefault(string greeting = Dummy)
        {
        }

        public void MethodWithStringDefault(string greeting = "Hello World")
        {
        }

        public void MethodWithDateTimeDefault(DateTime date = default)
        {
        }

        public void MethodWithEnumLongDefault(EnumLong value = EnumLong.First)
        {
        }

        public void MethodWithEnumDefault(DayOfWeek day = DayOfWeek.Monday)
        {
        }

        public void MethodWithMultipleDefaults(double factor = 1.0, bool enabled = true, string label = "default")
        {
        }
    }

    public class DefaultArgTests
    {
        [Fact]
        public void GetParametersWithArgTypesAndModifiers_Should_SkipDefaultValues()
        {
            var mock = Mock.Me(default(ClassWithDefaultArgs));

            {
                int result = 0;
                mock.Setup.MethodWithPrimitiveDefault(Arg.Any()).Callback(args => result = args.i);
                mock.MockedObject.MethodWithPrimitiveDefault();
                mock.Assert.MethodWithPrimitiveDefault(Arg.Any()).WasCalled();
                Assert.Equal(5, result);

                mock.MockedObject.MethodWithPrimitiveDefault(15);
                mock.Assert.MethodWithPrimitiveDefault(Arg.Any()).WasCalled(NumTimes.Exactly(2));
                Assert.Equal(15, result);
            }

            {
                int? result = 0;
                mock.Setup.MethodWithNullableDefault(Arg.Any()).Callback(args => result = args.arg);
                mock.MockedObject.MethodWithNullableDefault();
                mock.Assert.MethodWithNullableDefault(Arg.Any()).WasCalled();
                Assert.Equal(15, result);

                mock.MockedObject.MethodWithNullableDefault(5);
                mock.Assert.MethodWithNullableDefault(Arg.Any()).WasCalled(NumTimes.Exactly(2));
                Assert.Equal(5, result);
            }

            {
                bool result = false;
                mock.Setup.MethodWithBoolDefault(Arg.Any()).Callback(args => result = args.value);
                mock.MockedObject.MethodWithBoolDefault();
                mock.Assert.MethodWithBoolDefault(Arg.Any()).WasCalled();
                Assert.True(result);

                mock.MockedObject.MethodWithBoolDefault(false);
                mock.Assert.MethodWithBoolDefault(Arg.Any()).WasCalled(NumTimes.Exactly(2));
                Assert.False(result);
            }

            {
                string result = "";
                mock.Setup.MethodWithConstStringDefault(Arg.Any()).Callback(args => result = args.greeting);
                mock.MockedObject.MethodWithConstStringDefault();
                mock.Assert.MethodWithConstStringDefault(Arg.Any()).WasCalled();
                Assert.Equal("Hello World", result);

                mock.MockedObject.MethodWithConstStringDefault("Goodbye World");
                mock.Assert.MethodWithConstStringDefault(Arg.Any()).WasCalled(NumTimes.Exactly(2));
                Assert.Equal("Goodbye World", result);
            }

            {
                string result = "";
                mock.Setup.MethodWithStringDefault(Arg.Any()).Callback(args => result = args.greeting);
                mock.MockedObject.MethodWithStringDefault();
                mock.Assert.MethodWithStringDefault(Arg.Any()).WasCalled();
                Assert.Equal("Hello World", result);

                mock.MockedObject.MethodWithStringDefault("Goodbye World");
                mock.Assert.MethodWithStringDefault(Arg.Any()).WasCalled(NumTimes.Exactly(2));
                Assert.Equal("Goodbye World", result);
            }

            {
                DateTime result = default;
                mock.Setup.MethodWithDateTimeDefault(Arg.Any()).Callback(args => result = args.date);
                mock.MockedObject.MethodWithDateTimeDefault();
                mock.Assert.MethodWithDateTimeDefault(Arg.Any()).WasCalled();
                Assert.Equal(default(DateTime), result);

                mock.MockedObject.MethodWithDateTimeDefault(DateTime.Now);
                mock.Assert.MethodWithDateTimeDefault(Arg.Any()).WasCalled(NumTimes.Exactly(2));
                Assert.NotEqual(default(DateTime), result);
            }

            {
                EnumLong result = default;
                mock.Setup.MethodWithEnumLongDefault(Arg.Any()).Callback(args => result = args.value);
                mock.MockedObject.MethodWithEnumLongDefault();
                mock.Assert.MethodWithEnumLongDefault(Arg.Any()).WasCalled();
                Assert.Equal(EnumLong.First, result);

                mock.MockedObject.MethodWithEnumLongDefault(EnumLong.Unknown);
                mock.Assert.MethodWithEnumLongDefault(Arg.Any()).WasCalled(NumTimes.Exactly(2));
                Assert.Equal(EnumLong.Unknown, result);
            }

            {
                DayOfWeek result = default;
                mock.Setup.MethodWithEnumDefault(Arg.Any()).Callback(args => result = args.day);
                mock.MockedObject.MethodWithEnumDefault();
                mock.Assert.MethodWithEnumDefault(Arg.Any()).WasCalled();
                Assert.Equal(DayOfWeek.Monday, result);

                mock.MockedObject.MethodWithEnumDefault(DayOfWeek.Tuesday);
                mock.Assert.MethodWithEnumDefault(Arg.Any()).WasCalled(NumTimes.Exactly(2));
                Assert.Equal(DayOfWeek.Tuesday, result);
            }

            {
                (double factor, bool enabled, string label) result = (0.0, false, "");
                mock.Setup.MethodWithMultipleDefaults(Arg.Any(), Arg.Any(), Arg.Any())
                    .Callback(args => result = (args.factor, args.enabled, args.label));
                mock.MockedObject.MethodWithMultipleDefaults();
                mock.Assert.MethodWithMultipleDefaults(Arg.Any(), Arg.Any(), Arg.Any()).WasCalled();
                Assert.Equal((1.0, true, "default"), result);

                mock.MockedObject.MethodWithMultipleDefaults(2.0, false, "custom");
                mock.Assert.MethodWithMultipleDefaults(Arg.Any(), Arg.Any(), Arg.Any())
                    .WasCalled(NumTimes.Exactly(2));
                Assert.Equal((2.0, false, "custom"), result);
            }
        }
    }
}
