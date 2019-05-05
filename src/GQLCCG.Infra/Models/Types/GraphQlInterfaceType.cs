using System.Collections.Generic;
using GQLCCG.Infra.Models.Objects;

namespace GQLCCG.Infra.Models.Types
{
    public class GraphQlInterfaceType : GraphQlTypeBase
    {
        public override GraphQlKind Kind => GraphQlKind.Interface;

        public IList<GraphQlField> Fields { get; set; }

        public IList<GraphQlTypeBase> PossibleTypes { get; set; }
    }
}