using System.Collections.Generic;
using GQLCCG.Infra.Models.Objects;

namespace GQLCCG.Infra.Models.Types
{
    public class GraphQlObjectType : GraphQlTypeBase
    {
        public IReadOnlyList<GraphQlField> Fields { get; }

        public IReadOnlyList<GraphQlTypeBase> PossibleTypes { get; internal set; }


        public GraphQlObjectType(
            string name,
            string description,
            IReadOnlyList<GraphQlField> fields,
            IReadOnlyList<GraphQlTypeBase> possibleTypes)
            : base(name, description)
        {
            Fields = fields;
            PossibleTypes = possibleTypes;
        }
    }
}