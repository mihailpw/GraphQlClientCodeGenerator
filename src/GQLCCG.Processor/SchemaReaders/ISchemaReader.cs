using System.Threading.Tasks;
using GQLCCG.Infra.Models;

namespace GQLCCG.Processor.SchemaReaders
{
    public interface ISchemaReader
    {
        Task<GraphQlSchema> LoadSchemaDataAsync();
    }
}