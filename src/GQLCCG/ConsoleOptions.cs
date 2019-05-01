using CommandLine;

namespace GQLCCG
{
    internal sealed class ConsoleOptions
    {
        [Option('n', "name", Required = true, HelpText = "Generator name.")]
        public string GeneratorName { get; set; }

        [Option('u', "schema-uri", Required = true, HelpText = "Schema uri.")]
        public string SchemaUri { get; set; }

        [Option("inner-level-oftype", Required = false, HelpText = "Inner level of search in type ref.", Default = 4)]
        public int InnerLevelOfType { get; set; }
    }
}