namespace GQLCCG.Infra.Models.Types
{
    public class GraphQlNonNullType : GraphQlTypeBase
    {
        public GraphQlTypeBase OfType { get; set; }
    }
}