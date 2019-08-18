using System.Collections.Generic;

namespace GQLCCG.Infra.Exceptions
{
    public class GeneratorNotFoundException : GeneratorExceptionBase
    {
        public GeneratorNotFoundException(string generatorName, IEnumerable<string> availableGenerators)
            : base($"Generator '{generatorName}' not found. Available generators: {string.Join(", ", availableGenerators)}")
        {
        }
    }
}