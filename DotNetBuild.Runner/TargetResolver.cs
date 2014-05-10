using System;
using DotNetBuild.Core;
using DotNetBuild.Runner.Exceptions;

namespace DotNetBuild.Runner
{
    public interface ITargetResolver
    {
        ITarget Resolve(String targetName, IAssemblyWrapper assembly);
    }

    public class TargetResolver 
        : ITargetResolver
    {
        private readonly ITypeActivator _typeActivator;

        public TargetResolver(ITypeActivator typeActivator)
        {
            if (typeActivator == null)
                throw new ArgumentNullException("typeActivator");

            _typeActivator = typeActivator;
        }

        public ITarget Resolve(String targetName, IAssemblyWrapper assembly)
        {
            if (String.IsNullOrEmpty(targetName))
                throw new ArgumentNullException("targetName");

            if (assembly == null) 
                throw new ArgumentNullException("assembly");

            var targetTypeFilter = new TargetTypeFilter(targetName);
            var targetType = assembly.Get<ITarget>(targetTypeFilter);
            if (targetType == null)
                throw new UnableToResolveTargetException(targetName, assembly.Assembly.FullName);

            var target = _typeActivator.Activate<ITarget>(targetType);
            if (target == null)
                throw new UnableToActivateTargetException(targetType);

            return target;
        }
    }
}