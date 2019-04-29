using System.Collections.Generic;

namespace GQLCCG.Infra.Models.Types
{
    public class GraphQlUnionType : GraphQlTypeBase
    {
        public IReadOnlyList<GraphQlTypeBase> PossibleTypes { get; internal set; }


        public GraphQlUnionType(
            string name,
            string description,
            IReadOnlyList<GraphQlTypeBase> possibleTypes)
            : base(name, description)
        {
            PossibleTypes = possibleTypes;
        }
    }
}