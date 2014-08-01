using System;
using DotNetBuild.Runner;
using Xunit;

namespace DotNetBuild.Tests.Runner.BuildRunnerParametersBuilderTests
{
    public class BuildFrom_an_argument_array
        : TestSpecification<BuildRunnerParametersReader>
    {
        private String[] _args;
        private String _assembly;
        private String _target;
        private String _configuration;
        private String _result;

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

        protected override BuildRunnerParametersReader CreateSubjectUnderTest()
        {
            return new BuildRunnerParametersReader();
        }

        protected override void Act()
        {
            _result = Sut.Read(BuildRunnerParametersConstants.Target, _args);
        }

        [Fact]
        public void Reads_the_requested_parameter()
        {
            Assert.Equal(_target, _result);
        }
    }
}