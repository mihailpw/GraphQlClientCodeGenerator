using System;
using System.Collections.Generic;
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
            var infraTemplate = await _templateReader.ReadAsync("infra");
            var infraView = await _templateBuilder.BuildAsync(
                infraTemplate,
                new
                {
                    @namespace = context.Namespace,
                });

            var dtoPartial = await _templateReader.ReadAsync("dto");
            var builderPartial = await _templateReader.ReadAsync("builder");
            var partials = new Dictionary<string, string>
            {
                ["dto"] = dtoPartial,
                ["builder"] = builderPartial,
            };

            var clientTemplate = await _templateReader.ReadAsync("client");
            var clientView = await _templateBuilder.BuildAsync(
                clientTemplate,
                new
                {
                    @namespace = context.Namespace,
                    schema,
                },
                partials);

            await _writer.WriteAsync(string.Join(Environment.NewLine, infraView, clientView));
        }
    }
}