public class ConfigurationSettingsForTest : ConfigurationSettings
{
    public ConfigurationSettingsForTest()
    {
        Add("SolutionDirectory", @"..\");
    }
}

public class ConfigurationSettingsForAcceptance : ConfigurationSettings
{
    public ConfigurationSettingsForAcceptance()
    {
        Add("SolutionDirectory", @"..\");
    }
}