using System;
using DotNetBuild.Runner;
using Xunit;

namespace DotNetBuild.Tests.Runner.ParameterProviderTests
{
    public class Get_value
        : TestSpecification<ParameterProvider>
    {
        private String[] _parameters;
        private String _assembly;
        private String _target;
        private String _configuration;
        private String _result;

        protected override void Arrange()
        {
            _assembly = TestData.GenerateString();
            _target = TestData.GenerateString();
            _configuration = TestData.GenerateString();

            _parameters = new[]
            {
                ParameterConstants.Assembly + ":" + _assembly,
                ParameterConstants.Target + ":" + _target,
                ParameterConstants.Configuration + ":" + _configuration,
                "foo:bar",
                null
            };
        }

        protected override ParameterProvider CreateSubjectUnderTest()
        {
            return new ParameterProvider(_parameters);
        }

        protected override void Act()
        {
            _result = Sut.Get(ParameterConstants.Target);
        }

        [Fact]
        public void Reads_the_requested_parameter()
        {
            Assert.Equal(_target, _result);
        }
    }
}