using System.IO;
using CommandLine;

namespace GQLCCG
{
    internal sealed class ConsoleOptions
    {
        [Option('n', "name", Required = true, HelpText = "Generator name.")]
        public string GeneratorName { get; set; }

        [Option("plugins-dir", Required = false, HelpText = "Plugins directory path.")]
        public string PluginsDirPath { get; set; } = Directory.GetCurrentDirectory();
    }
}