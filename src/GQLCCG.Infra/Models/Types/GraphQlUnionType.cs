using System.Collections.Generic;
using GQLCCG.Infra.Models.Objects;

namespace GQLCCG.Infra.Models.Types
{
    public class GraphQlUnionType : GraphQlTypeBase, IGraphQlTypesHolder
    {
        public override GraphQlKind Kind => GraphQlKind.Union;

        public IList<GraphQlTypeBase> PossibleTypes { get; set; }

        public IList<GraphQlField> PossibleFields { get; set; }
    }
}