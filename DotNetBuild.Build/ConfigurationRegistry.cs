namespace DotNetBuild.Build
{
    public class ConfigurationRegistry : Core.ConfigurationRegistry
    {
        public ConfigurationRegistry()
        {
            Add(new ConfigurationSettingsForTest(), configurationName => configurationName == "Test");
            Add(new ConfigurationSettingsForAcceptance(), configurationName => configurationName == "Acceptance");
        }
    }
}