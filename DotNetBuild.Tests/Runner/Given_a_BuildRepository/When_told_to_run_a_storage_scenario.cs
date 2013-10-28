using System;
using System.Collections.Generic;
using System.Linq;
using DotNetBuild.Runner;
using Xunit;

namespace DotNetBuild.Tests.Runner.Given_a_BuildRepository
{
    public class When_told_to_run_a_storage_scenario
        : TestSpecification<BuildRepository>
    {
        private List<Build> _builds;
        private Build _buildToRetrieve;
        private Build _buildFromStore;

        protected override void Arrange()
        {
            _builds = new List<Build>
            {
                new Build(null, null, null, null),
                new Build(null, null, null, null),
                new Build(null, null, null, null),
                new Build(null, null, null, null)
            };
            _buildToRetrieve = _builds[new Random().Next(0, _builds.Count)];
        }

        protected override BuildRepository CreateSubjectUnderTest()
        {
            return new BuildRepository();
        }

        protected override void Act()
        {
            _builds.ForEach(Sut.Add);
            _buildFromStore = Sut.GetById(_buildToRetrieve.Id);
        }

        [Fact]
        public void Puts_all_builds_in_the_store()
        {
            Assert.Equal(_builds.Count(), Sut.Store.Count());
        }

        [Fact]
        public void Gets_the_appropriate_builds_from_the_store()
        {
            Assert.Equal(_buildToRetrieve, _buildFromStore);
        }
    }
}