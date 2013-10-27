using System.Collections.Generic;

namespace DotNetBuild.Core
{
    public interface ITarget
    {
        string Name { get; }
        bool ContinueOnError { get; }
        IEnumerable<ITarget> DependsOn { get; }

        bool Execute(IConfigurationSettings configurationSettings);
    }
}