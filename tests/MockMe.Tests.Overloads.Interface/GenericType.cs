namespace MockMe.Tests.Overloads
{
    public interface GenericType<T1, T2, T3>
    {
        public TM2 MixesClassAndMethodScopedGeneric<TM2>(T1 t1, TM2 t2, T3 t3);
        public T1 UsesMultipleClassScopedGeneric(T1 t1, T2 t2, T3 t3);
        public TM1 UsesMultipleMethodScopedGeneric<TM1, TM2, TM3>(TM1 t1, TM2 t2, TM3 t3);
        public T1 UsesSingleClassScopedGeneric(T1 t);
        public TMethod UsesSingleMethodScopedGeneric<TMethod>(TMethod t);
    }
}
