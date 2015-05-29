#r "DotNetBuild.Core.dll"
#r "DotNetBuild.Runner.dll"
#r "DotNetBuild.Runner.ScriptCs.dll"
#r "DotNetBuild.Tasks.dll"

#load configuration.csx
#load targets.ci.csx
#load targets.deploy.csx
#load targets.compilation.buildRelease.csx
#load targets.nuget.createCorePackage.csx
#load targets.nuget.createRunnerAssemblyPackage.csx
#load targets.nuget.createRunnerPackage.csx
#load targets.nuget.createRunnerScriptCsPackage.csx
#load targets.nuget.createTasksPackage.csx
#load targets.nuget.publishCorePackage.csx
#load targets.nuget.publishRunnerAssemblyPackage.csx
#load targets.nuget.publishRunnerPackage.csx
#load targets.nuget.publishRunnerScriptCsPackage.csx
#load targets.nuget.publishTasksPackage.csx
#load targets.testing.runTests.csx
#load targets.versioning.updateVersionNumber.csx

var dotNetBuild = Require<DotNetBuildScriptPackContext>();
dotNetBuild.AddTarget("ci", new CI());
dotNetBuild.AddTarget("deploy", new Deploy());
dotNetBuild.AddConfiguration("defaultConfig", new DefaultConfigurationSettings());
dotNetBuild.Run();