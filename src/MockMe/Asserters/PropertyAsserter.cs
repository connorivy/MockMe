namespace MockMe.Asserters;

public class GetPropertyAsserter<TProperty>(int getNumTimesCalled)
// generic type is only here for consistency and easier source gen
{
    public MemberAsserter Get() => new(getNumTimesCalled);
}

public class GetPropertyAsserter<TIndexer, TProperty>(
    Arg<TIndexer> index,
    List<TIndexer>? getCallStore
) : MockAsserter
// generic type is only here for consistency and easier source gen
{
    public MemberAsserter Get() => MockAsserter.GetMemberAsserter(getCallStore, index);
}

public class GetSetPropertyAsserter<TProperty>(int getNumTimesCalled, List<TProperty>? setCallStore)
    : MockAsserter
{
    public MemberAsserter Get() => new(getNumTimesCalled);

    public MemberAsserter Set(Arg<TProperty> propValue) =>
        MockAsserter.GetMemberAsserter(setCallStore, propValue);
}

public class GetSetPropertyAsserter<TIndexer, TProperty>(
    Arg<TIndexer> index,
    List<TIndexer>? getCallStore,
    List<(TIndexer, TProperty)>? setCallStore
) : MockAsserter
{
    public MemberAsserter Get() => MockAsserter.GetMemberAsserter(getCallStore, index);

    public MemberAsserter Set(Arg<TProperty> propValue) =>
        MockAsserter.GetMemberAsserter(setCallStore, index, propValue);
}

public class SetPropertyAsserter<TProperty>(List<TProperty>? setCallStore) : MockAsserter
{
    public MemberAsserter Set(Arg<TProperty> propValue) =>
        MockAsserter.GetMemberAsserter(setCallStore, propValue);
}

public class SetPropertyAsserter<TIndexer, TProperty>(
    Arg<TIndexer> index,
    List<(TIndexer, TProperty)>? setCallStore
) : MockAsserter
{
    public MemberAsserter Set(Arg<TProperty> propValue) =>
        MockAsserter.GetMemberAsserter(setCallStore, index, propValue);
}
