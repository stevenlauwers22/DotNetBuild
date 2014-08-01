public class CI : ITarget
{
    public String Description
    {
        get { return "Continuous integration target"; }
    }

    public Boolean ContinueOnError
    {
        get { return false; }
    }

    public IEnumerable<ITarget> DependsOn
    {
        get
        {
            return new List<ITarget>
            {
                new UpdateVersionNumber(),
                new BuildRelease(),
                new RunTests(),
                new CreateCorePackage(),
                new CreateRunnerPackage(),
                new CreateRunnerCommandLinePackage(),
                new CreateRunnerScriptCsPackage(),
                new CreateTasksPackage()
            };
        }
    }

    public Boolean Execute(TargetExecutionContext context)
    {
        return true;
    }
}