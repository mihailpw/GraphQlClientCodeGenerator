using System.Collections.Generic;
using GQLCCG.Infra.Models.Objects;

namespace GQLCCG.Infra.Models.Types
{
    public class GraphQlInterfaceType : GraphQlTypeBase
    {
        public IReadOnlyList<GraphQlField> Fields { get; }

        public IReadOnlyList<GraphQlTypeBase> PossibleTypes { get; internal set; }


        public GraphQlInterfaceType(
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