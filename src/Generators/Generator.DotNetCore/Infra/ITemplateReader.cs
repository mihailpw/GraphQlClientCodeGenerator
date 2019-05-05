using System.Threading.Tasks;

namespace Generator.DotNetCore.Infra
{
    public interface ITemplateReader
    {
        Task<string> ReadAsync(string name);
    }
}