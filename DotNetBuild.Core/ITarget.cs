using System.Collections.Generic;

namespace DotNetBuild.Core
{
    public interface ITarget
    {
        string Description { get; }
        bool ContinueOnError { get; }
        IEnumerable<ITarget> DependsOn { get; }

        bool Execute(IConfigurationSettings configurationSettings);
    }
}