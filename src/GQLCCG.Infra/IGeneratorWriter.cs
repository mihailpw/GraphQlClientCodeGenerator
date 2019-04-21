using System;
using System.Threading.Tasks;

namespace GQLCCG.Infra
{
    public interface IGeneratorWriter : IDisposable
    {
        Task WriteAsync(string code);
    }
}