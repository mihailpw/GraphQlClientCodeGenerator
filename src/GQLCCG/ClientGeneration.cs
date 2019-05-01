using System;
using System.IO;
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
            var currentDir = Directory.GetCurrentDirectory();

            var storage = new FromDirGeneratorStore(currentDir);
            var schemaLoader = new FromUrlSchemaReader(options.SchemaUri, options.InnerLevelOfType);
            var writer = new TextGeneratorWriterFactory(Console.Out);

            var processor = new GenerationProcessor(storage, schemaLoader, writer);
            var context = new GeneratorContext();
            await processor.ProcessAsync(options.GeneratorName, context);
        }
    }
}