using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using GQLCCG.Infra.Exceptions;

namespace Generator.DotNetCore.Infra
{
    public class ResourcesTemplateReader : ITemplateReader
    {
        private const string TemplatesPathFormat = "Generator.DotNetCore.Templates.{0}.handlebars";

        private readonly Assembly _assembly;


        public ResourcesTemplateReader()
        {
            _assembly = Assembly.GetAssembly(typeof(DotNetCoreGenerator));
        }


        public async Task<string> ReadAsync(string name)
        {
            var path = string.Format(TemplatesPathFormat, name);

            using (var stream = _assembly.GetManifestResourceStream(path))
            {
                if (stream == null)
                {
                    throw new GeneratorInvalidOperationException(
                        $"Resource '{path}' not found in assembly {_assembly.FullName}");
                }

                using (var reader = new StreamReader(stream))
                {
                    return await reader.ReadToEndAsync();
                }
            }
        }
    }
}