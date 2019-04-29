using System.Collections.Generic;
using GQLCCG.Infra.Models.Types;

namespace GQLCCG.Infra.Models
{
    public class GraphQlSchema
    {
        public GraphQlObjectType QueryType { get; }

        public GraphQlObjectType MutationType { get; }

        public GraphQlObjectType SubscriptionType { get; }

        public IReadOnlyList<GraphQlTypeBase> Types { get; }


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