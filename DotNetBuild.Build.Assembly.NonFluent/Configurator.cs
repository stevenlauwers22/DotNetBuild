using DotNetBuild.Build.Assembly.NonFluent.Configuration;
using DotNetBuild.Build.Assembly.NonFluent.Targets;
using DotNetBuild.Core;

namespace DotNetBuild.Build.Assembly.NonFluent
{
    public class Configurator : IConfigurator
    {
        public void Configure()
        {
            "ci".Target(new CI());
            "deploy".Target(new Deploy());

            "defaultConfig".Configure(new DefaultConfigurationSettings());
        }
    }
}