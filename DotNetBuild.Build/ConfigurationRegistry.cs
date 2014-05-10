namespace DotNetBuild.Build
{
    public class ConfigurationRegistry : Core.ConfigurationRegistry
    {
        public ConfigurationRegistry()
        {
            Add("test", new ConfigurationSettingsForTest());
            Add("acceptance", new ConfigurationSettingsForAcceptance());
        }
    }
}