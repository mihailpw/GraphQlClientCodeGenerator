using System;

namespace GQLCCG.Infra
{
    [AttributeUsage(AttributeTargets.Class)]
    public class GeneratorAttribute : Attribute
    {
        public string Name { get; }


        public GeneratorAttribute(string name)
        {
            Name = name;
        }
    }
}