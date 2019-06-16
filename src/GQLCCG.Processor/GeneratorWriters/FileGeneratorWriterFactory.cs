using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using GQLCCG.Infra;

namespace GQLCCG.Processor.GeneratorWriters
{
    public class FileGeneratorWriterFactory : IGeneratorWriterFactory
    {
        private sealed class FileGeneratorWriter : IGeneratorWriter
        {
            private readonly TextWriter _textWriter;
            private readonly Action _onDisposedAction;


            public FileGeneratorWriter(TextWriter textWriter, Action onDisposedAction)
            {
                _textWriter = textWriter;
                _onDisposedAction = onDisposedAction;
            }


            public async Task WriteAsync(string code)
            {
                await _textWriter.WriteLineAsync(code);
            }

            public void Dispose()
            {
                _onDisposedAction();
            }
        }



        private readonly Dictionary<string, TextWriter> _textWriters = new Dictionary<string, TextWriter>();


        public async Task<IGeneratorWriter> CreateAsync(string name)
        {
            await Task.CompletedTask;

            void OnDispose()
            {
                if (_textWriters.ContainsKey(name))
                {
                    _textWriters[name].Dispose();
                    _textWriters.Remove(name);
                }
            }

            if (!_textWriters.ContainsKey(name))
            {
                var fileInfo = new FileInfo(name);
                _textWriters.Add(name, fileInfo.CreateText());
            }

            return new FileGeneratorWriter(_textWriters[name], OnDispose);
        }
    }
}