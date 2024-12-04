using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

#pragma warning disable CA1716 // Using reserved word in namespace
namespace MockMe.Tests.Overloads
#pragma warning restore CA1716 // Using reserved word in namespace
{
    [ExcludeFromCodeCoverage]
    internal class AllOverloads
    {
        public int OutArgument(out int arg) => throw new NotImplementedException();

        public int OutArgument(int arg) => throw new NotImplementedException();

        protected virtual int ProtectedProp { get; set; }

        protected virtual int ProtectedMethod() => throw new NotImplementedException();

        internal virtual int InternalProp { get; set; }

        internal virtual int InternalMethod() => throw new NotImplementedException();

        public virtual int Prop_GetSet { get; set; }
        public virtual int Prop_GetInit { get; init; }
        public virtual int Prop_GetOnly { get; }
        public virtual int Prop_SetOnly
        {
            set => throw new NotImplementedException();
        }

        public string this[int index]
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public virtual int this[string index]
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public double this[double index]
        {
            set => throw new NotImplementedException();
        }
        internal virtual int this[float index]
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
        protected virtual int this[decimal index]
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public virtual void VoidReturn() => throw new NotImplementedException();

        public virtual void VoidReturn(int p1) => throw new NotImplementedException();

        public virtual void VoidReturn(int p1, int p2) => throw new NotImplementedException();

        public virtual void VoidReturn(int p1, int p2, int p3) =>
            throw new NotImplementedException();

        public virtual void VoidReturn(int p1, int p2, int p3, int p4) =>
            throw new NotImplementedException();

        public virtual void VoidReturn(int p1, int p2, int p3, int p4, int p5) =>
            throw new NotImplementedException();

        public virtual void VoidReturn(int p1, int p2, int p3, int p4, int p5, int p6) =>
            throw new NotImplementedException();

        public virtual void VoidReturn(int p1, int p2, int p3, int p4, int p5, int p6, int p7) =>
            throw new NotImplementedException();

        public virtual void VoidReturn(
            int p1,
            int p2,
            int p3,
            int p4,
            int p5,
            int p6,
            int p7,
            int p8
        ) => throw new NotImplementedException();

        public virtual void VoidReturn(
            int p1,
            int p2,
            int p3,
            int p4,
            int p5,
            int p6,
            int p7,
            int p8,
            int p9
        ) => throw new NotImplementedException();

        public virtual void VoidReturn(
            int p1,
            int p2,
            int p3,
            int p4,
            int p5,
            int p6,
            int p7,
            int p8,
            int p9,
            int p10
        ) => throw new NotImplementedException();

        public virtual void VoidReturn(
            int p1,
            int p2,
            int p3,
            int p4,
            int p5,
            int p6,
            int p7,
            int p8,
            int p9,
            int p10,
            int p11
        ) => throw new NotImplementedException();

        public virtual void VoidReturn(
            int p1,
            int p2,
            int p3,
            int p4,
            int p5,
            int p6,
            int p7,
            int p8,
            int p9,
            int p10,
            int p11,
            int p12
        ) => throw new NotImplementedException();

        public virtual void VoidReturn(
            int p1,
            int p2,
            int p3,
            int p4,
            int p5,
            int p6,
            int p7,
            int p8,
            int p9,
            int p10,
            int p11,
            int p12,
            int p13
        ) => throw new NotImplementedException();

        public virtual void VoidReturn(
            int p1,
            int p2,
            int p3,
            int p4,
            int p5,
            int p6,
            int p7,
            int p8,
            int p9,
            int p10,
            int p11,
            int p12,
            int p13,
            int p14
        ) => throw new NotImplementedException();

        public virtual void VoidReturn(
            int p1,
            int p2,
            int p3,
            int p4,
            int p5,
            int p6,
            int p7,
            int p8,
            int p9,
            int p10,
            int p11,
            int p12,
            int p13,
            int p14,
            int p15
        ) => throw new NotImplementedException();

        public virtual int SyncReturn() => throw new NotImplementedException();

        public virtual int SyncReturn(int p1) => throw new NotImplementedException();

        public virtual int SyncReturn(int p1, int p2) => throw new NotImplementedException();

        public virtual int SyncReturn(int p1, int p2, int p3) =>
            throw new NotImplementedException();

        public virtual int SyncReturn(int p1, int p2, int p3, int p4) =>
            throw new NotImplementedException();

        public virtual int SyncReturn(int p1, int p2, int p3, int p4, int p5) =>
            throw new NotImplementedException();

        public virtual int SyncReturn(int p1, int p2, int p3, int p4, int p5, int p6) =>
            throw new NotImplementedException();

        public virtual int SyncReturn(int p1, int p2, int p3, int p4, int p5, int p6, int p7) =>
            throw new NotImplementedException();

        public virtual int SyncReturn(
            int p1,
            int p2,
            int p3,
            int p4,
            int p5,
            int p6,
            int p7,
            int p8
        ) => throw new NotImplementedException();

        public virtual int SyncReturn(
            int p1,
            int p2,
            int p3,
            int p4,
            int p5,
            int p6,
            int p7,
            int p8,
            int p9
        ) => throw new NotImplementedException();

        public virtual int SyncReturn(
            int p1,
            int p2,
            int p3,
            int p4,
            int p5,
            int p6,
            int p7,
            int p8,
            int p9,
            int p10
        ) => throw new NotImplementedException();

        public virtual int SyncReturn(
            int p1,
            int p2,
            int p3,
            int p4,
            int p5,
            int p6,
            int p7,
            int p8,
            int p9,
            int p10,
            int p11
        ) => throw new NotImplementedException();

        public virtual int SyncReturn(
            int p1,
            int p2,
            int p3,
            int p4,
            int p5,
            int p6,
            int p7,
            int p8,
            int p9,
            int p10,
            int p11,
            int p12
        ) => throw new NotImplementedException();

        public virtual int SyncReturn(
            int p1,
            int p2,
            int p3,
            int p4,
            int p5,
            int p6,
            int p7,
            int p8,
            int p9,
            int p10,
            int p11,
            int p12,
            int p13
        ) => throw new NotImplementedException();

        public virtual int SyncReturn(
            int p1,
            int p2,
            int p3,
            int p4,
            int p5,
            int p6,
            int p7,
            int p8,
            int p9,
            int p10,
            int p11,
            int p12,
            int p13,
            int p14
        ) => throw new NotImplementedException();

        public virtual int SyncReturn(
            int p1,
            int p2,
            int p3,
            int p4,
            int p5,
            int p6,
            int p7,
            int p8,
            int p9,
            int p10,
            int p11,
            int p12,
            int p13,
            int p14,
            int p15
        ) => throw new NotImplementedException();

        public virtual Task AsyncReturn() => throw new NotImplementedException();

        public virtual Task AsyncReturn(int p1) => throw new NotImplementedException();

        public virtual Task AsyncReturn(int p1, int p2) => throw new NotImplementedException();

        public virtual Task AsyncReturn(int p1, int p2, int p3) =>
            throw new NotImplementedException();

        public virtual Task AsyncReturn(int p1, int p2, int p3, int p4) =>
            throw new NotImplementedException();

        public virtual Task AsyncReturn(int p1, int p2, int p3, int p4, int p5) =>
            throw new NotImplementedException();

        public virtual Task AsyncReturn(int p1, int p2, int p3, int p4, int p5, int p6) =>
            throw new NotImplementedException();

        public virtual Task AsyncReturn(int p1, int p2, int p3, int p4, int p5, int p6, int p7) =>
            throw new NotImplementedException();

        public virtual Task AsyncReturn(
            int p1,
            int p2,
            int p3,
            int p4,
            int p5,
            int p6,
            int p7,
            int p8
        ) => throw new NotImplementedException();

        public virtual Task AsyncReturn(
            int p1,
            int p2,
            int p3,
            int p4,
            int p5,
            int p6,
            int p7,
            int p8,
            int p9
        ) => throw new NotImplementedException();

        public virtual Task AsyncReturn(
            int p1,
            int p2,
            int p3,
            int p4,
            int p5,
            int p6,
            int p7,
            int p8,
            int p9,
            int p10
        ) => throw new NotImplementedException();

        public virtual Task AsyncReturn(
            int p1,
            int p2,
            int p3,
            int p4,
            int p5,
            int p6,
            int p7,
            int p8,
            int p9,
            int p10,
            int p11
        ) => throw new NotImplementedException();

        public virtual Task AsyncReturn(
            int p1,
            int p2,
            int p3,
            int p4,
            int p5,
            int p6,
            int p7,
            int p8,
            int p9,
            int p10,
            int p11,
            int p12
        ) => throw new NotImplementedException();

        public virtual Task AsyncReturn(
            int p1,
            int p2,
            int p3,
            int p4,
            int p5,
            int p6,
            int p7,
            int p8,
            int p9,
            int p10,
            int p11,
            int p12,
            int p13
        ) => throw new NotImplementedException();

        public virtual Task AsyncReturn(
            int p1,
            int p2,
            int p3,
            int p4,
            int p5,
            int p6,
            int p7,
            int p8,
            int p9,
            int p10,
            int p11,
            int p12,
            int p13,
            int p14
        ) => throw new NotImplementedException();

        public virtual Task AsyncReturn(
            int p1,
            int p2,
            int p3,
            int p4,
            int p5,
            int p6,
            int p7,
            int p8,
            int p9,
            int p10,
            int p11,
            int p12,
            int p13,
            int p14,
            int p15
        ) => throw new NotImplementedException();

        public virtual Task<int> AsyncOfTReturn() => throw new NotImplementedException();

        public virtual Task<int> AsyncOfTReturn(int p1) => throw new NotImplementedException();

        public virtual Task<int> AsyncOfTReturn(int p1, int p2) =>
            throw new NotImplementedException();

        public virtual Task<int> AsyncOfTReturn(int p1, int p2, int p3) =>
            throw new NotImplementedException();

        public virtual Task<int> AsyncOfTReturn(int p1, int p2, int p3, int p4) =>
            throw new NotImplementedException();

        public virtual Task<int> AsyncOfTReturn(int p1, int p2, int p3, int p4, int p5) =>
            throw new NotImplementedException();

        public virtual Task<int> AsyncOfTReturn(int p1, int p2, int p3, int p4, int p5, int p6) =>
            throw new NotImplementedException();

        public virtual Task<int> AsyncOfTReturn(
            int p1,
            int p2,
            int p3,
            int p4,
            int p5,
            int p6,
            int p7
        ) => throw new NotImplementedException();

        public virtual Task<int> AsyncOfTReturn(
            int p1,
            int p2,
            int p3,
            int p4,
            int p5,
            int p6,
            int p7,
            int p8
        ) => throw new NotImplementedException();

        public virtual Task<int> AsyncOfTReturn(
            int p1,
            int p2,
            int p3,
            int p4,
            int p5,
            int p6,
            int p7,
            int p8,
            int p9
        ) => throw new NotImplementedException();

        public virtual Task<int> AsyncOfTReturn(
            int p1,
            int p2,
            int p3,
            int p4,
            int p5,
            int p6,
            int p7,
            int p8,
            int p9,
            int p10
        ) => throw new NotImplementedException();

        public virtual Task<int> AsyncOfTReturn(
            int p1,
            int p2,
            int p3,
            int p4,
            int p5,
            int p6,
            int p7,
            int p8,
            int p9,
            int p10,
            int p11
        ) => throw new NotImplementedException();

        public virtual Task<int> AsyncOfTReturn(
            int p1,
            int p2,
            int p3,
            int p4,
            int p5,
            int p6,
            int p7,
            int p8,
            int p9,
            int p10,
            int p11,
            int p12
        ) => throw new NotImplementedException();

        public virtual Task<int> AsyncOfTReturn(
            int p1,
            int p2,
            int p3,
            int p4,
            int p5,
            int p6,
            int p7,
            int p8,
            int p9,
            int p10,
            int p11,
            int p12,
            int p13
        ) => throw new NotImplementedException();

        public virtual Task<int> AsyncOfTReturn(
            int p1,
            int p2,
            int p3,
            int p4,
            int p5,
            int p6,
            int p7,
            int p8,
            int p9,
            int p10,
            int p11,
            int p12,
            int p13,
            int p14
        ) => throw new NotImplementedException();

        public virtual Task<int> AsyncOfTReturn(
            int p1,
            int p2,
            int p3,
            int p4,
            int p5,
            int p6,
            int p7,
            int p8,
            int p9,
            int p10,
            int p11,
            int p12,
            int p13,
            int p14,
            int p15
        ) => throw new NotImplementedException();
    }
}
