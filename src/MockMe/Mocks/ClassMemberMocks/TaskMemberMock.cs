namespace MockMe.Mocks.ClassMemberMocks;

public class TaskMemberMock<TReturn> : MemberMock<Task<TReturn>>
{
    public TaskMemberMock<TReturn> ReturnsAsync(
        TReturn returnThis,
        params TReturn[] thenReturnThese
    )
    {
        this.Returns(
            Task.FromResult(returnThis),
            thenReturnThese.Select(ret => Task.FromResult(ret)).ToArray()
        );
        return this;
    }
}

public class TaskMemberMock<TArgCollection, TReturn> : MemberMock<TArgCollection, Task<TReturn>>
{
    public TaskMemberMock<TArgCollection, TReturn> ReturnsAsync(
        TReturn returnThis,
        params TReturn[] thenReturnThese
    )
    {
        this.Returns(
            Task.FromResult(returnThis),
            thenReturnThese.Select(ret => Task.FromResult(ret)).ToArray()
        );
        return this;
    }
}
