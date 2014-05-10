using System;
using DotNetBuild.Runner.Facilities.Logging;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Runner.Facilities.Logging.Given_a_Logger
{
    public class When_told_to_LogInfo
        : TestSpecification<Logger>
    {
        private Mock<DotNetBuild.Runner.Infrastructure.Logging.ILogger> _logger;
        private String _message;

        protected override void Arrange()
        {
            _message = TestData.GenerateString();
            _logger = new Mock<DotNetBuild.Runner.Infrastructure.Logging.ILogger>();
        }

        protected override Logger CreateSubjectUnderTest()
        {
            return new Logger(_logger.Object);
        }

        protected override void Act()
        {
            Sut.LogInfo(_message);
        }

        [Fact]
        public void Writes_the_message_to_the_log()
        {
            _logger.Verify(r => r.Write(_message));
        }
    }
}