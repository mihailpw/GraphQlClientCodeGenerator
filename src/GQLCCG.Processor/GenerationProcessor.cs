﻿using System.Threading.Tasks;
using GQLCCG.Infra;
using GQLCCG.Infra.Utils;
using GQLCCG.Processor.GeneratorStores;
using GQLCCG.Processor.SchemaReaders;

namespace GQLCCG.Processor
{
    public class GenerationProcessor
    {
        private readonly IGeneratorStore _generatorStore;
        private readonly ISchemaReader _schemaReader;
        private readonly IGeneratorWriterFactory _generatorWriterFactory;


        public GenerationProcessor(
            IGeneratorStore generatorStore,
            ISchemaReader schemaReader,
            IGeneratorWriterFactory generatorWriterFactory)
        {
            _generatorStore = generatorStore.VerifyNotNull(nameof(generatorStore));
            _schemaReader = schemaReader.VerifyNotNull(nameof(schemaReader));
            _generatorWriterFactory = generatorWriterFactory.VerifyNotNull(nameof(generatorWriterFactory));
        }


        public async Task ProcessAsync(string generatorName, GeneratorContext context)
        {
            var generator = _generatorStore.GetGenerator(generatorName);
            var schema = await _schemaReader.LoadSchemaDataAsync();
            await generator.GenerateAsync(schema, _generatorWriterFactory, context);
        }
    }
}