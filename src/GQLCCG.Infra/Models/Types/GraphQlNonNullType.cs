namespace GQLCCG.Infra.Models.Types
{
    public class GraphQlNonNullType : GraphQlTypeBase
    {
        public GraphQlTypeBase OfType { get; internal set; }


        public GraphQlNonNullType(
            string name,
            string description,
            GraphQlTypeBase ofType)
            : base(name, description)
        {
            OfType = ofType;
        }
    }
}