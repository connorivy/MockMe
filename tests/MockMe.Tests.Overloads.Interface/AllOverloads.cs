using System.Threading.Tasks;

#pragma warning disable CA1716 // Using reserved word in namespace
namespace MockMe.Tests.Overloads
#pragma warning restore IDE0130 // Using reserved word in namespace
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Style",
        "IDE0040:Add accessibility modifiers",
        Justification = "<Pending>"
    )]
    internal interface AllOverloads
    {
        public int OutArgument(out int arg);
        public int OutArgument(int arg);

        protected int ProtectedProp { get; set; }
        protected int ProtectedMethod();

        internal int InternalProp { get; set; }
        internal int InternalMethod();

        public double this[double index] { set; }
        public string this[int index] { get; set; }
        public int this[string index] { get; set; }

        internal int this[float index] { get; set; }
        protected int this[decimal index] { get; set; }

        public int Prop_GetInit { get; init; }
        public int Prop_GetOnly { get; }
        public int Prop_GetSet { get; set; }
        public int Prop_SetOnly { set; }

        public Task<int> AsyncOfTReturn();
        Task<int> AsyncOfTReturn(int p1);
        Task<int> AsyncOfTReturn(int p1, int p2);
        Task<int> AsyncOfTReturn(int p1, int p2, int p3);
        Task<int> AsyncOfTReturn(int p1, int p2, int p3, int p4);
        Task<int> AsyncOfTReturn(int p1, int p2, int p3, int p4, int p5);
        Task<int> AsyncOfTReturn(int p1, int p2, int p3, int p4, int p5, int p6);
        Task<int> AsyncOfTReturn(int p1, int p2, int p3, int p4, int p5, int p6, int p7);
        Task<int> AsyncOfTReturn(int p1, int p2, int p3, int p4, int p5, int p6, int p7, int p8);
        Task<int> AsyncOfTReturn(
            int p1,
            int p2,
            int p3,
            int p4,
            int p5,
            int p6,
            int p7,
            int p8,
            int p9
        );
        Task<int> AsyncOfTReturn(
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
        );
        Task<int> AsyncOfTReturn(
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
        );
        Task<int> AsyncOfTReturn(
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
        );
        Task<int> AsyncOfTReturn(
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
        );
        Task<int> AsyncOfTReturn(
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
        );
        Task<int> AsyncOfTReturn(
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
        );
        Task AsyncReturn();
        Task AsyncReturn(int p1);
        Task AsyncReturn(int p1, int p2);
        Task AsyncReturn(int p1, int p2, int p3);
        Task AsyncReturn(int p1, int p2, int p3, int p4);
        Task AsyncReturn(int p1, int p2, int p3, int p4, int p5);
        Task AsyncReturn(int p1, int p2, int p3, int p4, int p5, int p6);
        Task AsyncReturn(int p1, int p2, int p3, int p4, int p5, int p6, int p7);
        Task AsyncReturn(int p1, int p2, int p3, int p4, int p5, int p6, int p7, int p8);
        Task AsyncReturn(int p1, int p2, int p3, int p4, int p5, int p6, int p7, int p8, int p9);
        Task AsyncReturn(
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
        );
        Task AsyncReturn(
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
        );
        Task AsyncReturn(
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
        );
        Task AsyncReturn(
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
        );
        Task AsyncReturn(
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
        );
        Task AsyncReturn(
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
        );
        int SyncReturn();
        int SyncReturn(int p1);
        int SyncReturn(int p1, int p2);
        int SyncReturn(int p1, int p2, int p3);
        int SyncReturn(int p1, int p2, int p3, int p4);
        int SyncReturn(int p1, int p2, int p3, int p4, int p5);
        int SyncReturn(int p1, int p2, int p3, int p4, int p5, int p6);
        int SyncReturn(int p1, int p2, int p3, int p4, int p5, int p6, int p7);
        int SyncReturn(int p1, int p2, int p3, int p4, int p5, int p6, int p7, int p8);
        int SyncReturn(int p1, int p2, int p3, int p4, int p5, int p6, int p7, int p8, int p9);
        int SyncReturn(
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
        );
        int SyncReturn(
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
        );
        int SyncReturn(
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
        );
        int SyncReturn(
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
        );
        int SyncReturn(
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
        );
        int SyncReturn(
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
        );
        void VoidReturn();
        void VoidReturn(int p1);
        void VoidReturn(int p1, int p2);
        void VoidReturn(int p1, int p2, int p3);
        void VoidReturn(int p1, int p2, int p3, int p4);
        void VoidReturn(int p1, int p2, int p3, int p4, int p5);
        void VoidReturn(int p1, int p2, int p3, int p4, int p5, int p6);
        void VoidReturn(int p1, int p2, int p3, int p4, int p5, int p6, int p7);
        void VoidReturn(int p1, int p2, int p3, int p4, int p5, int p6, int p7, int p8);
        void VoidReturn(int p1, int p2, int p3, int p4, int p5, int p6, int p7, int p8, int p9);
        void VoidReturn(
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
        );
        void VoidReturn(
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
        );
        void VoidReturn(
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
        );
        void VoidReturn(
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
        );
        void VoidReturn(
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
        );
        void VoidReturn(
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
        );
    }
}
