using System.Collections.Generic;
using DotNetBuild.Runner;
using DotNetBuild.Runner.Infrastructure.Logging;
using DotNetBuild.Runner.StartBuild.BuildRequestedToStart;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Runner.StartBuild.BuildRequestedToStart.Given_a_BuildRequestedToStartHandler
{
    public class When_told_to_Handle
        : TestSpecification<BuildRequestedToStartHandler>
    {
        private DotNetBuild.Runner.StartBuild.BuildRequestedToStart.BuildRequestedToStart _event;
        private Build _build;
        private Mock<IBuildRepository> _buildRepository;
        private Mock<IBuildRunner> _buildRunner;
        private Mock<ILogger> _logger;

        protected override void Arrange()
        {
            _event = new DotNetBuild.Runner.StartBuild.BuildRequestedToStart.BuildRequestedToStart(TestData.GenerateGuid());
            
            _build = new Build(TestData.GenerateString(), TestData.GenerateString(), TestData.GenerateString(), new List<KeyValuePair<string, string>>());
            _buildRepository = new Mock<IBuildRepository>();
            _buildRepository.Setup(r => r.GetById(_event.BuildId)).Returns(_build);

            _buildRunner = new Mock<IBuildRunner>();
            _logger = new Mock<ILogger>();
        }

        protected override BuildRequestedToStartHandler CreateSubjectUnderTest()
        {
            return new BuildRequestedToStartHandler(_buildRepository.Object, _buildRunner.Object, _logger.Object);
        }

        protected override void Act()
        {
            Sut.Handle(_event);
        }

        [Fact]
        public void Gets_the_Build_from_the_Repository()
        {
            _buildRepository.Verify(r => r.GetById(_event.BuildId));
        }

        [Fact]
        public void Runs_the_BuildRunner()
        {
            _buildRunner.Verify(br => br.Run(It.Is<BuildRunnerParameters>(p =>
                p.Assembly == _build.Assembly &&
                p.Target == _build.Target &&
                p.Configuration == _build.Configuration &&
                Equals(p.AdditionalParameters, _build.AdditionalParameters))));
        }
    }
}