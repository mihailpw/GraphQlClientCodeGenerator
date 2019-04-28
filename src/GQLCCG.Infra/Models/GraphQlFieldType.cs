using System.Collections.Generic;

namespace GQLCCG.Infra.Models
{
    public class GraphQlFieldType : GraphQlType
    {
        public List<GraphQlArgumentType> Args { get; set; }

        public GraphQlType Type { get; set; }

        public bool IsDeprecated { get; set; }

        public string DeprecationReason { get; set; }
    }
}