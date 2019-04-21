using System.Threading.Tasks;
using GQLCCG.Infra;

namespace GQLCCG.Processor.SchemaReaders
{
    public interface ISchemaReader
    {
        Task<SchemaModel> ReadSchemaAsync();
    }
}