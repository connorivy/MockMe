using System;
using System.Linq;
using MockMe.Asserters;
using MockMe.Exceptions;
using Xunit;

namespace MockMe.Tests.Overloads
{
    public class SyncMethodOverloadsTests
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
        public void SyncReturnOverload_CallbackAndAssertShouldWork(params int[] ints)
        {
            var mock = Mock.Me(default(AllOverloads));

            int numCalls = 0;

            Type[] intTypes = Enumerable.Repeat(typeof(int), ints.Length).ToArray();
            Type[] argIntTypes = Enumerable.Repeat(typeof(Arg<int>), ints.Length).ToArray();
            object[] boxedInts = ints.Cast<object>().ToArray();
            object[] boxedArgInts = Enumerable.Repeat<Arg<int>>(Arg.Any(), ints.Length).ToArray();

            var setupMethod = mock
                .Setup.GetType()
                .GetMethod(nameof(AllOverloads.SyncReturn), argIntTypes)
                .NotNull()
                .Invoke(mock.Setup, boxedArgInts)
                .NotNull();

            void incrementNumCalls() => numCalls++;
            ((dynamic)setupMethod).Callback((Action)incrementNumCalls);

            ((dynamic)setupMethod).Returns(9999);

            AllOverloads sealedClass = mock.MockedObject;

            Assert.ThrowsAny<MockMeException>(
                () =>
                    (
                        (MemberAsserter)
                            mock
                                .Assert.GetType()
                                .GetMethod(nameof(AllOverloads.SyncReturn), argIntTypes)
                                .NotNull()
                                .Invoke(mock.Assert, boxedArgInts)
                                .NotNull()
                    ).WasCalled()
            );

            object ret = sealedClass
                .GetType()
                .GetMethod(nameof(AllOverloads.SyncReturn), intTypes)
                .NotNull()
                .Invoke(sealedClass, boxedInts)
                .NotNull();

            Assert.Equal(9999, (int)ret);

            Assert.Equal(1, numCalls);

            (
                (MemberAsserter)
                    mock
                        .Assert.GetType()
                        .GetMethod(nameof(AllOverloads.SyncReturn), argIntTypes)
                        .NotNull()
                        .Invoke(mock.Assert, boxedArgInts)
                        .NotNull()
            ).WasCalled();
        }
    }
}
