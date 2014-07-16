#r "DotNetBuild.Core.dll"
#r "DotNetBuild.Tasks.dll"
#r "DotNetBuild.Runner.dll"
#r "DotNetBuild.Runner.ScriptCs.dll"

using DotNetBuild.Core;
using DotNetBuild.Core.Facilities.Logging;
using DotNetBuild.Core.Facilities.State;
using DotNetBuild.Tasks;
using DotNetBuild.Tasks.NuGet;
using DotNetBuild.Runner.ScriptCs;

var dotNetBuild = Require<DotNetBuildScriptPackContext>();

dotNetBuild.Configure(() => {
	"ci".Target(new CI());

	"test".Configure(new ConfigurationSettingsForTest());
	"acceptance".Configure(new ConfigurationSettingsForAcceptance());
});

var target = Env.ScriptArgs[0];
var configuration = Env.ScriptArgs[1];
dotNetBuild.Run(target, configuration);

public class CI : ITarget
{
    public String Description
    {
        get { return "Continuous integration target"; }
    }

    public Boolean ContinueOnError
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

    public Boolean Execute(TargetExecutionContext context)
    {
        return true;
    }
}

public class BuildRelease : ITarget
{
    public String Description
    {
        get { return "Build in release mode"; }
    }

    public Boolean ContinueOnError
    {
        get { return false; }
    }

    public IEnumerable<ITarget> DependsOn
    {
        get { return null; }
    }

    public Boolean Execute(TargetExecutionContext context)
    {
        const String baseDir = @"..\";
        var msBuildTask = new MsBuildTask
        {
            Project = Path.Combine(baseDir, "DotNetBuild.sln"),
            Target = "Rebuild",
            Parameters = "Configuration=Release"
        };

        return msBuildTask.Execute();
    }
}

public class ConfigurationSettingsForTest : ConfigurationSettings
{
    public ConfigurationSettingsForTest()
    {
        Add("MyProperty", "ValueForTest");
    }
}

public class ConfigurationSettingsForAcceptance : ConfigurationSettings
{
    public ConfigurationSettingsForAcceptance()
    {
        Add("MyProperty", "ValueForAcceptance");
    }
}