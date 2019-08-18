using System;
using GQLCCG.Infra.Exceptions;

namespace GQLCCG.Infra.Utils
{
    public static class Extensions
    {
        public static T VerifyNotNull<T>(this T source, string paramName) where T : class
        {
            return source ?? throw new GeneratorArgumentNullException(paramName);
        }
    }
}