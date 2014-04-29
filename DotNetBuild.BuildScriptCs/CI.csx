#r "DotNetBuild.Core.dll"
#r "DotNetBuild.Tasks.dll"
#r "DotNetBuild.Runner.dll"

using System.Collections.Generic;
using DotNetBuild.Core;
using DotNetBuild.Tasks;
using DotNetBuild.Runner;

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

var targetInspector = new DotNetBuild.Runner.TargetInspector();
var loggerFactory = new DotNetBuild.Runner.Infrastructure.Logging.LoggerFactory();
var logger = loggerFactory.CreateLogger();
var targetExecutor = new TargetExecutor(targetInspector, logger, new DotNetBuild.Runner.Facilities.IFacilityProvider[] 
{ 
	new DotNetBuild.Runner.Facilities.Logging.LoggerFacilityProvider(logger, () => new DotNetBuild.Runner.Facilities.Logging.Logger(logger)),
	new DotNetBuild.Runner.Facilities.State.StateReaderFacilityProvider(logger, () => new DotNetBuild.Runner.Facilities.State.StateReader(new StateRepository())),
	new DotNetBuild.Runner.Facilities.State.StateWriterFacilityProvider(logger, () => new DotNetBuild.Runner.Facilities.State.StateWriter(new StateRepository())),
});

targetExecutor.Execute(new CI(), null);