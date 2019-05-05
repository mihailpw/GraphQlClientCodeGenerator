using System.Threading.Tasks;
using Stubble.Core.Builders;

namespace Generator.DotNetCore.Infra
{
    public class MustacheTemplateBuilder : ITemplateBuilder
    {
        public async Task<string> BuildAsync(string template, object data)
        {
            var builder = new StubbleBuilder().Build();
            var view = await builder.RenderAsync(template, data);

            return view;
        }
    }
}