using System.Threading.Tasks;
using GQLCCG.Infra;
using GQLCCG.Infra.Models;

namespace Generator.DotNetCore
{
    internal class GeneratorWriter
    {
        private readonly IGeneratorWriter _writer;


        public GeneratorWriter(IGeneratorWriter writer)
        {
            _writer = writer;
        }


        public async Task GenerateAsync(GraphQlSchema schema)
        {
        }
    }
}