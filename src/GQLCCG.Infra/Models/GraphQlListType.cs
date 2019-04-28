namespace GQLCCG.Infra.Models
{
    public class GraphQlListType : GraphQlType
    {
        public GraphQlType OfType { get; set; }
    }
}