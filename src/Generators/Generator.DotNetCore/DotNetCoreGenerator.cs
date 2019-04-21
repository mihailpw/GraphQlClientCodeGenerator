using System.Threading.Tasks;
using GQLCCG.Infra;
using GraphQLParser.AST;

namespace Generator.DotNetCore
{
    [Generator("dotnetcore")]
    public class DotNetCoreGenerator : IGenerator
    {
        public async Task GenerateAsync(GraphQLDocument schema, IGeneratorWriter writer)
        {
            await writer.WriteAsync("null-type", "null-code");
        }
    }
}