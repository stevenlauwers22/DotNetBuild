using System;
using System.Collections.Generic;

namespace DotNetBuild.Core
{
    public interface ITarget
    {
        String Description { get; }
        Boolean ContinueOnError { get; }
        IEnumerable<ITarget> DependsOn { get; }

        Boolean Execute(IConfigurationSettings configurationSettings);
    }
}