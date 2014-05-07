using DotNetBuild.Core;

namespace DotNetBuild.Build
{
    public class ConfigurationSettingsForAcceptance : ConfigurationSettings
    {
        public ConfigurationSettingsForAcceptance()
        {
            Add("baseDir", @"G:\Steven\Werk\Private\DotNetBuild\DotNetBuild");
        }
    }
}