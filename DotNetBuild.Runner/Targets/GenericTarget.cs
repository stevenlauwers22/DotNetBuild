using System;
using System.Collections.Generic;
using System.Linq;
using DotNetBuild.Core;

namespace DotNetBuild.Runner.Targets
{
    public class GenericTarget 
        : ITarget
    {
        private readonly String _description;
        private readonly IList<Func<ITarget>> _dependsOn; 

        public GenericTarget(String description)
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
            get; set;
        }

        public IEnumerable<ITarget> DependsOn
        {
            get { return _dependsOn.Select(t => t()).ToList(); }
        }

        public Func<IConfigurationSettings, Boolean> ExecuteFunc
        {
            get; set;
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