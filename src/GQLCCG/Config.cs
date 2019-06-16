using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using GQLCCG.Infra;
using GQLCCG.Infra.Utils;

namespace GQLCCG
{
    internal class Config
    {
        public string SchemaUri { get; set; }

        public string Generator { get; set; }

        public int InnerLevelOfType { get; set; } = 4;

        public bool OutputToConsole { get; set; } = false;

        public string OutputFilePath { get; set; }

        public string Namespace { get; set; } = "GraphQlClient";

        public string MainClientFactoryClassName { get; set; } = "AppClientFactory";

        public IList<string> AdditionalClientUsing { get; set; } = new List<string>();

        public bool GenerateDocs { get; set; } = false;

        public bool GenerateInputObjectConstructor { get; set; } = false;

        public TypeNames Names { get; set; } = new TypeNames();


        public static async Task<Config> ReadFromAsync(string path)
        {
            if (!File.Exists(path))
            {
                return null;
            }

            using (var stream = File.OpenRead(path))
            {
                return await JsonSerializer.ReadAsync<Config>(stream, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }
        }


        public bool Validate(out IReadOnlyList<string> errors)
        {
            var outErrors = new List<string>();

            if (string.IsNullOrEmpty(SchemaUri))
            {
                outErrors.Add("Schema url not provided.");
            } else if (!Uri.TryCreate(SchemaUri, UriKind.Absolute, out _))
            {
                outErrors.Add("Schema url is not valid.");
            }

            if (string.IsNullOrEmpty(Generator))
            {
                outErrors.Add("Generator not specified.");
            }

            if (!OutputToConsole)
            {
                if (string.IsNullOrEmpty(OutputFilePath))
                {
                    outErrors.Add("Output file path not specified.");
                }
                else if (!Uri.TryCreate(OutputFilePath, UriKind.RelativeOrAbsolute, out _))
                {
                    outErrors.Add("Output file path is not valid.");
                }
            }

            if (string.IsNullOrEmpty(Namespace))
            {
                outErrors.Add("Namespace not provided.");
            }

            if (string.IsNullOrEmpty(MainClientFactoryClassName))
            {
                outErrors.Add("Main client factory class name not provided.");
            }

            if (Names == null)
            {
                outErrors.Add("Names not provided.");
            }

            errors = outErrors;
            return outErrors.Count == 0;
        }

        public GeneratorContext CreateGeneratorContext()
        {
            return new GeneratorContext
            {
                WriterName = OutputFilePath,
                Namespace = Namespace,
                MainClientFactoryClassName = MainClientFactoryClassName,
                AdditionalClientUsing = AdditionalClientUsing ?? new List<string>(0),
                GenerateDocs = GenerateDocs,
                GenerateInputObjectConstructor = GenerateInputObjectConstructor,
                Names = Names,
            };
        }
    }
}
