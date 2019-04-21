using System;
using System.Collections.Generic;

namespace GQLCCG.Processor.GeneratorStores
{
    public class GeneratorNotFoundException : Exception
    {
        public GeneratorNotFoundException(string generatorName, IEnumerable<string> availableGenerators)
            : base($"Generator '{generatorName}' not found. Available generators: {string.Join(", ", availableGenerators)}")
        {
        }
    }
}