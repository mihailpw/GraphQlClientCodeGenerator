namespace GQLCCG.Infra.Models.Objects
{
    public abstract class GraphQlDeprecatableObjectBase : GraphQlObjectBase
    {
        public bool IsDeprecated { get; }

        public string DeprecationReason { get; }


        protected GraphQlDeprecatableObjectBase(
            string name,
            string description,
            bool isDeprecated,
            string deprecationReason)
            : base(name, description)
        {
            IsDeprecated = isDeprecated;
            DeprecationReason = deprecationReason;
        }
    }
}