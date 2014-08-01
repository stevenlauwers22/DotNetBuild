using DotNetBuild.Core;

namespace DotNetBuild.Build.Compiled.NonFluent.Configuration
{
    public class ConfigurationSettingsForAcceptance : ConfigurationSettings
    {
        public ConfigurationSettingsForAcceptance()
        {
            Add("SolutionDirectory", @"..\");
        }
    }
}