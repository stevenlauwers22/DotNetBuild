using System;
using DotNetBuild.Runner;
using Xunit;

namespace DotNetBuild.Tests.Runner.Given_a_BuildRepository
{
    public class When_told_to_Get_a_Build_from_the_store_with_unexisting_ID
        : TestSpecification<BuildRepository>
    {
        private Guid _id;
        private Build _result;

        protected override void Arrange()
        {
            _id = TestData.GenerateGuid();
        }

        protected override BuildRepository CreateSubjectUnderTest()
        {
            return new BuildRepository();
        }

        protected override void Act()
        {
            _result = Sut.GetById(_id);
        }

        [Fact]
        public void Returns_null()
        {
            Assert.Null(_result);
        }
    }
}