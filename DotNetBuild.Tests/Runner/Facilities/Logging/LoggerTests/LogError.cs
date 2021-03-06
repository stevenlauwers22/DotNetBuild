﻿using System;
using DotNetBuild.Runner.Facilities.Logging;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Runner.Facilities.Logging.LoggerTests
{
    public class LogError
        : TestSpecification<Logger>
    {
        private Mock<DotNetBuild.Runner.Infrastructure.Logging.ILogger> _logger;
        private String _message;
        private Exception _exception;

        protected override void Arrange()
        {
            _message = TestData.GenerateString();
            _exception = new Exception();
            _logger = new Mock<DotNetBuild.Runner.Infrastructure.Logging.ILogger>();
        }

        protected override Logger CreateSubjectUnderTest()
        {
            return new Logger(_logger.Object);
        }

        protected override void Act()
        {
            Sut.LogError(_message, _exception);
        }

        [Fact]
        public void Writes_the_message_to_the_log()
        {
            _logger.Verify(r => r.WriteError(_message, _exception));
        }
    }
}