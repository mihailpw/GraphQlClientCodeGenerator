using System.Collections.Generic;
using GQLCCG.Infra.Models.Objects;

namespace GQLCCG.Infra.Models.Types
{
    public interface IGraphQlTypesHolder
    {
        IList<GraphQlTypeBase> PossibleTypes { get; set; }

        IList<GraphQlField> PossibleFields { get; set; }
    }
}