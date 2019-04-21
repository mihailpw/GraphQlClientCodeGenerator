using System.Threading.Tasks;
using GQLCCG.Infra;

namespace Generator.DotNetCore
{
    [Generator("dotnetcore")]
    public class DotNetCoreGenerator : IGenerator
    {
        public async Task GenerateAsync(SchemaModel schema, IGeneratorWriter writer)
        {
            await writer.WriteAsync("null-type", "null-code");
        }
    }
}