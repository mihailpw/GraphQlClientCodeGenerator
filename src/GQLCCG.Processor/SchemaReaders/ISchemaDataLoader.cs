using System.Threading.Tasks;

namespace GQLCCG.Processor.SchemaReaders
{
    public interface ISchemaDataLoader
    {
        Task<string> LoadSchemaDataAsync();
    }
}