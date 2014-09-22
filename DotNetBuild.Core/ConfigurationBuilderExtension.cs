using System;

namespace DotNetBuild.Core
{
    public static class ConfigurationBuilderExtension
    {
        public static IConfigurationBuilder Configure(this String name)
        {
            var configurationRegistry = GetConfigurationRegistry();
            return new ConfigurationBuilder(configurationRegistry, name);
        }

        public static void Configure(this String name, IConfigurationSettings configurationSettings)
        {
            var configurationRegistry = GetConfigurationRegistry();
            configurationRegistry.Add(name, configurationSettings);
        }

        private static Func<IConfigurationRegistry> _resolveConfigurationRegistry;
        public static void ResolveConfigurationRegistry(Func<IConfigurationRegistry> resolveConfigurationRegistry)
        {
            _resolveConfigurationRegistry = resolveConfigurationRegistry;
        }

        private static IConfigurationRegistry GetConfigurationRegistry()
        {
            if (_resolveConfigurationRegistry == null)
                throw new InvalidOperationException("DotNetBuild hasn't been configured yet");

            var configurationRegistry = _resolveConfigurationRegistry();
            if (configurationRegistry == null)
                throw new InvalidOperationException("DotNetBuild hasn't been configured yet");

            return configurationRegistry;
        }
    }
}