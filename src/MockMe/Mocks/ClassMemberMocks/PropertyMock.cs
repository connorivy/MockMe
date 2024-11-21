using MockMe.Mocks.ClassMemberMocks.Setup;

namespace MockMe.Mocks.ClassMemberMocks;

public class GetPropertyMock<TProperty>(MemberMock<TProperty> getter)
{
    public MemberMock<TProperty> Get() => getter;
}

public class GetPropertyMock<TIndexer, TProperty>(MemberMock<TIndexer, TProperty> getter)
{
    public MemberMock<TIndexer, TProperty> Get() => getter;
}

public class GetSetPropertyMock<TProperty>(
    MemberMock<TProperty> getter,
    List<ArgBagWithVoidMemberMock<TProperty>> setterArgBag
)
{
    public MemberMock<TProperty> Get() => getter;

    public VoidMemberMock<TProperty> Set(Arg<TProperty> propValue) =>
        MemberMockSetup.SetupVoidMethod(setterArgBag, propValue);
}

public class GetSetPropertyMock<TIndexer, TProperty>(
    MemberMock<TIndexer, TProperty> getter,
    List<ArgBagWithVoidMemberMock<TIndexer, TProperty>> setterArgBag,
    Arg<TIndexer> indexer
)
{
    public MemberMock<TIndexer, TProperty> Get() => getter;

    public VoidMemberMock<TIndexer, TProperty> Set(Arg<TProperty> propValue) =>
        MemberMockSetup.SetupVoidMethod(setterArgBag, indexer, propValue);
}

public class SetPropertyMock<TProperty>(List<ArgBagWithVoidMemberMock<TProperty>> setterArgBag)
{
    public VoidMemberMock<TProperty> Set(Arg<TProperty> propValue) =>
        MemberMockSetup.SetupVoidMethod(setterArgBag, propValue);
}

public class SetPropertyMock<TIndexer, TProperty>(
    List<ArgBagWithVoidMemberMock<TIndexer, TProperty>> setterArgBag,
    Arg<TIndexer> indexer
)
{
    public VoidMemberMock<TIndexer, TProperty> Set(Arg<TProperty> propValue) =>
        MemberMockSetup.SetupVoidMethod(setterArgBag, indexer, propValue);
}
