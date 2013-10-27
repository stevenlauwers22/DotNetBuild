using System;

namespace DotNetBuild.Runner.StartBuild.BuildRequestedToStart
{
    public class BuildRequestedToStart
    {
        private readonly Guid _buildId;

        public BuildRequestedToStart(Guid buildId)
        {
            _buildId = buildId;
        }

        public Guid BuildId
        {
            get { return _buildId; }
        }
    }
}