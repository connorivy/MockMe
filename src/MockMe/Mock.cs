namespace MockMe;

public abstract class Mock<TObjectToMock>
{
    public TObjectToMock Value { get; } =
        (TObjectToMock)
#pragma warning disable SYSLIB0050 // Type or member is obsolete
            System.Runtime.Serialization.FormatterServices.GetUninitializedObject(
#pragma warning restore SYSLIB0050 // Type or member is obsolete
                typeof(TObjectToMock)
            );

    public static implicit operator TObjectToMock(Mock<TObjectToMock> mock) => mock.Value;
}

//public abstract class Mock<TSetup, TAsserter>
//{
//    public abstract TSetup Setup { get; }
//    public abstract TAsserter Assert { get; }
//}
