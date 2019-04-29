using System.Collections.Generic;
using GQLCCG.Infra.Models.Types;

namespace GQLCCG.Infra.Models.Objects
{
    public class GraphQlField : GraphQlDeprecatableObjectBase
    {
        public GraphQlTypeBase Type { get; internal set; }

        public IReadOnlyList<GraphQlInputValue> Args { get; }


        public GraphQlField(
            string name,
            string description,
            bool isDeprecated,
            string deprecationReason,
            GraphQlTypeBase type,
            IReadOnlyList<GraphQlInputValue> args)
            : base(name, description, isDeprecated, deprecationReason)
        {
            Type = type;
            Args = args;
        }
    }
}