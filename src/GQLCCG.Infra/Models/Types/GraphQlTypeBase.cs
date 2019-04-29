namespace GQLCCG.Infra.Models.Types
{
    public abstract class GraphQlTypeBase : GraphQlEntityBase
    {
        protected GraphQlTypeBase(string name, string description)
            : base(name, description)
        {
        }
    }
}