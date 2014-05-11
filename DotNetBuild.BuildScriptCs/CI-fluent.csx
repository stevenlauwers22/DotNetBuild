#r "DotNetBuild.Core.dll"
#r "DotNetBuild.Tasks.dll"
#r "DotNetBuild.Runner.dll"
#r "DotNetBuild.Runner.ScriptCs.dll"

using DotNetBuild.Core;
using DotNetBuild.Tasks;
using DotNetBuild.Runner.ScriptCs;

/* TARGET AND CONFIGURATIONSETTINGS SHOULD BE DETERMINED BASED ON SCRIPT ARGUMENTS */
var target = "ci";
var configurationSettings = "test";

var dotNetBuild = Require<DotNetBuildScriptPackContext>();
dotNetBuild.Run("ci", "test", () {
	"ci"
		.Target("Continuous integration target")
		.ContinueOnError(false)
		.DependsOn("buildRelease")
		.Do(configurationSettings => true);

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

	"acceptance"
		.Configure()
		.AddSetting("baseDir", @"G:\Steven\Werk\Private\DotNetBuild\DotNetBuild");
});