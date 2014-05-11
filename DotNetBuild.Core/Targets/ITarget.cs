using System;
using System.Collections.Generic;

namespace DotNetBuild.Core.Targets
{
    public interface ITarget
    {
        String Description { get; }
        Boolean ContinueOnError { get; }
        IEnumerable<ITarget> DependsOn { get; }

        Boolean Execute(IConfigurationSettings configurationSettings);
    }
}