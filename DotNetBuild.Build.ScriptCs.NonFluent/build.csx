#load configuration.csx
#load targets.ci.csx
#load targets.compilation.buildRelease.csx
#load targets.nuget.createCorePackage.csx
#load targets.nuget.createRunnerCommandLinePackage.csx
#load targets.nuget.createRunnerPackage.csx
#load targets.nuget.createRunnerScriptCsPackage.csx
#load targets.nuget.createTasksPackage.csx
#load targets.testing.runTests.csx
#load targets.versioning.updateVersionNumber.csx

var dotNetBuild = Require<DotNetBuildScriptPackContext>();
dotNetBuild.AddTarget("ci", new CI());
dotNetBuild.AddConfiguration("test", new ConfigurationSettingsForTest());
dotNetBuild.AddConfiguration("acceptance", new ConfigurationSettingsForAcceptance());
dotNetBuild.RunFromScriptArguments();