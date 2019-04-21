using System;
using System.Threading.Tasks;
using GQLCCG.Processor;
using GQLCCG.Processor.GeneratorStores;
using GQLCCG.Processor.GeneratorWriters;
using GQLCCG.Processor.SchemaReaders;

namespace GQLCCG
{
    internal static class ClientGeneration
    {
        public static async Task GenerateClientAsync(ConsoleOptions options)
        {
            var storage = new FromDirGeneratorStore(options.PluginsDirPath);
            var schemaReader = new NullSchemaReader();
            var writer = new TextGeneratorWriter(Console.Out);

            var processor = new GenerationProcessor(storage, schemaReader, writer);
            await processor.ProcessAsync(options.GeneratorName);
        }
    }
}