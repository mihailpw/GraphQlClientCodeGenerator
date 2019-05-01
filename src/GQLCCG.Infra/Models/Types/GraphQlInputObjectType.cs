using System.Collections.Generic;
using GQLCCG.Infra.Models.Objects;

namespace GQLCCG.Infra.Models.Types
{
    public class GraphQlInputObjectType : GraphQlTypeBase
    {
        public IList<GraphQlInputValue> InputFields { get; set; }
    }
}