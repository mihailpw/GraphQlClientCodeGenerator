﻿using System.Collections.Generic;
using GQLCCG.Infra.Models.Objects;

namespace GQLCCG.Infra.Models.Types
{
    public class GraphQlEnumType : GraphQlTypeBase
    {
        public IList<GraphQlEnumValue> EnumValues { get; set; }
    }
}