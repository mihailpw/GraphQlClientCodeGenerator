namespace GQLCCG.Infra.Models
{
    public class GraphQlNonNullType : GraphQlType
    {
        public GraphQlType OfType { get; set; }
    }
}