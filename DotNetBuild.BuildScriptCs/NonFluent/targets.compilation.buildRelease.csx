using DotNetBuild.Tasks;

public class BuildRelease : ITarget
{
    public String Description
    {
        get { return "Build in release mode"; }
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
        const String baseDir = @"..\..\";
        var msBuildTask = new MsBuildTask
        {
            Project = Path.Combine(baseDir, "DotNetBuild.sln"),
            Target = "Rebuild",
            Parameters = "Configuration=Release"
        };

        return msBuildTask.Execute();
    }
}