using System;
using System.Linq;
using MockMe.Exceptions;
using Xunit;

namespace MockMe.Tests.MethodOverloads
{
    public class SyncMethodOverloadsTests
    {
        private void XParams_CallbackReturnsAndAssertShouldWork(params int[] ints)
        {
            var mock = Mock.Me<SealedOverloadsClass>(default(SealedOverloadsClass));

            int numCalls = 0;

            Type[] intTypes = Enumerable.Repeat(typeof(int), ints.Length).ToArray();
            Type[] argIntTypes = Enumerable.Repeat(typeof(Arg<int>), ints.Length).ToArray();
            object[] boxedInts = ints.Cast<object>().ToArray();
            object[] boxedArgInts = Enumerable.Repeat<Arg<int>>(Arg.Any, ints.Length).ToArray();

            var setupMethod = mock
                .Setup.GetType()
                .GetMethod(nameof(SealedOverloadsClass.SyncReturn), argIntTypes)
                .Invoke(mock.Setup, boxedArgInts);

            Action incrementNumCalls = () => numCalls++;
            ((dynamic)setupMethod).Callback(incrementNumCalls);

            ((dynamic)setupMethod).Returns(9999);

            SealedOverloadsClass sealedClass = mock;

            Assert.ThrowsAny<MockMeException>(
                () =>
                    (
                        (MemberAsserter)
                            mock
                                .Assert.GetType()
                                .GetMethod(nameof(SealedOverloadsClass.SyncReturn), argIntTypes)
                                .Invoke(mock.Assert, boxedArgInts)
                    ).WasCalled()
            );

            object ret = sealedClass
                .GetType()
                .GetMethod(nameof(SealedOverloadsClass.SyncReturn), intTypes)
                .Invoke(sealedClass, boxedInts);

            Assert.Equal(9999, (int)ret);

            Assert.Equal(1, numCalls);

            (
                (MemberAsserter)
                    mock
                        .Assert.GetType()
                        .GetMethod(nameof(SealedOverloadsClass.SyncReturn), argIntTypes)
                        .Invoke(mock.Assert, boxedArgInts)
            ).WasCalled();
        }

        [Fact]
        public void Params_0_CallbackReturnsAndAssertShouldWork() =>
            this.XParams_CallbackReturnsAndAssertShouldWork();

        [Fact]
        public void Params_1_CallbackReturnsAndAssertShouldWork() =>
            this.XParams_CallbackReturnsAndAssertShouldWork(1);

        [Fact]
        public void Params_2_CallbackReturnsAndAssertShouldWork() =>
            this.XParams_CallbackReturnsAndAssertShouldWork(1, 2);

        [Fact]
        public void Params_3_CallbackReturnsAndAssertShouldWork() =>
            this.XParams_CallbackReturnsAndAssertShouldWork(1, 2, 3);

        [Fact]
        public void Params_4_CallbackReturnsAndAssertShouldWork() =>
            this.XParams_CallbackReturnsAndAssertShouldWork(1, 2, 3, 4);

        [Fact]
        public void Params_5_CallbackReturnsAndAssertShouldWork() =>
            this.XParams_CallbackReturnsAndAssertShouldWork(1, 2, 3, 4, 5);

        [Fact]
        public void Params_6_CallbackReturnsAndAssertShouldWork() =>
            this.XParams_CallbackReturnsAndAssertShouldWork(1, 2, 3, 4, 5, 6);

        [Fact]
        public void Params_7_CallbackReturnsAndAssertShouldWork() =>
            this.XParams_CallbackReturnsAndAssertShouldWork(1, 2, 3, 4, 5, 6, 7);

        [Fact]
        public void Params_8_CallbackReturnsAndAssertShouldWork() =>
            this.XParams_CallbackReturnsAndAssertShouldWork(1, 2, 3, 4, 5, 6, 7, 8);

        [Fact]
        public void Params_9_CallbackReturnsAndAssertShouldWork() =>
            this.XParams_CallbackReturnsAndAssertShouldWork(1, 2, 3, 4, 5, 6, 7, 8, 9);

        [Fact]
        public void Params_10_CallbackReturnsAndAssertShouldWork() =>
            this.XParams_CallbackReturnsAndAssertShouldWork(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);

        [Fact]
        public void Params_11_CallbackReturnsAndAssertShouldWork() =>
            this.XParams_CallbackReturnsAndAssertShouldWork(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11);

        [Fact]
        public void Params_12_CallbackReturnsAndAssertShouldWork() =>
            this.XParams_CallbackReturnsAndAssertShouldWork(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12);

        [Fact]
        public void Params_13_CallbackReturnsAndAssertShouldWork() =>
            this.XParams_CallbackReturnsAndAssertShouldWork(
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9,
                10,
                11,
                12,
                13
            );

        [Fact]
        public void Params_14_CallbackReturnsAndAssertShouldWork() =>
            this.XParams_CallbackReturnsAndAssertShouldWork(
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9,
                10,
                11,
                12,
                13,
                14
            );

        [Fact]
        public void Params_15_CallbackReturnsAndAssertShouldWork() =>
            this.XParams_CallbackReturnsAndAssertShouldWork(
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9,
                10,
                11,
                12,
                13,
                14,
                15
            );
    }
}
