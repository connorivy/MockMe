using System;
using System.Linq;
using MockMe.Asserters;
using MockMe.Exceptions;
using Xunit;

namespace MockMe.Tests.Overloads
{
    public class VoidMethodOverloadsTests
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
        public void VoidOverload_CallbackAndAssertShouldWork(params int[] ints)
        {
            var mock = Mock.Me<SealedOverloadsClass>(null);

            int numCalls = 0;

            Type[] intTypes = Enumerable.Repeat(typeof(int), ints.Length).ToArray();
            Type[] argIntTypes = Enumerable.Repeat(typeof(Arg<int>), ints.Length).ToArray();
            object[] boxedInts = ints.Cast<object>().ToArray();
            object[] boxedArgInts = Enumerable.Repeat<Arg<int>>(Arg.Any(), ints.Length).ToArray();

            var setupMethod = mock
                .Setup.GetType()
                .GetMethod(nameof(SealedOverloadsClass.VoidReturn), argIntTypes)
                .Invoke(mock.Setup, boxedArgInts);

            Action incrementNumCalls = () => numCalls++;
            ((dynamic)setupMethod).Callback(incrementNumCalls);

            SealedOverloadsClass sealedClass = mock;

            Assert.ThrowsAny<MockMeException>(
                () =>
                    (
                        (MemberAsserter)
                            mock
                                .Assert.GetType()
                                .GetMethod(nameof(SealedOverloadsClass.VoidReturn), argIntTypes)
                                .Invoke(mock.Assert, boxedArgInts)
                    ).WasCalled()
            );

            sealedClass
                .GetType()
                .GetMethod(nameof(SealedOverloadsClass.VoidReturn), intTypes)
                .Invoke(sealedClass, boxedInts);

            Assert.Equal(1, numCalls);

            (
                (MemberAsserter)
                    mock
                        .Assert.GetType()
                        .GetMethod(nameof(SealedOverloadsClass.VoidReturn), argIntTypes)
                        .Invoke(mock.Assert, boxedArgInts)
            ).WasCalled();
        }
    }
}
