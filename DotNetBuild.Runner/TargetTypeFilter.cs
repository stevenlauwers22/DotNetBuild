using System;

namespace DotNetBuild.Runner
{
    public class TargetTypeFilter 
        : AssemblyTypeFilter
    {
        private readonly string _targetName;

        public TargetTypeFilter(string targetName)
            : base(t => string.Equals(t.Name.Replace("Target", null), targetName.Replace("Target", null), StringComparison.OrdinalIgnoreCase))
        {
            if (string.IsNullOrEmpty(targetName))
                throw new ArgumentNullException("targetName");

            _targetName = targetName;
        }

        public string TargetName
        {
            get { return _targetName; }
        }
    }
}