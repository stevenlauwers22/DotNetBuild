using DotNetBuild.Core.Facilities;
using DotNetBuild.Runner.Infrastructure.Logging;

namespace DotNetBuild.Runner.Infrastructure.Facilities
{
    public interface IFacilityProvider
    {
        void InjectIfRequired(object value);
    }

    public abstract class FacilityProvider<TFacilityAcceptor, TFacility> : IFacilityProvider
        where TFacilityAcceptor : class, IFacilityAcceptor<TFacility>
        where TFacility : class, IFacility
    {
        private readonly ILogger _logger;

        protected FacilityProvider(ILogger logger)
        {
            _logger = logger;
        }

        public void InjectIfRequired(object value)
        {
            var facilityAcceptor = value as TFacilityAcceptor;
            if (facilityAcceptor == null)
                return;

            var facility = GetFacility();
            var facilityLogMessage = string.Format("Injecting facility {0} into {1}", facility.GetType().FullName, value.GetType().FullName);
            _logger.Write(facilityLogMessage);

            facilityAcceptor.Inject(facility);
        }

        protected abstract TFacility GetFacility();
    }
}