using System.Collections.Generic;
using GQLCCG.Infra.Models.Objects;

namespace GQLCCG.Infra.Models.Types
{
    public class GraphQlEnumType : GraphQlTypeBase
    {
        public IReadOnlyList<GraphQlEnumValue> EnumValues { get; }


        public GraphQlEnumType(
            string name,
            string description,
            IReadOnlyList<GraphQlEnumValue> enumValues)
            : base(name, description)
        {
            EnumValues = enumValues;
        }
    }
}