using System.Threading.Tasks;

namespace GQLCCG.Infra
{
    public interface IGeneratorWriter
    {
        Task WriteAsync(string type, string code);
    }
}