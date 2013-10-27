using System;

namespace DotNetBuild.Runner.Infrastructure
{
    public interface IAssemblyTypeFilter
    {
        Func<Type, bool> Filter { get; }
    }

    public class AssemblyTypeFilter 
        : IAssemblyTypeFilter
    {
        private readonly Func<Type, bool> _filter;

        public AssemblyTypeFilter(Func<Type, bool> filter)
        {
            if (filter == null) 
                throw new ArgumentNullException("filter");

            _filter = filter;
        }

        public Func<Type, bool> Filter
        {
            get { return _filter; }
        }
    }
}