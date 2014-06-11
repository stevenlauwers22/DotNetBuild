#r "DotNetBuild.Core.dll"
#r "DotNetBuild.Tasks.dll"
#r "DotNetBuild.Runner.dll"
#r "DotNetBuild.Runner.ScriptCs.dll"

using System.Collections.Generic;
using DotNetBuild.Core;
using DotNetBuild.Tasks;
using DotNetBuild.Runner.ScriptCs;
using ScriptCs.Contracts;

var dotNetBuild = Require<DotNetBuildScriptPackContext>();

dotNetBuild.Configure(() => {
	"ci".Target(new CI());

	"test".Configure(new ConfigurationSettingsForTest());
	"acceptance".Configure(new ConfigurationSettingsForAcceptance());
});

/* TARGET AND CONFIGURATIONSETTINGS SHOULD BE DETERMINED BASED ON SCRIPT ARGUMENTS */
var target = "ci";
var configurationSettings = "test";

dotNetBuild.Run(target, configurationSettings);

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
    public string Description
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
        const string baseDir = @"..\";
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