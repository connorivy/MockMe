using Xunit;

namespace MockMe.Tests.Overloads
{
    public class PropertyTests
    {
        [Fact]
        public void GetInitProperty_ReturnsAndCallbackShouldWork()
        {
            var mock = Mock.Me<SealedOverloadsClass>(default(SealedOverloadsClass));

            int numCalls = 0;
            mock.Setup.Prop_GetInit.Get().Returns(99).Callback(() => numCalls++);

            SealedOverloadsClass sealedOverloadsClass = mock;

            var val = sealedOverloadsClass.Prop_GetInit;

            Assert.Equal(99, val);
            Assert.Equal(1, numCalls);
            mock.Assert.Prop_GetInit.Get().WasCalled();
        }

        [Fact]
        public void GetOnlyProperty_ReturnsAndCallbackShouldWork()
        {
            var mock = Mock.Me<SealedOverloadsClass>(default(SealedOverloadsClass));

            int numCalls = 0;
            mock.Setup.Prop_GetOnly.Get().Returns(99).Callback(() => numCalls++);

            SealedOverloadsClass sealedOverloadsClass = mock;

            var val = sealedOverloadsClass.Prop_GetOnly;

            Assert.Equal(99, val);
            Assert.Equal(1, numCalls);
            mock.Assert.Prop_GetOnly.Get().WasCalled();
        }

        [Fact]
        public void GetSetProperty_ReturnsAndCallbackShouldWork()
        {
            var mock = Mock.Me<SealedOverloadsClass>(default(SealedOverloadsClass));

            int numCalls = 0;
            mock.Setup.Prop_GetSet.Get().Returns(99).Callback(() => numCalls++);
            mock.Setup.Prop_GetSet.Set(Arg.Any).Callback(() => numCalls++);

            SealedOverloadsClass sealedOverloadsClass = mock;

            var val = sealedOverloadsClass.Prop_GetSet;
            sealedOverloadsClass.Prop_GetSet = 999;

            Assert.Equal(99, val);
            Assert.Equal(2, numCalls);
            mock.Assert.Prop_GetSet.Get().WasCalled();
            mock.Assert.Prop_GetSet.Set(999).WasCalled();
        }

        [Fact]
        public void SetOnlyProperty_CallbackShouldWork()
        {
            var mock = Mock.Me<SealedOverloadsClass>(default(SealedOverloadsClass));

            int numCalls = 0;
            mock.Setup.Prop_SetOnly.Set(Arg.Any).Callback(() => numCalls++);

            SealedOverloadsClass sealedOverloadsClass = mock;

            sealedOverloadsClass.Prop_SetOnly = 999;

            Assert.Equal(1, numCalls);
            mock.Assert.Prop_SetOnly.Set(999).WasCalled();
        }

        [Fact]
        public void GetInitProperty_HasNoSetter()
        {
            var mock = Mock.Me<SealedOverloadsClass>(default(SealedOverloadsClass));

            var setSetter = mock.Setup.Prop_GetSet.GetType().GetMethod("Set");
            var initSetter = mock.Setup.Prop_GetInit.GetType().GetMethod("Set");

            Assert.NotNull(setSetter);
            Assert.Null(initSetter);
        }

        [Fact]
        public void SetOnlyProperty_HasNoGetter()
        {
            var mock = Mock.Me<SealedOverloadsClass>(default(SealedOverloadsClass));

            var getGetter = mock.Setup.Prop_GetSet.GetType().GetMethod("Set");
            var setGetter = mock.Setup.Prop_SetOnly.GetType().GetMethod("Get");

            Assert.NotNull(getGetter);
            Assert.Null(setGetter);
        }
    }
}
