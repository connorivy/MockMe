namespace MockMe.Mocks.ClassMemberMocks;

public class ValueTaskMemberMock<TReturn> : MemberMock<ValueTask<TReturn>>
{
    public ValueTaskMemberMock<TReturn> ReturnsAsync(
        TReturn returnThis,
        params TReturn[] thenReturnThese
    )
    {
        this.Returns(
            new ValueTask<TReturn>(returnThis),
            thenReturnThese.Select(ret => new ValueTask<TReturn>(ret)).ToArray()
        );
        return this;
    }
}
