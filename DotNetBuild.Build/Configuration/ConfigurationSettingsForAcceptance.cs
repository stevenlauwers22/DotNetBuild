using DotNetBuild.Core;

namespace DotNetBuild.Build.Configuration
{
    public class ConfigurationSettingsForAcceptance : ConfigurationSettings
    {
        public ConfigurationSettingsForAcceptance()
        {
            Add("MyProperty", @"ValueForAcceptance");
        }
    }
}