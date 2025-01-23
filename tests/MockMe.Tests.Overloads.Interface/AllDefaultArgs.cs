using System;

namespace MockMe.Tests.Overloads
{
    public enum EnumLong : long
    {
        Unknown = 0,
        First = 1,
    }

    public interface AllDefaultArgs
    {
        void MethodWithBoolDefault(bool value = true);
        void MethodWithConstStringDefault(string greeting = "Hello World");
        void MethodWithDateTimeDefault(DateTime date = default);
        void MethodWithEnumDefault(DayOfWeek day = DayOfWeek.Monday);
        void MethodWithEnumLongDefault(EnumLong value = EnumLong.First);
        void MethodWithMultipleDefaults(
            double factor = 1,
            bool enabled = true,
            string label = "default"
        );
        void MethodWithNullableDefault(int? arg = 15);
        void MethodWithPrimitiveDefault(int i = 5);
        void MethodWithStringDefault(string greeting = "Hello World");
    }
}
