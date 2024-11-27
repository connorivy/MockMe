using System;
using System.Linq;
using System.Threading.Tasks;
using MockMe.Asserters;
using MockMe.Exceptions;
using Xunit;

namespace MockMe.Tests.Overloads
{
    public class AsyncOfTMethodOverloadsTests
    {
        [Theory]
        [InlineData(new int[] { })]
        [InlineData(1)]
        [InlineData(1, 2)]
        [InlineData(1, 2, 3)]
        [InlineData(1, 2, 3, 4)]
        [InlineData(1, 2, 3, 4, 5)]
        [InlineData(1, 2, 3, 4, 5, 6)]
        [InlineData(1, 2, 3, 4, 5, 6, 7)]
        [InlineData(1, 2, 3, 4, 5, 6, 7, 8)]
        [InlineData(1, 2, 3, 4, 5, 6, 7, 8, 9)]
        [InlineData(1, 2, 3, 4, 5, 6, 7, 8, 9, 10)]
        [InlineData(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11)]
        [InlineData(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12)]
        [InlineData(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13)]
        [InlineData(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14)]
        [InlineData(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15)]
        public async Task AsyncOfTReturnOverload_CallbackAndAssertShouldWork(params int[] ints)
        {
            var mock = Mock.Me<AllOverloads>(default(AllOverloads));

            int numCalls = 0;

            Type[] intTypes = Enumerable.Repeat(typeof(int), ints.Length).ToArray();
            Type[] argIntTypes = Enumerable.Repeat(typeof(Arg<int>), ints.Length).ToArray();
            object[] boxedInts = ints.Cast<object>().ToArray();
            object[] boxedArgInts = Enumerable.Repeat<Arg<int>>(Arg.Any(), ints.Length).ToArray();

            var setupMethod = mock
                .Setup.GetType()
                .GetMethod(nameof(AllOverloads.AsyncOfTReturn), argIntTypes)
                .NotNull()
                .Invoke(mock.Setup, boxedArgInts)
                .NotNull();

            void incrementNumCalls() => numCalls++;
            ((dynamic)setupMethod).Callback((Action)incrementNumCalls);
            ((dynamic)setupMethod).ReturnsAsync(9999);

            AllOverloads sealedClass = mock.MockedObject;

            Assert.ThrowsAny<MockMeException>(
                () =>
                    (
                        (MemberAsserter)
                            mock
                                .Assert.GetType()
                                .GetMethod(nameof(AllOverloads.AsyncOfTReturn), argIntTypes)
                                .NotNull()
                                .Invoke(mock.Assert, boxedArgInts)
                                .NotNull()
                    ).WasCalled()
            );

            var ret = await (Task<int>)
                sealedClass
                    .GetType()
                    .GetMethod(nameof(AllOverloads.AsyncOfTReturn), intTypes)
                    .NotNull()
                    .Invoke(sealedClass, boxedInts)
                    .NotNull();

            Assert.Equal(9999, ret);
            Assert.Equal(1, numCalls);

            (
                (MemberAsserter)
                    mock
                        .Assert.GetType()
                        .GetMethod(nameof(AllOverloads.AsyncOfTReturn), argIntTypes)
                        .NotNull()
                        .Invoke(mock.Assert, boxedArgInts)
                        .NotNull()
            ).WasCalled();
        }
    }
}
