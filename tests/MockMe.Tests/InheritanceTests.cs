using System;
using Xunit;

namespace MockMe.Tests
{
    public class ParentClass
    {
        public virtual int Return99FromParent() => 99;

        public virtual int Return55FromChildMethodOverride() => 99;
    }

    public class ChildClass : ParentClass
    {
        private int i1;
        private int i2;
        private int i3;

        public override int Return99FromParent()
        {
            //simulate doing some work
            this.i1 = Random.Shared.Next();
            this.i2 = Random.Shared.Next();
            this.i3 = Random.Shared.Next();

            // call base class
            return base.Return99FromParent();
        }

        public override int Return55FromChildMethodOverride()
        {
            //simulate doing some work
            this.i1 = Random.Shared.Next();
            this.i2 = Random.Shared.Next();
            this.i3 = Random.Shared.Next();
            var x = base.Return55FromChildMethodOverride();

            return 55;
        }
    }

    public class InheritanceTests
    {
        [Fact]
        public void ChildClassWhichCallsBase_ShouldStillWork_WhenMocked()
        {
            var mock = Mock.Me(default(ChildClass));

            ChildClass notMocked = new();

            Assert.Equal(99, notMocked.Return99FromParent());
        }

        [Fact]
        public void ChildClassWhichOverridesBase_ShouldStillWork_WhenMocked()
        {
            var mock = Mock.Me(default(ChildClass));

            ChildClass notMocked = new();

            Assert.Equal(55, notMocked.Return55FromChildMethodOverride());
        }
    }
}
