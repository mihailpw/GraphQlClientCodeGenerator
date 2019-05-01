namespace GQLCCG.Infra.Models.Objects
{
    public abstract class GraphQlDeprecatableObjectBase : GraphQlObjectBase
    {
        public bool IsDeprecated { get; set; }

        public string DeprecationReason { get; set; }
    }
}