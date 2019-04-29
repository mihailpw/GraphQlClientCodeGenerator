namespace GQLCCG.Infra.Models.Objects
{
    public class GraphQlEnumValue : GraphQlDeprecatableObjectBase
    {
        public GraphQlEnumValue(
            string name,
            string description,
            bool isDeprecated,
            string deprecationReason)
            : base(name, description, isDeprecated, deprecationReason)
        {
        }
    }
}