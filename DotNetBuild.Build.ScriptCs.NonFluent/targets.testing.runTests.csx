using DotNetBuild.Tasks;

public class RunTests : ITarget
{
    public String Description
    {
        get { return "Run tests"; }
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
        const String baseDir = @"..\";
        var xunitTask = new XunitTask
        {
            XunitExe = Path.Combine(baseDir, @"packages\xunit.runners.1.9.2\tools\xunit.console.clr4.exe"),
            Assembly = Path.Combine(baseDir, @"DotNetBuild.Tests\bin\Release\DotNetBuild.Tests.dll")
        };

        return xunitTask.Execute();
    }
}