public class Deploy : ITarget
{
    public String Description
    {
        get { return "Deploy to NuGet"; }
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
                new PublishCorePackage(),
                new PublishRunnerPackage(),
                new PublishRunnerAssemblyPackage(),
                new PublishRunnerScriptCsPackage(),
                new PublishTasksPackage()
            };
        }
    }

    public Boolean Execute(TargetExecutionContext context)
    {
        return true;
    }
}