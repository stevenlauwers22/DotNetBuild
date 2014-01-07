using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DotNetBuild.Runner.Exceptions;

namespace DotNetBuild.Runner
{
    public interface IAssemblyWrapper
    {
        Assembly Assembly { get; }
        Type Get<T>();
        Type Get<T>(IAssemblyTypeFilter filter);
    }

    public class AssemblyWrapper 
        : IAssemblyWrapper
    {
        private readonly Assembly _assembly;

        public AssemblyWrapper(Assembly assembly)
        {
            if (assembly == null) 
                throw new ArgumentNullException("assembly");

            _assembly = assembly;
        }

        public Assembly Assembly
        {
            get { return _assembly; }
        }

        public Type Get<T>()
        {
            return Get<T>(null);
        }

        public Type Get<T>(IAssemblyTypeFilter filter)
        {
            var types = GetAll<T>(filter).ToList();
            if (types.Count > 1)
                throw new UnableToDetermineCorrectImplementationException(typeof(T), types);

            return types.SingleOrDefault();
        }

        private IEnumerable<Type> GetAll<T>(IAssemblyTypeFilter filter)
        {
            var types = _assembly
                .GetTypes()
                .Where(t => !t.IsInterface)
                .Where(t => !t.IsAbstract)
                .Where(t => typeof (T).IsAssignableFrom(t));

            if (filter != null && filter.Filter != null)
            {
                types = types.Where(filter.Filter);
            }

            return types.ToList();
        }
    }
}