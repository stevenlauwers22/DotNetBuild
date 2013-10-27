using System;
using System.Collections.Generic;

namespace DotNetBuild.Core
{
    public interface IConfigurationRegistry
    {
        IEnumerable<ConfigurationRegistration> Registrations { get; }
    }

    public abstract class ConfigurationRegistry : IConfigurationRegistry
    {
        private readonly ICollection<ConfigurationRegistration> _registrations;

        protected ConfigurationRegistry()
        {
            _registrations = new List<ConfigurationRegistration>();
        }

        public IEnumerable<ConfigurationRegistration> Registrations
        {
            get { return _registrations; }
        }

        protected void Add(IConfigurationSettings configurationSettings, Func<string, bool> useIf)
        {
            _registrations.Add(new ConfigurationRegistration(configurationSettings, useIf));
        }
    }
}