using MockMe.Mocks.ClassMemberMocks.Setup;

namespace MockMe.Mocks.ClassMemberMocks;

public class PropertySetterArgs<TProperty> : OriginalArgBag<TProperty>
{
    public PropertySetterArgs(TProperty value)
        : base(value) { }

    public TProperty Value => this.Arg1;
}

public class IndexerSetterArgs<TIndexer, TProperty> : OriginalArgBag<TIndexer, TProperty>
{
    public IndexerSetterArgs(TIndexer index, TProperty value)
        : base(index, value) { }

    public TIndexer Index => this.Arg1;
    public TProperty Value => this.Arg2;
}

public class IndexerGetterArgs<TIndexer> : OriginalArgBag<TIndexer>
{
    public IndexerGetterArgs(TIndexer index)
        : base(index) { }

    public TIndexer Index => this.Arg1;
}

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
    List<ArgBagWithMock<PropertySetterArgs<TProperty>>> setterArgBag
)
{
    public MemberMock<TProperty> Get() => getter;

    public VoidMemberMock<PropertySetterArgs<TProperty>> Set(Arg<TProperty> propValue) =>
        MemberMockSetup.SetupMethod<
            PropertySetterArgs<TProperty>,
            VoidMemberMock<PropertySetterArgs<TProperty>>
        >(setterArgBag, new ArgBag<TProperty>(propValue));
}

public class GetSetPropertyMock<TIndexer, TProperty>(
    MemberMock<IndexerGetterArgs<TIndexer>, TProperty> getter,
    List<ArgBagWithMock<IndexerSetterArgs<TIndexer, TProperty>>> setterArgBag,
    Arg<TIndexer> indexer
)
{
    public MemberMock<IndexerGetterArgs<TIndexer>, TProperty> Get() => getter;

    public VoidMemberMock<IndexerSetterArgs<TIndexer, TProperty>> Set(Arg<TProperty> propValue) =>
        MemberMockSetup.SetupMethod<
            IndexerSetterArgs<TIndexer, TProperty>,
            VoidMemberMock<IndexerSetterArgs<TIndexer, TProperty>>
        >(setterArgBag, new ArgBag<TIndexer, TProperty>(indexer, propValue));
}

public class SetPropertyMock<TProperty>(
    List<ArgBagWithMock<PropertySetterArgs<TProperty>>> setterArgBag
)
{
    public VoidMemberMock<PropertySetterArgs<TProperty>> Set(Arg<TProperty> propValue) =>
        MemberMockSetup.SetupMethod<
            PropertySetterArgs<TProperty>,
            VoidMemberMock<PropertySetterArgs<TProperty>>
        >(setterArgBag, new ArgBag<TProperty>(propValue));
}

public class SetPropertyMock<TIndexer, TProperty>(
    List<ArgBagWithMock<IndexerSetterArgs<TIndexer, TProperty>>> setterArgBag,
    Arg<TIndexer> indexer
)
{
    public VoidMemberMock<IndexerSetterArgs<TIndexer, TProperty>> Set(Arg<TProperty> propValue) =>
        MemberMockSetup.SetupMethod<
            IndexerSetterArgs<TIndexer, TProperty>,
            VoidMemberMock<IndexerSetterArgs<TIndexer, TProperty>>
        >(setterArgBag, new ArgBag<TIndexer, TProperty>(indexer, propValue));
}
