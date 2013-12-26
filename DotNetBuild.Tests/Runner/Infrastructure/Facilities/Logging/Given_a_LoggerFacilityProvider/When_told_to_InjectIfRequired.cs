using DotNetBuild.Core.Facilities.Logging;
using DotNetBuild.Runner.Infrastructure.Facilities.Logging;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Runner.Infrastructure.Facilities.Logging.Given_a_LoggerFacilityProvider
{
    public class When_told_to_InjectIfRequired
        : TestSpecification<LoggerFacilityProvider>
    {
        private Mock<DotNetBuild.Runner.Infrastructure.Logging.ILogger> _logger;
        private Mock<ILogger> _loggerFacility;
        private LoggerFacilityAcceptor _loggerFacilityAcceptor;

        protected override void Arrange()
        {
            _logger = new Mock<DotNetBuild.Runner.Infrastructure.Logging.ILogger>();
            _loggerFacility = new Mock<ILogger>();
            _loggerFacilityAcceptor = new LoggerFacilityAcceptor();
        }

        protected override LoggerFacilityProvider CreateSubjectUnderTest()
        {
            return new LoggerFacilityProvider(_logger.Object, () => _loggerFacility.Object);
        }

        protected override void Act()
        {
            Sut.InjectIfRequired(_loggerFacilityAcceptor);
        }

        [Fact]
        public void Injects_the_facility()
        {
            Assert.Equal(_loggerFacility.Object, _loggerFacilityAcceptor.Facility);
        }

        public class LoggerFacilityAcceptor
            : IWantToLog
        {
            public ILogger Facility { get; private set; }

            public void Inject(ILogger facility)
            {
                Facility = facility;
            }
        }
    }
}