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
        public void MethodWithBoolDefault(bool value = true);
        public void MethodWithConstStringDefault(string greeting = "Hello World");
        public void MethodWithDateTimeDefault(DateTime date = default);
        public void MethodWithEnumDefault(DayOfWeek day = DayOfWeek.Monday);
        public void MethodWithEnumLongDefault(EnumLong value = EnumLong.First);
        public void MethodWithMultipleDefaults(
            double factor = 1,
            bool enabled = true,
            string label = "default"
        );
        public void MethodWithNullableDefault(int? arg = 15);
        public void MethodWithPrimitiveDefault(int i = 5);
        public void MethodWithStringDefault(string greeting = "Hello World");
    }
}
