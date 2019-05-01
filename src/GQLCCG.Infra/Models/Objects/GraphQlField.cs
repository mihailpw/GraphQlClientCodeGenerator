using System.Collections.Generic;
using GQLCCG.Infra.Models.Types;

namespace GQLCCG.Infra.Models.Objects
{
    public class GraphQlField : GraphQlDeprecatableObjectBase
    {
        public GraphQlTypeBase Type { get; set; }

        public IList<GraphQlInputValue> Args { get; set; }
    }
}