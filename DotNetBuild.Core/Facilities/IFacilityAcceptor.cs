namespace DotNetBuild.Core.Facilities
{
    public interface IFacilityAcceptor<in TFacility>
        where TFacility : IFacility
    {
        void Inject(TFacility facility);
    }
}