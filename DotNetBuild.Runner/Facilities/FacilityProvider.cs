using System;
using System.Collections.Generic;
using System.Linq;
using DotNetBuild.Core.Facilities;

namespace DotNetBuild.Runner.Facilities
{
    public class FacilityProvider
        : IFacilityProvider
    {
        private readonly IEnumerable<IFacility> _facilities;

        public FacilityProvider(IEnumerable<IFacility> facilities)
        {
            if (facilities == null) 
                throw new ArgumentNullException("facilities");

            _facilities = facilities;
        }

        public TFacility Get<TFacility>() where TFacility : IFacility
        {
            var facility = _facilities.OfType<TFacility>().SingleOrDefault();
            return facility;
        }
    }
}
