using System.Collections.Generic;
using GQLCCG.Infra.Models.Types;

namespace GQLCCG.Infra.Models
{
    public class GraphQlSchema
    {
        public GraphQlObjectType QueryType { get; set; }

        public GraphQlObjectType MutationType { get; set; }

        public GraphQlObjectType SubscriptionType { get; set; }

        public IReadOnlyList<GraphQlTypeBase> Types { get; set; }


        public GraphQlSchema(
            GraphQlObjectType queryType,
            GraphQlObjectType mutationType,
            GraphQlObjectType subscriptionType,
            IReadOnlyList<GraphQlTypeBase> types)
        {
            QueryType = queryType;
            MutationType = mutationType;
            SubscriptionType = subscriptionType;
            Types = types;
        }
    }
}