using GQLCCG.Infra;

namespace GQLCCG.Processor.GeneratorStores
{
    public interface IGeneratorStore
    {
        IGenerator GetGenerator(string name);
    }
}