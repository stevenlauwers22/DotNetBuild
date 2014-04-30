#r "DotNetBuild.Core.dll"
#r "DotNetBuild.Tasks.dll"
#r "DotNetBuild.Runner.dll"
#r "DotNetBuild.Runner.ScriptCs.dll"

using System.Collections.Generic;
using DotNetBuild.Core;
using DotNetBuild.Tasks;
using DotNetBuild.Runner.ScriptCs;

public class CI : ITarget
{
    public string Name
    {
        get { return "Continuous integration target"; }
    }

    public bool ContinueOnError
    {
        get { return false; }
    }

    public IEnumerable<ITarget> DependsOn
    {
        get
        {
            return new List<ITarget>
            {
                new BuildRelease()
            };
        }
    }

    public bool Execute(IConfigurationSettings configurationSettings)
    {
        return true;
    }
}

public class BuildRelease : ITarget
{
    public string Name
    {
        get { return "Build in release mode"; }
    }

    public bool ContinueOnError
    {
        get { return false; }
    }

    public IEnumerable<ITarget> DependsOn
    {
        get { return null; }
    }

    public bool Execute(IConfigurationSettings configurationSettings)
    {
        var msBuildTask = new MsBuildTask
        {
            Project = @"G:\Steven\Werk\Private\DotNetBuild\DotNetBuild\DotNetBuild.sln",
            Target = "Rebuild",
            Parameters = "Configuration=Release"
        };

        return msBuildTask.Execute();
    }
}

var dotNetBuild = Require<DotNetBuildScriptPackContext>();
dotNetBuild.Run(new CI(), null);