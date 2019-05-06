using GQLCCG.Infra.Models.Types;

namespace GQLCCG.Infra.Models.Objects
{
    public class GraphQlInputValue : GraphQlObjectBase
    {
        public GraphQlTypeBase Type { get; set; }

        public string DefaultValue { get; set; }
    }
}