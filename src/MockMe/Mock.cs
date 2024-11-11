namespace MockMe;

public abstract class Mock<TObjectToMock>
{
    //    public virtual TObjectToMock MockedObject { get; } =
    //        (TObjectToMock)
    //#pragma warning disable SYSLIB0050 // Type or member is obsolete
    //            System.Runtime.Serialization.FormatterServices.GetUninitializedObject(
    //#pragma warning restore SYSLIB0050 // Type or member is obsolete
    //                typeof(TObjectToMock)
    //            );
    public abstract TObjectToMock MockedObject { get; }

    public static implicit operator TObjectToMock(Mock<TObjectToMock> mock) => mock.MockedObject;
}

public abstract class SealedTypeMock<TObjectToMock> : Mock<TObjectToMock>
{
    public override TObjectToMock MockedObject { get; } =
        (TObjectToMock)
#pragma warning disable SYSLIB0050 // Type or member is obsolete
            System.Runtime.Serialization.FormatterServices.GetUninitializedObject(
#pragma warning restore SYSLIB0050 // Type or member is obsolete
                typeof(TObjectToMock)
            );

    public static implicit operator TObjectToMock(SealedTypeMock<TObjectToMock> mock) =>
        mock.MockedObject;
}

public abstract class InterfaceMock<TObjectToMock> : Mock<TObjectToMock>
{
    public static implicit operator TObjectToMock(InterfaceMock<TObjectToMock> mock) =>
        mock.MockedObject;
}

//public abstract class Mock<TSetup, TAsserter>
//{
//    public abstract TSetup Setup { get; }
//    public abstract TAsserter Assert { get; }
//}
