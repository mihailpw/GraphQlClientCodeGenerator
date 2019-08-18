using System;
using System.Linq;
using System.Reflection;
using GQLCCG.Infra.Exceptions;

namespace GQLCCG.Configuration.Configurators
{
    public class ArrayConfigurator : ConfiguratorBase
    {
        protected override void ConfigureInternal(object target, PropertyInfo property, ConfiguratorPropertyAttribute configuratorPropertyAttr)
        {
            if (!property.PropertyType.IsArray)
            {
                throw new GeneratorInvalidOperationException($"Property {property.Name} (type={property.PropertyType.Name}) is not array.");
            }

            var defaultValue = property.GetValue(target);

            Console.WriteLine(configuratorPropertyAttr.SetupQuestion);
            if (defaultValue != null)
            {
                Console.WriteLine($"(default={defaultValue}).");
            }

            var values = Console.ReadLine()?.Split(",") ?? Enumerable.Empty<string>();
            var itemType = property.PropertyType.GetElementType();
            var convertedValues = values.Select(v => Convert.ChangeType(v, itemType)).ToArray();

            var convertedArray = Array.CreateInstance(itemType, convertedValues.Length);
            Array.Copy(convertedValues, convertedArray, convertedValues.Length);

            property.SetValue(target, convertedArray);
        }
    }
}