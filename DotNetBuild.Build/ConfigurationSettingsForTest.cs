using DotNetBuild.Core;

namespace DotNetBuild.Build
{
    public class ConfigurationSettingsForTest : ConfigurationSettings
    {
        public ConfigurationSettingsForTest()
        {
            Add("baseDir", @"G:\Steven\Werk\Private\DotNetBuild\DotNetBuild");
        }
    }
}