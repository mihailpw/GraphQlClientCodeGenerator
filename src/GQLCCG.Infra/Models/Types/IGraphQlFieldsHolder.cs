using System.Collections.Generic;
using GQLCCG.Infra.Models.Objects;

namespace GQLCCG.Infra.Models.Types
{
    public interface IGraphQlFieldsHolder
    {
        IList<GraphQlField> Fields { get; set; }
    }
}