using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using GQLCCG.Infra;

namespace GQLCCG.Processor.GeneratorWriters
{
    public sealed class TextGeneratorWriterFactory : IGeneratorWriterFactory
    {
        private sealed class TextGeneratorWriter : IGeneratorWriter
        {
            private readonly TextWriter _textWriter;
            private readonly string _type;
            private readonly Action _onDisposedAction;


            public TextGeneratorWriter(TextWriter textWriter, string type, Action onDisposedAction)
            {
                _textWriter = textWriter;
                _type = type;
                _onDisposedAction = onDisposedAction;
            }


            public async Task InitializeAsync()
            {
                await _textWriter.WriteLineAsync($"------ start '{_type}' ------");
            }

            public async Task WriteAsync(string code)
            {
                await _textWriter.WriteLineAsync(code);
            }

            public async void Dispose()
            {
                await _textWriter.WriteLineAsync($"------  end '{_type}'  ------");
                _onDisposedAction();
            }
        }



        private readonly Func<string, TextGeneratorWriter> _writerFactory;
        private readonly SemaphoreSlim _semaphore;


        public TextGeneratorWriterFactory(TextWriter textWriter)
        {
            _writerFactory = t => new TextGeneratorWriter(textWriter, t, () => _semaphore.Release());
            _semaphore = new SemaphoreSlim(1);
        }


        public async Task<IGeneratorWriter> CreateAsync(string type)
        {
            await _semaphore.WaitAsync();
            var writer = _writerFactory(type);
            await writer.InitializeAsync();

            return writer;
        }
    }
}