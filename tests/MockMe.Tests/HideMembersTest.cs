using Xunit;

namespace MockMe.Tests
{
    public interface IBaseInterface
    {
        // Property
        public string MyProperty { get; set; }

        //// Method
        string MyMethod();

        //// Indexer
        string this[int index] { get; set; }
    }

    public interface IDerivedInterface : IBaseInterface
    {
        // Hiding Property
        new int MyProperty { get; set; }

        //// Hiding Method
        new int MyMethod();

        //// Hiding Indexer
        new int this[int index] { get; set; }
    }

    public class HideMembersTest
    {
        [Fact]
        public void HideMembersTests_ShouldImplementAll()
        {
            var mock = Mock.Me(default(IDerivedInterface));
        }
    }
}
