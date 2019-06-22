using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public string Name { get; set; } = "Generated.cs";

        public bool OutputToFolder { get; set; } = true;

        public string OutputFolderPath { get; set; } = "./";

        public bool OutputToConsole { get; set; } = false;

        public string Namespace { get; set; } = "GraphQlClient";

        public string MainClientFactoryClassName { get; set; } = "AppClientFactory";

        public IList<string> AdditionalClientUsing { get; set; } = new List<string>();

        public bool GenerateDocs { get; set; } = false;

        public bool GenerateInputObjectConstructor { get; set; } = false;

        public TypeNamingModel TypeNaming { get; set; } = new TypeNamingModel();


        public static async Task<Config> ReadFromAsync(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"Config file not found (path={path}).");
            }

            using (var stream = File.OpenRead(path))
            {
                return await JsonSerializer.ReadAsync<Config>(stream, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }
        }


        public bool Validate(out IReadOnlyList<string> errors)
        {
            IEnumerable<string> GetErrors()
            {
                if (string.IsNullOrEmpty(SchemaUri))
                {
                    yield return "Schema url not provided.";
                }
                else if (!Uri.TryCreate(SchemaUri, UriKind.Absolute, out _))
                {
                    yield return "Schema url is not valid.";
                }

                if (string.IsNullOrEmpty(Generator))
                {
                    yield return "Generator not specified.";
                }

                if (string.IsNullOrEmpty(Name))
                {
                    yield return "Name not specified.";
                }

                if (OutputToFolder)
                {
                    if (OutputFolderPath == null)
                    {
                        yield return "Output file path is not provided.";
                    }
                    else if (!Uri.TryCreate(OutputFolderPath, UriKind.RelativeOrAbsolute, out _))
                    {
                        yield return "Output file path is not valid.";
                    }
                }

                if (string.IsNullOrEmpty(Namespace))
                {
                    yield return "Namespace not provided.";
                }

                if (string.IsNullOrEmpty(MainClientFactoryClassName))
                {
                    yield return "Main client factory class name not provided.";
                }

                if (TypeNaming == null)
                {
                    yield return "Type naming not provided.";
                }
            }

            errors = GetErrors().ToList();
            return errors.Count == 0;
        }

        public GeneratorContext CreateGeneratorContext()
        {
            return new GeneratorContext
            {
                WriterName = Name,
                Namespace = Namespace,
                MainClientFactoryClassName = MainClientFactoryClassName,
                AdditionalClientUsing = AdditionalClientUsing ?? new List<string>(0),
                GenerateDocs = GenerateDocs,
                GenerateInputObjectConstructor = GenerateInputObjectConstructor,
                TypeNaming = TypeNaming,
            };
        }
    }
}
