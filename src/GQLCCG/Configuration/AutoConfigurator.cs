using System;
using System.IO;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using GQLCCG.Configuration.Configurators;

namespace GQLCCG.Configuration
{
    internal static class AutoConfigurator
    {
        public static async Task<Config> CreateAndWriteConfig(string path)
        {
            var config = CreateConfig();

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            using (var stream = File.OpenWrite(path: path))
            {
                await JsonSerializer.WriteAsync(config, stream, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true,
                });
            }

            return config;
        }


        private static Config CreateConfig()
        {
            var config = new Config();

            foreach (var property in config.GetType().GetProperties())
            {
                var configuratorProperty = property.GetCustomAttribute<ConfiguratorPropertyAttribute>();
                if (configuratorProperty == null)
                {
                    continue;
                }

                var configurator = (ConfiguratorBase) Activator.CreateInstance(configuratorProperty.ConfiguratorType);
                configurator.Configure(config, property, configuratorProperty);
            }

            return config;
        }
    }
}