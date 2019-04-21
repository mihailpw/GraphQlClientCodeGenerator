using System.Threading.Tasks;

namespace GQLCCG.Infra
{
    public interface IGenerator
    {
        Task GenerateAsync(SchemaModel schema, IGeneratorWriter writer);
    }
}