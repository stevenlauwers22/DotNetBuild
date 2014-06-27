using System;
using DotNetBuild.Runner.Facilities.State;
using Xunit;

namespace DotNetBuild.Tests.Runner.Facilities.State.StateRegistryTests
{
    public class Get_state_with_unexisting_key
        : TestSpecification<StateRegistry>
    {
        private String _key;
        private Object _result;

        protected override void Arrange()
        {
            _key = TestData.GenerateString();
        }

        protected override StateRegistry CreateSubjectUnderTest()
        {
            return new StateRegistry();
        }

        protected override void Act()
        {
            _result = Sut.Get<Object>(_key);
        }

        [Fact]
        public void Returns_null()
        {
            Assert.Null(_result);
        }
    }
}