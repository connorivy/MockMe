using System;
using MockMe.Tests.ExampleClasses.Interfaces;
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

        [Fact]
        public void ICalculator_ShouldImplementAllInterfaceMethods()
        {
            var calculatorMock = Mock.Me(default(ICalculator));

            calculatorMock.Setup.Add(Arg.Any(), Arg.Any()).Returns(args => args.x + args.y);
            calculatorMock.Setup.Subtract(Arg.Any(), Arg.Any()).Returns(args => args.a - args.b);
            calculatorMock.Setup.Multiply(Arg.Any(), Arg.Any()).Returns(args => args.x * args.y);
            calculatorMock.Setup.Divide(Arg.Any(), Arg.Any()).Returns(args => args.a / args.b);

            Assert.Equal(4, calculatorMock.MockedObject.Add(2, 2));
            Assert.Equal(2, calculatorMock.MockedObject.Subtract(4, 2));
            Assert.Equal(4, calculatorMock.MockedObject.Multiply(2, 2));
            Assert.Equal(2, calculatorMock.MockedObject.Divide(4, 2));
        }
    }
}
