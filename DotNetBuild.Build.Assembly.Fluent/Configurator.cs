﻿using System;
using System.IO;
using DotNetBuild.Core;
using DotNetBuild.Core.Facilities.Logging;
using DotNetBuild.Core.Facilities.State;
using DotNetBuild.Tasks;
using DotNetBuild.Tasks.NuGet;

namespace DotNetBuild.Build.Assembly.Fluent
{
    public class Configurator : IConfigurator
    {
        public void Configure()
        {
            "ci"
                .Target("Continuous integration target")
                .DependsOn("updateVersionNumber")
                .And("buildRelease")
                .And("runTests")
                .And("createCorePackage")
                .And("createRunnerPackage")
                .And("createRunnerAssemblyPackage")
                .And("createRunnerScriptCsPackage")
                .And("createTasksPackage");

            "updateVersionNumber"
                .Target("Update version number")
                .Do(context =>
                {
                    var solutionDirectory = context.ConfigurationSettings.Get<String>("SolutionDirectory");
                    const String assemblyMajorVersion = "1";
                    const String assemblyMinorVersion = "0";
                    const String assemblyBuildNumber = "0";
                    var assemblyInfoTask = new AssemblyInfo
                    {
                        AssemblyInfoFiles = new[]
                        {
                            Path.Combine(solutionDirectory, @"DotNetBuild.Core\Properties\AssemblyInfo.cs"),
                            Path.Combine(solutionDirectory, @"DotNetBuild.Runner\Properties\AssemblyInfo.cs"),
                            Path.Combine(solutionDirectory, @"DotNetBuild.Runner.Assembly\Properties\AssemblyInfo.cs")
                        },
                        AssemblyInformationalVersion = String.Format("{0}.{1}.{2}-alpha", assemblyMajorVersion, assemblyMinorVersion, assemblyBuildNumber),
                        UpdateAssemblyInformationalVersion = true,
                        AssemblyMajorVersion = assemblyMajorVersion,
                        AssemblyMinorVersion = assemblyMinorVersion,
                        AssemblyBuildNumber = assemblyBuildNumber,
                        AssemblyRevisionType = "AutoIncrement",
                        AssemblyRevisionFormat = "0",
                        AssemblyFileMajorVersion = assemblyMajorVersion,
                        AssemblyFileMinorVersion = assemblyMinorVersion,
                        AssemblyFileBuildNumber = assemblyBuildNumber,
                        AssemblyFileRevisionType = "AutoIncrement",
                        AssemblyFileRevisionFormat = "0",
                    };

                    var result = assemblyInfoTask.Execute();
                    context.FacilityProvider.Get<ILogger>().LogInfo("Building assembly version: " + assemblyInfoTask.MaxAssemblyVersion);
                    context.FacilityProvider.Get<ILogger>().LogInfo("Building assembly informational version: " + assemblyInfoTask.AssemblyInformationalVersion);
                    context.FacilityProvider.Get<IStateWriter>().Add("VersionNumber", assemblyInfoTask.AssemblyInformationalVersion);

                    return result;
                });

            "buildRelease"
                .Target("Build in release mode")
                .Do(context =>
                {
                    var solutionDirectory = context.ConfigurationSettings.Get<String>("SolutionDirectory");
                    var msBuildTask = new MsBuildTask
                    {
                        Project = Path.Combine(solutionDirectory, "DotNetBuild.sln"),
                        Target = "Rebuild",
                        Parameters = "Configuration=Release"
                    };

                    return msBuildTask.Execute();
                });

            "runTests"
                .Target("Run tests")
                .Do(context =>
                {
                    var solutionDirectory = context.ConfigurationSettings.Get<String>("SolutionDirectory");
                    var xunitExe = context.ConfigurationSettings.Get<String>("PathToXUnitRunnerExe");
                    var xunitTask = new XunitTask
                    {
                        XunitExe = Path.Combine(solutionDirectory, xunitExe),
                        Assembly = Path.Combine(solutionDirectory, @"DotNetBuild.Tests\bin\Release\DotNetBuild.Tests.dll")
                    };

                    return xunitTask.Execute();
                });

            "createCorePackage"
                .Target("Create Core NuGet package")
                .Do(context =>
                {
                    var solutionDirectory = context.ConfigurationSettings.Get<String>("SolutionDirectory");
                    var nugetExe = context.ConfigurationSettings.Get<String>("PathToNuGetExe");
                    var nugetPackTask = new Pack
                    {
                        NuGetExe = Path.Combine(solutionDirectory, nugetExe),
                        NuSpecFile = Path.Combine(solutionDirectory, @"DotNetBuild.Core\package.nuspec"),
                        OutputDir = Path.Combine(solutionDirectory, @"packagesForNuGet\"),
                        Version = context.FacilityProvider.Get<IStateReader>().Get<String>("VersionNumber")
                    };

                    return nugetPackTask.Execute();
                });

            "createRunnerPackage"
                .Target("Create Runner NuGet package")
                .Do(context =>
                {
                    var solutionDirectory = context.ConfigurationSettings.Get<String>("SolutionDirectory");
                    var nugetExe = context.ConfigurationSettings.Get<String>("PathToNuGetExe");
                    var nugetPackTask = new Pack
                    {
                        NuGetExe = Path.Combine(solutionDirectory, nugetExe),
                        NuSpecFile = Path.Combine(solutionDirectory, @"DotNetBuild.Runner\package.nuspec"),
                        OutputDir = Path.Combine(solutionDirectory, @"packagesForNuGet\"),
                        Version = context.FacilityProvider.Get<IStateReader>().Get<String>("VersionNumber")
                    };

                    return nugetPackTask.Execute();
                });

            "createRunnerAssemblyPackage"
                .Target("Create Assembly Runner NuGet package")
                .Do(context =>
                {
                    var solutionDirectory = context.ConfigurationSettings.Get<String>("SolutionDirectory");
                    var nugetExe = context.ConfigurationSettings.Get<String>("PathToNuGetExe");
                    var nugetPackTask = new Pack
                    {
                        NuGetExe = Path.Combine(solutionDirectory, nugetExe),
                        NuSpecFile = Path.Combine(solutionDirectory, @"DotNetBuild.Runner.Assembly\package.nuspec"),
                        OutputDir = Path.Combine(solutionDirectory, @"packagesForNuGet\"),
                        Version = context.FacilityProvider.Get<IStateReader>().Get<String>("VersionNumber")
                    };

                    return nugetPackTask.Execute();
                });

            "createRunnerScriptCsPackage"
                .Target("Create ScriptCs Runner NuGet package")
                .Do(context =>
                {
                    var solutionDirectory = context.ConfigurationSettings.Get<String>("SolutionDirectory");
                    var nugetExe = context.ConfigurationSettings.Get<String>("PathToNuGetExe");
                    var nugetPackTask = new Pack
                    {
                        NuGetExe = Path.Combine(solutionDirectory, nugetExe),
                        NuSpecFile = Path.Combine(solutionDirectory, @"DotNetBuild.Runner.ScriptCs\package.nuspec"),
                        OutputDir = Path.Combine(solutionDirectory, @"packagesForNuGet\"),
                        Version = context.FacilityProvider.Get<IStateReader>().Get<String>("VersionNumber")
                    };

                    return nugetPackTask.Execute();
                });

            "createTasksPackage"
                .Target("Create Tasks NuGet package")
                .Do(context =>
                {
                    var solutionDirectory = context.ConfigurationSettings.Get<String>("SolutionDirectory");
                    var nugetExe = context.ConfigurationSettings.Get<String>("PathToNuGetExe");
                    var nugetPackTask = new Pack
                    {
                        NuGetExe = Path.Combine(solutionDirectory, nugetExe),
                        NuSpecFile = Path.Combine(solutionDirectory, @"DotNetBuild.Tasks\package.nuspec"),
                        OutputDir = Path.Combine(solutionDirectory, @"packagesForNuGet\"),
                        Version = context.FacilityProvider.Get<IStateReader>().Get<String>("VersionNumber")
                    };

                    return nugetPackTask.Execute();
                });

            "test"
                .Configure()
                .AddSetting("SolutionDirectory", @"..\")
                .AddSetting("PathToNuGetExe", @"packages\NuGet.CommandLine.2.8.2\tools\NuGet.exe")
                .AddSetting("PathToXUnitRunnerExe", @"packages\xunit.runners.1.9.2\tools\xunit.console.clr4.exe");

            "acceptance"
                .Configure()
                .AddSetting("SolutionDirectory", @"..\")
                .AddSetting("PathToNuGetExe", @"packages\NuGet.CommandLine.2.8.2\tools\NuGet.exe")
                .AddSetting("PathToXUnitRunnerExe", @"packages\xunit.runners.1.9.2\tools\xunit.console.clr4.exe");
        }
    }
}