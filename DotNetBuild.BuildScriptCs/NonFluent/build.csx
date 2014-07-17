#r "DotNetBuild.Core.dll"
#r "DotNetBuild.Tasks.dll"
#r "DotNetBuild.Runner.dll"
#r "DotNetBuild.Runner.ScriptCs.dll"

#load configuration.csx
#load targets.ci.csx
#load tasks.compilation.buildRelease.csx
#load tasks.nuget.createCorePackage.csx
#load tasks.nuget.createRunnerCommandLinePackage.csx
#load tasks.nuget.createRunnerPackage.csx
#load tasks.nuget.createRunnerScriptCsPackage.csx
#load tasks.nuget.createTasksPackage.csx
#load tasks.testing.runTests.csx
#load tasks.versioning.updateVersionNumber.csx

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