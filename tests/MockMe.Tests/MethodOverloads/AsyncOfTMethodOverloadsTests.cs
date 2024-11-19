using System;
using System.Linq;
using System.Threading.Tasks;
using MockMe.Exceptions;
using Xunit;

namespace MockMe.Tests.MethodOverloads
{
    public class AsyncOfTMethodOverloadsTests
    {
        private async Task XParams_CallbackAndAssertShouldWork(params int[] ints)
        {
            var mock = Mock.Me<SealedOverloadsClass>(null);

            int numCalls = 0;

            Type[] intTypes = Enumerable.Repeat(typeof(int), ints.Length).ToArray();
            Type[] argIntTypes = Enumerable.Repeat(typeof(Arg<int>), ints.Length).ToArray();
            object[] boxedInts = ints.Cast<object>().ToArray();
            object[] boxedArgInts = Enumerable.Repeat<Arg<int>>(Arg.Any, ints.Length).ToArray();

            var setupMethod = mock
                .Setup.GetType()
                .GetMethod(nameof(SealedOverloadsClass.AsyncOfTReturn), argIntTypes)
                .Invoke(mock.Setup, boxedArgInts);

            Action incrementNumCalls = () => numCalls++;
            ((dynamic)setupMethod).Callback(incrementNumCalls);
            ((dynamic)setupMethod).ReturnsAsync(9999);

            SealedOverloadsClass sealedClass = mock;

            Assert.ThrowsAny<MockMeException>(
                () =>
                    (
                        (MemberAsserter)
                            mock
                                .Assert.GetType()
                                .GetMethod(nameof(SealedOverloadsClass.AsyncOfTReturn), argIntTypes)
                                .Invoke(mock.Assert, boxedArgInts)
                    ).WasCalled()
            );

            var ret = await (Task<int>)
                sealedClass
                    .GetType()
                    .GetMethod(nameof(SealedOverloadsClass.AsyncOfTReturn), intTypes)
                    .Invoke(sealedClass, boxedInts);

            Assert.Equal(9999, ret);
            Assert.Equal(1, numCalls);

            (
                (MemberAsserter)
                    mock
                        .Assert.GetType()
                        .GetMethod(nameof(SealedOverloadsClass.AsyncOfTReturn), argIntTypes)
                        .Invoke(mock.Assert, boxedArgInts)
            ).WasCalled();
        }

        [Fact]
        public async Task Params_0_CallbackAndAssertShouldWork() =>
            this.XParams_CallbackAndAssertShouldWork();

        [Fact]
        public void Params_1_CallbackAndAssertShouldWork() =>
            this.XParams_CallbackAndAssertShouldWork(1);

        [Fact]
        public void Params_2_CallbackAndAssertShouldWork() =>
            this.XParams_CallbackAndAssertShouldWork(1, 2);

        [Fact]
        public void Params_3_CallbackAndAssertShouldWork() =>
            this.XParams_CallbackAndAssertShouldWork(1, 2, 3);

        [Fact]
        public void Params_4_CallbackAndAssertShouldWork() =>
            this.XParams_CallbackAndAssertShouldWork(1, 2, 3, 4);

        [Fact]
        public void Params_5_CallbackAndAssertShouldWork() =>
            this.XParams_CallbackAndAssertShouldWork(1, 2, 3, 4, 5);

        [Fact]
        public void Params_6_CallbackAndAssertShouldWork() =>
            this.XParams_CallbackAndAssertShouldWork(1, 2, 3, 4, 5, 6);

        [Fact]
        public void Params_7_CallbackAndAssertShouldWork() =>
            this.XParams_CallbackAndAssertShouldWork(1, 2, 3, 4, 5, 6, 7);

        [Fact]
        public void Params_8_CallbackAndAssertShouldWork() =>
            this.XParams_CallbackAndAssertShouldWork(1, 2, 3, 4, 5, 6, 7, 8);

        [Fact]
        public void Params_9_CallbackAndAssertShouldWork() =>
            this.XParams_CallbackAndAssertShouldWork(1, 2, 3, 4, 5, 6, 7, 8, 9);

        [Fact]
        public void Params_10_CallbackAndAssertShouldWork() =>
            this.XParams_CallbackAndAssertShouldWork(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);

        [Fact]
        public void Params_11_CallbackAndAssertShouldWork() =>
            this.XParams_CallbackAndAssertShouldWork(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11);

        [Fact]
        public void Params_12_CallbackAndAssertShouldWork() =>
            this.XParams_CallbackAndAssertShouldWork(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12);

        [Fact]
        public void Params_13_CallbackAndAssertShouldWork() =>
            this.XParams_CallbackAndAssertShouldWork(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13);

        [Fact]
        public void Params_14_CallbackAndAssertShouldWork() =>
            this.XParams_CallbackAndAssertShouldWork(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14);

        [Fact]
        public void Params_15_CallbackAndAssertShouldWork() =>
            this.XParams_CallbackAndAssertShouldWork(
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
