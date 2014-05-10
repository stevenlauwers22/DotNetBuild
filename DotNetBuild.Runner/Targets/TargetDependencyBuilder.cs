using System;

namespace DotNetBuild.Runner.Targets
{
    public interface ITargetDependencyBuilder 
        : ITargetBuilder
    {
        ITargetDependencyBuilder And(String target);
    }

    public class TargetDependencyBuilder 
        : TargetBuilderDecorator, ITargetDependencyBuilder
    {
        public TargetDependencyBuilder(ITargetBuilder targetBuilder)
            : base(targetBuilder)
        {
        }

        public ITargetDependencyBuilder And(String target)
        {
            TargetBuilder.DependsOn(target);
            return this;
        }
    }
}