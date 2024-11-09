namespace MockMe.Abstractions;

public abstract class MockBase<TObjectToMock, TSetup, TAsserter>
{
    public abstract TSetup Setup { get; }
    public abstract TAsserter Asserter { get; }
    public TObjectToMock Value { get; } =
        (TObjectToMock)
#pragma warning disable SYSLIB0050 // Type or member is obsolete
            System.Runtime.Serialization.FormatterServices.GetUninitializedObject(
#pragma warning restore SYSLIB0050 // Type or member is obsolete
                typeof(TObjectToMock)
            );

    public static implicit operator TObjectToMock(
        MockBase<TObjectToMock, TSetup, TAsserter> mock
    ) => mock.Value;
}
