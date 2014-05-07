namespace DotNetBuild.Runner.ScriptCs.Targets
{
    public interface ITargetDependencyBuilder 
        : ITargetBuilder
    {
        ITargetDependencyBuilder And(string target);
    }

    public class TargetDependencyBuilder 
        : TargetBuilderDecorator, ITargetDependencyBuilder
    {
        public TargetDependencyBuilder(ITargetBuilder targetBuilder)
            : base(targetBuilder)
        {
        }

        public ITargetDependencyBuilder And(string target)
        {
            _targetBuilder.DependsOn(target);
            return this;
        }
    }
}