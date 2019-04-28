using System.Collections.Generic;

namespace GQLCCG.Infra.Models
{
    public class GraphQlSchema
    {
        public GraphQlType QueryType { get; set; }

        public GraphQlType MutationType { get; set; }

        public GraphQlType SubscriptionType { get; set; }

        public List<GraphQlType> Types { get; set; }
    }
}