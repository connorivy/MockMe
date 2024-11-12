using System.Text;

namespace MockMe.Generator;

public class PropertyMetadata
{
    public required string Name { get; init; }
    public required string ReturnType { get; init; }
    public string? GetterField { get; set; }
    public string? SetterField { get; set; }
    public string? GetterLogic { get; set; }
    public string? SetterLogic { get; set; }

    public void AddPropToSb(StringBuilder sb)
    {
        sb.Append(
            @$"
            {this.GetterField}
            {this.SetterField}"
        );

        sb.Append(
            $@"
            public {this.ReturnType} {this.Name}
            {{"
        );

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

        sb.Append(
            @"
            }"
        );
    }

    //public void AddPropToSb(StringBuilder sb)
    //{
    //    sb.Append(
    //        $@"
    //        public {this.ReturnType} {this.Name}
    //        {{"
    //    );

    //    if (this.GetterLogic is not null)
    //    {
    //        sb.Append(
    //            $@"
    //            get => get_{this.Name}()"
    //        );
    //    }
    //    if (this.SetterLogic is not null)
    //    {
    //        sb.Append(
    //            $@"
    //            set => set_{this.Name}(value)"
    //        );
    //    }

    //    sb.Append(
    //        @"
    //        }"
    //    );
    //}
}
