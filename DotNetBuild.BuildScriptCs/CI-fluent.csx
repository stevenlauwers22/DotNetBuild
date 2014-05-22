#r "DotNetBuild.Core.dll"
#r "DotNetBuild.Tasks.dll"
#r "DotNetBuild.Runner.dll"
#r "DotNetBuild.Runner.ScriptCs.dll"

using DotNetBuild.Core;
using DotNetBuild.Tasks;
using DotNetBuild.Runner.ScriptCs;

var dotNetBuild = Require<DotNetBuildScriptPackContext>();

dotNetBuild.Configure(() => {
	"ci"
		.Target("Continuous integration target")
		.ContinueOnError(false)
		.DependsOn("buildRelease")
		.Do(configurationSettings => true);

	"buildRelease"
		.Target("Build in release mode")
		.ContinueOnError(false)
		.Do(configurationSettings => {
            const string baseDir = @"..\";
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
        .AddSetting("MyProperty", @"ValueForTest");

	"acceptance"
		.Configure()
        .AddSetting("MyProperty", @"ValueForAcceptance");
});

/* TARGET AND CONFIGURATIONSETTINGS SHOULD BE DETERMINED BASED ON SCRIPT ARGUMENTS */
var target = "ci";
var configurationSettings = "test";

dotNetBuild.Run("ci", "test");