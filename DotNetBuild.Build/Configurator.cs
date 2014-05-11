using DotNetBuild.Core;
using DotNetBuild.Core.Targets;

namespace DotNetBuild.Build
{
    public class Configurator : IConfigurator
    {
        public void Configure()
        {
            "ci".Target(new CI());
        }
    }
}