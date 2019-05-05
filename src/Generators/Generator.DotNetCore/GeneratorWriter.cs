using System.Threading.Tasks;
using Generator.DotNetCore.Infra;
using GQLCCG.Infra;
using GQLCCG.Infra.Models;

namespace Generator.DotNetCore
{
    internal class GeneratorWriter
    {
        private readonly ITemplateReader _templateReader;
        private readonly ITemplateBuilder _templateBuilder;
        private readonly IGeneratorWriter _writer;


        public GeneratorWriter(ITemplateReader templateReader, ITemplateBuilder templateBuilder, IGeneratorWriter writer)
        {
            _templateReader = templateReader;
            _templateBuilder = templateBuilder;
            _writer = writer;
        }


        public async Task GenerateAsync(GraphQlSchema schema, GeneratorContext context)
        {
            var infraTemplate = await _templateReader.ReadAsync("infra.mustache");
            var infraView = await _templateBuilder.BuildAsync(infraTemplate, new
            {
                @namespace = "GeneratedClient",
            });


        }
    }
}