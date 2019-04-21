using System.IO;
using System.Threading.Tasks;
using GQLCCG.Infra;

namespace GQLCCG.Processor.GeneratorWriters
{
    public sealed class TextGeneratorWriter : IGeneratorWriter
    {
        private readonly TextWriter _textWriter;


        public TextGeneratorWriter(TextWriter textWriter)
        {
            _textWriter = textWriter;
        }


        public async Task WriteAsync(string type, string code)
        {
            await _textWriter.WriteLineAsync($"------ start '{type}' ------");
            await _textWriter.WriteLineAsync(code);
            await _textWriter.WriteLineAsync($"------  end '{type}'  ------");
        }
    }
}