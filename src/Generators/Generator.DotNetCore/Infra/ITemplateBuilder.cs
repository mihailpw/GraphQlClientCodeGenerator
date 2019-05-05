using System.Threading.Tasks;

namespace Generator.DotNetCore.Infra
{
    public interface ITemplateBuilder
    {
        Task<string> BuildAsync(string template, object data);
    }
}