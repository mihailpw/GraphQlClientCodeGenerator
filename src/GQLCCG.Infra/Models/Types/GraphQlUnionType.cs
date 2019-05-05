using System.Collections.Generic;

namespace GQLCCG.Infra.Models.Types
{
    public class GraphQlUnionType : GraphQlTypeBase
    {
        public override GraphQlKind Kind => GraphQlKind.Union;

        public IList<GraphQlTypeBase> PossibleTypes { get; set; }
    }
}