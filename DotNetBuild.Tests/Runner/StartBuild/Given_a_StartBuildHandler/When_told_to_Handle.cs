using System;
using System.Collections.Generic;
using System.Linq;
using DotNetBuild.Runner;
using DotNetBuild.Runner.Infrastructure.Events;
using DotNetBuild.Runner.Infrastructure.Logging;
using DotNetBuild.Runner.StartBuild;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Runner.StartBuild.Given_a_StartBuildHandler
{
    public class When_told_to_Handle
        : TestSpecification<StartBuildHandler>
    {
        private StartBuildCommand _command;
        private Mock<IBuildRepository> _buildRepository;
        private Mock<IDomainEventInitializer> _domainEventInitializer;
        private Mock<ILogger> _logger;
        private DomainEvent _domainEventCatcher;
        private List<object> _domainEvents;

        protected override void Arrange()
        {
            _command = new StartBuildCommand(TestData.GenerateString(), TestData.GenerateString(), TestData.GenerateString(), new List<KeyValuePair<string, string>>());
            _buildRepository = new Mock<IBuildRepository>();

            _domainEvents = new List<object>();
            _domainEventCatcher = @event => _domainEvents.Add(@event);
            _domainEventInitializer = new Mock<IDomainEventInitializer>();
            _domainEventInitializer
                .Setup(i => i.Initialize(It.IsAny<object>()))
                .Callback<object>(obj => new DomainEventInitializer(_domainEventCatcher).Initialize(obj));

            _logger = new Mock<ILogger>();
        }

        protected override StartBuildHandler CreateSubjectUnderTest()
        {
            return new StartBuildHandler(_buildRepository.Object, _domainEventInitializer.Object, _logger.Object);
        }

        protected override void Act()
        {
            Sut.Handle(_command);
        }

        [Fact]
        public void Adds_the_Build_to_the_Repository()
        {
            _buildRepository.Verify(r => r.Add(It.Is<Build>(b => WorksOnTheCorrectBuild(b))));
        }

        [Fact]
        public void Initializes_the_DomainEvents_on_the_Build()
        {
            _domainEventInitializer.Verify(i => i.Initialize(It.Is<Build>(b => WorksOnTheCorrectBuild(b))));
        }

        [Fact]
        public void Raised_the_BuildRequestedToStart_event()
        {
            Assert.Equal(1, _domainEvents.Count);
            Assert.IsAssignableFrom<DotNetBuild.Runner.StartBuild.BuildRequestedToStart.BuildRequestedToStart>(_domainEvents.ElementAt(0));
        }

        private bool WorksOnTheCorrectBuild(Build build)
        {
            if (build == null) 
                throw new ArgumentNullException("build");

            Assert.NotEqual(Guid.Empty, build.Id);
            Assert.Equal(_command.Assembly, build.Assembly);   
            Assert.Equal(_command.Target, build.Target);   
            Assert.Equal(_command.Configuration, build.Configuration);   
            Assert.Equal(_command.AdditionalParameters, build.AdditionalParameters);
            
            return true;
        }
    }
}