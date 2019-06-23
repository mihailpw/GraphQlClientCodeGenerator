using System;
using System.Reflection;
using GQLCCG.Utils;

namespace GQLCCG.Configuration.Configurators
{
    public class DefaultConfigurator : ConfiguratorBase
    {
        protected override void ConfigureInternal(object target, PropertyInfo property, ConfiguratorPropertyAttribute configuratorPropertyAttr)
        {
            var defaultValue = property.GetValue(target);

            Console.WriteLine(configuratorPropertyAttr.SetupQuestion);
            if (defaultValue != null)
            {
                Console.WriteLine($"(default={defaultValue}).");
            }

            var convertedValue = ConsoleUtils.ReadObject($"Can not be presented as {property.PropertyType.Name}. Please enter value again.", property.PropertyType);

            property.SetValue(target, convertedValue);
        }
    }
}