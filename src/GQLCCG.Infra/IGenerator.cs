using System.Threading.Tasks;
using GQLCCG.Infra.Models;

namespace GQLCCG.Infra
{
    public interface IGenerator
    {
        Task GenerateAsync(GraphQlSchema schema, IGeneratorWriterFactory writerFactory, GeneratorContext context);
    }
}