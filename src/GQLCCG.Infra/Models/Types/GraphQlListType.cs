namespace GQLCCG.Infra.Models.Types
{
    public class GraphQlListType : GraphQlTypeBase
    {
        public GraphQlTypeBase OfType { get; internal set; }


        public GraphQlListType(
            string name,
            string description,
            GraphQlTypeBase ofType)
            : base(name, description)
        {
            OfType = ofType;
        }
    }
}