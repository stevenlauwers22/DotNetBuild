using System;
using DotNetBuild.Runner;
using Xunit;

namespace DotNetBuild.Tests.Runner.BuildRunnerParametersBuilderTests
{
    public class BuildFrom_an_argument_array
        : TestSpecification<BuildRunnerParametersBuilder>
    {
        private String[] _args;
        private String _assembly;
        private String _target;
        private String _configuration;
        private BuildRunnerParameters _result;

        protected override void Arrange()
        {
            _assembly = TestData.GenerateString();
            _target = TestData.GenerateString();
            _configuration = TestData.GenerateString();

            _args = new[]
            {
                BuildRunnerParametersConstants.Assembly + _assembly,
                BuildRunnerParametersConstants.Target + _target,
                BuildRunnerParametersConstants.Configuration + _configuration,
                "foo:bar",
                null
            };
        }

        protected override BuildRunnerParametersBuilder CreateSubjectUnderTest()
        {
            return new BuildRunnerParametersBuilder();
        }

        protected override void Act()
        {
            _result = Sut.BuildFrom(_args);
        }

        [Fact]
        public void Reads_the_assembly_parameter()
        {
            Assert.Equal(_assembly, _result.Assembly);
        }

        [Fact]
        public void Reads_the_target_parameter()
        {
            Assert.Equal(_target, _result.Target);
        }

        [Fact]
        public void Reads_the_configuration_parameter()
        {
            Assert.Equal(_configuration, _result.Configuration);
        }
    }
}