using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using GQLCCG.Infra;
using GQLCCG.Infra.Utils;

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



        private readonly string _folder;
        private readonly Dictionary<string, TextWriter> _textWriters = new Dictionary<string, TextWriter>();


        public FileGeneratorWriterFactory(string folder)
        {
            _folder = folder.VerifyNotNull(nameof(folder));
        }


        public async Task<IGeneratorWriter> CreateAsync(string name)
        {
            await Task.CompletedTask;

            var path = Path.Combine(_folder, name);

            void OnDispose()
            {
                if (_textWriters.ContainsKey(path))
                {
                    _textWriters[path].Dispose();
                    _textWriters.Remove(path);
                }
            }

            if (!_textWriters.ContainsKey(path))
            {
                var fileInfo = new FileInfo(path);
                _textWriters.Add(path, fileInfo.CreateText());
            }

            return new FileGeneratorWriter(_textWriters[path], OnDispose);
        }
    }
}