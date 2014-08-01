using DotNetBuild.Core.Facilities.Logging;
using DotNetBuild.Core.Facilities.State;
using DotNetBuild.Tasks;
using DotNetBuild.Tasks.NuGet;

var dotNetBuild = Require<DotNetBuildScriptPackContext>();

dotNetBuild.AddTarget("ci", "Continuous integration target", c 
    => c.DependsOn("updateVersionNumber")
        .And("buildRelease")
        .And("runTests")
        .And("createCorePackage")
        .And("createRunnerPackage")
        .And("createRunnerCommandLinePackage")
        .And("createRunnerScriptCsPackage")
        .And("createTasksPackage"));

dotNetBuild.AddTarget("updateVersionNumber", "Update version number", c 
    => c.Do(context => {
            const String baseDir = @"..\..\";
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
		}));

dotNetBuild.AddTarget("buildRelease", "Build in release mode", c 
	=> c.Do(context => {
            const String baseDir = @"..\..\";
			var msBuildTask = new MsBuildTask
			{
				Project = Path.Combine(baseDir, "DotNetBuild.sln"),
				Target = "Rebuild",
				Parameters = "Configuration=Release"
			};

			return msBuildTask.Execute();
		}));

dotNetBuild.AddTarget("runTests", "Run tests", c 
	=> c.Do(context => {
            const String baseDir = @"..\..\";
            var xunitTask = new XunitTask
            {
                XunitExe = Path.Combine(baseDir, @"packages\xunit.runners.1.9.2\tools\xunit.console.clr4.exe"),
                Assembly = Path.Combine(baseDir, @"DotNetBuild.Tests\bin\Release\DotNetBuild.Tests.dll")
            };

            return xunitTask.Execute();
		}));

dotNetBuild.AddTarget("createCorePackage", "Create Core NuGet package", c 
    => c.Do(context => {
            const String baseDir = @"..\..\";
            var nugetPackTask = new Pack
            {
                NuGetExe = Path.Combine(baseDir, @"packages\NuGet.CommandLine.2.7.3\tools\NuGet.exe"),
                NuSpecFile = Path.Combine(baseDir, @"packagesForNuGet\DotNetBuild.Core.nuspec"),
                OutputDir = Path.Combine(baseDir, @"packagesForNuGet\"),
                Version = context.FacilityProvider.Get<IStateReader>().Get<String>("VersionNumber")
            };

            return nugetPackTask.Execute();
		}));

dotNetBuild.AddTarget("createRunnerPackage", "Create Runner NuGet package", c 
    => c.Do(context => {
            const String baseDir = @"..\..\";
            var nugetPackTask = new Pack
            {
                NuGetExe = Path.Combine(baseDir, @"packages\NuGet.CommandLine.2.7.3\tools\NuGet.exe"),
                NuSpecFile = Path.Combine(baseDir, @"packagesForNuGet\DotNetBuild.Runner.nuspec"),
                OutputDir = Path.Combine(baseDir, @"packagesForNuGet\"),
                Version = context.FacilityProvider.Get<IStateReader>().Get<String>("VersionNumber")
            };

            return nugetPackTask.Execute();
		}));

dotNetBuild.AddTarget("createRunnerCommandLinePackage", "Create CommandLine Runner NuGet package", c 
    => c.Do(context => {
            const String baseDir = @"..\..\";
            var nugetPackTask = new Pack
            {
                NuGetExe = Path.Combine(baseDir, @"packages\NuGet.CommandLine.2.7.3\tools\NuGet.exe"),
                NuSpecFile = Path.Combine(baseDir, @"packagesForNuGet\DotNetBuild.Runner.CommandLine.nuspec"),
                OutputDir = Path.Combine(baseDir, @"packagesForNuGet\"),
                Version = context.FacilityProvider.Get<IStateReader>().Get<String>("VersionNumber")
            };

            return nugetPackTask.Execute();
		}));

dotNetBuild.AddTarget("createRunnerScriptCsPackage", "Create ScriptCs Runner NuGet package", c 
    => c.Do(context => {
            const String baseDir = @"..\..\";
            var nugetPackTask = new Pack
            {
                NuGetExe = Path.Combine(baseDir, @"packages\NuGet.CommandLine.2.7.3\tools\NuGet.exe"),
                NuSpecFile = Path.Combine(baseDir, @"packagesForNuGet\DotNetBuild.Runner.ScriptCs.nuspec"),
                OutputDir = Path.Combine(baseDir, @"packagesForNuGet\"),
                Version = context.FacilityProvider.Get<IStateReader>().Get<String>("VersionNumber")
            };

            return nugetPackTask.Execute();
		}));

dotNetBuild.AddTarget("createTasksPackage", "Create Tasks NuGet package", c 
    => c.Do(context => {
            const String baseDir = @"..\..\";
            var nugetPackTask = new Pack
            {
                NuGetExe = Path.Combine(baseDir, @"packages\NuGet.CommandLine.2.7.3\tools\NuGet.exe"),
                NuSpecFile = Path.Combine(baseDir, @"packagesForNuGet\DotNetBuild.Tasks.nuspec"),
                OutputDir = Path.Combine(baseDir, @"packagesForNuGet\"),
                Version = context.FacilityProvider.Get<IStateReader>().Get<String>("VersionNumber")
            };

            return nugetPackTask.Execute();
		}));

dotNetBuild.AddConfiguration("test", c 
	=> c.AddSetting("MyProperty", @"ValueForTest")
);

dotNetBuild.AddConfiguration("acceptance", c 
	=> c.AddSetting("MyProperty", @"ValueForAcceptance")
);

dotNetBuild.RunFromScriptArguments();