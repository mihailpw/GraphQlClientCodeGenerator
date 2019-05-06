﻿using GQLCCG.Infra.Models.Types;

namespace GQLCCG.Infra.Utils
{
    public static class GraphQlTypeExtensions
    {
        public static GraphQlTypeBase GetRealType(this GraphQlTypeBase type)
        {
            type.VerifyNotNull(nameof(type));

            switch (type)
            {
                case GraphQlListType listType:
                    return GetRealType(listType.OfType);
                case GraphQlNonNullType nonNullType:
                    return GetRealType(nonNullType.OfType);
                default:
                    return type;
            }
        }
    }
}