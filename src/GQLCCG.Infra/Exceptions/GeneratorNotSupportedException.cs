using System;

namespace GQLCCG.Infra.Exceptions
{
    public class GeneratorNotSupportedException : GeneratorExceptionBase
    {
        public GeneratorNotSupportedException(string message)
            : base(message)
        {
        }

        public GeneratorNotSupportedException(string message, Exception exception)
            : base(message, exception)
        {
        }
    }
}