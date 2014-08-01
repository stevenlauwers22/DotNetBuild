﻿using System;
using System.IO;
using DotNetBuild.Core;
using DotNetBuild.Core.Facilities.Logging;
using DotNetBuild.Core.Facilities.State;
using DotNetBuild.Tasks;
using DotNetBuild.Tasks.NuGet;

namespace DotNetBuild.Build.Compiled.Fluent
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
                .And("createRunnerCommandLinePackage")
                .And("createRunnerScriptCsPackage")
                .And("createTasksPackage");

            "updateVersionNumber"
                .Target("Update version number")
                .Do(context =>
                {
                    const String baseDir = @"..\";
                    const String assemblyMajorVersion = "1";
                    const String assemblyMinorVersion = "0";
                    const String assemblyBuildNumber = "0";
                    var assemblyInfoTask = new AssemblyInfo
                    {
                        AssemblyInfoFiles = new[]
                        {
                            Path.Combine(baseDir, @"DotNetBuild.Core\Properties\AssemblyInfo.cs"),
                            Path.Combine(baseDir, @"DotNetBuild.Runner\Properties\AssemblyInfo.cs"),
                            Path.Combine(baseDir, @"DotNetBuild.Runner.CommandLine\Properties\AssemblyInfo.cs")
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
                    const String baseDir = @"..\";
                    var msBuildTask = new MsBuildTask
                    {
                        Project = Path.Combine(baseDir, "DotNetBuild.sln"),
                        Target = "Rebuild",
                        Parameters = "Configuration=Release"
                    };

                    return msBuildTask.Execute();
                });

            "runTests"
                .Target("Run tests")
                .Do(context =>
                {
                    const String baseDir = @"..\";
                    var xunitTask = new XunitTask
                    {
                        XunitExe = Path.Combine(baseDir, @"packages\xunit.runners.1.9.2\tools\xunit.console.clr4.exe"),
                        Assembly = Path.Combine(baseDir, @"DotNetBuild.Tests\bin\Release\DotNetBuild.Tests.dll")
                    };

                    return xunitTask.Execute();
                });

            "createCorePackage"
                .Target("Create Core NuGet package")
                .Do(context =>
                {
                    const String baseDir = @"..\";
                    var nugetPackTask = new Pack
                    {
                        NuGetExe = Path.Combine(baseDir, @"packages\NuGet.CommandLine.2.7.3\tools\NuGet.exe"),
                        NuSpecFile = Path.Combine(baseDir, @"packagesForNuGet\DotNetBuild.Core.nuspec"),
                        OutputDir = Path.Combine(baseDir, @"packagesForNuGet\"),
                        Version = context.FacilityProvider.Get<IStateReader>().Get<String>("VersionNumber")
                    };

                    return nugetPackTask.Execute();
                });

            "createRunnerPackage"
                .Target("Create Runner NuGet package")
                .Do(context =>
                {
                    const String baseDir = @"..\";
                    var nugetPackTask = new Pack
                    {
                        NuGetExe = Path.Combine(baseDir, @"packages\NuGet.CommandLine.2.7.3\tools\NuGet.exe"),
                        NuSpecFile = Path.Combine(baseDir, @"packagesForNuGet\DotNetBuild.Runner.nuspec"),
                        OutputDir = Path.Combine(baseDir, @"packagesForNuGet\"),
                        Version = context.FacilityProvider.Get<IStateReader>().Get<String>("VersionNumber")
                    };

                    return nugetPackTask.Execute();
                });

            "createRunnerCommandLinePackage"
                .Target("Create CommandLine Runner NuGet package")
                .Do(context =>
                {
                    const String baseDir = @"..\";
                    var nugetPackTask = new Pack
                    {
                        NuGetExe = Path.Combine(baseDir, @"packages\NuGet.CommandLine.2.7.3\tools\NuGet.exe"),
                        NuSpecFile = Path.Combine(baseDir, @"packagesForNuGet\DotNetBuild.Runner.CommandLine.nuspec"),
                        OutputDir = Path.Combine(baseDir, @"packagesForNuGet\"),
                        Version = context.FacilityProvider.Get<IStateReader>().Get<String>("VersionNumber")
                    };

                    return nugetPackTask.Execute();
                });

            "createRunnerScriptCsPackage"
                .Target("Create ScriptCs Runner NuGet package")
                .Do(context =>
                {
                    const String baseDir = @"..\";
                    var nugetPackTask = new Pack
                    {
                        NuGetExe = Path.Combine(baseDir, @"packages\NuGet.CommandLine.2.7.3\tools\NuGet.exe"),
                        NuSpecFile = Path.Combine(baseDir, @"packagesForNuGet\DotNetBuild.Runner.ScriptCs.nuspec"),
                        OutputDir = Path.Combine(baseDir, @"packagesForNuGet\"),
                        Version = context.FacilityProvider.Get<IStateReader>().Get<String>("VersionNumber")
                    };

                    return nugetPackTask.Execute();
                });

            "createTasksPackage"
                .Target("Create Tasks NuGet package")
                .Do(context =>
                {
                    const String baseDir = @"..\";
                    var nugetPackTask = new Pack
                    {
                        NuGetExe = Path.Combine(baseDir, @"packages\NuGet.CommandLine.2.7.3\tools\NuGet.exe"),
                        NuSpecFile = Path.Combine(baseDir, @"packagesForNuGet\DotNetBuild.Tasks.nuspec"),
                        OutputDir = Path.Combine(baseDir, @"packagesForNuGet\"),
                        Version = context.FacilityProvider.Get<IStateReader>().Get<String>("VersionNumber")
                    };

                    return nugetPackTask.Execute();
                });

            "test"
                .Configure()
                .AddSetting("MyProperty", @"ValueForTest");

            "acceptance"
                .Configure()
                .AddSetting("MyProperty", @"ValueForAcceptance");
        }
    }
}