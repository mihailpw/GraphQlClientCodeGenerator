using System;
using System.Threading.Tasks;
using Generator.DotNetCore;
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
            var config = await Config.ReadFromAsync(options.Config);
            if (config == null)
            {
                Console.WriteLine("Config file not found.");
                return;
            }

            if (!config.Validate(out var configErrors))
            {
                Console.WriteLine("Wrong config file provided:");
                foreach (var configError in configErrors)
                {
                    Console.WriteLine(configError);
                }
                return;
            }

            var storage = new FromAssembliesGeneratorStore(new []
            {
                typeof(DotNetCoreGenerator).Assembly,
            });
            var schemaLoader = new FromUrlSchemaReader(config.SchemaUri, config.InnerLevelOfType);
            var writer = config.OutputToConsole
                ? (IGeneratorWriterFactory) new TextGeneratorWriterFactory(Console.Out)
                : new FileGeneratorWriterFactory();

            var processor = new GenerationProcessor(storage, schemaLoader, writer);
            await processor.ProcessAsync(config.Generator, config.CreateGeneratorContext());
        }
    }
}