using DotNetBuild.Tasks.NuGet;

public class PublishRunnerPackage : ITarget
{
    public String Description
    {
        get { return "Publish Runner NuGet package"; }
    }

    public Boolean ContinueOnError
    {
        get { return false; }
    }

    public IEnumerable<ITarget> DependsOn
    {
        get { return null; }
    }

    public Boolean Execute(TargetExecutionContext context)
    {
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
    }
}