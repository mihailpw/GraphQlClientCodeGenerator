using System.Threading.Tasks;
using GraphQLParser.AST;

namespace GQLCCG.Infra
{
    public interface IGenerator
    {
        Task GenerateAsync(GraphQLDocument schema, IGeneratorWriterFactory writerFactory);
    }
}