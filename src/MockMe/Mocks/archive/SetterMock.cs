namespace MockMe.Mocks.archive;

public class SetterMock<TProperty>
{
    internal List<Action<TProperty>>? Actions { get; private set; }

    protected void Callback(Action<TProperty> action)
    {
        Actions ??= [];
        Actions.Add(action);
    }
}
