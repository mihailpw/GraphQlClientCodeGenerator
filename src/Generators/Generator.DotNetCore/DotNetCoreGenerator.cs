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
            using (var writer = await writerFactory.CreateAsync("null-type-1"))
            {
                await writer.WriteAsync("null-code-1-1");
                await writer.WriteAsync("null-code-1-2");
            }

            using (var writer = await writerFactory.CreateAsync("null-type-2"))
            {
                await writer.WriteAsync("null-code-2-1");
                await writer.WriteAsync("null-code-2-2");
                await writer.WriteAsync("null-code-2-3");
            }
        }
    }
}