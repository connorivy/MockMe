using System;
using Xunit;

namespace MockMe.Tests
{
    public class ConnorsCoolGenericType<T>
    {
        public T GetRandomVal() => throw new NotImplementedException();

        public T MyCoolProp => throw new NotImplementedException();

        public string TakeAT(T input) => throw new NotImplementedException();
    }

    public class GenericClassTests
    {
        [Fact]
        public void Test()
        {
            var mockInt = Mock.Me<ConnorsCoolGenericType<int>>();
            var mockDouble = Mock.Me<ConnorsCoolGenericType<double>>();

            mockInt.Setup.GetRandomVal().Returns(99);
            mockDouble.Setup.GetRandomVal().Returns(99.99);

            ConnorsCoolGenericType<int> coolType = mockInt;
            ConnorsCoolGenericType<double> coolDoubleType = mockDouble;

            var returnVal = coolType.GetRandomVal();
            var returnDouble = coolDoubleType.GetRandomVal();
            ;
        }
    }
}
