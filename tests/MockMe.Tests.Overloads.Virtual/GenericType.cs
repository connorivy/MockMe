using System;

namespace MockMe.Tests.Overloads
{
    public class GenericType<T1, T2, T3>
    {
        public virtual T1 UsesSingleClassScopedGeneric(T1 t) => throw new NotImplementedException();

        public virtual T1 UsesMultipleClassScopedGeneric(T1 t1, T2 t2, T3 t3) =>
            throw new NotImplementedException();

        public virtual TMethod UsesSingleMethodScopedGeneric<TMethod>(TMethod t) =>
            throw new NotImplementedException();

        public virtual TM1 UsesMultipleMethodScopedGeneric<TM1, TM2, TM3>(TM1 t1, TM2 t2, TM3 t3) =>
            throw new NotImplementedException();

        public virtual TM2 MixesClassAndMethodScopedGeneric<TM2>(T1 t1, TM2 t2, T3 t3) =>
            throw new NotImplementedException();
    }
}
