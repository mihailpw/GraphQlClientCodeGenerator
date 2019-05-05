using System.Collections.Generic;
using GQLCCG.Infra.Models.Objects;

namespace GQLCCG.Infra.Models.Types
{
    public class GraphQlInputObjectType : GraphQlTypeBase
    {
        public override GraphQlKind Kind => GraphQlKind.InputObject;

        public IList<GraphQlInputValue> InputFields { get; set; }
    }
}