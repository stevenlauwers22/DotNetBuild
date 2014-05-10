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

    public Boolean Execute(IConfigurationSettings configurationSettings)
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

    public Boolean Execute(IConfigurationSettings configurationSettings)
    {
        var baseDir = configurationSettings.Get<String>("baseDir");
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
        Add("baseDir", @"G:\Steven\Werk\Private\DotNetBuild\DotNetBuild");
        Add("mySetting1", "value1ForTestEnvironment");
        Add("mySetting2", "value2ForTestEnvironment");
    }
}

public class ConfigurationSettingsForAcceptance : ConfigurationSettings
{
    public ConfigurationSettingsForAcceptance()
    {
        Add("baseDir", @"G:\Steven\Werk\Private\DotNetBuild\DotNetBuild");
        Add("mySetting1", "value1ForAcceptanceEnvironment");
        Add("mySetting2", "value2ForAcceptanceEnvironment");
    }
}

/* TARGET AND CONFIGURATIONSETTINGS SHOULD BE DETERMINED BASED ON SCRIPT ARGUMENTS */
var target = new CI();
var configurationSettings = new ConfigurationSettingsForTest();

var dotNetBuild = Require<DotNetBuildScriptPackContext>();
dotNetBuild.Run(target, configurationSettings);