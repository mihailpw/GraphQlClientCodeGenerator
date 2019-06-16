using CommandLine;

namespace GQLCCG
{
    internal sealed class ConsoleOptions
    {
        [Option('c', "config", Required = true, HelpText = "Config file path.")]
        public string Config { get; set; }
    }
}