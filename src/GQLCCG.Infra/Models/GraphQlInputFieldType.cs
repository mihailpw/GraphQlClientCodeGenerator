namespace GQLCCG.Infra.Models
{
    public class GraphQlInputFieldType : GraphQlType
    {
        public GraphQlType Type { get; set; }

        public string DefaultValue { get; set; }
    }
}