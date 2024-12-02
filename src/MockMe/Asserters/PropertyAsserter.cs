using MockMe.Mocks.ClassMemberMocks;

namespace MockMe.Asserters;

public class GetPropertyAsserter<TProperty>(int getNumTimesCalled)
// generic type is only here for consistency and easier source gen
{
    public MemberAsserter Get() => new(getNumTimesCalled);
}

public class GetPropertyAsserter<TIndexer, TProperty>(
    Arg<TIndexer> index,
    List<IndexerGetterArgs<TIndexer>>? getCallStore
) : MockAsserter
// generic type is only here for consistency and easier source gen
{
    public MemberAsserter Get() =>
        MockAsserter.GetMemberAsserter(getCallStore, new ArgBag<TIndexer>(index));
}

public class GetSetPropertyAsserter<TProperty>(
    int getNumTimesCalled,
    List<PropertySetterArgs<TProperty>>? setCallStore
) : MockAsserter
{
    public MemberAsserter Get() => new(getNumTimesCalled);

    public MemberAsserter Set(Arg<TProperty> propValue) =>
        MockAsserter.GetMemberAsserter(setCallStore, new ArgBag<TProperty>(propValue));
}

public class GetSetPropertyAsserter<TIndexer, TProperty>(
    Arg<TIndexer> index,
    List<IndexerGetterArgs<TIndexer>>? getCallStore,
    List<IndexerSetterArgs<TIndexer, TProperty>>? setCallStore
) : MockAsserter
{
    public MemberAsserter Get() =>
        MockAsserter.GetMemberAsserter(getCallStore, new ArgBag<TIndexer>(index));

    public MemberAsserter Set(Arg<TProperty> propValue) =>
        MockAsserter.GetMemberAsserter(
            setCallStore,
            new ArgBag<TIndexer, TProperty>(index, propValue)
        );
}

public class SetPropertyAsserter<TProperty>(List<PropertySetterArgs<TProperty>>? setCallStore)
    : MockAsserter
{
    public MemberAsserter Set(Arg<TProperty> propValue) =>
        MockAsserter.GetMemberAsserter(setCallStore, new ArgBag<TProperty>(propValue));
}

public class SetPropertyAsserter<TIndexer, TProperty>(
    Arg<TIndexer> index,
    List<IndexerSetterArgs<TIndexer, TProperty>>? setCallStore
) : MockAsserter
{
    public MemberAsserter Set(Arg<TProperty> propValue) =>
        MockAsserter.GetMemberAsserter(
            setCallStore,
            new ArgBag<TIndexer, TProperty>(index, propValue)
        );
}
