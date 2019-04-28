using System;
using System.Threading.Tasks;
using GQLCCG.Infra;
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
            var schemaLoader = new FromUrlSchemaLoader(options.SchemaUri);
            var writer = new TextGeneratorWriterFactory(Console.Out);

            var processor = new GenerationProcessor(storage, schemaLoader, writer);
            var context = new GeneratorContext();
            await processor.ProcessAsync(options.GeneratorName, context);
        }
    }
}