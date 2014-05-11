using System;

namespace DotNetBuild.Runner.Infrastructure.Reflection
{
    public interface ITypeFilter
    {
        Func<Type, Boolean> Filter { get; }
    }

    public class TypeFilter 
        : ITypeFilter
    {
        private readonly Func<Type, Boolean> _filter;

        public TypeFilter(Func<Type, Boolean> filter)
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