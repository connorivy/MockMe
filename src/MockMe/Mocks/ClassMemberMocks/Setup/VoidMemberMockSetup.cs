namespace MockMe.Mocks.ClassMemberMocks.Setup;

public partial class MemberMockSetup
{
    public static VoidMemberMock SetupVoidMethod(List<VoidMemberMock> mockAndArgsStore) =>
        SetupMemberMockBase<bool, VoidMemberMock, VoidMemberMock>(
            false,
            mockAndArgsStore,
            static (col, mock) => mock
        );

    public static VoidMemberMock<TArg1> SetupVoidMethod<TArg1>(
        List<ArgBagWithVoidMemberMock<TArg1>> mockAndArgsStore,
        Arg<TArg1> arg1
    ) =>
        SetupMemberMockBase<Arg<TArg1>, VoidMemberMock<TArg1>, ArgBagWithVoidMemberMock<TArg1>>(
            arg1,
            mockAndArgsStore,
            static (col, mock) => new(col, mock)
        );
}
