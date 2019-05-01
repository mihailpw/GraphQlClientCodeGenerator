using GQLCCG.Infra.Models.Types;

namespace GQLCCG.Infra.Models.Objects
{
    public class GraphQlInputValue : GraphQlEntityBase
    {
        public GraphQlTypeBase Type { get; set; }

        public string DefaultValue { get; set; }
    }
}