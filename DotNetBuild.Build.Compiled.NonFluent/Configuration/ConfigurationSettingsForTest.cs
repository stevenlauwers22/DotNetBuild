using DotNetBuild.Core;

namespace DotNetBuild.Build.Compiled.NonFluent.Configuration
{
    public class ConfigurationSettingsForTest : ConfigurationSettings
    {
        public ConfigurationSettingsForTest()
        {
            Add("SolutionDirectory", @"..\");
            Add("PathToNuGetExe", @"packages\NuGet.CommandLine.2.8.2\tools\NuGet.exe");
            Add("PathToXUnitRunnerExe", @"packages\xunit.runners.1.9.2\tools\xunit.console.clr4.exe");
        }
    }
}