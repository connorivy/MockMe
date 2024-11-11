namespace MockMe.Abstractions;

public abstract class MockBase<TObjectToMock>
{
    public abstract TObjectToMock MockedObject { get; }

    public static implicit operator TObjectToMock(MockBase<TObjectToMock> mock) =>
        mock.MockedObject;
}

public abstract class SealedTypeMock<TObjectToMock> : MockBase<TObjectToMock>
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

public abstract class InterfaceMock<TObjectToMock, TCallTracker> : MockBase<TObjectToMock>
    where TCallTracker : TObjectToMock
{
    protected TCallTracker CallTracker { get; }

    protected InterfaceMock(TCallTracker callTracker)
    {
        this.CallTracker = callTracker;
    }

    public override TObjectToMock MockedObject => this.CallTracker;

    public static implicit operator TObjectToMock(
        InterfaceMock<TObjectToMock, TCallTracker> mock
    ) => mock.MockedObject;
}
