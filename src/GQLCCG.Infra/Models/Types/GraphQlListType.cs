﻿namespace GQLCCG.Infra.Models.Types
{
    public class GraphQlListType : GraphQlTypeBase
    {
        public override GraphQlKind Kind => GraphQlKind.List;

        public GraphQlTypeBase OfType { get; set; }
    }
}