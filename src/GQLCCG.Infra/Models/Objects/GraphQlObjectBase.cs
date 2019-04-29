namespace GQLCCG.Infra.Models.Objects
{
    public abstract class GraphQlObjectBase : GraphQlEntityBase
    {
        protected GraphQlObjectBase(string name, string description)
            : base(name, description)
        {
        }
    }
}