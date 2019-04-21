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
        private readonly ISchemaReader _schemaReader;
        private readonly IGeneratorWriter _generatorWriter;


        public GenerationProcessor(
            IGeneratorStore generatorStore,
            ISchemaReader schemaReader,
            IGeneratorWriter generatorWriter)
        {
            _generatorStore = generatorStore.VerifyNotNull(nameof(generatorStore));
            _schemaReader = schemaReader.VerifyNotNull(nameof(schemaReader));
            _generatorWriter = generatorWriter.VerifyNotNull(nameof(generatorWriter));
        }


        public async Task ProcessAsync(string generatorName)
        {
            var generator = _generatorStore.GetGenerator(generatorName);
            var schema = await _schemaReader.ReadSchemaAsync();
            await generator.GenerateAsync(schema, _generatorWriter);
        }
    }
}