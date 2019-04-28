using System.Threading.Tasks;
using GQLCCG.Infra;
using GQLCCG.Infra.Models;

namespace Generator.DotNetCore
{
    [Generator("dotnetcore")]
    public class DotNetCoreGenerator : IGenerator
    {
        public async Task GenerateAsync(GraphQlSchema schema, IGeneratorWriterFactory writerFactory, GeneratorContext context)
        {
            using (var writer = await writerFactory.CreateAsync("schema"))
            {
                var generatorWriter = new GeneratorWriter(writer);
                await generatorWriter.GenerateAsync(schema);
            }
        }
    }
}