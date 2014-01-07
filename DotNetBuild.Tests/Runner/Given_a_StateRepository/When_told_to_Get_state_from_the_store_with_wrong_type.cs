using System;
using DotNetBuild.Runner;
using Xunit;

namespace DotNetBuild.Tests.Runner.Given_a_StateRepository
{
    public class When_told_to_Get_state_from_the_store_with_wrong_type
        : TestSpecification<StateRepository>
    {
        private string _key;
        private object _value;
        private InvalidCastException _exception;

        protected override void Arrange()
        {
            _key = TestData.GenerateString();
            _value = new object();
        }

        protected override StateRepository CreateSubjectUnderTest()
        {
            var sut = new StateRepository();
            sut.Add(_key, _value);

            return sut;
        }

        protected override void Act()
        {
            _exception = TestHelpers.CatchException<InvalidCastException>(() => Sut.Get<string>(_key));
        }

        [Fact]
        public void Throws_an_InvalidCastException()
        {
            Assert.NotNull(_exception);
        }
    }
}