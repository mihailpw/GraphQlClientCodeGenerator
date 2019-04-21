using System.IO;
using CommandLine;

namespace GQLCCG
{
    internal sealed class ConsoleOptions
    {
        [Option('n', "name", Required = true, HelpText = "Generator name.")]
        public string GeneratorName { get; set; }

        [Option('u', "schema-uri", Required = true, HelpText = "Schema uri.")]
        public string SchemaUri { get; set; } = Directory.GetCurrentDirectory();

        [Option("plugins-dir", Required = false, HelpText = "Plugins directory path.")]
        public string PluginsDirPath { get; set; } = Directory.GetCurrentDirectory();
    }
}