using System;
using DotNetBuild.Core;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Core.TargetBuilderTests
{
    public class Build_with_valid_arguments : TestSpecification<TargetBuilder>
    {
        private Mock<ITargetRegistry> _targetRegistry;
        private String _name;
        private String _description;
        private ITarget _result;

        protected override void Arrange()
        {
            _targetRegistry = new Mock<ITargetRegistry>();
            _name = TestData.GenerateString();
            _description = TestData.GenerateString();
        }

        protected override TargetBuilder CreateSubjectUnderTest()
        {
            return null;
        }

        protected override void Act()
        {
            _result = new TargetBuilder(_targetRegistry.Object, _name, _description).GetTarget();
        }

        [Fact]
        public void Adds_the_target_to_the_registry()
        {
            _targetRegistry.Verify(r => r.Add(_name, _result));
        }

        [Fact]
        public void Fills_in_the_appropriate_description()
        {
            Assert.NotNull(_result);
            Assert.Equal(_description, _result.Description);
        }
    }
}