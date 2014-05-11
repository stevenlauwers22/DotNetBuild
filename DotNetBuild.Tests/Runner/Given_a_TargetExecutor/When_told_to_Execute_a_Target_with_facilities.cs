using System;
using System.Collections.Generic;
using System.Diagnostics;
using DotNetBuild.Core;
using DotNetBuild.Core.Targets;
using DotNetBuild.Runner;
using DotNetBuild.Runner.Facilities;
using DotNetBuild.Runner.Infrastructure.Logging;
using DotNetBuild.Tests.Runner.Facilities;
using Moq;
using Xunit;

namespace DotNetBuild.Tests.Runner.Given_a_TargetExecutor
{
    public class When_told_to_Execute_a_Target_with_facilities
        : TestSpecification<TargetExecutor>
    {
        private TestTarget _target;
        private Mock<ITargetInspector> _targetInspector;
        private Mock<ILogger> _logger;
        private List<IFacilityProvider> _facilityProviders;
        private TestFacilityProvider _facilityProvider;
        private TestFacility _facility;

        protected override void Arrange()
        {
            _target = new TestTarget();

            _targetInspector = new Mock<ITargetInspector>();
            _logger = new Mock<ILogger>();
            _facility = new TestFacility();
            _facilityProvider = new TestFacilityProvider(_logger.Object, _facility);
            _facilityProviders = new List<IFacilityProvider> { _facilityProvider };
        }

        protected override TargetExecutor CreateSubjectUnderTest()
        {
            return new TargetExecutor(_targetInspector.Object, _logger.Object, _facilityProviders.ToArray());
        }

        protected override void Act()
        {
            Sut.Execute(_target, null);
        }

        [Fact]
        public void Injects_the_Facilities_into_the_Target()
        {
            Assert.Equal(_facility, _target.TestFacility);
        }

        private class TestTarget : TestFacilityAcceptor, ITarget
        {
            public String Description
            {
                get { return "TestTarget"; }
            }

            public Boolean ContinueOnError
            {
                get { return false; }
            }

            public IEnumerable<ITarget> DependsOn
            {
                get { return null; }
            }

            public Boolean Execute(IConfigurationSettings configurationSettings)
            {
                Debug.WriteLine("{0} - executing", Description);
                return true;
            }
        }
    }
}