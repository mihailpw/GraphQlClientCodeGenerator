namespace GQLCCG.Infra.Exceptions
{
    public class GeneratorInvalidOperationException : GeneratorExceptionBase
    {
        public GeneratorInvalidOperationException(string message)
            : base(message)
        {
        }
    }
}