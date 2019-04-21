using System.Threading.Tasks;

namespace GQLCCG.Infra
{
    public interface IGeneratorWriterFactory
    {
        Task<IGeneratorWriter> CreateAsync(string type);
    }
}