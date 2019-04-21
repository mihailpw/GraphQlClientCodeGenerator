using System.Threading.Tasks;
using GQLCCG.Infra;

namespace GQLCCG.Processor.SchemaReaders
{
    public sealed class NullSchemaReader : ISchemaReader
    {
        public Task<SchemaModel> ReadSchemaAsync()
        {
            return Task.FromResult(new SchemaModel());
        }
    }
}