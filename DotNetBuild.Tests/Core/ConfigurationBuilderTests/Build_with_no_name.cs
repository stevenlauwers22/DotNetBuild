using System;
using DotNetBuild.Core;
using DotNetBuild.Runner;
using Xunit;

namespace DotNetBuild.Tests.Core.ConfigurationBuilderTests
{
    public class Build_with_no_name : TestSpecification<ConfigurationBuilder>
    {
        private String _name;
        private ArgumentNullException _exception;

        protected override void Arrange()
        {
            _name = null;
        }

        protected override ConfigurationBuilder CreateSubjectUnderTest()
        {
            return null;
        }

        protected override void Act()
        {
            _exception = TestHelpers.CatchException<ArgumentNullException>(() => new ConfigurationBuilder(new ConfigurationRegistry(), _name));
        }

        [Fact]
        public void Throws_an_ArgumentNullException()
        {
            Assert.NotNull(_exception);
            Assert.Equal("name", _exception.ParamName);
        }
    }
}