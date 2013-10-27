using System.Linq;
using DotNetBuild.Core;
using DotNetBuild.Runner.Infrastructure.Exceptions;

namespace DotNetBuild.Runner.Infrastructure
{
    public interface IConfigurationSelector
    {
        IConfigurationSettings Select(string configuration, IConfigurationRegistry configurationRegistry);
    }

    public class ConfigurationSelector 
        : IConfigurationSelector
    {
        public IConfigurationSettings Select(string configuration, IConfigurationRegistry configurationRegistry)
        {
            if (configurationRegistry == null)
                return null;

            var registrations = configurationRegistry.Registrations;
            if (registrations == null)
                return null;

            var matchingConfigurationSettings = registrations.Where(cr => cr.UseIf(configuration)).Select(cr => cr.Configuration).ToList();
            if (matchingConfigurationSettings.Count > 1)
                throw new UnableToDetermineCorrectConfigurationSettingsException(matchingConfigurationSettings);

            var configurationRegistration = matchingConfigurationSettings.SingleOrDefault();
            return configurationRegistration;
        }
    }
}