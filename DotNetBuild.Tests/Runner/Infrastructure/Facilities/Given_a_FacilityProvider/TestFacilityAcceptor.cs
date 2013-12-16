﻿using DotNetBuild.Core.Facilities;

namespace DotNetBuild.Tests.Runner.Infrastructure.Facilities.Given_a_FacilityProvider
{
    public interface ITestFacilityAcceptor
        : IFacilityAcceptor<ITestFacility>
    {
    }

    public class TestFacilityAcceptor
        : ITestFacilityAcceptor
    {
        public ITestFacility TestFacility { get; private set; }

        public void Inject(ITestFacility testFacility)
        {
            TestFacility = testFacility;
        }
    }
}