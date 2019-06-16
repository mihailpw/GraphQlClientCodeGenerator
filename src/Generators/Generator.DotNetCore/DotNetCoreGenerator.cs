using System.Threading.Tasks;
using Generator.DotNetCore.Infra;
using GQLCCG.Infra;
using GQLCCG.Infra.Models;

namespace Generator.DotNetCore
{
    [Generator("dotnetcore")]
    public class DotNetCoreGenerator : IGenerator
    {
        public async Task GenerateAsync(GraphQlSchema schema, IGeneratorWriterFactory writerFactory, GeneratorContext context)
        {
            using (var writer = await writerFactory.CreateAsync(context.WriterName))
            {
                var generatorWriter = new GeneratorWriter(
                    new ResourcesTemplateReader(),
                    new HandlebarsTemplateBuilder(context),
                    writer);

                await generatorWriter.GenerateAsync(schema, context);
            }
        }
    }
}