using GQLCCG.Infra.Models.Types;

namespace GQLCCG.Infra.Models.Objects
{
    public abstract class GraphQlObjectBase : GraphQlEntityBase
    {
        public GraphQlTypeBase OwnerType { get; set; }
    }
}