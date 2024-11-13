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
