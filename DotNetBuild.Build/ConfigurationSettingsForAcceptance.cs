using DotNetBuild.Core;

namespace DotNetBuild.Build
{
    public class ConfigurationSettingsForAcceptance : ConfigurationSettings
    {
        public ConfigurationSettingsForAcceptance()
        {
            Add("MyProperty", @"ValueForAcceptance");
        }
    }
}