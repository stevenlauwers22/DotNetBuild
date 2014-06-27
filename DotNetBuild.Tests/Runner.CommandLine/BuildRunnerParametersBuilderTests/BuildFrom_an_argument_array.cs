using System;
using System.Linq;
using DotNetBuild.Runner.CommandLine;
using Xunit;

namespace DotNetBuild.Tests.Runner.CommandLine.BuildRunnerParametersBuilderTests
{
    public class BuildFrom_an_argument_array
        : TestSpecification<BuildRunnerParametersBuilder>
    {
        private String[] _args;
        private String _assembly;
        private String _target;
        private String _configuration;
        private String _additionalParameterInvalid;
        private BuildRunnerParameters _result;

        protected override void Arrange()
        {
            _assembly = TestData.GenerateString();
            _target = TestData.GenerateString();
            _configuration = TestData.GenerateString();
            _additionalParameterInvalid = TestData.GenerateString();

            _args = new[]
            {
                "-assembly:" + _assembly,
                "-target:" + _target,
                "-configuration:" + _configuration,
                "-foo:bar",
                "-bar:foo",
                _additionalParameterInvalid,
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

        [Fact]
        public void Reads_the_additional_parameters()
        {
            Assert.Equal(2, _result.AdditionalParameters.Count());

            Assert.Equal("foo", _result.AdditionalParameters.ElementAt(0).Key);
            Assert.Equal("bar", _result.AdditionalParameters.ElementAt(0).Value);

            Assert.Equal("bar", _result.AdditionalParameters.ElementAt(1).Key);
            Assert.Equal("foo", _result.AdditionalParameters.ElementAt(1).Value);
        }
    }
}