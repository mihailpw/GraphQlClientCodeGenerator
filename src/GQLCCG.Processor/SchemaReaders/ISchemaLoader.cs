using System.Threading.Tasks;
using GQLCCG.Infra.Models;

namespace GQLCCG.Processor.SchemaReaders
{
    public interface ISchemaLoader
    {
        Task<GraphQlSchema> LoadSchemaDataAsync();
    }
}