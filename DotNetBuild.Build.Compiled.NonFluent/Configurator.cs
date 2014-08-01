using DotNetBuild.Build.Compiled.NonFluent.Configuration;
using DotNetBuild.Build.Compiled.NonFluent.Targets;
using DotNetBuild.Core;

namespace DotNetBuild.Build.Compiled.NonFluent
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