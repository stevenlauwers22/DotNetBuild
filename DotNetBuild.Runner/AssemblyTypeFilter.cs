using System;

namespace DotNetBuild.Runner
{
    public interface IAssemblyTypeFilter
    {
        Func<Type, Boolean> Filter { get; }
    }

    public class AssemblyTypeFilter 
        : IAssemblyTypeFilter
    {
        private readonly Func<Type, Boolean> _filter;

        public AssemblyTypeFilter(Func<Type, Boolean> filter)
        {
            if (filter == null) 
                throw new ArgumentNullException("filter");

            _filter = filter;
        }

        public Func<Type, Boolean> Filter
        {
            get { return _filter; }
        }
    }
}