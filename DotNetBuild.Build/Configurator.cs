using DotNetBuild.Core;

namespace DotNetBuild.Build
{
    public class Configurator : IConfigurator
    {
        public void Configure()
        {
            "ci".Target(new CI());

            "test".Configure(new ConfigurationSettingsForTest());
            "acceptance".Configure(new ConfigurationSettingsForAcceptance());
        }
    }
}