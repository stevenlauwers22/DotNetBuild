using DotNetBuild.Core;

namespace DotNetBuild.Build.Assembly.NonFluent.Configuration
{
    public class ConfigurationSettingsForAcceptance : ConfigurationSettings
    {
        public ConfigurationSettingsForAcceptance()
        {
            Add("SolutionDirectory", @"..\");
            Add("PathToNuGetExe", @"packages\NuGet.CommandLine.2.8.2\tools\NuGet.exe");
            Add("NuGetApiKey", "");
            Add("PathToXUnitRunnerExe", @"packages\xunit.runners.1.9.2\tools\xunit.console.clr4.exe");
        }
    }
}