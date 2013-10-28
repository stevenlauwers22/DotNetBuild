using System.Collections.Generic;
using System.Linq;
using DotNetBuild.Runner;
using DotNetBuild.Runner.Infrastructure.Events;
using DotNetBuild.Runner.StartBuild.BuildRequestedToStart;
using Xunit;

namespace DotNetBuild.Tests.Runner.Given_a_Build
{
    public class When_told_to_RequestStart
        : TestSpecification<Build>
    {
        private DomainEvent _domainEventCatcher;
        private List<object> _domainEvents;

        protected override void Arrange()
        {
            _domainEvents = new List<object>();
            _domainEventCatcher = @event => _domainEvents.Add(@event);
        }

        protected override Build CreateSubjectUnderTest()
        {
            var build = new Build(null, null, null, null);
            build.Notify += _domainEventCatcher;

            return build;
        }

        protected override void Act()
        {
            Sut.RequestStart();
        }

        [Fact]
        public void Raised_the_BuildRequestedToStart_event()
        {
            Assert.Equal(1, _domainEvents.Count);

            var @event = _domainEvents.ElementAt(0);
            Assert.IsAssignableFrom<BuildRequestedToStart>(@event);

            var @eventCasted = (BuildRequestedToStart)_domainEvents.ElementAt(0);
            Assert.Equal(Sut.Id, @eventCasted.BuildId);
        }
    }
}