using System.Text;
using MockMe.Generator.Extensions;

namespace MockMe.Generator;

public class PropertyMetadata
{
    public required string Name { get; init; }
    public required string ReturnType { get; init; }
    public string? IndexerType { get; init; }
    public string? GetterField { get; set; }
    public string? SetterField { get; set; }
    public virtual string? GetterLogic { get; set; }
    public virtual string? SetterLogic { get; set; }
    public bool HasInit { get; set; }
    public const string ThrowExceptionLogic = "throw new global::System.NotImplementedException();";

    public void AddPropToSb(StringBuilder sb)
    {
        sb.Append(
            @$"
            {this.GetterField}
            {this.SetterField}"
        );

        if (this.IndexerType is null)
        {
            sb.Append(
                $@"
            public {this.ReturnType} {this.Name}
            {{"
            );
        }
        else
        {
            sb.Append(
                $@"
            public {this.ReturnType} this[{this.IndexerType} index]
            {{"
            );
        }

        if (this.GetterLogic is not null)
        {
            sb.Append(
                $@"
                get
                {{
                    {this.GetterLogic}
                }}"
            );
        }
        if (this.SetterLogic is not null)
        {
            sb.Append(
                $@"
                set
                {{
                    {this.SetterLogic}
                }}"
            );
        }
        else if (this.HasInit)
        {
            sb.Append(
                $@"
                init
                {{
                    {ThrowExceptionLogic}
                }}"
            );
        }

        sb.Append(
            @"
            }"
        );
    }
}

public class SetupPropertyMetadata
{
    public required string Name { get; init; }
    public required string PropertyType { get; init; }
    public string? GetterFieldName { get; set; }
    public string? SetterFieldName { get; set; }

    protected virtual string GetterField() =>
        $"private global::MockMe.Mocks.ClassMemberMocks.MemberMock<{this.PropertyType}>? {this.GetterFieldName};";

    protected virtual string SetterField() =>
        $"private List<ArgBagWithVoidMemberMock<{this.PropertyType}>>? {this.SetterFieldName};";

    protected virtual string Body() =>
        $@"
            public global::MockMe.Mocks.ClassMemberMocks.{(this.HasGet() ? "Get" : "")}{(this.HasSet() ? "Set" : "")}PropertyMock<{this.PropertyType}> {this.Name} =>
                new({this.GetterFieldName?.AddSuffixIfNotEmpty(" ??= new()")}{this.SetterFieldName?.AddOnIfNotEmpty(this.HasGet() ? ", " : "", " ??= new()")});";

    public void AddPropToSb(StringBuilder sb)
    {
        if (!string.IsNullOrEmpty(this.GetterFieldName))
        {
            sb.Append(
                @$"
                {this.GetterField()}"
            );
        }

        if (!string.IsNullOrEmpty(this.SetterFieldName))
        {
            sb.Append(
                @$"
                {this.SetterField()}"
            );
        }

        sb.Append(this.Body());
    }

    protected bool HasGet() => !string.IsNullOrEmpty(this.GetterFieldName);

    protected bool HasSet() => !string.IsNullOrEmpty(this.SetterFieldName);
}

public class IndexerSetupPropertyMetadata : SetupPropertyMetadata
{
    public required string IndexerType { get; init; }

    protected override string GetterField() =>
        $"private List<ArgBagWithMemberMock<{this.IndexerType}, {this.PropertyType}>>? {this.GetterFieldName};";

    protected override string SetterField() =>
        $"private List<ArgBagWithVoidMemberMock<{this.IndexerType}, {this.PropertyType}>>? {this.SetterFieldName};";

    protected override string Body() =>
        $@"
        public global::MockMe.Mocks.ClassMemberMocks.{(this.HasGet() ? "Get" : "")}{(this.HasSet() ? "Set" : "")}PropertyMock<{this.IndexerType}, {this.PropertyType}> this[global::MockMe.Arg<{this.IndexerType}> index] =>
            new(
                {(this.HasGet() ? $"SetupMethod(this.{this.GetterFieldName} ??= new(), index)".AddSuffixIfNotEmpty(this.HasSet() ? "," : "") : "")}
                {(this.HasSet() ? $"this.{this.SetterFieldName} ??= new()," : "")}
                {(this.HasSet() ? $"index" : "")}
            );
";
}

public class AssertPropertyMetadata
{
    public required string Name { get; init; }
    public required string PropertyType { get; init; }
    public string? IndexerType { get; init; }
    public string? GetterCallStoreName { get; set; }
    public string? SetterCallStoreName { get; set; }

    public void AddPropToSb(StringBuilder sb)
    {
        //if (!string.IsNullOrEmpty(this.GetterFieldName))
        //{
        //    sb.Append(
        //        @$"
        //        private global::MockMe.Mocks.ClassMemberMocks.MemberMock<{this.PropertyType}>? {this.GetterFieldName};"
        //    );
        //}

        //if (!string.IsNullOrEmpty(this.SetterFieldName))
        //{
        //    sb.Append(
        //        @$"
        //        private List<ArgBagWithVoidMemberMock<{this.PropertyType}>>? {this.SetterFieldName};"
        //    );
        //}

        if (this.IndexerType is null)
        {
            sb.Append(
                $@"
            public global::MockMe.Asserters.{(this.HasGet() ? "Get" : "")}{(this.HasSet() ? "Set" : "")}PropertyAsserter<{this.PropertyType}> {this.Name} =>
                new({this.GetterCallStoreName}{this.SetterCallStoreName?.AddPrefixIfNotEmpty(this.HasGet() ? ", " : "")});"
            );
        }
        else
        {
            sb.Append(
                $@"
            public global::MockMe.Asserters.{(this.HasGet() ? "Get" : "")}{(this.HasSet() ? "Set" : "")}PropertyAsserter<{this.IndexerType}, {this.PropertyType}> this[global::MockMe.Arg<{this.IndexerType}> index] =>
                new(index{this.GetterCallStoreName?.AddPrefixIfNotEmpty(", ")}{this.SetterCallStoreName?.AddPrefixIfNotEmpty(", ")});"
            );
        }
    }

    private bool HasGet() => !string.IsNullOrEmpty(this.GetterCallStoreName);

    private bool HasSet() => !string.IsNullOrEmpty(this.SetterCallStoreName);
}
