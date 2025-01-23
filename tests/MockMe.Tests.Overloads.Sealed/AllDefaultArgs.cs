using System;

namespace MockMe.Tests.Overloads
{
    public enum EnumLong : long
    {
        Unknown = 0,
        First = 1,
    }

    public sealed class AllDefaultArgs
    {
        private const string Dummy = "Hello World";

        public void MethodWithPrimitiveDefault(int i = 5) { }

        public void MethodWithNullableDefault(int? arg = 15) { }

        public void MethodWithBoolDefault(bool value = true) { }

        public void MethodWithConstStringDefault(string greeting = Dummy) { }

        public void MethodWithStringDefault(string greeting = "Hello World") { }

        public void MethodWithDateTimeDefault(DateTime date = default) { }

        public void MethodWithEnumLongDefault(EnumLong value = EnumLong.First) { }

        public void MethodWithEnumDefault(DayOfWeek day = DayOfWeek.Monday) { }

        public void MethodWithMultipleDefaults(
            double factor = 1.0,
            bool enabled = true,
            string label = "default"
        ) { }
    }
}
