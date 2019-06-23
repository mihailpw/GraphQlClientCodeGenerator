using System;
using System.Reflection;
using GQLCCG.Utils;

namespace GQLCCG.Configuration.Configurators
{
    public abstract class ConfiguratorBase
    {
        public void Configure(object target, PropertyInfo property, ConfiguratorPropertyAttribute configuratorProperty)
        {
            var shouldConfigure = true;

            if (configuratorProperty is OptionalConfiguratorPropertyAttribute optionalConfiguratorProperty)
            {
                shouldConfigure = ConsoleUtils.ReadYesNoKey(optionalConfiguratorProperty.OptionalQuestion);
                Console.WriteLine();
            }

            if (shouldConfigure)
            {
                ConfigureInternal(target, property, configuratorProperty);
            }
        }


        protected abstract void ConfigureInternal(
            object target,
            PropertyInfo property,
            ConfiguratorPropertyAttribute configuratorPropertyAttr);
    }
}