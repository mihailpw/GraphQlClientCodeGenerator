﻿namespace GQLCCG.Infra.Models.Types
{
    public enum GraphQlKind
    {
        Enum = 1,
        InputObject,
        Interface,
        List,
        NonNull,
        Object,
        Scalar,
        Union,
    }
}