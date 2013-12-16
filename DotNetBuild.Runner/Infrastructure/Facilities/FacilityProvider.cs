using DotNetBuild.Core.Facilities;

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
        public void InjectIfRequired(object value)
        {
            var facilityAcceptor = value as TFacilityAcceptor;
            if (facilityAcceptor == null)
                return;

            facilityAcceptor.Inject(GetFacility());
        }

        protected abstract TFacility GetFacility();
    }
}