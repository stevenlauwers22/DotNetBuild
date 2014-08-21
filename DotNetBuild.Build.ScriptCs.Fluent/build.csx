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
        .And("createRunnerAssemblyPackage")
        .And("createRunnerScriptCsPackage")
        .And("createTasksPackage"));

dotNetBuild.AddTarget("updateVersionNumber", "Update version number", c 
    => c.Do(context => {
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
                    Path.Combine(solutionDirectory, @"DotNetBuild.Runner.Assembly\Properties\AssemblyInfo.cs"),
                    Path.Combine(solutionDirectory, @"DotNetBuild.Runner.ScriptCs\Properties\AssemblyInfo.cs"),
                    Path.Combine(solutionDirectory, @"DotNetBuild.Tasks\Properties\AssemblyInfo.cs")
                },
                AssemblyInformationalVersion = String.Format("{0}.{1}.{2}-alpha2", assemblyMajorVersion, assemblyMinorVersion, assemblyBuildNumber),
                UpdateAssemblyInformationalVersion = true,
                AssemblyMajorVersion = assemblyMajorVersion,
                AssemblyMinorVersion = assemblyMinorVersion,
                AssemblyBuildNumber = assemblyBuildNumber,
                AssemblyRevision = "0",
                AssemblyFileMajorVersion = assemblyMajorVersion,
                AssemblyFileMinorVersion = assemblyMinorVersion,
                AssemblyFileBuildNumber = assemblyBuildNumber,
                AssemblyFileRevision = "0"
            };

            var result = assemblyInfoTask.Execute();
            context.FacilityProvider.Get<ILogger>().LogInfo("Building assembly version: " + assemblyInfoTask.MaxAssemblyVersion);
            context.FacilityProvider.Get<ILogger>().LogInfo("Building assembly informational version: " + assemblyInfoTask.AssemblyInformationalVersion);
            context.FacilityProvider.Get<IStateWriter>().Add("VersionNumber", assemblyInfoTask.AssemblyInformationalVersion);

            return result;
		}));

dotNetBuild.AddTarget("buildRelease", "Build in release mode", c 
	=> c.Do(context => {
            var solutionDirectory = context.ConfigurationSettings.Get<String>("SolutionDirectory");
			var msBuildTask = new MsBuildTask
			{
				Project = Path.Combine(solutionDirectory, "DotNetBuild.sln"),
				Target = "Rebuild",
				Parameters = "Configuration=Release"
			};

			return msBuildTask.Execute();
		}));

dotNetBuild.AddTarget("runTests", "Run tests", c 
	=> c.Do(context => {
            var solutionDirectory = context.ConfigurationSettings.Get<String>("SolutionDirectory");
            var xunitExe = context.ConfigurationSettings.Get<String>("PathToXUnitRunnerExe");
            var xunitTask = new XunitTask
            {
                XunitExe = Path.Combine(solutionDirectory, xunitExe),
                Assembly = Path.Combine(solutionDirectory, @"DotNetBuild.Tests\bin\Release\DotNetBuild.Tests.dll")
            };

            return xunitTask.Execute();
		}));

dotNetBuild.AddTarget("createCorePackage", "Create Core NuGet package", c 
    => c.Do(context => {
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
		}));

dotNetBuild.AddTarget("createRunnerPackage", "Create Runner NuGet package", c 
    => c.Do(context => {
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
		}));

dotNetBuild.AddTarget("createRunnerAssemblyPackage", "Create Assembly Runner NuGet package", c 
    => c.Do(context => {
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
		}));

dotNetBuild.AddTarget("createRunnerScriptCsPackage", "Create ScriptCs Runner NuGet package", c 
    => c.Do(context => {
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
		}));

dotNetBuild.AddTarget("createTasksPackage", "Create Tasks NuGet package", c 
    => c.Do(context => {
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
		}));

dotNetBuild.AddTarget("deploy", "Deploy to NuGet", c
	=> c.DependsOn("publishCorePackage")
        .And("publishRunnerPackage")
        .And("publishRunnerAssemblyPackage")
        .And("publishRunnerScriptCsPackage")
        .And("publishTasksPackage");

dotNetBuild.AddTarget("publishCorePackage", "Publish Core NuGet package", c
	=> c.Do(context => {
            var solutionDirectory = context.ConfigurationSettings.Get<String>("SolutionDirectory");
            var nugetExe = context.ConfigurationSettings.Get<String>("PathToNuGetExe");
            var nugetApiKey = context.ConfigurationSettings.Get<String>("NuGetApiKey");
            var nupkgFile = string.Format(@"packagesForNuGet\DotNetBuild.Core.{0}.nupkg", context.ParameterProvider.Get("VersionNumber"));
            var nugetPackTask = new Push
            {
                NuGetExe = Path.Combine(solutionDirectory, nugetExe),
                NuPkgFile = Path.Combine(solutionDirectory, nupkgFile),
                ApiKey = nugetApiKey
            };

            return nugetPackTask.Execute();
        }));

dotNetBuild.AddTarget("publishRunnerPackage", "Publish Runner NuGet package", c
	=> c.Do(context => {
            var solutionDirectory = context.ConfigurationSettings.Get<String>("SolutionDirectory");
            var nugetExe = context.ConfigurationSettings.Get<String>("PathToNuGetExe");
            var nugetApiKey = context.ConfigurationSettings.Get<String>("NuGetApiKey");
            var nupkgFile = string.Format(@"packagesForNuGet\DotNetBuild.Runner.{0}.nupkg", context.ParameterProvider.Get("VersionNumber"));
            var nugetPackTask = new Push
            {
                NuGetExe = Path.Combine(solutionDirectory, nugetExe),
                NuPkgFile = Path.Combine(solutionDirectory, nupkgFile),
                ApiKey = nugetApiKey
            };

            return nugetPackTask.Execute();
        }));

dotNetBuild.AddTarget("publishRunnerAssemblyPackage", "Publish Assembly Runner NuGet package", c
	=> c.Do(context => {
            var solutionDirectory = context.ConfigurationSettings.Get<String>("SolutionDirectory");
            var nugetExe = context.ConfigurationSettings.Get<String>("PathToNuGetExe");
            var nugetApiKey = context.ConfigurationSettings.Get<String>("NuGetApiKey");
            var nupkgFile = string.Format(@"packagesForNuGet\DotNetBuild.Runner.Assembly.{0}.nupkg", context.ParameterProvider.Get("VersionNumber"));
            var nugetPackTask = new Push
            {
                NuGetExe = Path.Combine(solutionDirectory, nugetExe),
                NuPkgFile = Path.Combine(solutionDirectory, nupkgFile),
                ApiKey = nugetApiKey
            };

            return nugetPackTask.Execute();
        }));

dotNetBuild.AddTarget("publishRunnerScriptCsPackage", "Publish ScriptCs Runner NuGet package", c
	=> c.Do(context => {
            var solutionDirectory = context.ConfigurationSettings.Get<String>("SolutionDirectory");
            var nugetExe = context.ConfigurationSettings.Get<String>("PathToNuGetExe");
            var nugetApiKey = context.ConfigurationSettings.Get<String>("NuGetApiKey");
            var nupkgFile = string.Format(@"packagesForNuGet\DotNetBuild.Runner.ScriptCs.{0}.nupkg", context.ParameterProvider.Get("VersionNumber"));
            var nugetPackTask = new Push
            {
                NuGetExe = Path.Combine(solutionDirectory, nugetExe),
                NuPkgFile = Path.Combine(solutionDirectory, nupkgFile),
                ApiKey = nugetApiKey
            };

            return nugetPackTask.Execute();
        }));

dotNetBuild.AddTarget("publishTasksPackage", "Publish Tasks NuGet package", c
	=> c.Do(context => {
            var solutionDirectory = context.ConfigurationSettings.Get<String>("SolutionDirectory");
            var nugetExe = context.ConfigurationSettings.Get<String>("PathToNuGetExe");
            var nugetApiKey = context.ConfigurationSettings.Get<String>("NuGetApiKey");
            var nupkgFile = string.Format(@"packagesForNuGet\DotNetBuild.Tasks.{0}.nupkg", context.ParameterProvider.Get("VersionNumber"));
            var nugetPackTask = new Push
            {
                NuGetExe = Path.Combine(solutionDirectory, nugetExe),
                NuPkgFile = Path.Combine(solutionDirectory, nupkgFile),
                ApiKey = nugetApiKey
            };

            return nugetPackTask.Execute();
        }));

dotNetBuild.AddConfiguration("defaultConfig", c 
	=> c.AddSetting("SolutionDirectory", @"..\")
        .AddSetting("PathToNuGetExe", @"packages\NuGet.CommandLine.2.8.2\tools\NuGet.exe")
        .AddSetting("NuGetApiKey", "")
        .AddSetting("PathToXUnitRunnerExe", @"packages\xunit.runners.1.9.2\tools\xunit.console.clr4.exe")
);

dotNetBuild.RunFromScriptArguments();