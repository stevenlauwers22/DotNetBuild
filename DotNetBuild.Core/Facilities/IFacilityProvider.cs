namespace DotNetBuild.Core.Facilities
{
    public interface IFacilityProvider
    {
        TFacility Get<TFacility>()
            where TFacility : IFacility;
    }
}