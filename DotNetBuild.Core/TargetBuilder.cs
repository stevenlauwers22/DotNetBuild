using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace DotNetBuild.Core
{
    public interface ITargetBuilder
    {
        ITargetBuilder ContinueOnError(Boolean continueOnError);
        ITargetDependencyBuilder DependsOn(String target);
        ITargetBuilder Do(Func<IConfigurationSettings, Boolean> executeFunc);
    }

    public class TargetBuilder
        : ITargetBuilder
    {
        private readonly ITargetRegistry _targetRegistry;
        private readonly GenericTarget _target;

        public TargetBuilder(ITargetRegistry targetRegistry, String name, String description)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            _target = GenerateTarget(name, description);
            _targetRegistry = targetRegistry;
            _targetRegistry.Add(name, _target);
        }

        private static GenericTarget GenerateTarget(string name, string description)
        {
            // Generate a class at runtime, otherwise the circular dependency resolver can't distinguish different generic targets from another
            
            var assemblyName = new AssemblyName(String.Format("DotNetBuild.Core.RuntimeTypes.{0}", Guid.NewGuid()));
            var assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            var moduleBuilder = assemblyBuilder.DefineDynamicModule(assemblyName.Name);
            var baseClass = typeof(GenericTarget);
            var typeBuilder = moduleBuilder.DefineType(String.Format("{0}.{1}", assemblyName, name), TypeAttributes.Public, baseClass);
            var constructorArguments = new[] { typeof(string) };
            var constructorBuilder = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, constructorArguments);
            var constructorGenerator = constructorBuilder.GetILGenerator();

            // Call the constructor of the base class with description as parameter
            constructorGenerator.Emit(OpCodes.Ldarg_0);
            constructorGenerator.Emit(OpCodes.Ldarg_1);
            constructorGenerator.Emit(OpCodes.Call, baseClass.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, constructorArguments, null));
            constructorGenerator.Emit(OpCodes.Ret);

            var type = typeBuilder.CreateType();
            var target = (GenericTarget)Activator.CreateInstance(type, new object[] { description });
            return target;
        }

        public ITarget GetTarget()
        {
            return _target;
        }

        public ITargetBuilder ContinueOnError(Boolean continueOnError)
        {
            _target.ContinueOnError = continueOnError;
            return this;
        }

        public ITargetDependencyBuilder DependsOn(String target)
        {
            _target.AddDependency(() => _targetRegistry.Get(target));
            return new TargetDependencyBuilder(this);
        }

        public ITargetBuilder Do(Func<IConfigurationSettings, Boolean> executeFunc)
        {
            _target.ExecuteFunc = executeFunc;
            return this;
        }

        public abstract class GenericTarget
            : ITarget
        {
            private readonly String _description;
            private readonly IList<Func<ITarget>> _dependsOn;

            protected GenericTarget(String description)
            {
                _description = description;
                _dependsOn = new List<Func<ITarget>>();
            }

            public String Description
            {
                get { return _description; }
            }

            public Boolean ContinueOnError
            {
                get;
                set;
            }

            public IEnumerable<ITarget> DependsOn
            {
                get { return _dependsOn.Select(t => t()).ToList(); }
            }

            public Func<IConfigurationSettings, Boolean> ExecuteFunc
            {
                get;
                set;
            }

            public void AddDependency(Func<ITarget> target)
            {
                _dependsOn.Add(target);
            }

            public Boolean Execute(IConfigurationSettings configurationSettings)
            {
                if (ExecuteFunc == null)
                    return true;

                return ExecuteFunc(configurationSettings);
            }
        }
    }
}