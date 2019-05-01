using System.Collections.Generic;

namespace GQLCCG.Infra.Models.Types
{
    public class GraphQlUnionType : GraphQlTypeBase
    {
        public IList<GraphQlTypeBase> PossibleTypes { get; set; }
    }
}