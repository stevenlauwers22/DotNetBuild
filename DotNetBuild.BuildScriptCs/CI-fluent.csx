#r "DotNetBuild.Runner.ScriptCs.dll"

using DotNetBuild.Runner.ScriptCs;

"ci"
    .Target("Continuous integration target")
    .ContinueOnError(false)
    .DependsOn("buildRelease")
    .Do(configurationSettings => {
        return true;
    });

"buildRelease"
    .Target("Build in release mode")
    .ContinueOnError(false)
    .Do(configurationSettings => {
        var baseDir = configurationSettings.Get<String>("baseDir");
        var msBuildTask = new MsBuildTask
        {
            Project = Path.Combine(baseDir, "DotNetBuild.sln"),
            Target = "Rebuild",
            Parameters = "Configuration=Release"
        };

        return msBuildTask.Execute();
    });

"test"
    .Configure()
    .AddSetting("baseDir", @"G:\Steven\Werk\Private\DotNetBuild\DotNetBuild");
    .AddSetting("mySetting1", "value1ForTestEnvironment")
    .AddSetting("mySetting2", "value2ForTestEnvironment");

"acceptance"
    .Configure()
    .AddSetting("baseDir", @"G:\Steven\Werk\Private\DotNetBuild\DotNetBuild");
    .AddSetting("mySetting1", "value1ForAcceptanceEnvironment")
    .AddSetting("mySetting2", "value2ForAcceptanceEnvironment");

/* TARGET AND CONFIGURATIONSETTINGS SHOULD BE DETERMINED BASED ON SCRIPT ARGUMENTS */
var target = "ci";
var configurationSettings = "test";

var dotNetBuild = Require<DotNetBuildScriptPackContext>();
dotNetBuild.Run("ci", "test");