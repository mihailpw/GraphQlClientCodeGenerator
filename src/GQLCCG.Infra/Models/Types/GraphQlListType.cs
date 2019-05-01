namespace GQLCCG.Infra.Models.Types
{
    public class GraphQlListType : GraphQlTypeBase
    {
        public GraphQlTypeBase OfType { get; set; }
    }
}