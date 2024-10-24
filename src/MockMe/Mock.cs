namespace MockMe;

public abstract class Mock<TObjectToMock>
{
    public TObjectToMock Value { get; } =
        (TObjectToMock)
            System.Runtime.Serialization.FormatterServices.GetUninitializedObject(
                typeof(TObjectToMock)
            );

    public static implicit operator TObjectToMock(Mock<TObjectToMock> mock) => mock.Value;
}

//public abstract class Mock<TSetup, TAsserter>
//{
//    public abstract TSetup Setup { get; }
//    public abstract TAsserter Assert { get; }
//}
