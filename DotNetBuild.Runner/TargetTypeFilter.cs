using System;

namespace DotNetBuild.Runner
{
    public class TargetTypeFilter 
        : AssemblyTypeFilter
    {
        private readonly String _targetName;

        public TargetTypeFilter(String targetName)
            : base(t => String.Equals(t.Name.Replace("Target", null), targetName.Replace("Target", null), StringComparison.OrdinalIgnoreCase))
        {
            if (String.IsNullOrEmpty(targetName))
                throw new ArgumentNullException("targetName");

            _targetName = targetName;
        }

        public String TargetName
        {
            get { return _targetName; }
        }
    }
}