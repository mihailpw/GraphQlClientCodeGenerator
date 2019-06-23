using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Generator.DotNetCore;
using GQLCCG.Configuration;
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
            var config = !options.SetupConfig
                ? await Config.ReadFromAsync(options.Config)
                : await AutoConfigurator.CreateAndWriteConfig(options.Config);

            if (!config.Validate(out var configErrors))
            {
                var resultMessage = string.Join(
                    Environment.NewLine,
                    new[] { "wrong config file provided.", "Config file errors:" }.Concat(configErrors));
                throw new InvalidOperationException(resultMessage);
            }

            var storage = new FromAssembliesGeneratorStore(new []
            {
                typeof(DotNetCoreGenerator).Assembly,
            });
            var schemaLoader = new FromUrlSchemaReader(config.SchemaUri, config.InnerLevelOfType);
            var writer = CreateWriterFactory(config);

            var processor = new GenerationProcessor(storage, schemaLoader, writer);
            await processor.ProcessAsync(config.Generator, config.CreateGeneratorContext());
        }


        private static IGeneratorWriterFactory CreateWriterFactory(Config config)
        {
            var factories = new List<IGeneratorWriterFactory>();

            if (config.OutputToFolder)
            {
                factories.Add(new FileGeneratorWriterFactory(config.OutputFolderPath));
            }

            if (config.OutputToConsole)
            {
                factories.Add(new TextGeneratorWriterFactory(Console.Out));
            }

            switch (factories.Count)
            {
                case 0:
                    throw new InvalidOperationException("Output target not provided.");
                case 1:
                    return factories.First();
                default:
                    return new CompositeGeneratorWriterFactory(factories);
            }
        }
    }
}