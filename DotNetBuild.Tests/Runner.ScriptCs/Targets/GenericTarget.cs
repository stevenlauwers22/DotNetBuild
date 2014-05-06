using System;
using System.Collections.Generic;
using System.Linq;
using DotNetBuild.Core;

namespace DotNetBuild.Tests.Runner.ScriptCs.Targets
{
    public class GenericTarget 
        : ITarget
    {
        private readonly string _name;
        private readonly IList<Func<ITarget>> _dependsOn; 

        public GenericTarget(string name)
        {
            _name = name;
            _dependsOn = new List<Func<ITarget>>();
        }

        public string Name
        {
            get { return _name; }
        }

        public bool ContinueOnError
        {
            get; set;
        }

        public IEnumerable<ITarget> DependsOn
        {
            get { return _dependsOn.Select(t => t()).ToList(); }
        }

        public Func<IConfigurationSettings, bool> ExecuteFunc
        {
            get; set;
        }

        public void AddDependency(Func<ITarget> target)
        {
            _dependsOn.Add(target);
        }

        public bool Execute(IConfigurationSettings configurationSettings)
        {
            if (ExecuteFunc == null)
                return true;

            return ExecuteFunc(configurationSettings);
        }
    }
}