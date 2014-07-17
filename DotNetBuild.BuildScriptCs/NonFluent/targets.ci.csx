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
                new BuildRelease()
            };
        }
    }

    public Boolean Execute(TargetExecutionContext context)
    {
        return true;
    }
}