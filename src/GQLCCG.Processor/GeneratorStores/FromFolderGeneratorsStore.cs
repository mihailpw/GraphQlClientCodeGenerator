using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using GQLCCG.Infra;
using GQLCCG.Infra.Utils;

namespace GQLCCG.Processor.GeneratorStores
{
    public sealed class FromDirGeneratorStore : IGeneratorStore
    {
        private readonly Dictionary<string, IGenerator> _generators;


        public FromDirGeneratorStore(string dirPath)
        {
            dirPath.VerifyNotNull(nameof(dirPath));

            _generators = FindAllGenerators(dirPath);
        }


        public IGenerator GetGenerator(string name)
        {
            if (!_generators.ContainsKey(name))
            {
                throw new GeneratorNotFoundException(name, _generators.Keys);
            }

            return _generators[name];
        }


        private static Dictionary<string, IGenerator> FindAllGenerators(string dirPath)
        {
            return Directory
                .GetFiles(dirPath, "generator.*.dll")
                .Select(Assembly.LoadFrom)
                .SelectMany(a => a.DefinedTypes)
                .Where(t =>
                    t.CustomAttributes.Count(a => a.AttributeType == typeof(GeneratorAttribute)) == 1
                    && t.ImplementedInterfaces.Contains(typeof(IGenerator)))
                .Select(t => new
                {
                    name = t.GetCustomAttribute<GeneratorAttribute>().Name,
                    generator = (IGenerator) Activator.CreateInstance(t)
                })
                .ToDictionary(g => g.name, g => g.generator);
        }
    }
}