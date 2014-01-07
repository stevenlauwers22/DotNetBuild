using System;

namespace DotNetBuild.Runner
{
    public class TargetTypeFilter 
        : AssemblyTypeFilter
    {
        private readonly string _target;

        public TargetTypeFilter(string target)
            : base(t => string.Equals(t.Name.Replace("Target", null), target.Replace("Target", null), StringComparison.OrdinalIgnoreCase))
        {
            if (string.IsNullOrEmpty(target)) 
                throw new ArgumentNullException("target");

            _target = target;
        }

        public string Target
        {
            get { return _target; }
        }
    }
}