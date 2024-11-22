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

    public readonly struct ConnorsCoolReadonlyStruct
    {
        public int Return99() => 99;

        public void AnotherMethod() => throw new NotImplementedException();
    }

    public struct ConnorsEvilMutableStruct
    {
        public int Return99 { get; set; }

        public void AnotherMethod() => throw new NotImplementedException();
    }

    public class GenericClassTests
    {
        [Fact]
        public void MultipleValueTypeOverloads_ShouldReturnConfiguredValues()
        {
            var mockInt = Mock.Me<ConnorsCoolGenericType<int>>();
            var mockDouble = Mock.Me<ConnorsCoolGenericType<double>>();

            mockInt.Setup.GetRandomVal().Returns(99);
            mockDouble.Setup.GetRandomVal().Returns(99.99);

            ConnorsCoolGenericType<int> coolType = mockInt;
            ConnorsCoolGenericType<double> coolDoubleType = mockDouble;

            var returnVal = coolType.GetRandomVal();
            var returnDouble = coolDoubleType.GetRandomVal();

            Assert.Equal(99, returnVal);
            Assert.Equal(99.99, returnDouble);
        }

        [Fact]
        public void MultipleCustomStructOverloads_ShouldReturnConfiguredValues()
        {
            var mockCool = Mock.Me<ConnorsCoolGenericType<ConnorsCoolReadonlyStruct>>();
            var mockEvil = Mock.Me<ConnorsCoolGenericType<ConnorsEvilMutableStruct>>();

            mockCool.Setup.GetRandomVal().Returns(new ConnorsCoolReadonlyStruct());
            mockEvil.Setup.GetRandomVal().Returns(new ConnorsEvilMutableStruct());

            //var x = mockCool.MockedObject

            ConnorsCoolGenericType<ConnorsCoolReadonlyStruct> coolType = mockCool;
            ConnorsCoolGenericType<ConnorsEvilMutableStruct> coolEvilType = mockEvil;

            var returnVal = coolType.GetRandomVal();
            var returnEvil = coolEvilType.GetRandomVal();

            Assert.Equal(new ConnorsCoolReadonlyStruct(), returnVal);
            Assert.Equal(new ConnorsEvilMutableStruct(), returnEvil);
        }

        [Fact]
        public void RefAndValueTypeOverloads_ShouldReturnConfiguredValues()
        {
            var mockInt = Mock.Me<ConnorsCoolGenericType<int>>();
            var mockString = Mock.Me<ConnorsCoolGenericType<string>>();

            mockInt.Setup.GetRandomVal().Returns(99);
            mockString.Setup.GetRandomVal().Returns("returnVal");

            ConnorsCoolGenericType<int> coolType = mockInt;
            ConnorsCoolGenericType<string> coolStringType = mockString;

            var returnVal = coolType.GetRandomVal();
            var returnString = coolStringType.GetRandomVal();

            Assert.Equal(99, returnVal);
            Assert.Equal("returnVal", returnString);
        }

        [Fact]
        public void MultipleReferenceTypeOverloads_ShouldReturnConfiguredValues()
        {
            var mockObject = Mock.Me<ConnorsCoolGenericType<object>>();
            var mockString = Mock.Me<ConnorsCoolGenericType<string>>();

            object myObj = new();

            mockObject.Setup.GetRandomVal().Returns(myObj);
            mockString.Setup.GetRandomVal().Returns("returnVal");

            ConnorsCoolGenericType<object> coolType = mockObject;
            ConnorsCoolGenericType<string> coolStringType = mockString;

            var returnVal = coolType.GetRandomVal();
            var returnString = coolStringType.GetRandomVal();

            Assert.Equal(myObj, returnVal);
            Assert.Equal("returnVal", returnString);
        }
    }
}
