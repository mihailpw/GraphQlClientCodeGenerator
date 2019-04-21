using System.Threading.Tasks;
using GQLCCG.Infra;
using GraphQLParser.AST;

namespace Generator.DotNetCore
{
    [Generator("dotnetcore")]
    public class DotNetCoreGenerator : IGenerator
    {
        public async Task GenerateAsync(GraphQLDocument schema, IGeneratorWriterFactory writerFactory)
        {
            using (var writer = await writerFactory.CreateAsync("schema"))
            {
                var generatorWriter = new GeneratorWriter(writer);
                await generatorWriter.GenerateAsync(schema.Definitions);
            }
        }
    }
}