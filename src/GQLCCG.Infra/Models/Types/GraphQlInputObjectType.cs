using System.Collections.Generic;
using GQLCCG.Infra.Models.Objects;

namespace GQLCCG.Infra.Models.Types
{
    public class GraphQlInputObjectType : GraphQlTypeBase
    {
        public IReadOnlyList<GraphQlInputValue> InputFields { get; }


        public GraphQlInputObjectType(
            string name,
            string description,
            IReadOnlyList<GraphQlInputValue> inputFields)
            : base(name, description)
        {
            InputFields = inputFields;
        }
    }
}