using System;
using System.Threading.Tasks;

namespace MockMe.Tests.Overloads
{
    internal sealed class SealedOverloadsClass
    {
        public int Prop_GetSet { get; set; }
        public int Prop_GetInit { get; init; }
        public int Prop_GetOnly { get; }
        public int Prop_SetOnly
        {
            set => throw new NotImplementedException();
        }

        public string this[int index] => throw new NotImplementedException();
        public int this[string index] => throw new NotImplementedException();

        public void VoidReturn() => throw new NotImplementedException();

        public void VoidReturn(int p1) => throw new NotImplementedException();

        public void VoidReturn(int p1, int p2) => throw new NotImplementedException();

        public void VoidReturn(int p1, int p2, int p3) => throw new NotImplementedException();

        public void VoidReturn(int p1, int p2, int p3, int p4) =>
            throw new NotImplementedException();

        public void VoidReturn(int p1, int p2, int p3, int p4, int p5) =>
            throw new NotImplementedException();

        public void VoidReturn(int p1, int p2, int p3, int p4, int p5, int p6) =>
            throw new NotImplementedException();

        public void VoidReturn(int p1, int p2, int p3, int p4, int p5, int p6, int p7) =>
            throw new NotImplementedException();

        public void VoidReturn(int p1, int p2, int p3, int p4, int p5, int p6, int p7, int p8) =>
            throw new NotImplementedException();

        public void VoidReturn(
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

        public void VoidReturn(
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

        public void VoidReturn(
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

        public void VoidReturn(
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

        public void VoidReturn(
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

        public void VoidReturn(
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

        public void VoidReturn(
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

        public int SyncReturn() => throw new NotImplementedException();

        public int SyncReturn(int p1) => throw new NotImplementedException();

        public int SyncReturn(int p1, int p2) => throw new NotImplementedException();

        public int SyncReturn(int p1, int p2, int p3) => throw new NotImplementedException();

        public int SyncReturn(int p1, int p2, int p3, int p4) =>
            throw new NotImplementedException();

        public int SyncReturn(int p1, int p2, int p3, int p4, int p5) =>
            throw new NotImplementedException();

        public int SyncReturn(int p1, int p2, int p3, int p4, int p5, int p6) =>
            throw new NotImplementedException();

        public int SyncReturn(int p1, int p2, int p3, int p4, int p5, int p6, int p7) =>
            throw new NotImplementedException();

        public int SyncReturn(int p1, int p2, int p3, int p4, int p5, int p6, int p7, int p8) =>
            throw new NotImplementedException();

        public int SyncReturn(
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

        public int SyncReturn(
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

        public int SyncReturn(
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

        public int SyncReturn(
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

        public int SyncReturn(
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

        public int SyncReturn(
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

        public int SyncReturn(
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

        public Task AsyncReturn() => throw new NotImplementedException();

        public Task AsyncReturn(int p1) => throw new NotImplementedException();

        public Task AsyncReturn(int p1, int p2) => throw new NotImplementedException();

        public Task AsyncReturn(int p1, int p2, int p3) => throw new NotImplementedException();

        public Task AsyncReturn(int p1, int p2, int p3, int p4) =>
            throw new NotImplementedException();

        public Task AsyncReturn(int p1, int p2, int p3, int p4, int p5) =>
            throw new NotImplementedException();

        public Task AsyncReturn(int p1, int p2, int p3, int p4, int p5, int p6) =>
            throw new NotImplementedException();

        public Task AsyncReturn(int p1, int p2, int p3, int p4, int p5, int p6, int p7) =>
            throw new NotImplementedException();

        public Task AsyncReturn(int p1, int p2, int p3, int p4, int p5, int p6, int p7, int p8) =>
            throw new NotImplementedException();

        public Task AsyncReturn(
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

        public Task AsyncReturn(
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

        public Task AsyncReturn(
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

        public Task AsyncReturn(
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

        public Task AsyncReturn(
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

        public Task AsyncReturn(
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

        public Task AsyncReturn(
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

        public Task<int> AsyncOfTReturn() => throw new NotImplementedException();

        public Task<int> AsyncOfTReturn(int p1) => throw new NotImplementedException();

        public Task<int> AsyncOfTReturn(int p1, int p2) => throw new NotImplementedException();

        public Task<int> AsyncOfTReturn(int p1, int p2, int p3) =>
            throw new NotImplementedException();

        public Task<int> AsyncOfTReturn(int p1, int p2, int p3, int p4) =>
            throw new NotImplementedException();

        public Task<int> AsyncOfTReturn(int p1, int p2, int p3, int p4, int p5) =>
            throw new NotImplementedException();

        public Task<int> AsyncOfTReturn(int p1, int p2, int p3, int p4, int p5, int p6) =>
            throw new NotImplementedException();

        public Task<int> AsyncOfTReturn(int p1, int p2, int p3, int p4, int p5, int p6, int p7) =>
            throw new NotImplementedException();

        public Task<int> AsyncOfTReturn(
            int p1,
            int p2,
            int p3,
            int p4,
            int p5,
            int p6,
            int p7,
            int p8
        ) => throw new NotImplementedException();

        public Task<int> AsyncOfTReturn(
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

        public Task<int> AsyncOfTReturn(
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

        public Task<int> AsyncOfTReturn(
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

        public Task<int> AsyncOfTReturn(
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

        public Task<int> AsyncOfTReturn(
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

        public Task<int> AsyncOfTReturn(
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

        public Task<int> AsyncOfTReturn(
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
