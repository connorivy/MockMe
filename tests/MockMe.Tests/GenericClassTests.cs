using System;
using Xunit;

namespace MockMe.Tests
{
    public class ConnorsCoolGenericType<T>
    {
        public T GetRandomVal() => throw new NotImplementedException();

        public T? MyCoolProp { get; set; }

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
            var mockInt = Mock.Me(default(ConnorsCoolGenericType<int>));
            var mockDouble = Mock.Me(default(ConnorsCoolGenericType<double>));

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
            var mockCool = Mock.Me(default(ConnorsCoolGenericType<ConnorsCoolReadonlyStruct>));
            var mockEvil = Mock.Me(default(ConnorsCoolGenericType<ConnorsEvilMutableStruct>));

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
            var mockInt = Mock.Me(default(ConnorsCoolGenericType<int>));
            var mockString = Mock.Me(default(ConnorsCoolGenericType<string>));

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
            var mockObject = Mock.Me(default(ConnorsCoolGenericType<object>));
            var mockString = Mock.Me(default(ConnorsCoolGenericType<string>));

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

        [Fact]
        public void PropertyOfGenericClassType_ShouldReturnConfiguredValues()
        {
            var mockString = Mock.Me(default(ConnorsCoolGenericType<string>));

            mockString.Setup.MyCoolProp.Get().Returns("returnVal");
            string? setVal = null;
            mockString.Setup.MyCoolProp.Set(Arg.Any()).Callback(args => setVal = args.Value);

            ConnorsCoolGenericType<string> coolStringType = mockString;

            Assert.Equal("returnVal", coolStringType.MyCoolProp);
            coolStringType.MyCoolProp = "set value";
            Assert.Equal("set value", setVal);
            mockString.Assert.MyCoolProp.Get().WasCalled();
            mockString.Assert.MyCoolProp.Set("set value").WasCalled();
        }
    }
}
