using System.Linq;
using DotNetBuild.Runner.CommandLine.StartBuild;
using DotNetBuild.Runner.StartBuild;
using Xunit;

namespace DotNetBuild.Tests.Runner.CommandLine.StartBuild.Given_a_StartBuildCommandBuilder
{
    public class When_told_to_BuildFrom_an_argument_array
        : TestSpecification<StartBuildCommandBuilder>
    {
        private string[] _args;
        private string _assembly;
        private string _target;
        private string _configuration;
        private string _additionalParameterInvalid;
        private StartBuildCommand _result;

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

        protected override StartBuildCommandBuilder CreateSubjectUnderTest()
        {
            return new StartBuildCommandBuilder();
        }

        protected override void Act()
        {
            _result = (StartBuildCommand) Sut.BuildFrom(_args);
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