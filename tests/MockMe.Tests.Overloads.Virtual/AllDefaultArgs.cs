using System;

namespace MockMe.Tests.Overloads
{
    public enum EnumLong : long
    {
        Unknown = 0,
        First = 1,
    }

    public class AllDefaultArgs
    {
        private const string Dummy = "Hello World";

        public virtual void MethodWithPrimitiveDefault(int i = 5) { }

        public virtual void MethodWithNullableDefault(int? arg = 15) { }

        public virtual void MethodWithBoolDefault(bool value = true) { }

        public virtual void MethodWithConstStringDefault(string greeting = Dummy) { }

        public virtual void MethodWithStringDefault(string greeting = "Hello World") { }

        public virtual void MethodWithDateTimeDefault(DateTime date = default) { }

        public virtual void MethodWithEnumLongDefault(EnumLong value = EnumLong.First) { }

        public virtual void MethodWithEnumDefault(DayOfWeek day = DayOfWeek.Monday) { }

        public virtual void MethodWithMultipleDefaults(
            double factor = 1.0,
            bool enabled = true,
            string label = "default"
        ) { }
    }
}
