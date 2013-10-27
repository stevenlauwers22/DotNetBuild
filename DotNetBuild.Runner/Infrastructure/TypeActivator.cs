using System;
using DotNetBuild.Runner.Infrastructure.Exceptions;

namespace DotNetBuild.Runner.Infrastructure
{
    public interface ITypeActivator
    {
        T Activate<T>(Type targetType);
    }

    public class TypeActivator 
        : ITypeActivator
    {
        public T Activate<T>(Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            var defaultConstructor = type.GetConstructor(Type.EmptyTypes);
            if (defaultConstructor == null)
                throw new UnableToActivateTypeWithNoDefaultConstructorException(type);

            var target = Activator.CreateInstance(type);
            return (T) target;
        }
    }
}