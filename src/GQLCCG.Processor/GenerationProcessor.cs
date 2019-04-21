using System.Threading.Tasks;
using GQLCCG.Infra;
using GQLCCG.Infra.Utils;
using GQLCCG.Processor.GeneratorStores;
using GQLCCG.Processor.SchemaReaders;

namespace GQLCCG.Processor
{
    public class GenerationProcessor
    {
        private readonly IGeneratorStore _generatorStore;
        private readonly ISchemaDataLoader _schemaDataLoader;
        private readonly ISchemaParser _schemaParser;
        private readonly IGeneratorWriter _generatorWriter;


        public GenerationProcessor(
            IGeneratorStore generatorStore,
            ISchemaDataLoader schemaDataLoader,
            ISchemaParser schemaParser,
            IGeneratorWriter generatorWriter)
        {
            _generatorStore = generatorStore.VerifyNotNull(nameof(generatorStore));
            _schemaDataLoader = schemaDataLoader.VerifyNotNull(nameof(schemaDataLoader));
            _schemaParser = schemaParser.VerifyNotNull(nameof(schemaParser));
            _generatorWriter = generatorWriter.VerifyNotNull(nameof(generatorWriter));
        }


        public async Task ProcessAsync(string generatorName)
        {
            var generator = _generatorStore.GetGenerator(generatorName);
            var schemaData = await _schemaDataLoader.LoadSchemaDataAsync();
            var schema = _schemaParser.Parse(schemaData);
            await generator.GenerateAsync(schema, _generatorWriter);
        }
    }
}