using System;
using System.Reflection;
using GQLCCG.Utils;

namespace GQLCCG.Configuration.Configurators
{
    public class BooleanConfigurator : ConfiguratorBase
    {
        protected override void ConfigureInternal(object target, PropertyInfo property, ConfiguratorPropertyAttribute configuratorPropertyAttr)
        {
            Console.WriteLine($"{configuratorPropertyAttr.SetupQuestion} (y/n)");
            Console.Write($"(default={property.GetValue(target)}): ");
            property.SetValue(target, ConsoleUtils.ReadYesNoKey());
            Console.WriteLine();
        }
    }
}