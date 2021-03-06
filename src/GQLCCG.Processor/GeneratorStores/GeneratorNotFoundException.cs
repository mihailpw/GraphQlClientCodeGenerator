﻿using System.Collections.Generic;
using GQLCCG.Infra.Exceptions;

namespace GQLCCG.Processor.GeneratorStores
{
    public class GeneratorNotFoundException : GeneratorExceptionBase
    {
        public GeneratorNotFoundException(string generatorName, IEnumerable<string> availableGenerators)
            : base($"Generator '{generatorName}' not found. Available generators: {string.Join(", ", availableGenerators)}")
        {
        }
    }
}